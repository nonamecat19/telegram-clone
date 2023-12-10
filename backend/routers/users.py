from pydantic import BaseModel
from starlette import status
from pydantic import Field
from fastapi import HTTPException, Path, APIRouter
from database import db_dependency
from models import Users

router = APIRouter(prefix="/users")

class CreateUserRequest(BaseModel):
    name: str = Field()
    surname: str = Field()
    password: str = Field()

@router.post("/", status_code=status.HTTP_201_CREATED)
async def create_user(db: db_dependency, user_request: CreateUserRequest):
    user_model = Users(**user_request.model_dump())

    db.add(user_model)
    db.commit()


@router.get("/")
async def users(db: db_dependency):
    return db.query(Users).all()


@router.get("/{user_id}")
async def user(db: db_dependency, user_id: int = Path(gt=0)):
    user_model = db.query(Users).filter(Users.id == user_id).first()
    if user_model is not None:
        return user_model
    raise HTTPException(status_code=404, detail="User not found")


class UpdateUserRequest(BaseModel):
    name: str = Field(min_length=3)
    surname: str = Field(min_length=3)
    banned: bool = Field(default=False)

@router.put("/{user_id}", status_code=status.HTTP_204_NO_CONTENT)
async def update_user(db: db_dependency, user_id: int, user_request: UpdateUserRequest):
    user_model = db.query(Users).filter(Users.id == user_id).first()
    if user_model is None:
        raise HTTPException(status_code=404, detail="User not found")

    user_model.name = user_request.name
    user_model.surname = user_request.surname
    user_model.banned = user_request.banned

    db.add(user_model)
    db.commit()


@router.delete("/{user_id}", status_code=status.HTTP_204_NO_CONTENT)
async def delete_user(db: db_dependency, user_id: int):
    user_model = db.query(Users).filter(Users.id == user_id).first()
    if user_model is None:
        raise HTTPException(status_code=404, detail="User not found")
    db.query(Users).filter(Users.id == user_id).delete()
    db.commit()
