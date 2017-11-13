# Learn Blockchains by Building One

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

## Heroku

This blockchain program can also be run on the web with heroku.

### Use Heroku Git
1. Clone this repository


2. Build new app in [heroku](https://www.heroku.com/)


3. Install [Heroku CLI](https://devcenter.heroku.com/articles/heroku-command-line)


4. Create a new Git repository(for heroku)

'''
$ heroku login
$ cd blockchain/
$ git init
$ heroku git:remote -a <Your app name>
'''


5. Deploy your application

'''
pip install -r requirements.txt 
git add .
git commit -am "Deploy blockchain program"
git push heroku master
'''


## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

