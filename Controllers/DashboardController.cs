using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using PuhdasApp.Interfaces;
using PuhdasApp.Models;
using PuhdasApp.ViewModels;

namespace PuhdasApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;
        private readonly IUserRepository _userRepository;

        public DashboardController(IDashboardRepository dashboardRepository,
            IHttpContextAccessor httpContextAccessor,
            IPhotoService photoService,
            IUserRepository userRepository)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
            _userRepository = userRepository;
        }
        private void MapUserEdit(AppUser user,
            EditUserProfileViewModel editVM,
            ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.FirstName = editVM.FirstName;
            user.LastName = editVM.LastName;
            user.State = editVM.State;
            user.City = editVM.City;
            user.ProfileImageUrl = photoResult.Url.ToString();
        }
        public async Task<IActionResult> Index()
        {
            var userOrders = await _dashboardRepository.GetOrdersAsync();
            var dashboardVM = new DashboardViewModel()
            {
                Orders = userOrders,
            };
            return View(dashboardVM);
        }
        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _userRepository.GetById(curUserId);
            if (user == null) { return View ("Error"); }
            var editVM = new EditUserProfileViewModel()
            {
                Id = curUserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                City = user.City,
                State = user.State,
            };
            return View(editVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserProfileViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View(editVM);
            }
            var user = await _userRepository.GetByIdNoTracking(editVM.Id);
            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                MapUserEdit(user, editVM, photoResult);
                _userRepository.Update(user);
                return RedirectToAction("UserProfile", "User");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Failed to delete profile picture");
                }
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                MapUserEdit(user, editVM, photoResult);
                _userRepository.Update(user);
                return RedirectToAction("UserProfile", "User");
            }
        }
    }
}
