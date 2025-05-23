namespace BookReviewAPI;
// Profiles/MappingProfile.cs
using AutoMapper;
using BookReviewAPI.Data.Dto;
using BookReviewAPI.Data.Entities;

    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
            CreateMap<Book, BookDto>();

            CreateMap<CreateReviewDto, Review>();
            CreateMap<UpdateReviewDto, Review>();
            CreateMap<Review, ReviewDto>();
        }
    }

