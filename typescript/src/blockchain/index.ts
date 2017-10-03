import { hash } from './hash';
import { Node } from './node';
import { validateChain, validateProof } from './validate';

export const transactions: Array<ITransaction> = [];
export const nodes = new Map<string, Node>();

export let chain: Array<IBlock> = [];

/*
	Simple Proof of Work Algorithm:
	- Find a number p' such that hash(pp') contains leading 4 zeroes, where p is the previous p'
	- p is the previous proof, and p' is the new proof
	@param last_proof: <int>
	@return: <int>
*/
export function proofOfWork(lastProof: number): number {
	let proof = 0;

	while (!validateProof(lastProof, proof))
		proof += 1;

	return proof;
}

export function lastBlock(): IBlock {
	// Returns the last Block in the chain
	return chain[chain.length - 1];
}

/*
	Create a new Block in the Blockchain
	@param proof: <int> The proof given by the Proof of Work algorithm
	@param previous_hash: (Optional) <str> Hash of previous Block
	@return: <dict> New Block
*/
export function createBlock(proof: number, previousHash?: string): IBlock {
	// Creates a new Block and adds it to the chain

	let block = {
		index: chain.length + 1,
		timestamp: Date.now(),
		transactions: transactions.slice(),
		proof,
		previousHash: previousHash || hash(lastBlock()),
	};

	// Reset the current list of transactions
	transactions.length = 0;

	chain.push(block);

	return block;
}

/*
	Creates a new transaction to go into the next mined Block
	@param sender: <str> Address of the Sender
	@param recipient: <str> Address of the Recipient
	@param amount: <int> Amount
	@return: <int> The index of the Block that will hold this transaction
*/
export function createTransaction(sender: string, recipient: string, amount: number): number {
	// Adds a new transaction to the list of transactions

	transactions.push({
		sender,
		recipient,
		amount
	});

	return lastBlock().index + 1;
}

/*
	Add a new node to the list of nodes
	@param address: <str> Address of node. Eg. 'http://192.168.0.5:5000'
	@return: None
*/
export function registerNode(address: string): void {
	nodes.set(address, new Node(address));
}

/*
	This is our Consensus Algorithm, it resolves conflicts
	by replacing our chain with the longest one in the network.
	@return: <bool> True if our chain was replaced, False if not
*/
export async function resolveConflicts(): Promise<boolean> {
	let replacement = null;

	// We're only looking for chains longer than ours
	let maxLength = chain.length;
	let neighbors = Array.from(nodes.values());

  // Grab and verify the chains from all the nodes in our network
	for (let i = 0; i < neighbors.length; i++) {
		let neighbor = neighbors[i];
		let response = await neighbor.getChain();

		if (response) {
			let { chain, length } = response;

			// Check if the length is longer and the chain is valid
			if (length > maxLength && validateChain(chain)) {
				maxLength = length;
				replacement = chain;
			}
		}
	}

  // Replace our chain if we discovered a new, valid chain longer than ours
	if (replacement !== null) {
		chain = replacement;
		return true;
	}

	return false;
}

(function init() {
	// Create the genesis block
	createBlock(100, '1');
})();
