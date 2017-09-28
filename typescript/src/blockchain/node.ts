import { Server } from 'quiq';

const webClient = new Server.JsonWebClient('blockchain');

export class Node {
	constructor(private address: string) { }

	public async getChain(): Promise<IChainResponse> {
		let { code, data } = await webClient.get<IChainResponse>(`${ this.address }/chain`);

		if (code === 200)
			return data;

		return;
	}
}
