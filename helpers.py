from hashlib import sha256

from sqlalchemy import func

from database import Config, Peer, db


def set_config(key, value, replace=False):
    config_value = get_config(key)

    if config_value is None:
        db.add(Config(key=key, value=value))
        db.commit()
        return value

    if config_value != value and replace is True:
        db.add(Config(key=key, value=value))
        db.commit()
        return value

    return config_value


def get_config(key, default=None):
    config = db.query(Config).filter_by(key=key).first()
    if config:
        return config.value
    else:
        return default


def get_random_peers(limit=10):
    """
    Returns random peers

    :param limit: How many peers to return
    :return:
    """
    return db.query(Peer).order_by(func.random()).limit(limit)


def hash_block(block):
    """
    Creates a SHA-256 hash_block of the fields for a Block
    """
    byte_array = f"{block['height']}" \
                 f"{block['timestamp']}" \
                 f"{block['transactions']}" \
                 f"{block['previous_hash']}" \
                 f"{block['proof']}".encode()

    return sha256(byte_array).hexdigest()
