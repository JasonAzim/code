#if DEBUG
namespace spaBackbone.Controllers
{
    using System.Web.Mvc;

    public class MochaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
#endif
