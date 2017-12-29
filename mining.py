import asyncio
import logging
import multiprocessing
from datetime import datetime

from helpers import hash_block
from tasks import we_should_still_be_mining


log = logging.getLogger('root.mining')


def proof_of_work(current_block, difficulty, event):
    """
    Simple Proof of Work Algorithm

    :param current_block: The partially complete block currently being mined
    :param difficulty: The minimum number of leading zeros
    """

    # String of 64 f's replaced with 3 leading zeros (if the difficulty is 3): 000fff...f
    target = str.ljust("0" * difficulty, 64, "f")

    guess_hash = hash_block(current_block)

    while guess_hash > target:
        # Check if we should still be mining
        # if not event.is_set():
        #     raise Exception("STOP MINING")
        current_block['timestamp'] = datetime.utcnow()
        current_block['proof'] += 1
        guess_hash = hash_block(current_block)

    current_block['hash'] = guess_hash
    return current_block


def miner(pipe, event):
    while True:
        task = pipe.recv()
        log.debug(f"Received new mining task with difficulty {task['difficulty']}")

        if task:
            found_block = proof_of_work(task['block'], task['difficulty'], event)
            pipe.send({'found_block': found_block})


async def mining_controller(app):
    pipe, remote_pipe = multiprocessing.Pipe()
    event = multiprocessing.Event()

    # Spawn a new process consisting of the miner() function
    # and send the right end of the pipe to it
    process = multiprocessing.Process(target=miner, args=(remote_pipe, event))
    process.start()

    pipe.send({'block': app.blockchain.build_block(), 'difficulty': 5})

    while True:
        event.set()

        # We'll check the pipe every 100 ms
        await asyncio.sleep(0.1)

        # Check if we should still be mining
        if not we_should_still_be_mining():
            event.clear()

        if pipe.poll():
            result = pipe.recv()
            found_block = result['found_block']

            app.blockchain.save_block(found_block)
            log.info(f"Mined Block {found_block['height']} containing {len(found_block['transactions'])} transactions")
            pipe.send({'block': app.blockchain.build_block(), 'difficulty': 5})
