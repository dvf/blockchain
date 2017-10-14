require 'digest/sha2'
require 'json'

class Blockchain
  def initialize
    @chain = []
    @current_transactions = []

    new_block(previous_hash: 1, proof: 100)
  end

  def new_block(previous_hash: nil, proof: nil)
    block = {
      index: @chain.size + 1,
      timestamp: Time.now,
      transactions: current_transactions,
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

    last_block['index'] + 1
  end

  def self.hash(block)
    Digest::SHA256.hexdigest block.to_json
  end

  protected

  def last_block
    @chain[-1]
  end
end