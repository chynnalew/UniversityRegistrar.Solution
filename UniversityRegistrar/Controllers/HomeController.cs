using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;

namespace UniversityRegistrar.Controllers
{
  public class HomeController : Controller
  {
    [Route("/")]
    public ActionResult Index() 
    { 
      return View(); 
    }
  }
}