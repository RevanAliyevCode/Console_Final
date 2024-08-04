
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.User
{
    public class UserRepo<TUser> : Repository<TUser>, IUserRepo<TUser> where TUser : E.Base.User
    {
        readonly CommerseDbContext _context;
        public UserRepo(CommerseDbContext context) : base(context)
        {
            _context = context;
        }

        private DbSet<TUser> Table => _context.Set<TUser>();

        public TUser? GetUserByEmail(string email)
        {
            return Table.FirstOrDefault(t => t.Email.ToLower() == email.ToLower());
        }

        public TUser? GetUserByNumber(string number)
        {
            return Table.FirstOrDefault(t => t.Number.ToLower() == number.ToLower());
        }

        public TUser? GetUserByPin(string pin)
        {
            return Table.FirstOrDefault(t => t.Pin.ToLower() == pin.ToLower());
        }

        public TUser? GetUserBySeriaNumber(string seriaNumber)
        {
            return Table.FirstOrDefault(t => t.SeriaNumber.ToLower() == seriaNumber.ToLower());
        }
    }
}
