using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tdp_241230817.Models;

namespace Tdp_241230817.Controllers;

public class HangHoaController : Controller
{
    private readonly QLHangHoaContext _db;

    public HangHoaController(QLHangHoaContext db) => _db = db;

   
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await NapLoaiHang();
        return View(new HangHoa());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HangHoa hangHoa)
    {
        ModelState.Remove(nameof(HangHoa.MaLoaiNavigation));

        if (!ModelState.IsValid)
        {
            await NapLoaiHang(hangHoa.MaLoai);
            return View(hangHoa);            
        }

        _db.HangHoas.Add(hangHoa);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index", "Home");
    }

    private async Task NapLoaiHang(int? chon = null)
    {
        var ds = await _db.LoaiHangs.AsNoTracking().OrderBy(l => l.MaLoai).ToListAsync();
        ViewBag.MaLoai = new SelectList(ds, nameof(LoaiHang.MaLoai), nameof(LoaiHang.TenLoai), chon);
    }
}
