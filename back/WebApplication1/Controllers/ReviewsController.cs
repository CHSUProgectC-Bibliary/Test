using BookReviewAPI.Data.Dto;
using BookReviewAPI.Services;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
namespace BookReviewAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet("All")]
        public async Task<ActionResult> GetAllReviews(CancellationToken cancellationToken)
        {
            var allReviews = await _reviewService.GetAllReviews(cancellationToken);
            return Ok(allReviews);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id, CancellationToken cancellationToken)
        {
            var review = await _reviewService.GetReviewById(id, cancellationToken);
            if (review == null) return NotFound();
            return Ok(review);
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(CreateReviewDto reviewDto, CancellationToken cancellationToken)
        {
            var review = await _reviewService.CreateReview(reviewDto, cancellationToken);
            return CreatedAtAction(nameof(GetReviewById), new { id = review.Review_Id }, review);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateReview(int id, UpdateReviewDto reviewDto, CancellationToken cancellationToken)
        {
            try
            {
                await _reviewService.UpdateReview(id, reviewDto, cancellationToken);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteReview(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _reviewService.DeleteReview(id, cancellationToken);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
