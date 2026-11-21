using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tdp_241230817.Models;

namespace Tdp_241230817.Controllers;

public class HomeController : Controller
{
    private readonly QLHangHoaContext _db;

    public HomeController(QLHangHoaContext db) => _db = db;

    // Câu 3: hàng hóa có Gia >= 100 (Gia NULL tự bị loại), tiêu đề = họ tên SV
    public async Task<IActionResult> Index()
    {
        var ds = await _db.HangHoas
            .Where(h => h.Gia != null && h.Gia >= 100)
            .OrderBy(h => h.MaHang)
            .ToListAsync();

        ViewData["TieuDe"] = SinhVien.HoTen;
        return View(ds);
    }

    // Câu 4: lọc theo loại hàng bằng AJAX
    public async Task<IActionResult> TheoLoai(int id)
    {
        var loai = await _db.LoaiHangs.FindAsync(id);
        if (loai == null) return NotFound();

        var ds = await _db.HangHoas
            .Where(h => h.MaLoai == id)
            .OrderBy(h => h.MaHang)
            .ToListAsync();

        ViewData["TieuDe"] = loai.TenLoai;

        // Request AJAX (jQuery) -> chỉ trả về phần main content, không tải lại cả trang
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_Tdp_MainContent", ds);

        // Truy cập trực tiếp bằng URL (hoặc từ trang Create) -> trả cả trang
        return View("Index", ds);
    }

    public IActionResult Error() => Content("Đã xảy ra lỗi.");
}
