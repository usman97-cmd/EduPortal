using Student_Mangement_System.Models;
using Student_Mangement_System.ViewModels;

namespace Student_Mangement_System.Services
{
    public interface IAccountService
    {
        bool Register(RegisterViewModel vm);

        User? Login(LoginViewModel vm);

    }
}
