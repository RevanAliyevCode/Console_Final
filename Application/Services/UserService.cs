using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class UserService
    {
        readonly UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateCustomer()
        {
            string name = GetUserInput.GetInput("name");
            string surname = GetUserInput.GetInput("surname");

        PinLanel: string pin = GetUserInput.GetInput("pin", Validations.IsValidPin);
            if (_unitOfWork.Customer.GetUserByPin(pin) is not null || _unitOfWork.Seller.GetUserByPin(pin) is not null)
            {
                Messages.Exist("This", "pin");
                goto PinLanel;
            }

        SeriaLabel: string seriaNumber = GetUserInput.GetInput("seria number", Validations.IsValidSeriaNumber);

            if (_unitOfWork.Customer.GetUserBySeriaNumber(seriaNumber) is not null || _unitOfWork.Seller.GetUserBySeriaNumber(seriaNumber) is not null)
            {
                Messages.Exist("This", "seria");
                goto SeriaLabel;
            }


        NumberLabel: string number = GetUserInput.GetInput("phone number", NumberRegex.IsValidNumber);

            if (_unitOfWork.Customer.GetUserByNumber(number) is not null || _unitOfWork.Seller.GetUserByNumber(number) is not null)
            {
                Messages.Exist("This", "number");
                goto NumberLabel;
            }

        EmailLabel: string email = GetUserInput.GetInput("email", EmailRegex.IsValidEmail);

            if (_unitOfWork.Customer.GetUserByEmail(email) is not null || _unitOfWork.Seller.GetUserByEmail(email) is not null)
            {
                Messages.Exist("This", "email");
                goto EmailLabel;
            }

            string password = GetUserInput.GetInput("password", PasswordRegex.IsValidPassword);
   
            E.Customer customer = new()
            {
                Name = name,
                Surname = surname,
                Pin = pin,
                SeriaNumber = seriaNumber,
                Number = number,
                Email = email,
            };

            PasswordHasher<E.Customer> passwordHasher = new();
            customer.Password = passwordHasher.HashPassword(customer, password);

            _unitOfWork.Customer.Add(customer);


            if (_unitOfWork.Commit())
                Messages.SuccessMessage("account", "created");
        }

        public void CreateSeller()
        {
            string name = GetUserInput.GetInput("name");
            string surname = GetUserInput.GetInput("surname");

            PinLanel: string pin = GetUserInput.GetInput("pin", Validations.IsValidPin);
            if (_unitOfWork.Customer.GetUserByPin(pin) is not null || _unitOfWork.Seller.GetUserByPin(pin) is not null)
            {
                Messages.Exist("This", "pin");
                goto PinLanel;
            }

            SeriaLabel: string seriaNumber = GetUserInput.GetInput("seria number", Validations.IsValidSeriaNumber);

            if (_unitOfWork.Customer.GetUserBySeriaNumber(seriaNumber) is not null || _unitOfWork.Seller.GetUserBySeriaNumber(seriaNumber) is not null)
            {
                Messages.Exist("This", "seria");
                goto SeriaLabel;
            }


            NumberLabel: string number = GetUserInput.GetInput("phone number", NumberRegex.IsValidNumber);

            if (_unitOfWork.Customer.GetUserByNumber(number) is not null || _unitOfWork.Seller.GetUserByNumber(number) is not null)
            {
                Messages.Exist("This", "number");
                goto NumberLabel;
            }

            EmailLabel: string email = GetUserInput.GetInput("email", EmailRegex.IsValidEmail);

            if (_unitOfWork.Customer.GetUserByEmail(email) is not null || _unitOfWork.Seller.GetUserByEmail(email) is not null)
            {
                Messages.Exist("This", "email");
                goto EmailLabel;
            }

            string password = GetUserInput.GetInput("password", PasswordRegex.IsValidPassword);

            E.Seller seller = new()
            {
                Name = name,
                Surname = surname,
                Pin = pin,
                SeriaNumber = seriaNumber,
                Number = number,
                Email = email,
            };

            PasswordHasher<E.Seller> passwordHasher = new();
            seller.Password = passwordHasher.HashPassword(seller, password);

            _unitOfWork.Seller.Add(seller);


            if (_unitOfWork.Commit())
                Messages.SuccessMessage("account", "created");
        }

        //private bool CheckExistance(string pin, string seriaNumber, string email, string number)
        //{
        //    return _unitOfWork.Customer.GetUserByPin(pin) is not null || _unitOfWork.Customer.GetUserByPin(seriaNumber) is not null || _unitOfWork.Customer.GetUserByPin(number) is not null || _unitOfWork.Customer.GetUserByPin(email) is not null || _unitOfWork.Seller.GetUserByPin(pin) is not null || _unitOfWork.Seller.GetUserByPin(seriaNumber) is not null || _unitOfWork.Seller.GetUserByPin(number) is not null || _unitOfWork.Seller.GetUserByPin(email) is not null;
        //}

        public E.Customer? LoginAsCustomer()
        {
            string email = GetUserInput.GetInput("email", EmailRegex.IsValidEmail);
            string password = GetUserInput.GetInput("password", PasswordRegex.IsValidPassword);

            E.Customer? customer = _unitOfWork.Customer.GetUserByEmail(email);

            if (customer == null)
            {
                Messages.LoginError();
                return null;
            }

            PasswordHasher<E.Customer> passwordHasher = new();
            var result = passwordHasher.VerifyHashedPassword(customer, customer.Password, password);

            if (result == PasswordVerificationResult.Failed)
            {
                Messages.LoginError();
                return null;
            }


            return customer;
        }

        public E.Seller? LoginAsSeller()
        {
            string email = GetUserInput.GetInput("email", EmailRegex.IsValidEmail);
            string password = GetUserInput.GetInput("password", PasswordRegex.IsValidPassword);

            E.Seller? seller = _unitOfWork.Seller.GetUserByEmail(email);

            if (seller == null)
            {
                Messages.LoginError();
                return null;
            }

            PasswordHasher<E.Seller> passwordHasher = new();
            var result = passwordHasher.VerifyHashedPassword(seller, seller.Password, password);

            if (result == PasswordVerificationResult.Failed)
            {
                Messages.LoginError();
                return null;
            }

            return seller;
        }

        public E.Admin? LoginAdmin()
        {
            string email = GetUserInput.GetInput("email", EmailRegex.IsValidEmail);
            string password = GetUserInput.GetInput("password", PasswordRegex.IsValidPassword);

            E.Admin? admin = _unitOfWork.Admin.GetAdminByEmail(email);

            if (admin == null)
            {
                Messages.LoginError();
                return null;
            }

            PasswordHasher<E.Admin> passwordHasher = new();
            var result = passwordHasher.VerifyHashedPassword(admin, admin.Password, password);

            if (result == PasswordVerificationResult.Failed)
            {
                Messages.LoginError();
                return null;
            }

            return admin;
        }

        public void VerifySeller()
        {
            List<E.Seller> sellers = _unitOfWork.Seller.GetNotVerified().ToList();

            Console.WriteLine($"{"Id",-20}{"Email",-20}");
            foreach (E.Seller s in sellers)
            {
                Console.WriteLine($"{s.Id,-20}{s.Email,-20}");
            }

            int id = GetUserInput.GetNumber<int>("id");
            E.Seller? seller = _unitOfWork.Seller.Get(id);

            if (seller is null)
            {
                Messages.NotFound("seller");
                return;
            }

            seller.IsVerified = true;
            _unitOfWork.Seller.Update(seller);

            if (_unitOfWork.Commit())
                Messages.SuccessMessage("seller", "verified");
        }
    }
}
