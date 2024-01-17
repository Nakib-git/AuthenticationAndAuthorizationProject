using Microsoft.EntityFrameworkCore;
using Register.Application.Service.IContracts;
using Register.Domain.Models;
using Register.Infrastructure.AppContext;

namespace Register.Application.AppService {
    public class UserService : IUserService {
        public readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context) {
            _context = context;
        }
        public bool Remove(User user) {
            var exisitingUser = _context.Users.FindAsync(user.Id);
            if (exisitingUser == null) {
                throw new Exception("User not found");
            }
            _context.Remove(exisitingUser);
            var isRemove = _context.SaveChanges() > 0;
            return isRemove;
        }

        public bool Add(User user) {
            var userList = _context.Users.ToList();
            if (userList.Count > 0) {
                user.RoleId = _context.Roles.Where(c => c.Name == "User").First().Id;
            } else {
                user.RoleId = _context.Roles.Where(c => c.Name == "Admin").First().Id;
            }
            _context.AddAsync(user);
            var isAdded = _context.SaveChanges() > 0;
            return isAdded;
        }

        public bool Update(User user) {
            var exisitingUser = _context.Users.Find(user.Id);
            if (exisitingUser == null) {
                throw new Exception("User not found");
            }
            exisitingUser.UserName = user.UserName;
            exisitingUser.PhoneNumber = user.PhoneNumber;
            exisitingUser.Email = user.Email;
            exisitingUser.Password = user.Password;
            exisitingUser.RoleId = user.RoleId;
            _context.Update(exisitingUser);
            var isUpdated = _context.SaveChanges() > 0;
            return isUpdated;
        }

        public IList<User> GetAll() {
            var userList = _context.Users.Include(c => c.Role).ToList();
            return userList;
        }
        public User GetById(int id) {
            var exisitingUser = _context.Users.Include(c => c.Role).Where(c => c.Id == id).First();
            if (exisitingUser == null) {
                throw new Exception("User not found");
            }
            return exisitingUser;
        }

        public User? GetByEmail(string email) {
            var exisitingUser = _context.Users.Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefault();
            return exisitingUser;
        }

        public User? GetByEmailWithOutId(string email, int id) {
            var exisitingUser = _context.Users.Where(u => u.Email.ToLower() == email.ToLower() && u.Id != id)?.FirstOrDefault();
            return exisitingUser;
        }

        public User GetByEmailAndPassword(string email, string password) {
            var exisitingUser = _context.Users.Where(u => u.Email.ToLower() == email.ToLower() && u.Password == password).FirstOrDefault();
            if (exisitingUser == null) {
                throw new Exception("User not found");
            }
            return exisitingUser;
        }
    }

}
