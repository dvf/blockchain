interface IBlock {
	index: number;
	timestamp: number;
	transactions: Array<ITransaction>;
	proof: number;
	previousHash: string;
}
