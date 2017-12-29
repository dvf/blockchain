import logging
from datetime import datetime
from hashlib import sha256
import signal

from blockchain import Blockchain


log = logging.getLogger('root.mining')


def proof_of_work(current_block, difficulty, event):
    """
    Simple Proof of Work Algorithm

    :param current_block: The partially complete block currently being mined
    :param difficulty: The minimum number of leading zeros
    """

    # String of 64 f's replaced with 3 leading zeros (if the difficulty is 3): 000fff...f
    target = str.ljust("0" * difficulty, 64, "f")

    current_block['timestamp'] = datetime.utcnow()
    current_block['proof'] = 0
    guess_hash = Blockchain.hash(current_block)

    while guess_hash > target:
        # Check if we should still be mining
        # if not event.is_set():
        #     raise Exception("STOP MINING")
        current_block['timestamp'] = datetime.utcnow()
        current_block['proof'] += 1
        guess_hash = Blockchain.hash(current_block)

    current_block['hash'] = guess_hash
    return current_block


def miner(right, event):

    while True:
        task = right.recv()
        log.info(f"Received new mining task with difficulty {task['difficulty']}")

        if task:
            found_block = proof_of_work(task['block'], task['difficulty'], event)
            right.send({'found_block': found_block})
