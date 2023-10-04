using Microsoft.AspNetCore.Mvc;
using PuhdasApp.Interfaces;
using PuhdasApp.Models;
using PuhdasApp.ViewModels;

namespace PuhdasApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public OrderController(IOrderRepository orderRepository,
            IProductRepository productRepository,
            IEmailService emailService,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepository.GetOrders();
            return View(orders);
        }
        public async Task<IActionResult> Create(int id) 
        {
            var product = await _productRepository.GetProductById(id);
            var orderVM = new OrderViewModel()
            {
                ProductId = product.Id,
                Image = product.Image,
                Name = product.Name,
                Price = product.Price,
            };
            return View(orderVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create (int id, OrderViewModel orderVM)
        {
            var curUserId =  _httpContextAccessor.HttpContext?.User.GetUserId();
            var customer = await _userRepository.GetById(curUserId);
            var product = await _productRepository.GetProductById(id);
            
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    ProductId = product.Id,
                    Quantity = orderVM.Quantity,
                    Size = orderVM.Size,
                    CreatedAt = DateTime.Now,
                    AppUserId = curUserId
                };
                _orderRepository.Add(order);

                var myMail = "cliffordukela@gmail.com";
                var CustomerMail = customer.Email;

                var subject = "NEW ORDER";
                var body = $"ORDER DETAILS: \n" +
                           $"Name of Product: {product.Name}.\n" +
                           $"Stated Price:{product.Price}\n " +
                           $"Quantity: {order.Quantity}.\n " +
                           $"Size: {order.Size}. \n" +
                           $"CUSTOMER DETAILS: \n" +
                           $"Customer Name: {customer.FirstName}, {customer.LastName} \n" +
                           $"Customer Location: {customer.City},{customer.State} \n" +
                           $"Phone Number: {customer.PhoneNumber} \n" +
                           $"Email: {customer.Email}";
                var customerSubject = "ORDER RECEIVED!";
                var bodyCustomer = $"Hello {customer.LastName}, {customer.FirstName}\n" +
                    $"We are delighted receive your order for {order.Quantity} pairs of {product.Name}. The selected size is {order.Size} \n" +
                    $"We will contact you shortly on how to proceed with payment for your order! \n" +
                    $"Thank you for your patronage \n" +
                    $"Puhdas footies";
                try
                {
                     await _emailService.SendEmail(myMail, subject, body);
                     await _emailService.SendEmail(CustomerMail, customerSubject, bodyCustomer);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "Product");
                }
                return RedirectToAction("Confirmation");
            }
            else
            {
                return View(orderVM);
            }
        }
        public async Task<IActionResult> Confirmation()
        {
            return View();
        }
    }
}
