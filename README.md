# 🎬 Cinema API

RESTful API для управления кинотеатром: просмотр фильмов, сеансов, доступных залов и бронирование билетов.  

## 🛠 Технологии
- **.NET 8.0**
- **PostgreSQL** 
- **Entity Framework Core**
- **Redis**: кэширование запрашиваемых фильмов
- **Serilog**: логирование в консоль и файлы с ротацией по дням
- **Аутентификация**: JWT + Cookies (`Microsoft.AspNetCore.Authentication.JwtBearer`)
- **Валидация**: `FluentValidation` 
- **Хеширование паролей**: `BCrypt.Net-Next`
- **Документация API**: `Swashbuckle.AspNetCore` 
- **Result Pattern**: [`ResultSharp`](https://github.com/4q-dev/ResultSharp)

## 🏗 Архитектура
Проект следует принципам Clean Architecture с четким разделением на слои:
1. **Domain** - бизнес-логика и доменные модели
2. **Infrastructure** - реализация репозиториев, работа с БД
3. **Application** - сценарии использования, DTO, валидация
4. **API** - контроллеры, middleware

## 🔐 Авторизация
Реализована кастомная система аутентификации:
- Ручная обработка JWT-токенов
- Access Token с коротким временем жизни 
- Refresh Token с длительным временем жизни
- Cookie-based аутентификация
- Роли пользователей (User, Moderator, Admin)
- Хеширование паролей через BCrypt

## 📌 В планах
- Хранение .jpg
- Реализация двухфакторной аутентификации
- Работа с почтой
