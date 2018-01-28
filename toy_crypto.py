# -*- coding: utf-8 -*-
"""
Pedagogic tool to understand cryptography.

Follows the example shown in this
`wiki book <https://en.wikibooks.org/wiki/Cryptography/A_Basic_Public_Key_Example>`_.

"""
import hashlib
import re



def isprime(n):
    """
    Is integer prime.

    :param n: integer
    :return: bool
    """
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
    """
    Returns the factors of the provided integer.

    :param n: An integer to factor
    :return: list of factors
    """
    return [i for i in range(1,n) if (n % i) == 0]

def gen_primes(n=1):
    """
    Iterator that will generate successively larger prime numbers start at the int provided.

    :param n: Number to start looking for primes.
    :return: iterator
    """
    if isprime(n):
        yield n
    yield from gen_primes(n+1)

def gen_coprimes(N, n=0, seed=None):
    """
    Generate co-primes.

    See the explanation here:
    https://en.wikipedia.org/wiki/Coprime_integers#Generating_all_coprime_pairs

    | Branch 1: (2m-n,m)
    | Branch 2: (2m+n,m)
    | Branch 3: (m+2n,n)

    :param N: how many levels deep to go
    :param n:
    :param seed:
    :return: iterator that generates tuples of coprime numbers
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
    """
    Return whether the two integers are co-prime (meaning the only common factor
    they share is 1).

    :param a:
    :param b:
    :return: True if only common factor is 1
    """
    seta = set(factors(a))
    setb = set(factors(b))
    if (seta & setb) == set({1}):
        return True
    return False

def get_coprimes(N):
    """
    Return numbers that are co-prime and less than the number provided.

    Very naive and HORRIBLY inefficient, but this is just educational. :)
    """
    return [n for n in range(N-1,0,-1) if iscoprime(n,N)]        


class Key:
    """
    Class to hold a public or private key.

    Holds an exponent and modulus value. Encryption/decryption is done by
    (val**exponent) % modulus

    Remember that the modulus sets the upper limit of value that can be encrypted/decrypted.
    """

    def __init__(self,e,m):
        self._e = e
        self._m = m
                
    def _encrypt(self,val):
        return (val**self._e) % self._m
    
    def encrypt(self,barray):
        """
        Returns an encrypted version of the message passed.

        :param barray: expects a *bytes* object.
        :return: list of encrypted values as int (will be in range of 0 to modulus of object).
        """
        if not isinstance(barray,bytes):
            raise TypeError
            
        return [self._encrypt(x) for x in barray]
    
    def _decrypt(self,val):
        return (val**self._e) % self._m
    
    def decrypt(self,intlist):
        """
        Returns a decypted version of the message passed.

        :param intlist: List of int (generated by :meth:`encrypt`).
        :return: bytes of decrypted message
        """
        if not isinstance(intlist,list):
            raise TypeError
        result = bytearray()
        for i in intlist:
            result.append(self._decrypt(i))
        return bytes(result)

    def signature(self, msg):
        """
        Generates a signature of the given message.

        First generates a hash of the message and then encrypts that signature.
        :param msg: bytes of message to be signed.
        :return: list of ints of the signature
        """
        h = hashlib.sha256(msg)
        return self.encrypt(h.digest())

    def verify_signature(self, sig, msg):
        """
        Determines if signature is valid.

        Hashes msg and compares to the decrypted signature.
        :param sig: Signature generated by :meth:`encrypt`
        :param msg:
        :return:
        """
        if any([x > self._m for x in sig]):
            return False
        return self.decrypt(sig) == hashlib.sha256(msg).digest()

    def __eq__(self, other):
        """
        Determines if two :class:`Key` objects are equal by comparing modulus and exponent

        :param other:
        :return:
        """
        return (self._e == other._e) and (self._m == other._m)

    def __repr__(self):
        return "<Key: {},{}>".format(self._e,self._m)

    @staticmethod
    def fromstring(s):
        """
        Create Key instance from a string.

        :param s: String of the form "<Key: 11,17>"
        :return: Key instance
        """
        m = re.search('<Key: (\d+),(\d+)>', s)
        return Key(int(m.group(1)), int(m.group(2)))

class KeyGen:
    """
    Class to generate a public/private key pair.

    Initialize with two prime numbers
    """

    def __init__(self,p,q):
        if (not isprime(p)) or (not isprime(q)):
            raise Exception("Must initialize with prime numbers.")
        self._p = p
        self._q = q
        if (self._p*self._q) < 255:
            print("WARNING: product of primes must be larger than 255 to encrypt ascii. ({})".format(self._q*self._p))
        
    def gen_keys(self,coprime_idx=0):
        """
        Returns a (public,private) Key pair

        :param coprime_idx:
        :return:
        """
        
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
        




