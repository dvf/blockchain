import ecdsa


class Transaction:
    def __init__(self, sender, recipient, amount, signature):
        self.sender = sender
        self.recipient = recipient
        self.amount = amount
        self.signature = signature

    def to_dict(self):
        return {
            "sender": self.sender,
            "recipient": self.recipient,
            "amount": self.amount,
            "signature": self.signature
        }

    def is_signature_verified(self):
        raw_transaction = {"sender": self.sender, "recipient": self.recipient, "amount": self.amount}
        verification_key = self.sender_pub_key
        try:
            verification_key.verify(message=raw_transaction, signature=self.signature)
        except ecdsa.BadSignatureError:
            return False
        return True

    @property
    def sender_pub_key(self) -> ecdsa.VerifyingKey:
        public_key = ecdsa.VerifyingKey.from_string(self.sender)
        return public_key
