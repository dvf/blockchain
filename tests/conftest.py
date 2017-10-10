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