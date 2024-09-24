using AutoMapper;
using Ecommerce.Data;
using Ecommerce.Helper;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
	public class KhachHangController : Controller
	{
		private readonly Hshop2023Context db;
		private readonly IMapper _mapper;

		public KhachHangController(Hshop2023Context context, IMapper mapper)
		{
			db = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult DangKy()
		{
			return View();
		}

		[HttpPost]
		public IActionResult DangKy(RegisterVM model, IFormFile hinh)
		{

			if (ModelState.IsValid)
			{
				try
				{
					var khachHang = _mapper.Map<KhachHang>(model);
					khachHang.RandomKey = Util.GenerateRamdomKey();
					khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
					khachHang.HieuLuc = true;
					khachHang.VaiTro = 0;
					if (hinh != null)
					{
						khachHang.Hinh = Util.UpLoadHinh(hinh, "KhachHang");
						db.Add(khachHang);
						db.SaveChanges();
						return RedirectToAction("Index", "HangHoa");
					}
				}
				catch (Exception ex)
				{
				}

			}
			return View();
		}

		#region Login
		[HttpGet]
		public IActionResult DangNhap(string? ReturnURL)
		{
			ViewBag.ReturnURL = ReturnURL;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnURL)
		{
			ViewBag.ReturnURL = ReturnURL;
			if (ModelState.IsValid)
			{
				var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.UserName
				);
				if (khachHang == null)
				{
					ModelState.AddModelError("Loi", "Khong co khach hàng này");
				}
				else
				{
					if (!khachHang.HieuLuc)
					{
						ModelState.AddModelError("loi", "tai khoan da bi khoa. vui long lien he admin");
					}
					else
					{
						if (khachHang.MatKhau != model.Password.ToMd5Hash(khachHang.RandomKey))
						{
							ModelState.AddModelError("loi", "Sai mat khảu");
						}
						else
						{
							// ghi nhận 
							var claim = new List<Claim> {
							new Claim(ClaimTypes.Email, khachHang.Email),
							new Claim(ClaimTypes.Name, khachHang.HoTen),
							new Claim("CustomerID", khachHang.MaKh),
							new Claim(ClaimTypes.Role,"Customer")
							};
							var claimIdentyti = new ClaimsIdentity(claim,"Login");
							var claimPrincipal = new ClaimsPrincipal(claimIdentyti);
							await HttpContext.SignInAsync(claimPrincipal);
							if (Url.IsLocalUrl(ReturnURL)) {
							return	Redirect(ReturnURL);
							}
                            else
                            {
								return Redirect("/");
                            }

                        }
					}
				}
			}
			return View();
		}
		#endregion
		[Authorize]
		public IActionResult Profile()
		{
			return View();
		}

		[Authorize]
		public async Task<IActionResult> DangXuat()
		{
			await HttpContext.SignOutAsync();
			return Redirect("/");
		}
	}

}
