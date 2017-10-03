package org.learn.blockchains.model;

import java.util.List;

public class ChainResponse {
	private Integer length;
	private List<Block> chain;

	public Integer getLength() {
		return length;
	}

	public void setLength(Integer length) {
		this.length = length;
	}

	public List<Block> getChain() {
		return chain;
	}

	public void setChain(List<Block> chain) {
		this.chain = chain;
	}
}
