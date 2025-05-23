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

@router.post("/{book_id}", response_model=shemas.Review)
def add_review(book_id: int, review: shemas.ReviewCreate, db: Session = Depends(get_db)):
    return crud.create_review(db, review, book_id)
