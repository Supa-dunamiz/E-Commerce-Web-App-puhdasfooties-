
using Microsoft.AspNetCore.Mvc;
using PuhdasApp.Interfaces;
using PuhdasApp.ViewModels;

namespace PuhdasApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("Customers")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userVM = new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    City = user.City,
                    State = user.State,
                    ProfileImageUrl = user.ProfileImageUrl,
                };
                result.Add(userVM);
            }
            return View(result);
        }
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetById(id);
            var userDetailVM = new UserDetailViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                City = user.City,
                State = user.State,
                ProfileImageUrl = user.ProfileImageUrl,
            };
            return View(userDetailVM);
        }
        public async Task<IActionResult> UserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _userRepository.GetById(curUserId);
            var userVm = new UserDetailViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                LastName = user.LastName,
                State = user.State,
                City = user.City,
                ProfileImageUrl = user.ProfileImageUrl,
            };
            return View(userVm);
        }
    }
}
