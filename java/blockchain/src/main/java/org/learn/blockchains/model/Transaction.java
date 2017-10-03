package org.learn.blockchains.model;

import org.hibernate.validator.constraints.NotBlank;

public class Transaction {
	@NotBlank(message= "is blank")
	private String sender;
	@NotBlank(message= "is blank")
	private String recipient;
	@NotBlank(message= "is blank")
	private String amount;

	public String getSender() {
		return sender;
	}

	public void setSender(String sender) {
		this.sender = sender;
	}

	public String getRecipient() {
		return recipient;
	}

	public void setRecipient(String recipient) {
		this.recipient = recipient;
	}

	public String getAmount() {
		return amount;
	}

	public void setAmount(String amount) {
		this.amount = amount;
	}

	@Override
	public String toString() {
		return "Transaction [sender=" + sender + ", recipient=" + recipient + ", amount=" + amount + "]";
	}

}
