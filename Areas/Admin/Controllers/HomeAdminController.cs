using BanHangAsp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BanHangAsp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("HomeAdmin")]

    public class HomeAdminController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();

        [Route("")]
        [Route("index")]

        public IActionResult Index()
        {
            return View();
        }

        [Route("quanlysanpham")]

        public IActionResult QuanLySanPham(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var listSP = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> list = new PagedList<TDanhMucSp>(listSP, pageNumber, pageSize);

            return View(list);
        }

        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPham()
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
            return View();
        }

        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPham(TDanhMucSp sanpham)
        {
            if (ModelState.IsValid)
            {
                db.TDanhMucSps.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction("quanlysanpham");
            }
            return View(sanpham);
        }

        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(string masp)
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");

            var sanPham = db.TDanhMucSps.Find(masp);
            return View(sanPham);
        }

        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(TDanhMucSp sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("quanlysanpham", "HomeAdmin");
            }
            return View(sanpham);
        }

        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(string masp)
        {
            TempData["Message"] = "";
            var chiTietSp = db.TChiTietSanPhams.Where(x => x.MaSp == masp).ToList();
            if (chiTietSp.Count() > 0)
            {
                TempData["Message"] = "Không xóa được sản phẩm này!";
                return RedirectToAction("quanlysanpham", "HomeAdmin");
            }
            var anhSp = db.TAnhSps.Where(x => x.MaSp == masp);
            if (anhSp.Any()) db.RemoveRange(anhSp);
            db.Remove(db.TDanhMucSps.Find(masp));
            db.SaveChanges();
            TempData["Message"] = "Xóa sản phẩm thành công!";
            return RedirectToAction("quanlysanpham", "HomeAdmin");
        }

        // DanhMucSanPham
        [Route("quanlydanhmuc")]

        public IActionResult QuanLyDanhMuc(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var listDM = db.TLoaiSps.AsNoTracking().OrderBy(x => x.Loai);
            PagedList<TLoaiSp> list = new PagedList<TLoaiSp>(listDM, pageNumber, pageSize);

            return View(list);
        }

        [Route("ThemDanhMucMoi")]
        [HttpGet]

        public IActionResult ThemDanhMuc()
        {
            return View();
        }

        [Route("ThemDanhMucMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult ThemDanhMuc(TLoaiSp danhmuc)
        {
            if (ModelState.IsValid)
            {
                db.TLoaiSps.Add(danhmuc);
                db.SaveChanges();
                return RedirectToAction("quanlydanhmuc");
            }
            return View();
        }

        [Route("SuaDanhMuc")]
        [HttpGet]

        public IActionResult SuaDanhMuc(string madm)
        {
            var MaDm = db.TLoaiSps.Find(madm);
            return View(MaDm);
        }

        [Route("SuaDanhMuc")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult SuaDanhMuc(TLoaiSp madm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(madm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("quanlydanhmuc", "HomeAdmin");

            }
            return View();
        }

        [Route("XoaDanhMuc")]
        [HttpGet]
        public IActionResult XoaDanhMuc(string madm)
        {
            TempData["Message"] = "";
            var danhMuc = db.TLoaiSps.Find(madm);
            if (danhMuc != null)
            {
                db.Remove(danhMuc);
                db.SaveChanges();
                TempData["Message"] = "Xóa danh mục thành công!";
            }
            else
            {
                TempData["Message"] = "Danh mục không tồn tại!";
            }
            return RedirectToAction("quanlydanhmuc", "HomeAdmin");
        }

    }
}
