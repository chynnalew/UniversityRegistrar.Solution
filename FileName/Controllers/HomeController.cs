using Microsoft.AspNetCore.Mvc;
using FileName.Models;

namespace FileName.Controllers
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