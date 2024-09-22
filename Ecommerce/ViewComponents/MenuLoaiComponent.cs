using Ecommerce.Data;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
[ViewComponent(Name = "MenuLoai")]
public class MenuLoaiComponent : ViewComponent
{
    private readonly Hshop2023Context db;

    public MenuLoaiComponent(Hshop2023Context context) => db = context;

    public IViewComponentResult Invoke()
    {
        var data = db.Loais.Select(lo => new MenuLoaiVM
        {
            Maloai = lo.MaLoai,
            Tenloai = lo.TenLoai,
            SoLuong = lo.HangHoas.Count
        }).OrderBy(p => p.Tenloai);
        return View(data);
    }

}

