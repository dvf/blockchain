import { JsonController, UseBefore, Post, BodyParam, HttpCode } from 'routing-controllers';
import { middleware } from 'quiq';
import { createTransaction } from '../blockchain';

interface INewTransactionResponse {
	block: number
}

@JsonController('/transactions/')
export class TransactionsController {
	@UseBefore(
		middleware.PayloadValidator('sender', 'recipient', 'amount')
	)
	@Post('new')
	@HttpCode(201)
	public new(
		@BodyParam('sender') sender: string,
		@BodyParam('recipient') recipient: string,
		@BodyParam('amount') amount: number): INewTransactionResponse {

    // Create a new Transaction
		let index = createTransaction(sender, recipient, amount);

		return {
			block: index
		};
	}
}
