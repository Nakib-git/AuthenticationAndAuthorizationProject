using Register.Domain.Models;

namespace Register.Application.Service.IContracts {
    public interface IAuthenticationService {
        AuthenticateResponse Authenticate(Login model);
    }
}
