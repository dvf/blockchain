import { JsonController, Get } from 'routing-controllers';
import { chain } from '../blockchain';

@JsonController('/chain')
export class ChainController {
	@Get()
	public chain(): IChainResponse {
		let { length } = chain;

		return {
			chain,
			length
		};
	}
}
