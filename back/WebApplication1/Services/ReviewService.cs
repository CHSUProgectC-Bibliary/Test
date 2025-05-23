using AutoMapper;
using BookReviewAPI.Data;
using BookReviewAPI.Data.Dto;
using BookReviewAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
namespace BookReviewAPI.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetAllReviews(CancellationToken cancellationToken);
        Task<ReviewDto> GetReviewById(int id, CancellationToken cancellationToken);
        Task<ReviewDto> CreateReview(CreateReviewDto reviewDto, CancellationToken cancellationToken);
        Task UpdateReview(int id, UpdateReviewDto reviewDto, CancellationToken cancellationToken);
        Task DeleteReview(int id, CancellationToken cancellationToken);
    }
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ReviewService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ReviewDto> CreateReview(CreateReviewDto reviewDto, CancellationToken cancellationToken)
        {
            var review = _mapper.Map<Review>(reviewDto);
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task DeleteReview(int id, CancellationToken cancellationToken)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) throw new NotFoundException("review not found");
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviews(CancellationToken cancellationToken)
        {
            var allReviews = await _context.Reviews.ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ReviewDto>>(allReviews);
        }

        public async Task<ReviewDto> GetReviewById(int id, CancellationToken cancellationToken)
        {
            var review = await _context.Reviews.FindAsync(id);
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task UpdateReview(int id, UpdateReviewDto reviewDto, CancellationToken cancellationToken)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) throw new NotFoundException("review not found");

            _mapper.Map(reviewDto, review);
            await _context.SaveChangesAsync();
        }
    }
}
