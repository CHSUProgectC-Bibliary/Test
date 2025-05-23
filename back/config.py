import os
from dotenv import load_dotenv

load_dotenv()  # Загружаем переменные окружения из .env
DATABASE_URL = os.getenv("DATABASE_URL", "postgresql://user:password@localhost:5432/booksdb") #url бдшки
SECRET_KEY = os.getenv("SECRET_KEY", "supersecretkey") # ключик безопасности
ACCESS_TOKEN_EXPIRE_MINUTES = int(os.getenv("ACCESS_TOKEN_EXPIRE_MINUTES", 30)) # время жизни токена для авторизации
