import unittest
import toy_crypto

class ToyCrypto(unittest.TestCase):

    def test_signature(self):

        msg = 'This is the message to be transmitted'.encode()

        kg = toy_crypto.KeyGen(11, 29)
        pub, priv = kg.gen_keys()

        sig = priv.signature(msg)

        self.assertTrue(pub.verify_signature(sig, msg))

