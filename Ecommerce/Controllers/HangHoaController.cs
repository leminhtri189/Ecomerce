using Ecommerce.Data;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly Hshop2023Context db;

        public HangHoaController(Hshop2023Context context) {
            db = context;
        }
        public IActionResult Index(int? loai)
        {
            var hangHoas = db.HangHoas.AsQueryable();
            if (loai.HasValue)
            {
                hangHoas = hangHoas.Where(p => p.MaLoai == loai.Value);

            }
            var result = hangHoas.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHh = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTaNgan = p.MoTaDonVi,
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }
        public IActionResult Search(string? query)
        {
			var hangHoas = db.HangHoas.AsQueryable();
           
			if (query != null)
			{
				hangHoas = hangHoas.Where(p => p.TenHh.Contains(query));

			}
			var result = hangHoas.Select(p => new HangHoaVM
			{
				MaHh = p.MaHh,
				TenHh = p.TenHh,
				DonGia = p.DonGia ?? 0,
				Hinh = p.Hinh ?? "",
				MoTaNgan = p.MoTaDonVi,
				TenLoai = p.MaLoaiNavigation.TenLoai
			});
			return View(result);
		}

        public IActionResult Detail(int id)
        {
            var data = db.HangHoas
                .Include(p =>p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id);

            if (data == null)
            {
                TempData["Message"] = $"Khong tim thay ma co san pham voi ma {id}";
                return Redirect("/404");

            }
            var result = new ChiTietHangHoaVM
            {
                MaHh = data.MaHh, 
                TenHh = data.TenHh,
                DonGia = data.DonGia ?? 0,
                Chitiet = data.MoTa ?? string.Empty,
                DiemDanhGia = 5, //check sau
                MoTaNgan = data.MoTaDonVi ?? string.Empty,
                SoLuongTon = 10,
                Hinh = data.Hinh ?? string.Empty,
                TenLoai = data.MaLoaiNavigation.TenLoai,

            };
            return View(result);
        }
    }
}
