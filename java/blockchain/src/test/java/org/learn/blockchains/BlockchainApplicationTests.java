package org.learn.blockchains;



import static org.assertj.core.api.Assertions.assertThat;

import java.net.MalformedURLException;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.learn.blockchains.components.BlockChain;
import org.learn.blockchains.model.Block;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import com.fasterxml.jackson.core.JsonProcessingException;

@RunWith(SpringRunner.class)
@SpringBootTest
public class BlockchainApplicationTests {

	@Test
	public void validProof() throws JsonProcessingException {
		BlockChain blockChain = new BlockChain();
		Block lastBlock = blockChain.lastBlock();
		Long proofOfWork = blockChain.proofOfWork(lastBlock.getProof());
		assertThat(blockChain.validProof(lastBlock.getProof(), proofOfWork)).isEqualTo(true);
	}
	
	@Test
	public void addingNode() throws JsonProcessingException, MalformedURLException {
		BlockChain blockChain = new BlockChain();
		blockChain.registerNode("https://github.com/edelfinato/blockchain/blob/master/blockchain.py");
		assertThat(blockChain.getNodes().size()).isEqualTo(1);
	}
	
	@Test(expected=MalformedURLException.class)
	public void addingNodeEx() throws JsonProcessingException, MalformedURLException {
		BlockChain blockChain = new BlockChain();
		blockChain.registerNode("/github.com/edelfinato/blockchain/blob/master/blockchain.py");
	}
	
	@Test
	public void validChain() throws JsonProcessingException{
		BlockChain blockChain = new BlockChain();
		Block lastBlock = blockChain.lastBlock();
		Long proofOfWork = blockChain.proofOfWork(lastBlock.getProof());
		blockChain.newBlock(proofOfWork, BlockChain.hash(lastBlock));
		assertThat(blockChain.validChain()).isEqualTo(true);
	}
	
	@Test
	public void noValidChain() throws JsonProcessingException{
		BlockChain blockChain = new BlockChain();
		Block lastBlock = blockChain.lastBlock();
		blockChain.newBlock(12L, BlockChain.hash(lastBlock));
		assertThat(blockChain.validChain()).isEqualTo(false);
	}

}
