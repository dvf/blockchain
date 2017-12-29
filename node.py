from sanic import Sanic
from sanic.response import json
from sqlalchemy import func

from database import Peer, db, reset_db
from tasks import initiate_node, peer_discovery
from mining import mining_controller


app = Sanic()

reset_db()
initiate_node(app)

app.add_task(peer_discovery(app))
app.add_task(mining_controller(app))


@app.route("/")
async def peers(request):
    random_peers = db.query(Peer).order_by(func.random()).limit(256).all()
    return json(random_peers)


@app.route("/transactions")
async def current_transactions(request):
    if request.method == 'GET':
        return json(app.blockchain.current_transactions)
    elif request.method == 'POST':
        return json({"text": "thanks for your transaction"})


@app.route("/blocks")
async def blocks(request):
    # height = request.parsed_args['height']
    random_blocks = app.blockchain.get_blocks()
    return json(random_blocks)


if __name__ == "__main__":
    app.run(debug=False, host="0.0.0.0", port=8080)
