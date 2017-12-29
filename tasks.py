import asyncio
import logging
from uuid import uuid4

import aiohttp

from blockchain import Blockchain
from database import db
from helpers import get_config, get_random_peers, set_config
from networking import PortMapper


log = logging.getLogger('root.tasks')


def initiate_node(app):
    # Set up TCP Redirect (Port Forwarding)
    # port_mapper = PortMapper()
    # port_mapper.add_portmapping(8080, 8080, 'TCP', 'Electron')

    # Set the identifier (unique Id) for our node (if it doesn't exist)
    node_identifier = set_config(key='node_identifier', value=uuid4().hex)

    # app.request_headers = {
    #     'content-type': 'application/json',
    #     'x-node-identifier': node_identifier,
    #     'x-node-ip': port_mapper.external_ip,
    #     'x-node-port': port_mapper.external_port,
    # }

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


def we_should_still_be_mining():
    return True
