# 🎬 Cinema API

RESTful API для управления кинотеатром: просмотр фильмов, сеансов, доступных залов и бронирование билетов.  
Реализована аутентификация через JWT + Cookies, валидация запросов и работа с PostgreSQL.

## 🛠 Технологии
- **.NET 8.0**
- **PostgreSQL** (через Npgsql)
- **Entity Framework Core** (v9.0.5)
- **Аутентификация**: JWT + Cookies (`Microsoft.AspNetCore.Authentication.JwtBearer`)
- **Валидация**: `FluentValidation` (v12)
- **Хеширование паролей**: `BCrypt.Net-Next`
- **Документация API**: `Swashbuckle.AspNetCore` (Swagger)

## 📌 В процессе
- Авторизация
- Юнит-тесты
- Хранение .jpg
