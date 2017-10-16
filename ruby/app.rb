require './blockchain.rb'
require 'json'
require 'sinatra'
require 'sinatra/reloader' if development?

node_identifire = SecureRandom.uuid.gsub('-', '')
blockchain = Blockchain.new

post '/transactions' do
  params = JSON.parse request.body.read

  if %w[sender recipient amount].any? { |key| params[key].nil? || params[key].to_s.length == 0 }
    return [400, 'Missing values']
  end

  index = blockchain.new_transaction(params['sender'], params['recipient'], params['amount'])

  [201, { message: "Transaction will be added to Block #{index}" }.to_json]
end

get '/mine' do
  last_block = blockchain.last_block
  last_proof = last_block[:proof]
  proof = blockchain.proof_of_work(last_proof)

  blockchain.new_transaction("0", node_identifire, 1)

  block = blockchain.new_block(proof: proof)

  response = {
    message: 'New Block Forged',
    index: block[:index],
    transactions: block[:transactions],
    proof: block[:proof],
    previous_hash: block[:previous_hash]
  }

  [200, response.to_json]
end

get '/chain' do
  {
    chain: blockchain.chain,
    length: blockchain.chain.size
  }.to_json
end