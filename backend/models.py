from sqlalchemy import Column, Integer, String, Boolean

from database import Base


class Users(Base):
    __tablename__ = 'users'
    id = Column(Integer, primary_key=True, index=True)
    name = Column(String)
    surname = Column(String)
    banned = Column(Boolean, default=False)



