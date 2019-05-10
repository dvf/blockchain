const Blockchain = require("./blockchain");
const {send} = require("micro");

const blockchain = new Blockchain();


module.exports = async (request, response) => {
    const route = request.url;

    // Keep track of the peers that have contacted us
    blockchain.addPeer(request.headers.host);

    let output;

    switch (route) {
        case "/new_block":
            output = blockchain.newBlock();
            break;

        case "/last_block":
            output = blockchain.lastBlock();
            break;

        case "/get_peers":
            output = blockchain.getPeers();
            break;

        case "/submit_transaction":
            output = blockchain.addTransaction(transaction);
            break;

        default:
            output = blockchain.lastBlock();

    }
    send(response, 200, output);
};
