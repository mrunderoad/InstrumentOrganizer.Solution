using Microsoft.AspNetCore.Mvc;
using Music.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Music.Controllers
{
  public class HomeController : Controller
  {

    public readonly MusicContext _db;
    public HomeController(MusicContext db)
    {
      _db = db;
    }

    [HttpGet("/")]

    public ActionResult Index()
    {
      return View();
    }
    
    public ActionResult Search(string Search)
    {
      var instruments = _db.Instruments.Where(instrument => (instrument.Name.Contains(Search) || (instrument.Name == Search))).ToList();
      var items =_db.Items.Where(item => (item.Name.Contains(Search) || (item.Name == Search))).ToList();
      ViewBag.Instruments = instruments;
      ViewBag.Items = items;
      return View();
    }

    public ActionResult All()
    {
      var items =_db.Items.ToList();
      var instruments =_db.Instruments.ToList();
      ViewBag.Items = items;
      ViewBag.Instruments = instruments;
      return View();
    }
  }
}