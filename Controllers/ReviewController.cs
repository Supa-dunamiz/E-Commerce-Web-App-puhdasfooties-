using Microsoft.AspNetCore.Mvc;
using PuhdasApp.Interfaces;
using PuhdasApp.Models;
using PuhdasApp.ViewModels;

namespace PuhdasApp.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public ReviewController(IReviewRepository reviewRepository,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        public IActionResult Create(int id)
        {
            var reviewVM = new CreateReviewViewModel()
            {
                ProductId = id,
            };
            return View(reviewVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(int id, CreateReviewViewModel reviewVM)
        {
            if(ModelState.IsValid)
            {
                var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
                var user = await _userRepository.GetById(curUserId);
                var review = new Review()
                {
                    Reviewer = user.FirstName + " " + user.LastName,
                    Content = reviewVM.Content,
                    CreatedAt = DateTime.Now,
                    ProductId = id
                };
                _reviewRepository.Add(review);
                return RedirectToAction("Index", "Product");
            }
            return View(reviewVM);
        }
    }
}
