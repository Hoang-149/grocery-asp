using BanHangAsp.Models;
using BanHangAsp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BanHangAsp.ViewComponents
{
    public class LoaiSpMenuViewComponent : ViewComponent
    {
        private readonly ILocalRepository _loaiSp;

        public LoaiSpMenuViewComponent(ILocalRepository loaiSpRepository)
        {
            _loaiSp = loaiSpRepository;
        }

        public IViewComponentResult Invoke()
        {
            var loaisp = _loaiSp.GetAllLoaiSp().OrderBy(x=> x.Loai);
            return View(loaisp);
        }
    }
}
