namespace Data.Repository.User
{
    public interface IUserRepo<TUser> : IRepository<TUser> where TUser : E.Base.User
    {
        TUser? GetUserByEmail(string email);
        TUser? GetUserByNumber(string number);
        TUser? GetUserByPin(string pin);
        TUser? GetUserBySeriaNumber(string seriaNumber);
    }
}
