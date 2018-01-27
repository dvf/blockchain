# -*- coding: utf-8 -*-

import hashlib
import re

# https://en.wikibooks.org/wiki/Cryptography/A_Basic_Public_Key_Example

def isprime(n):
    # 0 and 1 are not primes
    if n < 2:
        return False
    # 2 is the only even prime number
    if n == 2: 
        return True    
    # all other even numbers are not primes
    if not n & 1: 
        return False
    # range starts with 3 and only needs to go up the squareroot of n
    # for all odd numbers
    for x in range(3, int(n**0.5)+1, 2):
        if n % x == 0:
            return False
    return True

def factors(n):
    return [i for i in range(1,n) if (n % i) == 0]

def gen_primes(n=1):
    if isprime(n):
        yield n
    yield from gen_primes(n+1)

def gen_coprimes(n,N,seed=None):
    """
    https://en.wikipedia.org/wiki/Coprime_integers#Generating_all_coprime_pairs
    Branch 1: {\displaystyle (2m-n,m)} (2m-n,m)
    Branch 2: {\displaystyle (2m+n,m)} (2m+n,m)
    Branch 3: {\displaystyle (m+2n,n)} (m+2n,n)
    """
    if n == N:
        return
    if seed is None:
        yield from gen_coprimes(n,N,(2,1))
        yield from gen_coprimes(n,N,(3,1))
    else:
        if seed[0] < seed[1]: return
        yield seed
        yield from gen_coprimes(n+1,N,(2*seed[0]-  seed[1],seed[0]))
        yield from gen_coprimes(n+1,N,(2*seed[0]+  seed[1],seed[0]))
        yield from gen_coprimes(n+1,N,(  seed[0]+2*seed[1],seed[1]))

def iscoprime(a,b):
    seta = set(factors(a))
    setb = set(factors(b))
    if (seta & setb) == set({1}):
        return True
    return False

def get_coprimes(N):
    """HORRIBLY inefficient, but this is just educational. :)
    """
    return [n for n in range(N-1,0,-1) if iscoprime(n,N)]        

class Key:
    
    """
    modulus set the upper limit of value that can be encrypted/decrypted
    """
    
    def __init__(self,e,m):
        self._e = e
        self._m = m
                
    def _encrypt(self,val):
        return (val**self._e) % self._m
    
    def encrypt(self,barray):
        if not isinstance(barray,bytes):
            raise TypeError
            
        return [self._encrypt(x) for x in barray]
    
    def _decrypt(self,val):
        return (val**self._e) % self._m
    
    def decrypt(self,intlist):
        if not isinstance(intlist,list):
            raise TypeError
        result = bytearray()
        for i in intlist:
            result.append(self._decrypt(i))
        return bytes(result)

    def signature(self, msg):
        h = hashlib.sha256(msg)
        #print(h.hexdigest())
        return self.encrypt(h.hexdigest().encode())

    def verify_signature(self, sig, msg):
        if any([x > 255 for x in sig]):
            return False
        return self.decrypt(sig) == hashlib.sha256(msg).hexdigest().encode()

    def __eq__(self, other):
        return (self._e == other._e) and (self._m == other._m)

    def __repr__(self):
        return "<Key: {},{}>".format(self._e,self._m)

    @staticmethod
    def fromstring(s):
        m = re.search('<Key: (\d+),(\d+)>',s)
        return Key(int(m.group(1)), int(m.group(2)))

class KeyGen:

    def __init__(self,p,q):
        if (not isprime(p)) or (not isprime(q)):
            raise Exception("Must initalize with prime numbers.")
        self._p = p
        self._q = q
        if (self._p*self._q) < 255:
            print("WARNING: product of primes must be larger than 255 to encrypt ascii. ({})".format(self._q*self._p))
        
    def gen_keys(self,coprime_idx=0):
        
        # Modulus is product of the two primes
        m = self._p * self._q
        
        # f(n) function the find co-prime
        fn = (self._p-1)*(self._q-1)
        
        coprimes = get_coprimes(fn)
        e = coprimes[coprime_idx]
        pub = Key(e, m)
        priv = Key(KeyGen.find_private_decrypt_exp(e,fn), m)
        while pub == priv:
            coprime_idx += 1
            e = coprimes[coprime_idx]
            pub = Key(e, m)
            priv = Key(KeyGen.find_private_decrypt_exp(e, fn), m)
        
        return pub, priv

    @staticmethod
    def find_private_decrypt_exp(pub_e, fn):
        """
        Brute force find a number that satisfies:
            (private_decrypt_exponent * public_encrypt_exponent) % fn == 1
        """
        f = lambda priv_e : (priv_e * pub_e) % fn
        priv_e = 1
        while f(priv_e) != 1:
            priv_e += 1
            
        return priv_e
        




