package org.learn.blockchains.model;

import java.util.List;

/*
 *   'message': "New Block Forged",
	        'index': block['index'],
	        'transactions': block['transactions'],
	        'proof': block['proof'],
	        'previous_hash': block['previous_hash'],
 */
public class MineResponse {
	private String message;
	private Long index;
	private List<Transaction> transactions;
	private Long proof;
	private String previousHsh;

	public String getMessage() {
		return message;
	}

	public void setMessage(String message) {
		this.message = message;
	}

	public Long getIndex() {
		return index;
	}

	public void setIndex(Long index) {
		this.index = index;
	}

	public List<Transaction> getTransactions() {
		return transactions;
	}

	public void setTransactions(List<Transaction> transactions) {
		this.transactions = transactions;
	}

	public Long getProof() {
		return proof;
	}

	public void setProof(Long proof) {
		this.proof = proof;
	}

	public String getPreviousHsh() {
		return previousHsh;
	}

	public void setPreviousHsh(String previousHsh) {
		this.previousHsh = previousHsh;
	}

	public MineResponse(String message, Long index, List<Transaction> transactions, Long proof, String previousHsh) {
		super();
		this.message = message;
		this.index = index;
		this.transactions = transactions;
		this.proof = proof;
		this.previousHsh = previousHsh;
	}

}
