import os
import blockchain
import unittest
import requests
import threading
from multiprocessing import Process
from toy_crypto import KeyGen

class FlaskBlockchainTestCase(unittest.TestCase):

    addr = '127.0.0.1'
    port = 5000

    def setUp(self):

        blockchain.app.testing = True
        self.alice_public, self.alice_private = KeyGen(11, 29).gen_keys()
        self.bob_public, self.bob_private = KeyGen(11, 43).gen_keys()

        self.t = threading.Thread(target=blockchain.app.run, daemon=True,
                                  kwargs={'threaded': True,
                                         'host':'0.0.0.0','port':self.port})
        self.t.start()

    def test_register_addr(self):
        req = requests.get(f'http://{self.addr}:{self.port}/addresses')
        print("Addresses:", req.content.decode())

        # Register a new address with public key
        req = requests.post(f'http://{self.addr}:{self.port}/addresses/register',
                            json={'address': 'alice',
                             'key': str(self.alice_public)})
        print("Register new key attempt:", req.content.decode())

        req = requests.post(f'http://{self.addr}:{self.port}/addresses/register',
                            json={'address': 'bob',
                             'key': str(self.bob_public)})
        print("Register new key attempt:", req.content.decode())

        # Make sure invalid key string generates error
        req = requests.post(f'http://{self.addr}:{self.port}/addresses/register',
                            json={'address': 'alice',
                             'key': 'malformed'})
        self.assertTrue(req.status_code != 200,"Malformed key should generate error.")


        # Mine to get added to blockchain
        req = requests.get(f'http://{self.addr}:{self.port}/mine?recipient_addr=alice')
        print("Mine:", req.content.decode())
        self.assertTrue(req.status_code == 200,"Mining response not equal to 200")

        req = requests.get(f'http://{self.addr}:{self.port}/addresses')
        print("Addresses:", req.content.decode())

        #req = requests.get(f'http://{self.addr}:{self.port}/chain')
        #print("Chain:", req.content.decode())

        req = requests.get(f'http://{self.addr}:{self.port}/balances')
        print("Balances:", req.content.decode())

        # Alice now has 1 token.  Send to bob
        j = {'sender': 'alice',
              'recipient': 'bob',
              'amount': 1}
        msg = f'sender:{j["sender"]},recipient:{j["recipient"]},amount:{j["amount"]}'
        sig = self.alice_private.signature(msg.encode())
        j['signature'] = sig
        req = requests.post(f'http://{self.addr}:{self.port}/transactions/new',
                            json=j)
        print("Transaction:", req.content.decode())

        # Transaction doesn't actually show up until new block mined
        req = requests.get(f'http://{self.addr}:{self.port}/mine?recipient_addr=alice')
        print("Mine:", req.content.decode())
        self.assertTrue(req.status_code == 200,"Mining response not equal to 200")

        req = requests.get(f'http://{self.addr}:{self.port}/balances')
        print("Balances2:", req.content.decode())

        # See what happens with invalid signature
        j = {'sender': 'alice',
              'recipient': 'bob',
              'amount': 1}
        msg = f'sender:{j["sender"]},recipient:{j["recipient"]},amount:{j["amount"]}'
        sig = self.bob_private.signature(msg.encode())  # using wrong key to encrypt
        j['signature'] = sig
        req = requests.post(f'http://{self.addr}:{self.port}/transactions/new',
                            json=j)
        self.assertTrue(req.status_code != 200, "Wrong key should have generated error")
        print("Transaction:", req.content.decode())

        # See what happens with invalid signature
        j = {'sender': 'alice',
              'recipient': 'bob',
              'amount': 1}
        msg = f'sender:{j["sender"]},recipient:{j["recipient"]},amount:{j["amount"]}'
        sig = self.alice_private.signature(msg.encode())
        sig[0] += 1
        j['signature'] = sig
        req = requests.post(f'http://{self.addr}:{self.port}/transactions/new',
                            json=j)
        self.assertTrue(req.status_code != 200, "Wrong key should have generated error")
        print("Transaction:", req.content.decode())


    def test_view_chain(self):
        #req = requests.get(f'http://{self.addr}:{self.port}/chain')
        #print(req.content.decode())
        pass


    def tearDown(self):
        pass #self.t._._Thread_stop()
