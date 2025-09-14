using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.@interface;
using KASHOP.DAL.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe.BillingPortal;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service.classes
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICartRepository _repo;
        private readonly IOrderRepository _orderRepo;
        private readonly IEmailSender _email;
        private readonly IOrderItemRepository _orderitemRepo;
        private readonly IProductRepository _productRepo;

        public CheckOutService(ICartRepository repo,IOrderRepository orderRepo,IEmailSender email,IOrderItemRepository orderitemRepo,
            IProductRepository productRepo)
        {
            _repo = repo;
            _orderRepo = orderRepo;
            _email = email;
            _orderitemRepo = orderitemRepo;
            _productRepo = productRepo;
        }

        public async Task<bool> HandlePaymentSuccessAsync(int orderId)
        {
            var order = await _orderRepo.GetUserByOrder(orderId);
            var subject = "Payment Successful";
            var body = "";
            if (order.PaymentMethod == PaymentMethodEnum.Visa)
            {
                order.Status = OrderStatusEnum.Approved;
                var carts = await _repo.GetUserCart(order.UserId);
                var orderItems = new List<OrderItem>();
                var productsUpdate = new List<(int productId, int quantity)>();
                foreach (var item in carts)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        TotalPrice = item.Count * item.Product.Price,
                        Price = item.Product.Price,
                        Count = item.Count,
                    };
                    orderItems.Add(orderItem);
                    productsUpdate.Add((item.ProductId, item.Count));
                }
                await _orderitemRepo.AddRangeAsync(orderItems);
                await _repo.ClearCartAsync(order.UserId);
                await _productRepo.DecreaseQuantity(productsUpdate);
                body = $"<h1>Payment Successful- KSHOP</h1><p>Your payment for order {order.Id} has been processed successfully.</p>";
            }
            await _email.SendEmailAsync(order.User.Email, subject, body);
            return true;

        }

        public async Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, string userId, HttpRequest Request)
        {
            var Items = await _repo.GetUserCart(userId);

            if(!Items.Any())
            {
                return new CheckOutResponse
                {
                    Success = false,
                    Message = "Cart is empty",
                };
            }
            Order order = new Order
            {
                UserId = userId,
                PaymentMethod = request.PaymentMethod,
                TotalAmount = Items.Sum(i => i.Product.Price * i.Count),
            };
            await _orderRepo.AddAsync(order);
            if (request.PaymentMethod == PaymentMethodEnum.Visa)
            {
                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>(),
                    

                    Mode = "payment",
                    SuccessUrl = $"{Request.Scheme}://{Request.Host}/api/Customer/CheckOut/Success/{order.Id}",
                    CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
                };
                foreach(var item in Items)
                {
                   options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Product.Price * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                                Description = item.Product.Description,
                            },
                        },
                        Quantity = item.Count,
                    });
                }
                var service = new Stripe.Checkout.SessionService();
                var session = await service.CreateAsync(options);
                order.PaymentId = session.Id;
                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Payment processed successfully",
                    Url = session.Url
                };
            }
            return new CheckOutResponse
            {
                Success = false,
                Message = "Payment method not supported",
            };
        }
    }
}
