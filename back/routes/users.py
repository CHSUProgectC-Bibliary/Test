from fastapi import APIRouter, Depends
from sqlalchemy.orm import Session
import shemas, crud
from database import SessionLocal

router = APIRouter()

def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()

@router.post("/register", response_model=shemas.User)
def register(user: shemas.UserCreate, db: Session = Depends(get_db)):
    return crud.create_user(db, user)
