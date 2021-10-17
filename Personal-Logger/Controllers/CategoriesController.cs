using System.Web.Mvc;


namespace PersonalLogger.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        //GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }


        public ActionResult Details(int id)
        {
            return View();
        }
    }
}