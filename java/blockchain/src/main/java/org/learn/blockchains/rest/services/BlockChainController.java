package org.learn.blockchains.rest.services;

import static org.learn.blockchains.util.Maps.entriesToMap;
import static org.learn.blockchains.util.Maps.entry;

import java.io.IOException;
import java.net.MalformedURLException;
import java.util.Map;
import java.util.UUID;
import java.util.stream.Collectors;
import java.util.stream.Stream;

import javax.validation.Valid;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;

import org.learn.blockchains.components.BlockChain;
import org.learn.blockchains.model.Block;
import org.learn.blockchains.model.ChainResponse;
import org.learn.blockchains.model.MineResponse;
import org.learn.blockchains.model.NodesRequest;
import org.learn.blockchains.model.Transaction;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import com.fasterxml.jackson.core.JsonProcessingException;

@Path("/")
@Component
public class BlockChainController {

	@Autowired
	private BlockChain blockChain;
	private static UUID nodeId = UUID.randomUUID();

	@Path("/mine")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	public MineResponse mine() throws JsonProcessingException {
		Block lastBlock = blockChain.lastBlock();
		Long lastProof = lastBlock.getProof();
		Long proof = blockChain.proofOfWork(lastProof);
		blockChain.newTransaction("0", nodeId.toString(), "1");
		Block newBlock = blockChain.newBlock(proof, BlockChain.hash(lastBlock));
		return new MineResponse("New Block Forged", newBlock.getIndex(), newBlock.getTransactions(),
				newBlock.getProof(), newBlock.getPreviousHash());
	}

	@Path("/chain")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	public ChainResponse fullChain() throws JsonProcessingException {
		ChainResponse chainResponse = new ChainResponse();
		chainResponse.setChain(blockChain.getChain());
		chainResponse.setLength(blockChain.getChain().size());
		return chainResponse;
	}

	@Path("/transactions/new")
	@POST
	@Produces(MediaType.APPLICATION_JSON)
	@Consumes(MediaType.APPLICATION_JSON)
	public Map<String, String> newTransaction(@Valid Transaction trans) throws JsonProcessingException {
		Long index = blockChain.newTransaction(trans.getSender(), trans.getRecipient(), trans.getAmount());
		return Stream.of(entry("message", "Transaction will be added to Block " + index)).collect(entriesToMap());
	}

	@Path("/nodes/register")
	@POST
	@Produces(MediaType.APPLICATION_JSON)
	@Consumes(MediaType.APPLICATION_JSON)
	public Map<String, String> regNodes(@Valid NodesRequest nodeReq) throws JsonProcessingException {
		nodeReq.getNodes().stream().forEach(t -> {
			try {
				blockChain.registerNode(t);
			} catch (MalformedURLException e) {
				e.printStackTrace();
			}
		});
		return Stream.of(entry("message", "New nodes have been added "),
				entry("total_nodes", "" + blockChain.getNodes().size())).collect(entriesToMap());
	}

	@Path("/nodes/resolve")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	public Map<String, Object> consensus() throws IOException {
		boolean resolveConflicts = blockChain.resolveConflicts();
		if (resolveConflicts)
			return Stream.of(entry("message", "Our chain was replaced "),
					entry("chain",  blockChain.getChain())).collect(Collectors.toMap((e) -> e.getKey(), (e) -> e.getValue()));
		else
			return Stream.of(entry("message", "Our chain is authoritative "),
					entry("chain",  blockChain.getChain())).collect(Collectors.toMap((e) -> e.getKey(), (e) -> e.getValue()));
	}

}
