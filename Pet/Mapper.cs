using Pet.Contracts;
using Pet.Models;
using System;

namespace Pet
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
                Duration = movieEntity.Time,
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
                Time = movieDto.Duration,
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
                Time = sessionEntity.Time,
            };
        }
        
        public static SessionEntity MapToEntity(SessionDto sessionDto)
        {
            return new SessionEntity
            {
                Time = sessionDto.Time,
                DateTime = sessionDto.DateTime,
                Price = sessionDto.Price,
                HallId = sessionDto.HallId,
                MovieId = sessionDto.MovieId,
            };
        }
    }
}
