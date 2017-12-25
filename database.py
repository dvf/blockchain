import json

from sqlalchemy import Column, DateTime, Integer, PickleType, String, create_engine
from sqlalchemy.ext.declarative import declarative_base, declared_attr
from sqlalchemy.orm import scoped_session, sessionmaker


# Set up the Database
engine = create_engine('sqlite:///electron.db')
db = scoped_session(sessionmaker(bind=engine))


class BaseModel(object):
    @declared_attr
    def __tablename__(self):
        return self.__name__.lower()  # Ensures all tables have the same name as their models (below)

    def to_json(self):
        """
        Convenience method to convert any database row to JSON

        :return: <JSON>
        """
        return json.dumps({c.name: getattr(self, c.name) for c in self.__table__.columns}, sort_keys=True)

    def to_dict(self):
        """
        Convenience method to convert any database row to dict

        :return: <JSON>
        """
        return {c.name: getattr(self, c.name) for c in self.__table__.columns}


Base = declarative_base(cls=BaseModel)


class Peer(Base):
    identifier = Column(String(32), primary_key=True)
    ip = Column(String, index=True, unique=True)


class Block(Base):
    height = Column(Integer, primary_key=True, autoincrement=True)
    timestamp = Column(DateTime, index=True)
    transactions = Column(PickleType)
    previous_hash = Column(String(64))
    proof = Column(String(64))
    hash = Column(String(64))


class Config(Base):
    key = Column(String(64), primary_key=True, unique=True)
    value = Column(PickleType)


def reset_db():
    """
    Deletes and Re-creates the Database

    :return:
    """
    Base.metadata.drop_all(bind=engine)
    Base.metadata.create_all(bind=engine)
