import time
import asyncio
import multiprocessing

import aiohttp
from sqlalchemy import func

from database import Peer, db


def get_random_peers(limit=10):
    """
    Returns random peers

    :param limit: How many peers to return
    :return:
    """
    return db.query(Peer).order_by(func.random()).limit(limit)


async def populate_peers(app):
    """
    Ask random peers to return peers they know about
    """
    print(app)
    while True:
        peers = get_random_peers()

        async with aiohttp.ClientSession() as session:
            for peer in peers:
                try:
                    async with session.get(f'http://{peer.ip}:8000', timeout=3) as resp:
                        print(resp.status)
                        print(await resp.text())
                except asyncio.TimeoutError:
                    db.delete(peer)
                    db.commit()
                    print(f"{peer.ip}: Deleting node")

        await asyncio.sleep(10)


async def watch_blockchain(app):
    while True:
        print(f"TXN: {app.blockchain.current_transactions}")
        await asyncio.sleep(2)


async def add_stuff(app):
    while True:
        await asyncio.sleep(1.5)
        app.blockchain.current_transactions.append("a")


async def consensus():
    """
    Our Consensus Algorithm. It makes sure we have a valid up-to-date chain.
    """

    # Asynchronously grab the chain from each peer
    # Validate it, then replace ours if necessary
    def resolve_conflicts(self):
        """
        This is our consensus algorithm, it resolves conflicts
        by replacing our chain with the longest one in the network.

        :return: True if our chain was replaced, False if not
        """

        neighbours = self.nodes
        new_chain = None

        # We're only looking for chains longer than ours
        max_length = len(self.chain)

        # Grab and verify the chains from all the nodes in our network
        for node in neighbours:
            response = requests.get(f'http://{node}/chain')

            if response.status_code == 200:
                length = response.json()['length']
                chain = response.json()['chain']

                # Check if the length is longer and the chain is valid
                if length > max_length and self.valid_chain(chain):
                    max_length = length
                    new_chain = chain

        # Replace our chain if we discovered a new, valid chain longer than ours
        if new_chain:
            self.chain = new_chain
            return True

        return False


def miner():
    while True:
        time.sleep(2)
        print('Hey! Mining is happening!')


async def mining_controller():
    p = multiprocessing.Process(target=miner, args=())
    p.start()
