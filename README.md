# 🎬 Cinema API

RESTful API для управления кинотеатром: просмотр фильмов, сеансов, доступных залов и бронирование билетов.  
Реализована аутентификация через JWT + Cookies, валидация запросов и работа с PostgreSQL.

## 🛠 Технологии
- **.NET 8.0**
- **PostgreSQL** 
- **Entity Framework Core** (v9.0.5)
- **Аутентификация**: JWT + Cookies (`Microsoft.AspNetCore.Authentication.JwtBearer`)
- **Валидация**: `FluentValidation` (v12)
- **Хеширование паролей**: `BCrypt.Net-Next`
- **Документация API**: `Swashbuckle.AspNetCore` (Swagger)
- **Result Pattern**: [`ResultSharp`](https://github.com/4q-dev/ResultSharp) для обработки ошибок

## 🏗 Архитектура
Проект следует принципам Clean Architecture с четким разделением на слои:
1. **Domain** - бизнес-логика и доменные модели
2. **Infrastructure** - реализация репозиториев, работа с БД
3. **Application** - сценарии использования, DTO, валидация
4. **API** - контроллеры, middleware

## 🔐 Авторизация
Реализована кастомная система аутентификации:
- Ручная обработка JWT-токенов
- Cookie-based аутентификация
- Роли пользователей (User, Moderator, Admin)
- Хеширование паролей через BCrypt

## 📌 В планах
- Юнит-тесты
- Хранение .jpg
- Refresh-токен
