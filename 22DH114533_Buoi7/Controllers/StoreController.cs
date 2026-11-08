using DBSportStore.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DBSportStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly DBSportStoreEntities db = new DBSportStoreEntities();

        // GET: Store
        public ActionResult Index(
            int? categoryId,
            string keyword,
            decimal? minPrice,
            decimal? maxPrice,
            string sortOrder,
            int page = 1)
        {
            const int pageSize = 6;
            page = Math.Max(1, page);

            var products = db.Products.Where(p => p.IsActive);

            if (categoryId.HasValue)
                products = products.Where(p => p.CategoryID == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                products = products.Where(p =>
                    p.ProductName.Contains(keyword) || p.Description.Contains(keyword));
            }

            if (minPrice.HasValue)
                products = products.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                products = products.Where(p => p.Price <= maxPrice.Value);

            var totalItems = products.Count();
            var totalPages = Math.Max(1, (int)Math.Ceiling(totalItems / (double)pageSize));
            page = Math.Min(page, totalPages);

            var orderedProducts = products;
            switch (sortOrder)
            {
                case "price_asc":
                    orderedProducts = products.OrderBy(p => p.Price).ThenBy(p => p.ProductID);
                    break;
                case "price_desc":
                    orderedProducts = products.OrderByDescending(p => p.Price).ThenBy(p => p.ProductID);
                    break;
                default:
                    orderedProducts = products.OrderByDescending(p => p.ProductID);
                    sortOrder = "";
                    break;
            }

            var model = new StoreHomeViewModel
            {
                Categories = db.Categories.OrderBy(c => c.CategoryName).ToList(),
                Products = orderedProducts
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => new ProductCardViewModel
                    {
                        ProductID = p.ProductID,
                        NamePro = p.ProductName,
                        CategoryName = p.Category.CategoryName,
                        Price = p.Price,
                          StockQuantity = p.StockQuantity,
                        ImagePro = p.ImageUrl,
                        ShortDescription = p.Description
                    })
                    .ToList(),
                SelectedCategoryId = categoryId,
                SearchTerm = keyword,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                SortOrder = sortOrder,
                CurrentPage = page,
                TotalPages = totalPages,
                TotalItems = totalItems
            };

            return View(model);
        }

        // GET: Store/AboutStore
        public ActionResult AboutStore()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
