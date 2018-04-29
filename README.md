[WIP]kotlin-blockchain
====

Kotlinでブロックチェーンを実装する

## Description

以下記事を参考にブロックチェーンをKotlinで実装しています。

- [ブロックチェーンを作ることで学ぶ 〜ブロックチェーンがどのように動いているのか学ぶ最速の方法は作ってみることだ〜](https://qiita.com/hidehiro98/items/841ece65d896aeaa8a2a)
- [Learn Blockchains by Building One -The fastest way to learn how Blockchains work is to build one-](https://hackernoon.com/learn-blockchains-by-building-one-117428612f46)

## Demo

```
# Get current chain info
$ curl -s http://localhost:4567/chain
```

```
# Register a new transaction
$ curl -s http://localhost:4567/transactions/new -X POST -d '{"sender":"testSender","recipient":"testRecipient","amount":1}'
```

```
# Register a new node
$ curl -s http://localhost:4567/nodes/register -X POST -d '{"url":"http://hoge.co.jp"}'
```

```
# 
$ curl -s http://localhost:4567/nodes/resolve
```

## Requirement

## Usage

## Install

## Contributing
Contributions are welcome! Please feel free to submit a Pull Request.
