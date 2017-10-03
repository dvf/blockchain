import { v4 } from 'uuid';
import { JsonController, Get } from 'routing-controllers';
import { createTransaction, createBlock, lastBlock, proofOfWork } from '../blockchain';

const nodeIdentifier = v4().replace(/-/g, '');

@JsonController('/mine')
export class MineController {
	@Get()
	public mine(): IBlock {
		// We run the proof of work algorithm to get the next proof...
		let lastProof = lastBlock().proof;
		let proof = proofOfWork(lastProof);

    // We must receive a reward for finding the proof.
    // The sender is "0" to signify that this node has mined a new coin.
		createTransaction('0', nodeIdentifier, 1);

    // Forge the new Block by adding it to the chain
		let block = createBlock(proof);

		return block;
	}
}
