from pydantic import Field
from typing import Annotated
from fastapi import FastAPI, Depends, HTTPException, Path
from pydantic import BaseModel
from sqlalchemy.orm import Session
from starlette import status

from models import Users
from database import engine
import models

app = FastAPI()

models.Base.metadata.create_all(bind=engine)


def get_db():
    db = Session(engine)
    try:
        yield db
    finally:
        db.close()


db_dependency = Annotated[Session, Depends(get_db)]


class UserRequest(BaseModel):
    name: str = Field(min_length=3)
    surname: str = Field(min_length=3)
    banned: bool = Field(default=False)


@app.get("/")
async def root(db: db_dependency):
    return db.query(Users).all()


@app.get("/users")
async def users(db: db_dependency):
    return db.query(Users).all()


@app.get("/users/{user_id}")
async def user(db: db_dependency, user_id: int = Path(gt=0)):
    user_model = db.query(Users).filter(Users.id == user_id).first()
    if user_model is not None:
        return user_model
    raise HTTPException(status_code=404, detail="User not found")


@app.post("/users", status_code=status.HTTP_201_CREATED)
async def create_user(db: db_dependency, user_request: UserRequest):
    user_model = Users(**user_request.model_dump())

    db.add(user_model)
    db.commit()


@app.put("/users/{user_id}", status_code=status.HTTP_204_NO_CONTENT)
async def update_user(db: db_dependency, user_id: int, user_request: UserRequest):
    user_model = db.query(Users).filter(Users.id == user_id).first()
    if user_model is None:
        raise HTTPException(status_code=404, detail="User not found")

    user_model.name = user_request.name
    user_model.surname = user_request.surname
    user_model.banned = user_request.banned

    db.add(user_model)
    db.commit()


@app.delete("/users/{user_id}", status_code=status.HTTP_204_NO_CONTENT)
async def delete_user(db: db_dependency, user_id: int):
    user_model = db.query(Users).filter(Users.id == user_id).first()
    if user_model is None:
        raise HTTPException(status_code=404, detail="User not found")
    db.query(Users).filter(Users.id == user_id).delete()
    db.commit()