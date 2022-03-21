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
  public class ItemsController : Controller
  {

    private readonly MusicContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public ItemsController(UserManager<ApplicationUser> userManager, MusicContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userItems = _db.Items.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userItems);
    }

    public ActionResult Create()
    {
      ViewBag.InstrumentId = new SelectList(_db.Instruments, "InstrumentId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Item item, int InstrumentId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      item.User = currentUser;
      _db.Items.Add(item);
      _db.SaveChanges();
        if (InstrumentId != 0)
        {
          _db.InstrumentItem.Add(new InstrumentItem() { InstrumentId = InstrumentId, ItemId = item.ItemId });
        }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisItem = _db.Items
        .Include(item => item.JoinEntities)
        .ThenInclude(join => join.Instrument)
        .FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    public ActionResult Edit(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      ViewBag.InstrumentId = new SelectList(_db.Instruments, "InstrumentId", "Name");
      return View(thisItem);
    }

    [HttpPost]
    public ActionResult Edit(Item item, int InstrumentId)
    {
      if (InstrumentId != 0)
      {
        _db.InstrumentItem.Add(new InstrumentItem() { InstrumentId = InstrumentId, ItemId = item.ItemId });
      }
      _db.Entry(item).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

      public ActionResult Delete(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      _db.Items.Remove(thisItem);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

     public async Task<ActionResult> AddInstrument(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      ViewBag.InstrumentId = new SelectList(_db.Instruments.Where(entry => entry.User.Id == currentUser.Id), "InstrumentId", "Name");
      return View(thisItem);
    }

    [HttpPost]
    public async Task<ActionResult> AddInstrument(Item item, int InstrumentId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      item.User = currentUser;
      if (InstrumentId != 0)
      {
        _db.InstrumentItem.Add(new InstrumentItem() { InstrumentId = InstrumentId, ItemId = item.ItemId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteInstrument(int joinId)
    {
      var joinEntry = _db.InstrumentItem.FirstOrDefault(joinEntry => joinEntry.InstrumentItemId == joinId);
      _db.InstrumentItem.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}