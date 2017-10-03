package org.learn.blockchains;



import static org.assertj.core.api.Assertions.assertThat;

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

}
