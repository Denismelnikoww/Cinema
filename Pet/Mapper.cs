using Cinema.Contracts;
using Cinema.Models;
using System;

namespace Cinema
{
    public static class Mapper
    {
        public static MovieDto MapToDto(MovieEntity movieEntity)
        {
            return new MovieDto
            {
                Author = movieEntity.Author,
                Title = movieEntity.Title,
                Description = movieEntity.Description,
                Duration = movieEntity.Duration,
                Rating = movieEntity.Rating,
            };
        }

        public static MovieEntity MapToEntity(MovieDto movieDto)
        {
            return new MovieEntity
            {
                Author = movieDto.Author,
                Title = movieDto.Title,
                Description = movieDto.Description,
                Duration = movieDto.Duration,
                Rating = movieDto.Rating,
            };
        }

        public static HallDto MapToDto(HallEntity hallEntity)
        {
            return new HallDto
            {
                Name = hallEntity.Name,
                IsWorking = hallEntity.IsWorking,
                CountSeats = hallEntity.CountSeats,
            };
        }

        public static HallEntity MapToEntity(HallDto hallDto)
        {
            return new HallEntity
            {
                CountSeats = hallDto.CountSeats,
                Name = hallDto.Name,
                IsWorking = hallDto.IsWorking,
            };
        }

        public static SessionDto MapToDto(SessionEntity sessionEntity)
        {
            return new SessionDto
            {
                HallId = sessionEntity.HallId,
                MovieId = sessionEntity.MovieId,
                DateTime = sessionEntity.DateTime,
                Price = sessionEntity.Price,
                Duration = sessionEntity.Duration,
            };
        }

        public static SessionEntity MapToEntity(SessionDto sessionDto)
        {
            return new SessionEntity
            {
                Duration = sessionDto.Duration,
                DateTime = sessionDto.DateTime,
                Price = sessionDto.Price,
                HallId = sessionDto.HallId,
                MovieId = sessionDto.MovieId,
            };
        }

        public static BookingEntity MapToEntity(BookingDto bookingDto)
        {
            return new BookingEntity
            {
                SeatNumber = bookingDto.SeatNumber,
                SessionId = bookingDto.SessionId,
                UserId = bookingDto.UserId,
            };
        }

        public static BookingDto MapToDto(BookingEntity bookingDto)
        {
            return new BookingDto
            {
                UserId = bookingDto.UserId,
                SeatNumber = bookingDto.SeatNumber,
                SessionId = bookingDto.SessionId
            };
        }
    }
}
