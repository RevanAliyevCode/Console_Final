using Core.Concrets.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SellerService
    {
        readonly UnitOfWork _unitOfWork;

        public SellerService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void ShowSellerOrders(int id)
        {
            E.Seller? seller = _unitOfWork.Seller.GetWithOrders(id);

            if (seller is null)
            {
                Messages.NotFound("seller");
                return;
            }

            Console.WriteLine(seller.Name + " " + seller.Surname);
            Console.WriteLine($"Orders ({seller.Orders.Count}): ");
            Console.WriteLine($"{"Id",-20}{"Product name",-20}{"Buyer",-20}{"Quantity",-20}{"Total Amount",-20}{"Status",-20}");
            foreach (var order in seller.Orders.OrderByDescending(o => o.CreatedDate))
                Console.WriteLine($"{order.Id,-20}{order.Product.Name,-20}{order.Customer.Name,-20}{order.Quantity,-20}{order.TotalAmount,-20}{order.Status,-20}");

            UpdateOrderStatus(id);

        }

        public void ShowSellerOrdersByDate(int id)
        {
            E.Seller? seller = _unitOfWork.Seller.GetWithOrders(id);

            if (seller is null)
            {
                Messages.NotFound("seller");
                return;
            }

        DateLabel: Messages.InputMessages("date");
            bool isSucceded = DateTime.TryParse(Console.ReadLine(), out DateTime dateTime);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto DateLabel;
            }

            List<E.Order> orders = seller.Orders.Where(o => o.CreatedDate.Date == dateTime.Date).ToList();

            Console.WriteLine(seller.Name + " " + seller.Surname);
            Console.WriteLine($"Orders ({seller.Orders.Count}): ");
            Console.WriteLine($"{"Product name",-20}{"Buyer",-20}");
            foreach (var order in orders)
                Console.WriteLine($"{order.Product.Name,-20}{order.Customer.Name,-20}");
        }

        public void ShowCategoryAll()
        {
            Console.WriteLine($"{"Id",-20}{"Name",-20}");
            foreach (var category in _unitOfWork.Category.GetAll())
                Console.WriteLine($"{category.Id,-20}{category.Name,-20}");
        }

        public void AddProduct(int sellerId)
        {
            string name = GetUserInput.GetInput("name");
            string description = GetUserInput.GetInput("description");
            int stock = GetUserInput.GetNumber<int>("stock");
            decimal price = GetUserInput.GetNumber<decimal>("price");

        CategoryLable: ShowCategoryAll();
            int categoryId = GetUserInput.GetNumber<int>("category id");

            E.Category? category = _unitOfWork.Category.Get(categoryId);

            if (category is null)
            {
                Messages.NotFound("category");
                goto CategoryLable;
            }

            E.Product product = new()
            {
                Name = name,
                Description = description,
                Stock = stock,
                Price = price,
                CategoryId = categoryId,
                SellerId = sellerId
            };

            _unitOfWork.Product.Add(product);

            if (_unitOfWork.Commit())
                Messages.SuccessMessage("product", "added");
        }

        public void ShowSellerProducts(int id)
        {
            E.Seller? seller = _unitOfWork.Seller.GetWithProducts(id);

            if (seller is null)
            {
                Messages.NotFound("seller");
                return;
            }

            Console.WriteLine($"{"Id",-20}{"Name",-20}{"Price",-20}");
            foreach (E.Product product in seller.Products)
            {
                Console.WriteLine($"{product.Id,-20}{product.Name,-20}{product.Price,-20}");
            }
        }

        public void DeleteSellerProduct(int id)
        {
            ShowSellerProducts(id);

            int productId = GetUserInput.GetNumber<int>("product id");

            E.Product? product = _unitOfWork.Product.Get(productId);

            if (product is null || product.SellerId != id)
            {
                Messages.NotFound("product");
                return;
            }

            char choice = GetUserInput.GetOpinion("product", "delete");

            if (choice.Equals('y'))
            {
                _unitOfWork.Product.Delete(product);

                if (_unitOfWork.Commit())
                    Messages.SuccessMessage("product", "delete");
            }
        }

        public void ShowTotalIncome(int userId)
        {
            E.Seller? seller = _unitOfWork.Seller.GetWithOrders(userId);

            if (seller == null)
            {
                Messages.NotFound("Seller");
                return;
            }

            Console.WriteLine($"Total income: {seller.TotalIncome}");
        }

        public void UpdateProduct(int id)
        {
            ShowSellerProducts(id);

            int productId = GetUserInput.GetNumber<int>("product id");

            E.Product? product = _unitOfWork.Product.Get(productId);

            if (product is null || product.SellerId != id)
            {
                Messages.NotFound("Product");
                return;
            }

            char choice = GetUserInput.GetOpinion("stock", "change");

            if (choice.Equals('y'))
            {
                int stock = GetUserInput.GetNumber<int>("new stock");

                product.Stock = stock;
            }

            choice = GetUserInput.GetOpinion("price", "change");

            if (choice.Equals('y'))
            {
                decimal price = GetUserInput.GetNumber<decimal>("new price");

                product.Price = price;
            }

            _unitOfWork.Product.Update(product);

            if (_unitOfWork.Commit())
                Messages.SuccessMessage("product", "updated");
        }

        public void UpdateOrderStatus(int userId)
        {
            E.Seller? seller = _unitOfWork.Seller.GetWithOrders(userId);

            int orderId = GetUserInput.GetNumber<int>("order id for change status (0 for exit)");

            E.Order? order = _unitOfWork.Order.Get(orderId);

            if (order is null || !seller.Orders.Any(o => o.Id == order.Id))
            {
                Messages.NotFound("order");
                return;
            }

            Console.WriteLine("1.Pending");
            Console.WriteLine("2.Processing");
            Console.WriteLine("3.Shipped");
            Console.WriteLine("4.Delivered");
            Console.Write("Write order status: ");
            bool isSucceded = Enum.TryParse<Status>(Console.ReadLine(), out Status result);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                return;
            }

            order.Status = result;

            _unitOfWork.Order.Update(order);

            if (_unitOfWork.Commit())
                Messages.SuccessMessage("order", "updated");
        }
    }
}
