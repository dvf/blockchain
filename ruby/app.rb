require './blockchain.rb'
require 'json'
require 'sinatra'
require 'sinatra/reloader' if development?

blockchain = Blockchain.new

post '/transactions' do
  params = JSON.parse request.body.read

  if %w[sender recipient amount].any? { |key| params[key].nil? || params[key].length == 0 }
    return [400, 'Missing values']
  end

  index = blockchain.new_transaction(params['sender'], params['recipient'], params['amount'])

  [201, { message: "Transaction added to #{index}" }.to_json]
end

get '/mine' do
end

get '/chain' do
  {
    chain: blockchain.chain,
    length: blockchain.chain.size
  }.to_json
end