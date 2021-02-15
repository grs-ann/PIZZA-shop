using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaShopApplication.Models.Data.Domain.Interfaces;
using PizzaShopApplication.Models.Data.Entities.Data;
using PizzaShopApplication.Models.Secondary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrder _orderRepository;
        public OrderController(IOrder orderRepository)
        {
            _orderRepository = orderRepository;
        }
        [HttpGet]
        public IActionResult SetOrder(string totalSum, List<UserCartInformer> userCart)
        {
            ViewBag.userCart = userCart;
            ViewBag.totalSum = totalSum;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SetOrder(Order order)
        {
            await _orderRepository.AddOrderToDBAsync(order);
            order = await _orderRepository.GetOrderFromDBAsync(order);
            return Content($"Спасибо за ваш заказ! В течении 10 минут с вами свяжется наш работник " +
                $"для уточнения заказа.\nЕсли этого не произошло, позвоните нам по номеру: 8-846-322." +
                $"\nВаш номер заказа: {order.Id}");
        }
        [HttpGet]
        [Authorize(Roles = "admin, dispatcher")]
        public IActionResult GetOrders(int? orderStatusId, int? orderId, DateTime date)
        {
            //var orders = _orderRepository.GetOrders();
            var ordersViewModel = _orderRepository.GetOrdersWithFiltration(orderStatusId, orderId, date).Result;
            return View(ordersViewModel);
        }
        // Получает заказ по его Id. В заказе отображается информация 
        // по заказанным товарам, а так же информация о заказчике.
        [HttpGet]
        [Authorize(Roles = "admin, dispatcher")]
        public IActionResult GetConcreteOrder(string userCartId, int orderId)
        {
            var order  = _orderRepository.GetOrderFromDBAsync(orderId).Result;
            ViewBag.OrderStatus = _orderRepository.GetOrderStatuses();
            ViewBag.Order = order;
            // Получение корзины заказчика.
            var cartItems = _orderRepository.GetConcreteCartFromOrder(userCartId);
            return View(cartItems);
        }
        // Изменяет статус заказа.
        [HttpPost]
        [Authorize(Roles = "admin, dispatcher")]
        public async Task<IActionResult> GetConcreteOrder(int orderId, int statusId, Guid cartUserId)
        {
            await _orderRepository.ChangeOrderStatusAsync(orderId, statusId);
            ViewBag.OrderStatus = _orderRepository.GetOrderStatuses();
            ViewBag.Order = await _orderRepository.GetOrderFromDBAsync(orderId);
            var cartItems = _orderRepository.GetConcreteCartFromOrder(cartUserId.ToString());
            return View(cartItems);
        }
    }
}
