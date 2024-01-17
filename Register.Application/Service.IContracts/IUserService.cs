using Register.Domain.Models;

namespace Register.Application.Service.IContracts {
    public interface IUserService {
        bool Remove(User user);
        bool Add(User user);
        bool Update(User user);
        IList<User> GetAll();
        User GetById(int id);
        User? GetByEmail(string email);
        User? GetByEmailWithOutId(string email, int id);
        User GetByEmailAndPassword(string email, string password);
    }
}
