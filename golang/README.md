
# GoChain

A basic implementation of blockchain in go.

### Building

```
$ cd cmd
$ go build -o gochain
```


# Usage

## Starting a node

You can start as many nodes as you want with the following command

`./gochain <port-number>`


## Endpoints


### Requesting the Blockchain of a node

* `GET 127.0.0.1:8000/chain`

### Mining some coins

* `GET 127.0.0.1:8000/mine`

### Adding a new transaction

* `POST 127.0.0.1:8000/transactions/new`

* __Body__: A transaction to be added

  ```json
  {
    "sender": "sender-address-te33412uywq89234g",
    "recipient": "recipient-address-j3h45jk23hjk543gf",
    "amount": 1000
  }
  ```

### Register a new node in the network
Currently you must add each new node to each running node.

* `POST 127.0.0.1:8000/nodes/register`

* __Body__: A list of nodes to add

  ```json
  {
     "nodes": ["http://127.0.0.1:8001", <more-nodes>]
  }
  ```

### Resolving Blockchain differences in each node

* `GET 127.0.0.1:8000/nodes/resolve`
