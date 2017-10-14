require './blockchain.rb'
require 'json'
require 'sinatra'

blockchain = Blockchain.new

post '/transactions' do
end

get '/mine' do
end

get '/chain' do
  {
    chain: blockchain.chain,
    length: blockchain.chain.size
  }.to_json
end