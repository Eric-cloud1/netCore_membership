using Microsoft.AspNetCore.Mvc;

namespace MemberApi.Controllers
{

    public class ErrorController : Controller
    {

        public ViewResult Error() => View();
    }
}
