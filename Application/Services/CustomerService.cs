using Core.Entities.Basket;
using Core.Concrets.Enums;

namespace Application.Services
{
    public class CustomerService
    {
        readonly UnitOfWork _unitOfWork;

        public CustomerService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private Basket? GetBasket(int id)
        {
            E.Customer customer = _unitOfWork.Customer.GetCustomerWithBasketAndOrder(id)!;

            Basket? targetBasket = null;

            if (customer.Baskets.Any())
                targetBasket = customer.Baskets.FirstOrDefault();
            else
            {
                targetBasket = new() { CustomerId = customer.Id };
                _unitOfWork.Basket.Add(targetBasket);
            }

            _unitOfWork.Commit();
            return targetBasket;
        }

        public int GetBasketItems(int id)
        {
            Basket? basket = GetBasket(id);
            if (basket == null)
            {
                Messages.NotFound("basket");
                return -1;
            }

            Basket? result = _unitOfWork.Basket.GetBasketWithBasketItem(basket.Id);

            if (result == null)
            {
                Messages.NotFound("basket");
                return -1;
            }

            Console.WriteLine($"{"Id",-20}{"Product id",-20}{"Name",-20}{"Quantity",-20}");
            foreach (var item in result.Items)
            {
                Console.WriteLine($"{item.Id,-20}{item.Product.Id,-20}{item.Product.Name,-20}{item.Quantity,-20}");
            }

            return result.Id;
        }

        public void AddBasketItem(int id, int productId, int quantity = 1)
        {
            Basket? basket = GetBasket(id);

            if (basket == null)
            {
                Messages.NotFound("basket");
                return;
            }

            BasketItem? existingItem = _unitOfWork.BasketItem.GetWhere(b => b.BasketId == basket.Id && b.ProductId == productId).FirstOrDefault();

            E.Product product = _unitOfWork.Product.Get(productId)!;

            if (product.Stock < quantity)
            {
                Messages.OutOfStock();
                return;
            }

            if (existingItem is not null)
            {
                existingItem.Quantity += quantity;
                _unitOfWork.BasketItem.Update(existingItem);
            }
            else
            {
                BasketItem item = new()
                {
                    ProductId = productId,
                    Quantity = quantity,
                    BasketId = basket.Id
                };

                _unitOfWork.BasketItem.Add(item);
            }

            product.Stock -= quantity;
            _unitOfWork.Product.Update(product);

            if (_unitOfWork.Commit())
                Messages.SuccessMessage("to basket", "added");
        }

        public void UpdateQuantity(int userId)
        {
            Basket? basket = GetBasket(userId);

            if (basket == null)
            {
                Messages.NotFound("basket");
                return;
            }

            int id = GetUserInput.GetNumber<int>("id");
            BasketItem? basketItem = _unitOfWork.BasketItem.GetWhere(b => b.BasketId == basket.Id && b.Id == id).FirstOrDefault();

            if (basketItem is null)
            {
                Messages.NotFound("basket item");
                return;
            }

            E.Product product = _unitOfWork.Product.Get(basketItem.ProductId);

            product.Stock += basketItem.Quantity;

            int quantity = GetUserInput.GetNumber<int>("quantity");

            if (quantity < 0) return;
            if (product.Stock < quantity)
            {
                Messages.OutOfStock();
                return;
            }

            basketItem.Quantity = quantity;
            product.Stock -= quantity;

            _unitOfWork.BasketItem.Update(basketItem);
            _unitOfWork.Product.Update(product);
            _unitOfWork.Commit();
        }

        public void DeleteBasketItem(int userId)
        {
            Basket? basket = GetBasket(userId);

            if (basket == null)
            {
                Messages.NotFound("basket");
                return;
            }

            int id = GetUserInput.GetNumber<int>("id");
            BasketItem? basketItem = _unitOfWork.BasketItem.GetWhere(b => b.BasketId == basket.Id && b.Id == id).FirstOrDefault();

            if (basketItem is null)
            {
                Messages.NotFound("basket item");
                return;
            }

            E.Product product = _unitOfWork.Product.Get(basketItem.ProductId);

            char choice = GetUserInput.GetOpinion("basket item", "delete");

            if (choice.Equals('y'))
            {
                _unitOfWork.BasketItem.Delete(basketItem);
                product.Stock += basketItem.Quantity;

                _unitOfWork.Product.Update(product);

                if (_unitOfWork.Commit())
                    Messages.SuccessMessage("basket item", "deleted");
            }
        }

        public void ShowAllProducts()
        {
            List<E.Product> products = _unitOfWork.Product.GetAll().ToList();

            Console.WriteLine($"{"Id",-20}{"Name",-20}{"Price",-20}");
            foreach (E.Product product in products)
            {
                Console.WriteLine($"{product.Id,-20}{product.Name,-20}{product.Price,-20}");
            }
        }

        public void ShowProduct(int id)
        {
            E.Product? product = _unitOfWork.Product.GetProductWithCategoryAndSeller(id);

            if (product is null)
            {
                Messages.NotFound("product");
                return;
            }

            Console.WriteLine($"Title: {product.Name}");
            Console.WriteLine($"Description: {product.Description}");
            Console.WriteLine($"Price: {product.Price}");
            Console.WriteLine($"Stock: {product.Stock}");
            Console.WriteLine($"Category: {product.Category.Name}");
            Console.WriteLine($"Seller: {product.Seller.Name}");
        }

        public void ShowSearchedProducts()
        {
            string query = GetUserInput.GetInput("query");
            List<E.Product> products = _unitOfWork.Product.GetWhere(p => p.Name.Contains(query)).ToList();

            Console.WriteLine($"{"Id",-20}{"Name",-20}{"Price",-20}");
            foreach (E.Product product in products)
            {
                Console.WriteLine($"{product.Id,-20}{product.Name,-20}{product.Price,-20}");
            }
        }

        public void ShowCustomerOrders(int id)
        {
            E.Customer? customer = _unitOfWork.Customer.GetCustomerWithOrder(id);

            if (customer is null)
            {
                Messages.NotFound("customer");
                return;
            }

            Console.WriteLine(customer.Name + " " + customer.Surname);
            Console.WriteLine($"Orders ({customer.Orders.Count}): ");
            Console.WriteLine($"{"Product name",-20}{"Seller",-20}{"Quantity",-20}{"Total amount",-20}{"Status",-20}");
            foreach (var order in customer.Orders.OrderByDescending(o => o.CreatedDate))
                Console.WriteLine($"{order.Product.Name,-20}{order.Seller.Name,-20}{order.Quantity, -20}{order.TotalAmount,-20}{order.Status}");
        }

        public void ShowCustomerOrdersByDate(int id)
        {
            E.Customer? customer = _unitOfWork.Customer.GetCustomerWithOrder(id);

            if (customer is null)
            {
                Messages.NotFound("customer");
                return;
            }

        DateLabel: Messages.InputMessages("date");
            bool isSucceded = DateTime.TryParse(Console.ReadLine(), out DateTime dateTime);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto DateLabel;
            }

            List<E.Order> orders = customer.Orders.Where(o => o.CreatedDate.Date == dateTime.Date).ToList();


            Console.WriteLine(customer.Name + " " + customer.Surname);
            Console.WriteLine($"Orders ({customer.Orders.Count}): ");
            Console.WriteLine($"{"Product name",-20}{"Seller",-20}");
            foreach (var order in orders)
                Console.WriteLine($"{order.Product.Name,-20}{order.Seller.Name,-20}");
        }

        public void AddToOrder(int basketId)
        {
            Basket? result = _unitOfWork.Basket.GetBasketWithBasketItem(basketId);

            if (result == null)
            {
                Messages.NotFound("basket");
                return;
            }

            decimal totalAmount = 0;

            if (result.Items.Count == 0)
            {
                Console.WriteLine("There is no item for add to order");
                return;
            }

            foreach (var item in result.Items)
            {
                totalAmount = item.Quantity * item.Product.Price;
                E.Order order = new()
                {
                    SellerId = item.Product.SellerId,
                    CustomerId = result.CustomerId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalAmount = totalAmount,
                    Status = Status.Pending
                };

                E.Seller seller = _unitOfWork.Seller.Get(item.Product.SellerId);

                seller.TotalIncome += totalAmount;

                _unitOfWork.Order.Add(order);
                _unitOfWork.Seller.Update(seller);
                _unitOfWork.BasketItem.Delete(item);
            }


            if (_unitOfWork.Commit())
                Messages.SuccessMessage("order", "created");
        }

        public void ShowProtuctsToCustomer(int userId)
        {
            ShowAllProducts();
            Console.WriteLine("Write 0 for exit");
            int id = GetUserInput.GetNumber<int>();
            if (id == 0) return;

            ShowProduct(id);

            Console.WriteLine("0.Back");
            Console.WriteLine("1.Add to basket");
            Console.WriteLine("2.Add to basket with any quantity");
            int choice = GetUserInput.GetNumber<int>();

            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    AddBasketItem(userId, id);
                    break;
                case 2:
                    int quantity = GetUserInput.GetNumber<int>("quantity");
                    if (quantity < 0) break;
                    AddBasketItem(userId, id, quantity);
                    break;
                default:
                    Messages.InvalidChoice();
                    break;
            }
        }

        public void BasketOperations(int userId)
        {
            int basketId = GetBasketItems(userId);

            Console.WriteLine("0.Back");
            Console.WriteLine("1.Update quantity");
            Console.WriteLine("2.Remove item");
            Console.WriteLine("3.Checkout to order");
            int choice = GetUserInput.GetNumber<int>();

            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    UpdateQuantity(userId);
                    break;
                case 2:
                    DeleteBasketItem(userId);
                    break;
                case 3:
                    AddToOrder(basketId);
                    break;
                default:
                    Messages.InvalidChoice();
                    break;
            }
        }
    }
}
