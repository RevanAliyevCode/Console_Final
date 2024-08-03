namespace Application.Services
{
    public class AdminService
    {
        readonly UnitOfWork _unitOfWork;

        public AdminService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddCategory()
        {
        NameLabel: string name = Validation.GetInput("name");

            if (_unitOfWork.Category.GetAll().FirstOrDefault(x => x.Name == name) is not null)
            {
                Messages.Exist("Category", name);
                goto NameLabel;
            }

            E.Category category = new() { Name = name };
            _unitOfWork.Category.Add(category);

            if (_unitOfWork.Commit())
                Messages.SuccessMessage("category", "added");
        }

        public void ShowAll()
        {
            Console.WriteLine($"{"Id",-20}{"Name",-20}");
            foreach (var category in _unitOfWork.Category.GetAll())
                Console.WriteLine($"{category.Id,-20}{category.Name,-20}");
        }

        public void ShowAllOrders()
        {
            Console.WriteLine($"{"Product name",-20}{"Buyer",-20}{"Seller",-20}");
            foreach (var order in _unitOfWork.Order.GetWithSellerAndCustomer())
                Console.WriteLine($"{order.Product.Name,-20}{order.Customer.Name,-20}{order.Seller.Name,-20}");
        }

        public void ShowSellerOrders()
        {
            ShowAllSellers();
            int id = Validation.GetNumber<int>("id");

            E.Seller? seller = _unitOfWork.Seller.GetWithOrders(id);

            if (seller is null)
            {
                Messages.NotFound("seller");
                return;
            }

            Console.WriteLine(seller.Name + " " + seller.Surname);
            Console.WriteLine($"Orders ({seller.Orders.Count}): ");
            Console.WriteLine($"{"Product name",-20}{"Buyer",-20}");
            foreach (var order in seller.Orders)
                Console.WriteLine($"{order.Product.Name,-20}{order.Customer.Name,-20}{order.TotalAmount,-20}");
        }

        public void ShowCustomerOrders()
        {
            ShowAllCustomers();
            int id = Validation.GetNumber<int>("id");

            E.Customer? customer = _unitOfWork.Customer.GetCustomerWithOrder(id);

            if (customer is null)
            {
                Messages.NotFound("customer");
                return;
            }

            Console.WriteLine(customer.Name + " " + customer.Surname);
            Console.WriteLine($"Orders ({customer.Orders.Count}): ");
            Console.WriteLine($"{"Product name",-20}{"Seller",-20}{"Total amount",-20}");
            foreach (var order in customer.Orders)
                Console.WriteLine($"{order.Product.Name,-20}{order.Seller.Name,-20}{order.TotalAmount,-20}");
        }

        public void ShowOrdersByDate()
        {
        DateLabel: Messages.InputMessages("date");
            bool isSucceded = DateTime.TryParse(Console.ReadLine(), out DateTime dateTime);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto DateLabel;
            }

            List<E.Order> orders = _unitOfWork.Order.GetByDate(dateTime).ToList();


            Console.WriteLine($"{"Product name",-20}{"Buyer",-20}{"Seller",-20}");
            foreach (var order in orders)
                Console.WriteLine($"{order.Product.Name,-20}{order.Customer.Name,-20}{order.Seller.Name,-20}");
        }

        public void ShowDetailed(int id = default)
        {
            if (id == default)
                id = Validation.GetNumber<int>("id");

            E.Category? category = _unitOfWork.Category.GetWithProducts(id);

            if (category == null)
            {
                Messages.NotFound("category");
                return;
            }

            Console.WriteLine("Id");
            Console.WriteLine("Name");
            Console.WriteLine("Products:");
            foreach (var product in category.Products)
                Console.WriteLine($"\t{product.Name}");

        }

        public void DeleteCustomer()
        {
            ShowAllCustomers();
            int id = Validation.GetNumber<int>("id");

            E.Customer? user = _unitOfWork.Customer.Get(id);

            if (user is null)
            {
                Messages.NotFound("customer");
                return;
            }

            char choice = Validation.GetOpinion("customer", "delete");

            if (choice.Equals('y'))
            {
                _unitOfWork.Customer.Delete(user);

                if (_unitOfWork.Commit())
                    Messages.SuccessMessage("customer", "delete");
            }
        }

        public void DeleteSeller()
        {
            ShowAllSellers();
            int id = Validation.GetNumber<int>("id");

            E.Seller? user = _unitOfWork.Seller.Get(id);

            if (user is null)
            {
                Messages.NotFound("seller");
                return;
            }

            char choice = Validation.GetOpinion("seller", "delete");

            if (choice.Equals('y'))
            {
                _unitOfWork.Seller.Delete(user);

                if (_unitOfWork.Commit())
                    Messages.SuccessMessage("seller", "delete");
            }
        }

        public void ShowAllCustomers()
        {
            Console.WriteLine($"{"Id",-20}{"Name",-20}{"Pin",-20}{"Email",-20}");
            foreach (var user in _unitOfWork.Customer.GetAll())
                Console.WriteLine($"{user.Id,-20}{user.Name + " " + user.Surname,-20}{user.Pin,-20}{user.Email,-20}");
        }

        public void ShowAllSellers()
        {
            Console.WriteLine($"{"Id",-20}{"Name",-20}{"Pin",-20}{"Email",-20}");
            foreach (var user in _unitOfWork.Seller.GetAll())
                Console.WriteLine($"{user.Id,-20}{user.Name + " " + user.Surname,-20}{user.Pin,-20}{user.Email,-20}");
        }
    }
}
