import { JsonController, UseBefore, Get, Post, BodyParam, HttpCode } from 'routing-controllers';
import { Server } from 'quiq';
import { nodes, chain, registerNode, resolveConflicts } from '../blockchain';

const { PayloadValidator } = Server.middleware;

interface IRegisterNodesResponse {
	nodes: Array<string>;
}

interface IResolveNodesResponse {
	replaced?: Array<IBlock>;
	chain?: Array<IBlock>;
}

@JsonController('/nodes/')
export class NodesController {
	@UseBefore(
		PayloadValidator('nodes')
	)
	@Post('register')
	@HttpCode(201)
	public register(@BodyParam('nodes') addresses: Array<string>): IRegisterNodesResponse {
		addresses.forEach((address) => registerNode(address));

		return {
			nodes: Array.from(nodes.keys())
		};
	}

	@Get('resolve')
	public async resolve(): Promise<IResolveNodesResponse> {
		let isReplaced = await resolveConflicts();

		if (isReplaced) {
			return {
				replaced: chain
			}
		}
		else {
			return {
				chain
			}
		}
	}
}
