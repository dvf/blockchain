import asyncio
import logging
import multiprocessing
import time
from uuid import uuid4

import aiohttp

from blockchain import Blockchain
from database import db
from helpers import get_config, set_config, get_random_peers
from mining import miner
from networking import PortMapper


log = logging.getLogger('root.tasks')


def initiate_node(app):
    # Set up TCP Redirect (Port Forwarding)
    port_mapper = PortMapper()
    port_mapper.add_portmapping(8080, 8080, 'TCP', 'Electron')

    # Set the identifier (unique Id) for our node
    node_identifier = get_config('node_identifier')
    if not node_identifier:
        node_identifier = set_config(key='node_identifier', value=uuid4().hex)

    app.request_headers = {
        'content-type': 'application/json',
        'x-node-identifier': node_identifier,
        'x-node-ip': port_mapper.external_ip,
        'x-node-port': port_mapper.external_port,
    }

    log.info('Node Identifier: %s', node_identifier)

    # Add the Blockchain helper class to the app
    app.blockchain = Blockchain()


async def peer_discovery(app):
    """
    Ask random peers to return peers they know about
    """
    while True:
        peers = get_random_peers()

        for peer in peers:
            try:
                response = await aiohttp.request('GET', 'peer.hostname', headers=app.request_headers)
                print(f'Made request: {response.status}')

            except asyncio.TimeoutError:
                db.delete(peer)
                db.commit()
                print(f'{peer.hostname}: Deleted node')

        await asyncio.sleep(10)


async def watch_blockchain(app):
    while True:
        print(f'TXN: {app.blockchain.current_transactions}')
        await asyncio.sleep(2)


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


def we_should_still_be_mining():
    return True


async def mining_controller(app):
    left, right = multiprocessing.Pipe()
    event = multiprocessing.Event()

    # Spawn a new process consisting of the miner() function
    # and send the right end of the pipe to it
    process = multiprocessing.Process(target=miner, args=(right, event))
    process.start()

    left.send({'last_hash': 123, 'difficulty': 6})

    while True:
        event.set()

        # We'll check the pipe every 100 ms
        await asyncio.sleep(1)

        # Check if we should still be mining
        if not we_should_still_be_mining():
            event.clear()

        if left.poll():
            result = left.recv()
            proof = result['proof']
            previous_hash = result['last_hash']
            app.blockchain.new_block(proof, previous_hash)
            last_block_hash = app.blockchain.hash(app.blockchain.last_block)

            left.send({'last_hash': last_block_hash, 'difficulty': 6})
