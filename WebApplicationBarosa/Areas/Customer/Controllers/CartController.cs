using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplicationBarosa.DataAccess.Repository.IRepository;
using WebApplicationBarosa.Models;

namespace WebApplicationBarosa.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCartItems = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Dog");

            double orderTotal = 0;
            foreach (var item in shoppingCartItems)
            {
                if (item.Dog != null)
                {
                    orderTotal += item.Dog.ListPrice * item.Count;
                }
            }

            ShoppingCartVM = new ShoppingCartVM
            {
                ShoppingCartList = shoppingCartItems,
                OrderHeader = new OrderHeader
                {
                    OrderTotal = orderTotal
                }
            };

            return View(ShoppingCartVM);
        }

        public IActionResult Summary()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var shoppingCartItems = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Dog");
            if (shoppingCartItems == null)
            {
                return NotFound();
            }

            if (!shoppingCartItems.Any())
            {
                return View("EmptyCart"); // Prilagodi naziv view-a
            }

            double orderTotal = 0;
            foreach (var item in shoppingCartItems)
            {
                if (item.Dog != null)
                {
                    orderTotal += item.Dog.ListPrice * item.Count;
                }
            }

            var applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            if (applicationUser == null)
            {
                return NotFound();
            }

            var shoppingCartVM = new ShoppingCartVM
            {
                ShoppingCartList = shoppingCartItems,
                OrderHeader = new OrderHeader
                {
                    ApplicationUser = applicationUser,
                    OrderTotal = orderTotal
                }
            };

            shoppingCartVM.OrderHeader.Name = applicationUser.Name;
            shoppingCartVM.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
            shoppingCartVM.OrderHeader.StreetAddress = applicationUser.StreetAddress;
            shoppingCartVM.OrderHeader.City = applicationUser.City;
            shoppingCartVM.OrderHeader.PostalCode = applicationUser.PostalCode;

            return View(shoppingCartVM);
        }


        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPOST()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCartItems = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Dog");

            double orderTotal = 0;
            foreach (var item in shoppingCartItems)
            {
                if (item.Dog != null)
                {
                    orderTotal += item.Dog.ListPrice * item.Count;
                }
            }

            var applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            var orderHeader = new OrderHeader
            {
                ApplicationUser = applicationUser,
                Name = applicationUser.Name,
                PhoneNumber = applicationUser.PhoneNumber,
                StreetAddress = applicationUser.StreetAddress,
                City = applicationUser.City,
                PostalCode = applicationUser.PostalCode,
                OrderTotal = orderTotal,
                OrderDetails = shoppingCartItems.Select(item => new OrderDetail
                {
                    DogId = item.Dog.Id,
                    Count = item.Count,
                    Price = item.Dog.ListPrice
                }).ToList()
            };

            _unitOfWork.OrderHeader.Add(orderHeader);

            foreach (var item in shoppingCartItems)
            {
                _unitOfWork.ShoppingCart.Remove(item);
            }

            _unitOfWork.Save();

            return RedirectToAction("OrderConfirmation", "Cart", new { id = orderHeader.Id });
        }

        public IActionResult OrderConfirmation(int id)
        {
            var orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == id, includeProperties: "ApplicationUser,OrderDetails.Dog");

            if (orderHeader == null)
            {
                return NotFound();
            }

            return View(orderHeader);
        }



        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            if (cartFromDb.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
