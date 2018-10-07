using System;
using System.Collections.Generic;
using System.Linq;
using BlockChain.Domain;
using BlockChain.Domain.Model;
using FluentAssertions;
using Xunit;

namespace BlockChain.Tests
{
    public class BlockchainTests
    {
      
        #region TestInitialization
        [Fact]
        public void Should_Create_Genesis_Block()
        {
            // arrange
            var expected = Block.Genesis();

            // act
            var blockchain = new Blockchain();
            var actual = blockchain.Chain.First();

            // assert
            blockchain.Chain.Should().HaveCount(1);
            actual.Should().NotBeNull();
            actual.Index.Should().Be(1);
            actual.Proof.Should().Be(new ProofOfWork(1));
            actual.PreviousHash.Should().Be(Sha256Hash.Of("Genesis"));
            actual.Transactions.Should().BeEmpty();
        }

        #endregion

        #region TestTransactionCreationSha256Hash.Of(Sha256Hash.Of(Sha256Hash.Of(Sha256Hash.Of(
        [Fact]
        public void Should_Create_New_Transaction()
        {
            // arrange
            var blockchain = new Blockchain();
            var sender = Guid.NewGuid();
            var recipient = Guid.NewGuid();

            // act
            blockchain.NewTransaction(sender, recipient, 1);
            var actual = blockchain.CurrentTransactions.First();

            // assert
            blockchain.CurrentTransactions.Should().HaveCount(1);
            actual.Should().NotBeNull();
            actual.Sender.Should().Be(Account.Of(sender));
            actual.Recipient.Should().Be(Account.Of(recipient));
            actual.Amount.Should().Be(1);
        }

        [Fact]
        public void Should_Return_Index_Of_Next_Block()
        {
            // arrange
            var blockchain = new Blockchain();
            var sender = Guid.NewGuid();
            var recipient = Guid.NewGuid();

            // act
            var actual = blockchain.NewTransaction(sender, recipient, 1);

            // assert
            actual.Should().Be(2);
        }

        [Fact]
        public void Should_Throw_Exception_When_Recipient_Is_Default()
        {
            var blockchain = new Blockchain();
            blockchain
                .Invoking(b => b.NewTransaction(Guid.NewGuid(), Guid.Empty, 1))
                .Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void Should_Throw_Exception_When_Amount_Is_Negative()
        {
            var blockchain = new Blockchain();
            blockchain
                .Invoking(b => b.NewTransaction(Guid.NewGuid(), Guid.NewGuid(), -1))
                .Should()
                .Throw<ArgumentException>();
        }
        #endregion

        #region TestMining
        [Fact]
        public void Should_Calculate_Proof_Of_Work()
        {
            // arrange
            var blockchain = new Blockchain();
            var genesis = blockchain.Chain.First();
            var expected = new ProofOfWork(72608);

            // act
            var actual = blockchain.Mine();

            // assert
            actual.Proof.Should().Be(expected);
        }

        [Fact]
        public void Should_Reward_Miner_With_Transaction()
        {
            // arrange 
            var blockchain = new Blockchain();

            // act
            var block = blockchain.Mine();
            var actual = block.Transactions.Last();

            // assert
            actual.Should().NotBeNull();
            actual.Sender.Should().BeNull();
            actual.Recipient.Should().Be(blockchain.Account);
            actual.Amount.Should().Be(1);
        }

        [Fact]
        public void Should_Forge_New_Block()
        {
            // arrange 
            var blockchain = new Blockchain();

            // act
            var actual = blockchain.Mine();

            // assert
            actual.Should().NotBeNull();
            actual.Index.Should().Be(2);
            actual.Proof.Should().Be(new ProofOfWork(72608));
            actual.PreviousHash.Should().Be(blockchain.Chain.First().Hash());
            actual.Transactions.Should().HaveCount(1);
        }

        [Fact]
        public void Should_Reset_Transactions()
        {
            // arrange
            var blockchain = new Blockchain();

            // act
            blockchain.Mine();

            // assert
            blockchain.CurrentTransactions.Should().BeEmpty();
        }

        [Fact]
        public void Should_Add_Transactions_To_New_Block()
        {
            // arrange
            var blockchain = new Blockchain();
            var sender = Guid.NewGuid();
            var recipient = Guid.NewGuid();

            // act
            blockchain.NewTransaction(sender, recipient, 1);
            blockchain.NewTransaction(sender, recipient, 2);
            var actual = blockchain.Mine();

            // assert
            actual.Transactions.Should().HaveCount(3);
        }
        #endregion

        #region TestRegistration
        [Fact]
        public void Should_Register_Address_In_Blockchain()
        {
            // arrange
            var address = "http://127.0.0.1:9999/";
            var expected = new Node(address);
            var blockchain = new Blockchain();

            // act
            blockchain.Register(new Uri(address));

            // assert
            blockchain.Nodes.Should().HaveCount(1);
            blockchain.Nodes.Should().Contain(expected);
        }

        [Fact]
        public void Should_Register_Address_Idempotently()
        {
            // arrange
            var address = "http://127.0.0.1:9999/";
            var expected = new Node(address);
            var blockchain = new Blockchain();

            // act
            blockchain.Register(new Uri(address));
            blockchain.Register(new Uri(address));

            // assert
            blockchain.Nodes.Should().HaveCount(1);
            blockchain.Nodes.Should().Contain(expected);
        }

        [Fact]
        public void Should_Throw_Exception_When_Address_Is_Null()
        {
            var blockchain = new Blockchain();
            blockchain
                .Invoking(b => b.Register(null))
                .Should()
                .Throw<ArgumentNullException>();
        }
        #endregion

        #region TestConsensusAlgorithm
        [Fact]
        public void Should_Replace_Chain_With_Longer_Blockchain()
        {
            // arrange
            var blockchain = new Blockchain();
            var longerBlockchain = new Blockchain();
            longerBlockchain.NewTransaction(Guid.NewGuid(),Guid.NewGuid(),2);
            longerBlockchain.Mine();

            // act
            var actual = blockchain.ResolveConflicts(new List<IList<Block>>{longerBlockchain.Chain.ToList()});

            // assert
            actual.Should().BeTrue();
            blockchain.Chain.Should().Equal(longerBlockchain.Chain);
        }

        [Fact]
        public void Should_Discard_Shorter_Blockchain()
        {
            // arrange
            var blockchain = new Blockchain();
            blockchain.NewTransaction(Guid.NewGuid(),Guid.NewGuid(),2);
            blockchain.Mine();
            var shorterBlockchain = new Blockchain();

            // act
            var actual = blockchain.ResolveConflicts(new List<IList<Block>>{shorterBlockchain.Chain.ToList()});

            // assert
            actual.Should().BeFalse();
            blockchain.Chain.Should().NotBeEmpty();
            blockchain.Chain.Should().NotEqual(shorterBlockchain.Chain);
        }

        [Fact]
        public void Should_Discard_Invalid_Blockchain()
        {
            // arrange
             var blockchain = new Blockchain();
            var genesis = Block.Genesis();
            var secondBlock = MakeBlock(2, new Challenge().Solve(genesis.Proof), Sha256Hash.Of("invalid"), new List<Transaction>());
            var thirdBlock = MakeBlock(3, secondBlock);
            var invalidChain = new List<Block>
            {
                genesis, secondBlock, thirdBlock
            };

            // act
            var actual = blockchain.ResolveConflicts(new List<IList<Block>>{invalidChain.ToList()});

            // assert
            actual.Should().BeFalse();
            blockchain.Chain.Should().NotBeEmpty();
            blockchain.Chain.Should().NotEqual(invalidChain);
        }

        [Fact]
        public void Should_Verify_Correct_Blockchain()
        {
            // arrange
            var genesis = Block.Genesis();
            var secondBlock = MakeBlock(2, genesis);
            var thirdBlock = MakeBlock(3, secondBlock);
            var chain = new List<Block>
            {
                genesis, secondBlock, thirdBlock
            };

            // act
            var actual = Blockchain.Validate(chain);

            // assert
            actual.Should().BeTrue();
        }

        [Fact]
        public void Should_Verify_Incorrect_Hash()
        {
            // arrange
            var genesis = Block.Genesis();
            var secondBlock = MakeBlock(2, new Challenge().Solve(genesis.Proof), Sha256Hash.Of("invalid"), new List<Transaction>());
            var thirdBlock = MakeBlock(3, secondBlock);
            var chain = new List<Block>
            {
                genesis, secondBlock, thirdBlock
            };

            // act
            var actual = Blockchain.Validate(chain);

            // assert
            actual.Should().BeFalse();
        }

        [Fact]
        public void Should_Verify_Incorrect_Proof_Of_Work()
        {
            // arrange
            var genesis = Block.Genesis();
            var secondBlock = MakeBlock(2, new Challenge().Solve(new ProofOfWork(999)), genesis.Hash(), new List<Transaction>());
            var thirdBlock = MakeBlock(3, secondBlock);
            var chain = new List<Block>
            {
                genesis, secondBlock, thirdBlock
            };

            // act
            var actual = Blockchain.Validate(chain);

            // assert
            actual.Should().BeFalse();   
        }

        private static Block MakeBlock(long index, Block previousBlock)
        {
            return new Block(
                index,
                new Challenge().Solve(previousBlock.Proof),
                previousBlock.Hash(),
                new List<Transaction> {
                    new Transaction(null, Account.Of(Guid.NewGuid()),1)
                });
        }

        private static Block MakeBlock( long index, ProofOfWork proof, Sha256Hash hash, IEnumerable<Transaction> transactions)
        {
            return new Block(index, proof, hash, transactions);
        }
        #endregion
    }
}
