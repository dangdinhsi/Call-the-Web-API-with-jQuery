using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeleteAll_with_JS.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeleteAll_with_JS.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Product.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(Product product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();
            TempData["message"] = "Insert success!";
            return Redirect("Index");
        }

        public IActionResult Edit(long id)
        {
            var product = _context.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Update(Product product)
        {
            var exisProduct = _context.Product.Find(product.Id);
            if (exisProduct == null)
            {
                return NotFound();
            }

            exisProduct.Name = product.Name;
            exisProduct.Price = product.Price;
            _context.Product.Update(exisProduct);
            _context.SaveChanges();
            TempData["message"] = "Update success!";
            return Redirect("Index");
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var product = _context.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            _context.SaveChanges();
            return Json(product);
        }

        [HttpDelete]
        public IActionResult DeleteAll(string ids)
        {
          foreach (var id in ids.Split(","))
            {
               var exitProduct = _context.Product.Find(Convert.ToInt64(id));
                if (exitProduct == null)
                {
                    return NotFound();
                }

              _context.Product.Remove(exitProduct);
                _context.SaveChanges();

            }

           return RedirectToAction(nameof(Index));
        }
    }
}