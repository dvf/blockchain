from database import db, Config


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


def get_config(key, default=None):
    config = db.query(Config).filter_by(key=key).first()
    if config:
        return config.value
    else:
        return default
