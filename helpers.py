from sqlalchemy import func

from database import Config, Peer, db


def set_config(key, value, replace=False):
    config_value = get_config(key)

    if config_value is None:
        db.add(Config(key=key, value=value))
        db.commit()
        return

    if config_value != value and replace is True:
        db.add(Config(key=key, value=value))
        db.commit()
        return

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
