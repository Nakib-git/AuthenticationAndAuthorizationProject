using Register.Application.Service.IContracts;
using Register.Domain.Models;
using Register.Infrastructure.AppContext;

namespace Register.Application.AppService {
    public class RoleService : IRoleService {
        public readonly ApplicationDbContext _context;
        public RoleService(ApplicationDbContext context) {
            _context = context;
        }
        public IList<Role> GetRoleList() {
            var roleList = _context.Roles.ToList();
            return roleList;
        }
    }

}
