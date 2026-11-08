using DBSportStore.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DBSportStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly DBSportStoreEntities db = new DBSportStoreEntities();

        // GET: Store
        public ActionResult Index()
        {
            var products = db.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .OrderBy(p => p.ProductID)
                .Select(p => new ProductCardViewModel
                {
                    ProductID = p.ProductID,
                    NamePro = p.ProductName,
                    CategoryName = p.Category.CategoryName,
                    Price = p.Price,
                    ImagePro = p.ImageUrl,
                    ShortDescription = p.Description
                })
                .ToList();

            return View(products);
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
