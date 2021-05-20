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
            "sender": self.sender,
            "recipient": self.recipient,
            "amount": self.amount,
            "signature": self.signature
        }

    def is_signature_verified(self):
        verification_key = self.sender_pub_key
        try:
            verification_key.verify(signature=b64decode(self.signature), data=str(self.raw_transaction).encode())
        except ecdsa.BadSignatureError:
            return False
        return True

    def create_signature(self, private_key: ecdsa.SigningKey):
        transaction_data = self.raw_transaction
        sender_verifying_key = ecdsa.VerifyingKey.from_string(b64decode(transaction_data.get('sender')))
        # raise sender_verifying_key
        if sender_verifying_key != private_key.get_verifying_key():
            raise ValueError('SigningKey is not the sender')
        self.signature = self.sign(private_key)

    def sign(self, private_key: ecdsa.SigningKey):
        transaction_data = str(self.raw_transaction).encode()
        signature = b64encode(private_key.sign(transaction_data))
        return signature

    @property
    def sender_pub_key(self) -> ecdsa.VerifyingKey:
        public_key = ecdsa.VerifyingKey.from_string(b64decode(self.sender))
        return public_key

    @property
    def raw_transaction(self):
        return {"sender": self.sender, "recipient": self.recipient, "amount": self.amount}