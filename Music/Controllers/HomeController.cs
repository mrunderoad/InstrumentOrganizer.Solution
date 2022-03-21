using Microsoft.AspNetCore.Mvc;
using Music.Models;
using System.Collections.Generic;
using System.Linq;

namespace Music.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}