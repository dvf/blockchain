import unittest
import toy_crypto

class ToyCrypto(unittest.TestCase):

    def test_signature(self):

        msg = 'This is the message to be transmitted'.encode()

        kg = toy_crypto.KeyGen(11, 29)
        pub, priv = kg.gen_keys()

        self.assertTrue(pub != priv,"Public and private keys are the same!")
        print(pub)
        print(priv)
        sig = priv.signature(msg)
        self.assertTrue(pub.verify_signature(sig, msg),"Signature not verified")

    def test_serialization(self):
        kg = toy_crypto.KeyGen(11, 29)
        pub, priv = kg.gen_keys()

        s = str(pub)

        pub2 = toy_crypto.Key.fromstring(s)

        self.assertTrue(pub == pub2, "Deserialized not equal to original")


