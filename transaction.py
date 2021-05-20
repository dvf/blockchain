from base64 import b64encode, b64decode
import ecdsa


class Transaction:
    def __init__(self, sender, recipient, amount, signature=None):
        self.sender = sender
        self.recipient = recipient
        self.amount = amount
        self.signature = signature

    def to_dict(self):
        return {
            "sender": b64encode(self.sender).decode(),
            "recipient": b64encode(self.recipient).decode(),
            "amount": self.amount,
            "signature": b64encode(self.signature)
        }

    def is_signature_verified(self):
        verification_key = self.sender_pub_key
        try:
            verification_key.verify(signature=self.signature, data=str(self.raw_transaction).encode())
        except ecdsa.BadSignatureError:
            return False
        return True

    def create_signature(self, private_key: ecdsa.SigningKey):
        transaction_data = self.raw_transaction
        sender_verifying_key = ecdsa.VerifyingKey.from_string(transaction_data.get('sender'))
        # raise sender_verifying_key
        if sender_verifying_key != private_key.get_verifying_key():
            raise ValueError('SigningKey is not the sender')
        self.signature = self.sign(private_key)

    def sign(self, private_key: ecdsa.SigningKey):
        transaction_data = str(self.raw_transaction).encode()
        return private_key.sign(transaction_data)

    @property
    def sender_pub_key(self) -> ecdsa.VerifyingKey:
        public_key = ecdsa.VerifyingKey.from_string(self.sender)
        return public_key

    @property
    def raw_transaction(self):
        return {"sender": self.sender, "recipient": self.recipient, "amount": self.amount}

    @classmethod
    def from_dict(cls, sender, recipient, amount, signature):
        sender = b64decode(sender)
        recipient = b64decode(recipient)
        signature = b64decode(signature)
        return Transaction(sender, recipient, amount, signature)
