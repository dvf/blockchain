import os
import blockchain
import unittest
from toy_crypto import KeyGen
import json

class FlaskBlockchainTestCase(unittest.TestCase):

    def setUp(self):

        blockchain.app.testing = True
        self.alice_public, self.alice_private = KeyGen(11, 29).gen_keys()
        self.bob_public, self.bob_private = KeyGen(11, 43).gen_keys()

        self.app = blockchain.app.test_client()

    def test_register_addr(self):
        req = self.app.get('/addresses')
        print("Addresses:", req.data.decode())

        # Register a new address with public key
        req = self.app.post('/addresses/register',
                            data=json.dumps({'address': 'alice',
                                  'key': str(self.alice_public)}),
                            content_type='application/json')
        print("Register new key attempt:", req.data.decode())

        req = self.app.post('/addresses/register',
                            data=json.dumps({'address': 'bob',
                                             'key': str(self.bob_public)}),
                            content_type='application/json')
        print("Register new key attempt:", req.data.decode())

        # Make sure invalid key string generates error
        req = self.app.post('/addresses/register',
                            data=json.dumps({'address': 'alice',
                                             'key': 'malformed'}),
                            content_type='application/json')
        self.assertTrue(req.status_code != 200,"Malformed key should generate error.")

        # Mine to get added to blockchain
        req = self.app.get('/mine?recipient_addr=alice')
        print("Mine:", req.data.decode())
        self.assertTrue(req.status_code == 200,"Mining response not equal to 200")

        req = self.app.get('/addresses')
        print("Addresses:", req.data.decode())

        req = self.app.get('/balances')
        print("Balances:", req.data.decode())

        # Alice now has 1 token.  Send to bob
        j = {'sender': 'alice',
              'recipient': 'bob',
              'amount': 1}
        msg = f'sender:{j["sender"]},recipient:{j["recipient"]},amount:{j["amount"]}'
        sig = self.alice_private.signature(msg.encode())
        j['signature'] = sig
        req = self.app.post('/transactions/new',
                            data=json.dumps(j),
                            content_type='application/json')
        print("Transaction:", req.data.decode())

        # Transaction doesn't actually show up until new block mined
        req = self.app.get('/mine?recipient_addr=alice')
        print("Mine:", req.data.decode())
        self.assertTrue(req.status_code == 200,"Mining response not equal to 200")

        req = self.app.get('/balances')
        print("Balances2:", req.data.decode())

        # See what happens with invalid signature
        j = {'sender': 'alice',
              'recipient': 'bob',
              'amount': 1}
        msg = f'sender:{j["sender"]},recipient:{j["recipient"]},amount:{j["amount"]}'
        sig = self.bob_private.signature(msg.encode())  # using wrong key to encrypt
        j['signature'] = sig
        req = self.app.post('/transactions/new',
                            data=json.dumps(j),
                            content_type='application/json')
        self.assertTrue(req.status_code != 200, "Wrong key should have generated error")
        print("Transaction:", req.data.decode())

        # See what happens with invalid signature
        j = {'sender': 'alice',
              'recipient': 'bob',
              'amount': 1}
        msg = f'sender:{j["sender"]},recipient:{j["recipient"]},amount:{j["amount"]}'
        sig = self.alice_private.signature(msg.encode())
        sig[0] += 1
        j['signature'] = sig
        req = self.app.post('/transactions/new',
                            data=json.dumps(j),
                            content_type='application/json')
        self.assertTrue(req.status_code != 200, "Wrong key should have generated error")
        print("Transaction:", req.data.decode())

