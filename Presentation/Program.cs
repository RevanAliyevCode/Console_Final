using Application.Services;
using Core.Concrets;
using Core.Concrets.Enums;
using Core.Entities;
using Core.Menus;
using Core.Validates;
using Data;
using Data.UnitOfWork;

namespace Commerse_App
{
    public static class Program
    {
        static UnitOfWork unitOfWork = new();
        static AdminService adminService = new(unitOfWork);
        static CustomerService customerService = new(unitOfWork);
        static SellerService sellerService = new(unitOfWork);
        static UserService userService = new(unitOfWork);


        public static void Main()
        {
            DbInitilazer.SeedAdmin();

  

            while (true)
            {
                DefaultMenu.Menu();
                DefaultOperations choice = (DefaultOperations)Validation.GetNumber<int>();

                switch (choice)
                {
                    case DefaultOperations.LoginAsCustomer:
                        CustomerApp(userService.LoginAsCustomer());
                        break;
                    case DefaultOperations.SignUpAsCustomer:
                        userService.CreateCustomer();
                        break;
                    case DefaultOperations.LoginAsSeller:
                        SellerApp(userService.LoginAsSeller());
                        break;
                    case DefaultOperations.SignUpAsSeller:
                        userService.CreateSeller();
                        break;
                    case DefaultOperations.LoginAdmin:
                        AdminApp(userService.LoginAdmin());
                        break;
                    default:
                        Messages.InvalidChoice();
                        break;
                }
            }
        }

        static void CustomerApp(Customer? user)
        {
            if (user == null) return;


            while (true)
            {
                Console.WriteLine("----CUSTOMER MENU----");
                Console.WriteLine("0.Exit");
                CustomerMenu.Menu();
                CustomerOperations operations = (CustomerOperations)Validation.GetNumber<int>();

                switch (operations)
                {
                    case (CustomerOperations)DefaultOperations.Exit:
                        return;
                    case CustomerOperations.ShowProducts:
                        customerService.ShowProtuctsToCustomer(user.Id);
                        break;
                    case CustomerOperations.SearchProduct:
                        customerService.ShowSearchedProducts();
                        break;
                    case CustomerOperations.ShowBasket:
                        customerService.BasketOperations(user.Id);
                        break;
                    case CustomerOperations.ShowOrders:
                        customerService.ShowCustomerOrders(user.Id);
                        break;
                    case CustomerOperations.ShowOrdersByDate:
                        customerService.ShowCustomerOrdersByDate(user.Id);
                        break;
                    default:
                        Messages.InvalidChoice();
                        break;
                }

            }
        }

        static void SellerApp(Seller? user)
        {
            if (user == null) return;

            if (!user.IsVerified)
            {
                Console.WriteLine("Wait for admin to verify you");
                return;
            }


            while (true)
            {
                Console.WriteLine("----SELLER MENU----");
                Console.WriteLine("0.Exit");
                SellerMenu.Menu();
                SellerOperations operations = (SellerOperations)Validation.GetNumber<int>();
                switch (operations)
                {
                    case (SellerOperations)DefaultOperations.Exit:
                        return;
                    case SellerOperations.AddProduct:
                        sellerService.AddProduct(user.Id);
                        break;
                    case SellerOperations.UpdateProduct:
                        sellerService.UpdateProduct(user.Id);
                        break;
                    case SellerOperations.DeleteProduct:
                        sellerService.DeleteSellerProduct(user.Id);
                        break;
                    case SellerOperations.ShowOrders:
                        sellerService.ShowSellerOrders(user.Id);
                        break;
                    case SellerOperations.ShowOrdersByDate:
                        sellerService.ShowSellerOrdersByDate(user.Id);
                        break;
                    case SellerOperations.SearchOrders:
                        break;
                    case SellerOperations.ShowTotalIncome:
                        sellerService.ShowTotalIncome(user.Id);
                        break;
                    default:
                        Messages.InvalidChoice();
                        break;
                }

            }
        }

        static void AdminApp(Admin? admin)
        {
            if (admin == null) return;


            while (true)
            {
                Console.WriteLine("----ADMIN MENU----");
                Console.WriteLine("0.Exit");
                AdminMenu.Menu();
                AdminOperations operations = (AdminOperations)Validation.GetNumber<int>();
                switch (operations)
                {
                    case (AdminOperations)DefaultOperations.Exit:
                        return;
                    case AdminOperations.VerifySeller:
                        userService.VerifySeller();
                        break;
                    case AdminOperations.CreateSeller:
                        userService.CreateSeller();
                        break;
                    case AdminOperations.DeleteSeller:
                        adminService.DeleteSeller();
                        break;
                    case AdminOperations.DeleteCustomer:
                        adminService.DeleteCustomer();
                        break;
                    case AdminOperations.ShowSellers:
                        adminService.ShowAllSellers();
                        break;
                    case AdminOperations.ShowCustomers:
                        adminService.ShowAllCustomers();
                        break;
                    case AdminOperations.AddCategory:
                        adminService.AddCategory();
                        break;
                    case AdminOperations.ShowAllOrders:
                        adminService.ShowAllOrders();
                        break;
                    case AdminOperations.ShowCustomerOrders:
                        adminService.ShowCustomerOrders();
                        break;
                    case AdminOperations.ShowSellerOrders:
                        adminService.ShowSellerOrders();
                        break;
                    case AdminOperations.ShowOrdersByDate:
                        adminService.ShowOrdersByDate();
                        break;
                    default:
                        Messages.InvalidChoice();
                        break;
                }

            }
        }
    }
}



//static void ShowProtuctsToCustomer(int userId)
//{
//    ShowProductsService.ShowAllProducts();
//    Console.WriteLine("Write 0 for exit");
//    int id = Validation.GetNumber<int>();
//    if (id == 0) return;

//    ShowProductsService.ShowProduct(id);

//    Console.WriteLine("0.Back");
//    Console.WriteLine("1.Add to basket");
//    Console.WriteLine("2.Add to basket with any quantity");
//    int choice = Validation.GetNumber<int>();

//    switch (choice)
//    {
//        case 0:
//            return;
//        case 1:
//            BasketService.AddBasketItem(userId, id);
//            break;
//        case 2:
//            int quantity = Validation.GetNumber<int>("quantity");
//            BasketService.AddBasketItem(userId, id, quantity);
//            break;
//        default:
//            Messages.InvalidChoice();
//            break;
//    }
//}

//static void BasketOperations(int userId)
//{
//    int basketId = BasketService.GetBasketItems(userId);

//    Console.WriteLine("0.Back");
//    Console.WriteLine("1.Update quantity");
//    Console.WriteLine("2.Remove item");
//    Console.WriteLine("3.Checkout to order");
//    int choice = Validation.GetNumber<int>();

//    switch (choice)
//    {
//        case 0:
//            return;
//        case 1:
//            BasketService.UpdateQuantity(userId);
//            break;
//        case 2:
//            BasketService.DeleteBasketItem(userId);
//            break;
//        case 3:
//            AddToOrderService.AddToOrder(basketId);
//            break;
//        default:
//            Messages.InvalidChoice();
//            break;
//    }
//}