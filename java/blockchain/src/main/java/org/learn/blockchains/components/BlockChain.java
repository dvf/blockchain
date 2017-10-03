package org.learn.blockchains.components;

import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import org.learn.blockchains.model.Block;
import org.learn.blockchains.model.Transaction;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.common.hash.Hashing;

public class BlockChain {

	private List<Transaction> currentTransactions;
	private List<Block> chain;
	private Set<String> nodes;

	public BlockChain() throws JsonProcessingException {
		super();
		currentTransactions = new ArrayList<>();
		chain = new ArrayList<>();
		nodes = new HashSet<>();
		this.newBlock(100L, "1");
	}

	public Block lastBlock() {
		return this.chain.get(this.chain.size() - 1);
	}

	public Block newBlock(Long proof, String previusHash) throws JsonProcessingException {
		Block block = new Block();
		int chainSize = this.chain.size();
		block.setIndex(chainSize + 1L);
		block.setTimestamp(new Date().getTime());
		block.setTransactions(currentTransactions);
		block.setProof(proof);
		block.setPreviousHash((previusHash != null) ? previusHash : this.hash(lastBlock()));
		this.currentTransactions = new ArrayList<>();
		this.chain.add(block);
		return block;
	}

	public Long newTransaction(String sender, String recipient, String amount) {
		Transaction transaction = new Transaction();
		transaction.setAmount(amount);
		transaction.setRecipient(recipient);
		transaction.setSender(sender);
		this.currentTransactions.add(transaction);
		return lastBlock().getIndex() + 1L;
	}

	private String hash(Block block) throws JsonProcessingException {
		ObjectMapper mapper = new ObjectMapper();
		String json = mapper.writeValueAsString(block);
		return Hashing.sha256().hashString(json, StandardCharsets.UTF_8).toString();
	}

	public Long proofOfWork(Long lastProof) {
		Long proof = 0L;
		while (validProof(lastProof, proof) != true)
			proof += 1L;
		return proof;
	}

	public static boolean validProof(Long lastProof, Long proof) {
		String s = "" + lastProof + "" + proof;
		String sha256 = Hashing.sha256().hashString(s, StandardCharsets.UTF_8).toString();
		return sha256.endsWith("0000");
	}

	@Override
	public String toString() {
		return "BlockChain [currentTransactions=" + currentTransactions + ", chain=" + chain + ", nodes=" + nodes + "]";
	}

}
