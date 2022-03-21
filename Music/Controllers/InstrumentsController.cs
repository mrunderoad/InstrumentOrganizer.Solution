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
  [Authorize]
  public class InstrumentsController : Controller
  {

    private readonly MusicContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public InstrumentsController(UserManager<ApplicationUser> userManager, MusicContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var model = _db.Instruments.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
        public async Task<ActionResult> Create(Instrument instrument)
        {
          var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          var currentUser = await _userManager.FindByIdAsync(userId);
          instrument.User = currentUser;
          _db.Instruments.Add(instrument);
          _db.SaveChanges();
          return RedirectToAction("Index");
        }

    public ActionResult Details(int id)
    {
      var thisInstrument = _db.Instruments
          .Include(instrument => instrument.JoinEntities)
          .ThenInclude(join => join.Item)
          .FirstOrDefault(instrument => instrument.InstrumentId == id);
      return View(thisInstrument);
    }
    public ActionResult Edit(int id)
    {
      var thisInstrument = _db.Instruments.FirstOrDefault(instrument => instrument.InstrumentId == id);
      return View(thisInstrument);
    }

    [HttpPost]
    public ActionResult Edit(Instrument instrument)
    {
      _db.Entry(instrument).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisInstrument = _db.Instruments.FirstOrDefault(instrument => instrument.InstrumentId == id);
      return View(thisInstrument);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisInstrument = _db.Instruments.FirstOrDefault(instrument => instrument.InstrumentId == id);
      _db.Instruments.Remove(thisInstrument);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}