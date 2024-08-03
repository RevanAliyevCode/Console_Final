using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concrets.Enums
{
    public enum DefaultOperations
    {
        Exit,
        LoginAsCustomer,
        SignUpAsCustomer,
        LoginAsSeller,
        SignUpAsSeller,
        LoginAdmin = 65435
    }

    public enum AdminOperations
    {
        VerifySeller = 1,
        CreateSeller,
        DeleteSeller,
        DeleteCustomer,
        ShowSellers,
        ShowCustomers,
        AddCategory,
        ShowAllOrders,
        ShowCustomerOrders,
        ShowSellerOrders,
        ShowOrdersByDate,
    }

    public enum SellerOperations
    {
        AddProduct = 1,
        UpdateProduct,
        DeleteProduct,
        ShowOrders,
        ShowOrdersByDate,
        SearchOrders,
        ShowTotalIncome
    }

    public enum CustomerOperations
    {
        ShowProducts = 1,
        SearchProduct,
        ShowBasket,
        ShowOrders,
        ShowOrdersByDate,
    }

}
