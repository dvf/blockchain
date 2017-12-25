from uuid import uuid4

from sanic import Sanic
from sanic.response import json
from sqlalchemy import func

from blockchain import Blockchain
from database import Peer, db, reset_db
from helpers import get_config, set_config
from tasks import populate_peers, watch_blockchain, add_stuff, mining_controller


app = Sanic()
app.debug = True


@app.listener('before_server_start')
async def set_node_identifier(_app, loop):
    node_identifier = get_config('node_identifier')
    if not node_identifier:
        set_config(key='node_identifier', value=uuid4().hex)

reset_db()
app.blockchain = Blockchain()
app.add_task(populate_peers(app))
# app.add_task(watch_blockchain(app))
# app.add_task(add_stuff(app))
app.add_task(mining_controller)


@app.route("/")
async def peers(request):
    random_peers = db.query(Peer).order_by(func.random()).limit(256).all()
    return json(random_peers)


@app.route("/transactions")
async def current_transactions(request):
    return json(app.blockchain.current_transactions())


@app.route("/blocks")
async def blocks(request):
    # height = request.parsed_args['height']
    blocks = app.blockchain.get_blocks()
    return json(blocks)


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=8080)
