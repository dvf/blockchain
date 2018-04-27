import pytest
import blockchain


@pytest.fixture(scope='function')
def app():
    """Define the app fixture that has a new blockchain in every function scope."""
    _app = blockchain.app
    blockchain.blockchain = blockchain.Blockchain()
    yield _app


@pytest.fixture(scope='function')
def client(app):
    """A Flask test client. An instance of :class:`flask.testing.TestClient`
    by default.
    """
    with app.test_client() as client:
        yield client

@pytest.fixture(scope='function')
def blockchain_init():
    """Initialize blockchain instance"""
    yield blockchain.Blockchain()

@pytest.fixture(scope='function')
def create_block(blockchain_init):
    """create new block from initialized blockchain instance"""
    yield blockchain_init.new_block(proof=123, previous_hash='abc')

@pytest.fixture(scope='function')
def create_transaction(blockchain_init):
    tr = blockchain_init.new_transaction(
        sender='a',
        recipient='b',
        amount=1
    )
    yield tr


