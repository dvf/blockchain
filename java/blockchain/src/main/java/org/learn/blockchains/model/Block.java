package org.learn.blockchains.model;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonPropertyOrder;

@JsonPropertyOrder(alphabetic=true)
public class Block {
	private Long index;
	private Long timestamp;
	private List<Transaction> transactions;
	private Long proof;
	private String previousHash;

	public Long getIndex() {
		return index;
	}

	public void setIndex(Long index) {
		this.index = index;
	}

	public Long getTimestamp() {
		return timestamp;
	}

	public void setTimestamp(Long timestamp) {
		this.timestamp = timestamp;
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

	public String getPreviousHash() {
		return previousHash;
	}

	public void setPreviousHash(String previousHash) {
		this.previousHash = previousHash;
	}

	@Override
	public String toString() {
		return "Block [index=" + index + ", timestamp=" + timestamp + ", transactions=" + transactions + ", proof="
				+ proof + ", previousHash=" + previousHash + "]";
	}

}
