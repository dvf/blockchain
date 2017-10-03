package org.learn.blockchains.model;

import java.util.List;

import org.hibernate.validator.constraints.NotEmpty;

public class NodesRequest {
	@NotEmpty
	private List<String> nodes;

	public List<String> getNodes() {
		return nodes;
	}

	public void setNodes(List<String> nodes) {
		this.nodes = nodes;
	}
	
	
}
