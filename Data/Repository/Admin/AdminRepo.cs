
namespace Data.Repository.Admin
{
    public class AdminRepo : Repository<E.Admin>, IAdminRepo
    {
        readonly CommerseDbContext _context;
        public AdminRepo(CommerseDbContext context) : base(context)
        {
            _context = context;
        }

        public E.Admin? GetAdminByEmail(string email)
        {
            return _context.Admins.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
