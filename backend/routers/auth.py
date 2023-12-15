from datetime import timedelta, datetime
from typing import Annotated

from fastapi import APIRouter, Depends, HTTPException
from fastapi.security import OAuth2PasswordRequestForm, OAuth2PasswordBearer
from passlib.context import CryptContext
from pydantic import Field, BaseModel
from starlette import status
from database import db_dependency
from models import Users
from jose import jwt, JWTError

router = APIRouter(
    prefix="/auth",
    tags=["auth"]
)

SECRET_KEY = "947c6ee9f033347618e3ba6e197ed95487d375633a9fc2315169b93388fcda7e"
ALGORITHM = "HS256"
bcrypt_context = CryptContext(schemes=["bcrypt"], deprecated="auto")
oauth2_bearer = OAuth2PasswordBearer(tokenUrl="auth/login")


class Token(BaseModel):
    access_token: str = Field()
    token_type: str = Field()


def create_access_token(username: str, user_id: str, expires_delta: timedelta):
    encode = {
        "sub": username,
        "id": user_id
    }
    expires = datetime.utcnow() + expires_delta
    encode.update({"exp": expires})
    return jwt.encode(encode, SECRET_KEY, algorithm=ALGORITHM)


async def get_users(token: Annotated[str, Depends(oauth2_bearer)]):
    try:
        payload = jwt.decode(token, SECRET_KEY, algorithms=[ALGORITHM])
        username: str = payload.get("sub")
        user_id: str = payload.get("id")
        if username is None or user_id is None:
            raise HTTPException(
                status_code=status.HTTP_401_UNAUTHORIZED,
                detail="Could not validate credentials"
            )
        return {"username": username, "id": user_id}
    except JWTError:
        raise HTTPException(
            status_code=status.HTTP_401_UNAUTHORIZED,
            detail="Could not validate credentials"
        )


@router.post('/login', response_model=Token)
async def login(form_data: Annotated[OAuth2PasswordRequestForm, Depends()], db: db_dependency):
    user = db.query(Users).filter(Users.name == form_data.username).first()
    if not user:
        raise HTTPException(404, "User not found")
    if not bcrypt_context.verify(form_data.password, user.password):
        raise HTTPException(400, "Incorrect password")
    access_token = create_access_token(
        username=user.name,
        user_id=user.id,
        expires_delta=timedelta(minutes=20)
    )
    return {"access_token": access_token, "token_type": "Bearer"}
