# Learn Blockchains by Building One

[![Build Status](https://travis-ci.org/dvf/blockchain.svg?branch=master)](https://travis-ci.org/dvf/blockchain)

This is the source code for my post on [Building a Blockchain](https://medium.com/p/117428612f46). 

## Installation

1. Make sure [Python 3.6+](https://www.python.org/downloads/) is installed. 
2. Install [pipenv](https://github.com/kennethreitz/pipenv). 

```
$ pip install pipenv 
```

3. Create a _virtual environment_ and specify the Python version to use. 

```
$ pipenv --python=python3.6
```

4. Install requirements.  

```
$ pipenv install 
``` 

5. Run the server:
    * `$ pipenv run python blockchain.py` 
    * `$ pipenv run python blockchain.py -p 5001`
    * `$ pipenv run python blockchain.py --port 5002`
    
## Docker

Another option for running this blockchain program is to use Docker.  Follow the instructions below to create a local Docker container:

1. Clone this repository
2. Build the docker container

```
$ docker build -t blockchain .
```

3. Run the container

```
$ docker run --rm -p 80:5000 blockchain
```

4. To add more instances, vary the public port number before the colon:

```
$ docker run --rm -p 81:5000 blockchain
$ docker run --rm -p 82:5000 blockchain
$ docker run --rm -p 83:5000 blockchain
```

## End-to-End Tests

Rather than testing the blockchain manually by making HTTP calls using CURL or Postman like in the blog post, end-to-end tests can also be done automated using [this Python script](https://github.com/floscha/blockchain-end2end-test/blob/master/end2end_test.py).
In addition to the Python implementation, the script should also work for all other languages as long as they follow the same API and provide a Docker image.

To run the tests, simply following these steps:

1. Make sure the docker image has been built as described in the previous section.

2. Clone the test script from its repository and navigate to the folder:

```
$ git clone https://github.com/floscha/blockchain-end2end-test
$ cd blockchain-end2end-test
```
3. Install the required dependencies:

```
$ pip install -r requirements.txt
```

4. Run the test script as follows:

```
$ python end2end_test.py --nodes 10 --tasks clean setup connect sync-test
```

More details on how the script works can be found [here](https://github.com/floscha/blockchain-end2end-test/blob/master/README.md).

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

