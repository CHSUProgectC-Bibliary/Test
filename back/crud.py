from sqlalchemy.orm import Session
import models, shemas
from passlib.context import CryptContext

pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")

#Функции для работы с ббдбддбдбдб

def get_books(db: Session, skip: int = 0, limit: int = 10): # гет для типа фильтра
    return db.query(models.Book).offset(skip).limit(limit).all()

def get_book(db: Session, book_id: int): # гет по ID
    return db.query(models.Book).filter(models.Book.id == book_id).first()

def create_book(db: Session, book: shemas.BookCreate): #создать буку
    db_book = models.Book(**book.dict())
    db.add(db_book)
    db.commit()
    db.refresh(db_book)
    return db_book
 
def create_review(db: Session, review: shemas.ReviewCreate, book_id: int): # создать комментик
    db_review = models.Review(**review.dict(), book_id=book_id)
    db.add(db_review)
    db.commit()
    db.refresh(db_review)
    return db_review

def create_user(db: Session, user: shemas.UserCreate): # зарегаться
    hashed_password = pwd_context.hash(user.password)
    db_user = models.User(username=user.username, hashed_password=hashed_password)
    db.add(db_user)
    db.commit()
    db.refresh(db_user)
    return db_user
