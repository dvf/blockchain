import logging
from hashlib import sha256
import signal


log = logging.getLogger('root.mining')


def valid_proof(last_hash, proof, target):
    """
    Validates the Proof

    :param last_hash: Hash of the previous block
    :param proof: Current Proof
    :param target: Target Difficulty
    :return: True if correct, False if not.
    """

    guess = f'{last_hash}{proof}'.encode()
    guess_hash = sha256(guess).hexdigest()
    return guess_hash < target


def proof_of_work(last_hash, difficulty, event):
    """
    Simple Proof of Work Algorithm

    :param last_hash: The hash of the previous block
    :param difficulty: The minimum number of leading zeros
    """

    # String of 64 f's replaced with 3 leading zeros (if the difficulty is 3): 000fff...f
    target = str.ljust("0" * difficulty, 64, "f")

    proof = 0
    while valid_proof(last_hash, proof, target) is False:
        # Check if we should still be mining
        # if not event.is_set():
        #     raise Exception("STOP MINING")
        proof += 1
    return proof


def miner(right, event):

    while True:
        log.info(f'Waiting for task')
        latest_task = right.recv()
        log.info(f"Received new task for hash {latest_task['last_hash']} "
                 f"with difficulty {latest_task['difficulty']}")

        if latest_task:
            proof = proof_of_work(latest_task['last_hash'], latest_task['difficulty'], event)
            right.send({'proof': proof, 'last_hash': latest_task['last_hash']})
