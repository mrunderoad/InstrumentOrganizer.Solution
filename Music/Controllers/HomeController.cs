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

    [HttpPost]
    public async Task<ActionResult> Index(string Search)
    {
      var items = from i in _db.Items select i;
      if (!string.IsNullOrEmpty(Search))
      {
        items = items.Where(item => item.Name!.Contains(Search));
      }
      return View(await items.ToListAsync());
    }
  }
}