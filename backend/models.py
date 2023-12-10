from sqlalchemy import Column, Integer, String, Boolean, ForeignKey, DateTime
from sqlalchemy.sql import func
from database import Base


class Users(Base):
    __tablename__ = 'users'
    id = Column(Integer, primary_key=True, index=True)
    name = Column(String)
    surname = Column(String)
    banned = Column(Boolean, default=False)
    password = Column(String)


class ChatRoom(Base):
    __tablename__ = 'chat_room'
    id = Column(Integer, primary_key=True, index=True)
    name = Column(String)


class ChatMembers(Base):
    __tablename__ = 'chat_members'
    id = Column(Integer, primary_key=True, index=True)
    user_id = Column(Integer, ForeignKey('users.id'))
    chat_id = Column(Integer, ForeignKey('chats.id'))


class ChatMessages(Base):
    __tablename__ = 'chat_messages'
    id = Column(Integer, primary_key=True, index=True)
    user_id = Column(Integer, ForeignKey('users.id'))
    chat_id = Column(Integer, ForeignKey('chat_room.id'))
    text = Column(String)
    timestamp = Column(DateTime(timezone=True), server_default=func.now())

