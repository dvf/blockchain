require 'digest/sha2'
require 'json'
require 'uri'

class Blockchain
  attr_reader :chain

  def initialize
    @chain = []
    @current_transactions = []
    @nodes = {}

    new_block(previous_hash: 1, proof: 100)
  end

  def register_node(address)
    uri = URI.parse(address)

    @nodes << uri.host
    @nodes.uniq!
  end

  def valid_chain?(chain)
    last_block = chain[0]

    (1..chain.size).each do |index||
      block = chain[index]

      return false if block[:previous_hash] != hash(last_block)
      return false unless valid_proof?(last_block[:proof], block[:proof])

      last_block = block
    end

    true
  end

  def new_block(previous_hash: nil, proof: nil)
    block = {
      index: @chain.size + 1,
      timestamp: Time.now,
      transactions: @current_transactions,
      proof: proof,
      previous_hash: previous_hash || self.class.hash(@chain[-1])
    }

    @current_transactions = []
    @chain << block

    block
  end

  def new_transaction(sender, recipient, amount)
    @current_transactions << {
      sender: sender,
      recipient: recipient,
      amount: amount
    }

    last_block[:index] + 1
  end

  def proof_of_work(last_proof)
    proof = 0
    while not self.class.valid_proof?(last_proof, proof)
      proof += 1
    end
    
    proof
  end

  def self.hash(block)
    Digest::SHA256.hexdigest block.to_json
  end

  def self.valid_proof?(last_proof, proof)
    guess = "#{last_proof}#{proof}"

    Digest::SHA256.hexdigest(guess)[0..3] == '0000'
  end

  def last_block
    @chain[-1]
  end
end