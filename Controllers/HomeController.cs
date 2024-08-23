using BanHangAsp.Models;
using BanHangAsp.Models.Authentication;
using BanHangAsp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace BanHangAsp.Controllers
{
    public class HomeController : Controller
    {
        QlbanVaLiContext db=new QlbanVaLiContext();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //[Authentication]

        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var listSP = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> list = new PagedList<TDanhMucSp>(listSP, pageNumber, pageSize);

            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SanPhamTheoLoai(String maloai, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var listSP = db.TDanhMucSps.AsNoTracking().Where(x=>x.MaLoai==maloai).OrderBy(x => x.TenSp);

            PagedList<TDanhMucSp> list = new PagedList<TDanhMucSp>(listSP, pageNumber, pageSize);

            ViewBag.maloai = maloai;


            return View(list); 
        }

        public IActionResult ChiTietSanPham(string maSp)
        {
            var sanpham = db.TDanhMucSps.SingleOrDefault(x=>x.MaSp==maSp);
            var anhSanpham = db.TAnhSps.Where(x=>x.MaSp==maSp).ToList();

            ViewBag.anhSanPham = anhSanpham;
            return View(sanpham);
        }

        public IActionResult ProductDetail(string maSp)
        {
            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhSanpham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
            var homeProductDetailViewModel = new HomeProductsDetailViewModel{danhMucSp = sanPham, anhSps = anhSanpham};
            return View(homeProductDetailViewModel);
        }

        public IActionResult ShoppingCart(string maSp)
        {
            // Lấy thông tin người dùng hiện tại (giả sử có UserId từ session hoặc authentication)
            //string userId = HttpContext.Session.GetString("UserId");
            string userId = "1";

            if (userId == null)
            {
                // Xử lý khi người dùng chưa đăng nhập, có thể chuyển hướng đến trang đăng nhập
                return RedirectToAction("Login", "Account");
            }

            // Tạo một bản ghi giỏ hàng mới
            TCart newCart = new TCart
            {
                MaKhanhHang = userId,
                MaSp = maSp,
                Quantity = 1, 
                DateCreated = DateTime.Now
            };

            // Thêm bản ghi mới vào cơ sở dữ liệu
            db.TCarts.Add(newCart);
            db.SaveChanges();

            // Chuyển hướng người dùng tới trang giỏ hàng hoặc bất kỳ trang nào bạn muốn
            return RedirectToAction("Index", "Cart"); // Điều hướng tới trang giỏ hàng
        }


    }
}
