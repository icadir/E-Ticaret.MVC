using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace Abc.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        DataContext _context = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            var urunler = _context.Products.Where(x => x.IsHome && x.IsApproved).Select(x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description.Length > 50 ? x.Description.Substring(0, 47) + "..." : x.Description,
                Price = x.Price,
                Stock = x.Stock,
                CategoryId = x.CategoryId,
                Image = x.Image ?? "1.jpg"
            }).ToList();

            return View(urunler);
        }

        public ActionResult Details(int id)
        {

            return View(_context.Products.FirstOrDefault(x => x.Id == id));
        }

        public ActionResult List(int? id)
        {
            var urunler = _context.Products.Where(x => x.IsApproved).Select(x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name.Length > 50 ? x.Name.Substring(0, 40) + "..." : x.Name,
                Description = x.Description.Length > 50 ? x.Description.Substring(0, 47) + "..." : x.Description,
                Price = x.Price,
                Stock = x.Stock,
                CategoryId = x.CategoryId,
                Image = x.Image ?? "1.jpg"
            }).AsQueryable();

            if (id != null)
            {
                urunler = urunler.Where(x => x.CategoryId == id);
            }

            return View(urunler.ToList());
        }

        public PartialViewResult GetCategories()
        {
            return PartialView(_context.Categories.ToList());
        }
    }
}