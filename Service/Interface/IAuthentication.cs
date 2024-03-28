using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAuthentication
    {
        Task<ResponseViewModel> LogIn(LoginViewModel loginViewModel);
        Task<ResponseViewModel> SignIn(RegisterViewModel registerViewModel);
    }
}
