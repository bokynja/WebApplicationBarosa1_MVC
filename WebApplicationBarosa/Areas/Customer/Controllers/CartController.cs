using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplicationBarosa.DataAccess.Repository.IRepository;
using WebApplicationBarosa.Models;
using WebApplicationBarosa.Utility;

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

            var shoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Dog");

            double orderTotal = 0;
            foreach (var item in shoppingCartList)
            {
                if (item.Dog != null)
                {
                    orderTotal += item.Dog.ListPrice * item.Count;
                }
            }

            var applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

            if (applicationUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var existingOrderHeader = _unitOfWork.OrderHeader.Get(u => u.ApplicationUserId == userId && u.OrderStatus == SD.StatusPending);
            if (existingOrderHeader != null)
            {
                existingOrderHeader.OrderTotal = orderTotal;
                existingOrderHeader.Name = applicationUser.Name;
                existingOrderHeader.PhoneNumber = applicationUser.PhoneNumber;
                existingOrderHeader.StreetAddress = applicationUser.StreetAddress;
                existingOrderHeader.City = applicationUser.City;
                existingOrderHeader.PostalCode = applicationUser.PostalCode;
                _unitOfWork.OrderHeader.Update(existingOrderHeader);
            }
            else
            {
                ShoppingCartVM.OrderHeader = new OrderHeader
                {
                    ApplicationUserId = userId,
                    Name = applicationUser.Name,
                    PhoneNumber = applicationUser.PhoneNumber,
                    StreetAddress = applicationUser.StreetAddress,
                    City = applicationUser.City,
                    PostalCode = applicationUser.PostalCode,
                    OrderTotal = orderTotal,
                    PaymentStatus = applicationUser.CompanyID.GetValueOrDefault() == 0 ? SD.PaymentstatusPending : SD.PaymentStatusDelayedPayment,
                    OrderStatus = applicationUser.CompanyID.GetValueOrDefault() == 0 ? SD.StatusPending : SD.StatusApproved,
                };

                _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
                _unitOfWork.Save();
            }

            // Stripe logika
            var domain = "https://localhost:7294/";
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
        {
            "card",
        },
                LineItems = shoppingCartList.Select(item => new Stripe.Checkout.SessionLineItemOptions
                {
                    PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Dog.ListPrice * 100), // Stripe expects the amount in cents
                        Currency = "usd",
                        ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Dog.Breed,
                        },
                    },
                    Quantity = item.Count,
                }).ToList(),
                Mode = "payment",
                SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
                CancelUrl = domain + "customer/cart/index",
            };

            var service = new Stripe.Checkout.SessionService();
            var session = service.Create(options);

            // Dodaj sesiju za Stripe u OrderHeader
            ShoppingCartVM.OrderHeader.SessionId = session.Id;
            ShoppingCartVM.OrderHeader.PaymentIntentId = session.PaymentIntentId;
            _unitOfWork.Save();

            // Preusmeri korisnika na Stripe Checkout
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
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
