namespace Data.Repository.Admin
{
    public interface IAdminRepo : IRepository<E.Admin>
    {
        E.Admin? GetAdminByEmail(string email);
    }
}
