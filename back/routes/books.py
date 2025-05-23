from fastapi import APIRouter, Depends
from sqlalchemy.orm import Session
import models, shemas, crud
from database import SessionLocal

router = APIRouter()

def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()

@router.get("/", response_model=list[shemas.Book])
def read_books(skip: int = 0, limit: int = 10, db: Session = Depends(get_db)):
    return crud.get_books(db, skip=skip, limit=limit)

@router.post("/", response_model=shemas.Book)
def create_book(book: shemas.BookCreate, db: Session = Depends(get_db)):
    return crud.create_book(db, book)
