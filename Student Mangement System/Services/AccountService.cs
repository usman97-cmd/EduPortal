using Student_Mangement_System.Data;
using Student_Mangement_System.Models;
using Student_Mangement_System.ViewModels;

namespace Student_Mangement_System.Services
{
    public class AccountService (AppDbContext context): IAccountService
    {
        public bool Register(RegisterViewModel vm)
        {
            var existingUsername = context.Users.Any(u => u.Username == vm.Username);
            if (existingUsername)
            {
                return false;
            }
            var existingEmail = context.Users.Any(u => u.Email == vm.Email);
            if (existingEmail)
            {
                return false;
            }
            var user = new User
            {
                Name = vm.Name,
                Email = vm.Email,
                Username = vm.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(vm.Password),
                Role = "User"
            };
            context.Users.Add(user);
            context.SaveChanges();
            return true;
        }
        public User? Login(LoginViewModel vm)
        {
            var user = context.Users.FirstOrDefault(
               x => x.Username == vm.Username
           );

            if (user == null)
            {
                return null;
            }
            bool passwordvalid = BCrypt.Net.BCrypt.Verify(vm.Password, user.PasswordHash);
            if (!passwordvalid)
            {
                return null;
            }
            return user;
        }
    }
}
