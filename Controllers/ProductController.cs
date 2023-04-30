using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MemberApi.Models;
using MemberApi.Models.ViewModels;
using MemberApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MemberApi.Controllers
{
   // [Authorize(Roles = "Dispensary,Processor,TestLab,Admin")]
    [Authorize(Roles = "Approved,Admin")]
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }


        //single select list drop down
        private List<SelectListItem> GetCategory(int? id)
        {
            string selectedValue = repository.Category(id).Result;

            var categoryList = new List<SelectListItem>();

            foreach (Product p in repository.Product)
            { 
            if (categoryList.SingleOrDefault(s => s.Value == p.Category) != null)
                    continue;

             categoryList.Add(new SelectListItem() { Value = p.Category, Text = p.Category, Selected = (selectedValue == p.Category) });
             }

            if (id is null)
                categoryList.Add(new SelectListItem() { Value = "0", Text = "Select a Category", Selected = true });

            return categoryList;
        }



        public ViewResult List(string category, int productPage = 1)
            => View(new ProductsListViewModel
            {
                Product = repository.Product
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductId)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        repository.Product.Count() :
                        repository.Product.Where(e =>
                            e.Category == category).Count()
                },
                CurrentCategory = category
            });

        // GET: Products/Edit/5
        //    [HttpPost]
        //   [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await  repository.Product.SingleOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return View();
            }

            ViewData["ListCategory"] = GetCategory(id); ;
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,,Category,Name,Description,Price")] Product product)
        {
            if (id != product.ProductId)
            {
                return View();
            }

            if (ModelState.IsValid && await repository.SaveProductAsync(product))
            {
                    return RedirectToAction(nameof(ProductController.List));
            }
            else
            {
                ViewData["ListCategory"] = GetCategory(id);
                return View(product);
            }
        
        }


        //    // POST: Products/Create
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,, Category, Name, Description, Price")] Product product)
        {
            ViewData["ListCategory"] = GetCategory(null);

            if (ModelState.IsValid && await repository.SaveProductAsync(product))
            {
                return RedirectToAction(nameof(ProductController.List));
            }
                return View(product);
        }


        //    // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await repository.Product
                .SingleOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await repository.Product
              .SingleOrDefaultAsync(p => p.ProductId == id);

            if (await repository.DeleteProductAsync(id)== true)
            {
                return RedirectToAction(nameof(ProductController.List));
            }

            return View(product);
        }

        private bool ProductExists(int id)
        {
            IQueryable<Product> product = repository.Product;
            return product.Any(e => e.ProductId == id);
        }
    }
}