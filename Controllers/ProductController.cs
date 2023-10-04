using Microsoft.AspNetCore.Mvc;
using PuhdasApp.Interfaces;
using PuhdasApp.Models;
using PuhdasApp.ViewModels;

namespace PuhdasWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IPhotoService _photoService;
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ProductController(IProductRepository productRepository,
            IPhotoService photoService,
            IOrderRepository orderRepository,
            IReviewRepository reviewRepository)
        {
            _productRepository = productRepository;
            _photoService = photoService;
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetProducts();
            return View(products);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productRepository.GetProductById(id);
            return View(product);
        }

        public IActionResult AddNewProduct()
        {
            var productVM = new AddNewProductViewModel();
            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewProduct(AddNewProductViewModel productVM)
        {
            var productCheck = await _productRepository.GetProductByName(productVM.Name);
            if(productCheck != null)
            {
                TempData["Error"] = "Product name already in use! " +
                    "Pick a new name or check and update products";
                return View(productVM);
            }
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(productVM.Image);
                var product = new Product()
                {
                    Name = productVM.Name,
                    Price = productVM.Price,
                    Image = result.Url.ToString(),
                };
                _productRepository.Add(product);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to upload image");
            }
            return View(productVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                TempData["Error"] = "Product not found";
                return RedirectToAction("Index");
            }
            var productVM = new EditViewModel()
            {
                Name = product.Name,
                Price = product.Price,
                Url = product.Image,
                Reviews = product.Reviews,
                Orders = product.Orders,
            };
            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,  EditViewModel productVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit product");
                TempData["Error"] = "Failed to edit product";
                return View("Edit", productVM);
            }
            var productCheck = await _productRepository.GetProductByIdNoTracking(id);
            if (productCheck != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(productCheck.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(productVM);
                }
                
                var photoResult = await _photoService.AddPhotoAsync(productVM.Image);
                var product = new Product()
                {
                    Id = id,
                    Name = productVM.Name,
                    Price = productVM.Price,
                    Image = photoResult.Url.ToString(),
                    Reviews = productVM.Reviews,
                    Orders = productVM.Orders,
                };
                _productRepository.Update(product);
                return RedirectToAction("Index");
            }
            return View(productVM);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var productDetails = await _productRepository.GetProductById(id);
            if (productDetails == null)
            {
                return View("Error");
            }
            return View(productDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product= await _productRepository.GetProductById(id);
            var productOrders = await _orderRepository.GetProductOrders(id);
            var productReviews = await _reviewRepository.GetReviewsOfAProductAsync(id);
            if (product == null)
            {
                return View("Error");
            }
            foreach(var order in productOrders) 
            {
                _orderRepository.Delete(order);
            }
            foreach(var review in productReviews)
            {
                _reviewRepository.Delete(review);
            }
            _productRepository.Delete(product);
            return RedirectToAction("Index");
        }
    }
}
