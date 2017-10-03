import { Writeln } from 'writeln';
import { sha256, hash } from './hash';

const logger = new Writeln('Blockchain');

/*
	Determine if a given blockchain is valid
	@param chain: <list> A blockchain
	@return: <bool> True if valid, False if not
*/
export function validateChain(chain: Array<IBlock>): boolean {
	let lastBlock = chain[0];
	let currentIndex = 1;

	while (currentIndex < chain.length) {
		let block = chain[currentIndex];

		logger.debug('validate', {
			lastBlock,
			block
		});

		// Check that the hash of the block is correct
		if (block.previousHash !== hash(lastBlock))
			return false;

		// Check that the Proof of Work is correct
		if (!validateProof(lastBlock.proof, block.proof))
			return false;

		lastBlock = block;
		currentIndex += 1;
	}

	return true
}

/*
	Validates the Proof: Does hash(last_proof, proof) contain 4 leading zeroes?
	@param last_proof: <int> Previous Proof
	@param proof: <int> Current Proof
	@return: <bool> True if correct, False if not.
*/
export function validateProof(lastProof: number, proof: number): boolean {
	let guess = `${ lastProof}${ proof }`;
	let guessHash = sha256(guess);
	return guessHash.indexOf('0000') === 0;
}