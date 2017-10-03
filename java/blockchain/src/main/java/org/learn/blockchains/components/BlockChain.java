package org.learn.blockchains.components;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import org.learn.blockchains.model.Block;
import org.learn.blockchains.model.ChainResponse;
import org.learn.blockchains.model.Transaction;
import org.springframework.stereotype.Component;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.common.hash.Hashing;

import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;

@Component
public class BlockChain {

	private List<Transaction> currentTransactions;
	private List<Block> chain;
	private Set<String> nodes;

	public void registerNode(String address) throws MalformedURLException {
		URL url = new URL(address);
		nodes.add(url.getHost());
	}

	public BlockChain() throws JsonProcessingException {
		super();
		currentTransactions = new ArrayList<>();
		chain = new ArrayList<>();
		nodes = new HashSet<>();
		this.newBlock(100L, "1");
	}

	//TODO handle the IOException or false node
	public boolean resolveConflicts() throws IOException {
		Set<String> neighbours = this.nodes;
		List<Block> newChain = null;
		int max_length = this.chain.size();

		for (String node : neighbours) {
			Request request = new Request.Builder().url("http://" + node + "/chain").build();
			Response response = new OkHttpClient().newCall(request).execute();
			if (response.code() == 200) {
				ObjectMapper mappper = new ObjectMapper();
				ChainResponse map = mappper.readValue(response.body().string(), ChainResponse.class);
				Integer length = map.getLength();
				List<Block> chain = map.getChain();
				if (length > max_length && BlockChain.validChain(chain)) {
					max_length = length;
					newChain = chain;
				}
			}
		}
		if (newChain != null) {
			this.chain = newChain;
			return true;
		}
		return false;
	}

	public static boolean validChain(List<Block> chain) throws JsonProcessingException {
		if (chain == null || chain.isEmpty())
			return false;
		Block lastBlock = chain.get(0);
		for (int currentIndex = 1; currentIndex < chain.size(); currentIndex++) {
			Block currentBlock = chain.get(currentIndex);
			System.out.println(lastBlock);
			System.out.println(currentBlock);
			if (!currentBlock.getPreviousHash().equals(hash(lastBlock))) {
				return false;
			}
			if (!BlockChain.validProof(lastBlock.getProof(), currentBlock.getProof())) {
				return false;
			}
			lastBlock = currentBlock;
		}
		return true;
	}

	public boolean validChain() throws JsonProcessingException {
		return validChain(chain);
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
		block.setPreviousHash((previusHash != null) ? previusHash : hash(lastBlock()));
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

	public static String hash(Block block) throws JsonProcessingException {
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

	public List<Transaction> getCurrentTransactions() {
		return currentTransactions;
	}

	public List<Block> getChain() {
		return chain;
	}

	public Set<String> getNodes() {
		return nodes;
	}

	@Override
	public String toString() {
		return "BlockChain [currentTransactions=" + currentTransactions + ", chain=" + chain + ", nodes=" + nodes + "]";
	}

}
