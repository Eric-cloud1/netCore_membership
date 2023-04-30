using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MemberApi.Models;

namespace MemberApi.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository repository;

        public NavigationMenuViewComponent(IProductRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Product
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
