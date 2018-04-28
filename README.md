[WIP]kotlin-blockchain
====

Kotlinでブロックチェーンを実装する

## Description

以下記事を参考にブロックチェーンをKotlinで実装しています。

- [ブロックチェーンを作ることで学ぶ 〜ブロックチェーンがどのように動いているのか学ぶ最速の方法は作ってみることだ〜](https://qiita.com/hidehiro98/items/841ece65d896aeaa8a2a)
- [Learn Blockchains by Building One -The fastest way to learn how Blockchains work is to build one-](https://hackernoon.com/learn-blockchains-by-building-one-117428612f46)

## Demo

```
# 現在のブロックの状態を確認する
$ curl -s http://localhost:4567/chain
```

```
# トランザクションの登録
$ curl -s http://localhost:4567/transactions/new -X POST -d '{"sender":"testSender","recipient":"testRecipient","amount":1}'
```

```
# ノード登録
$ curl -s http://localhost:4567/nodes/register -X POST -d '{"url":"http://hoge.co.jp"}'
```

## VS.

## Requirement

## Usage

## Install

## Contribution

## Licence

[MIT](https://github.com/tcnksm/tool/blob/master/LICENCE)

## Author

[masayuki5160](https://github.com/masayuki5160)
