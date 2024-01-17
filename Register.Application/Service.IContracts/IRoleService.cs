using Register.Domain.Models;

namespace Register.Application.Service.IContracts {
    public interface IRoleService {
        IList<Role> GetRoleList();
    }
}