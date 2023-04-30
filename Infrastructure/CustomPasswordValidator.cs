using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MemberApi.Models.AccountViewModels;
using System.Linq;

namespace MemberApi.Infrastructure
{
    public class CustomPasswordValidator: PasswordValidator<LoginViewModel>
    {
        public override async Task<IdentityResult> ValidateAsync(
                UserManager<LoginViewModel> manager, LoginViewModel user, string password)
        {

            IdentityResult result = await base.ValidateAsync(manager,
                user, password);

            List<IdentityError> errors = result.Succeeded ?
                new List<IdentityError>() : result.Errors.ToList();

            if (password.ToLower().Contains(user.Email.ToLower()))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordContainsUserName",
                    Description = "Password cannot contain username"
                });
            }
            if (password.Contains("12345"))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordContainsSequence",
                    Description = "Password cannot contain numeric sequence"
                });
            }

            return errors.Count == 0 ? IdentityResult.Success
                : IdentityResult.Failed(errors.ToArray());
        }
    }
}
