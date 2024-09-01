using MvcCoffeeShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace MvcCoffeeShop.Controllers
{
    public class CoffeeShopController : Controller
    {
        // Use DbContext to manage database
        QLQUANCAPHEEntities2 database = new QLQUANCAPHEEntities2();
        private List<SanPham> LaySanPham(int soluong)
        {
            //Sắp xếp sản phẩm theo tên và lọc theo mã phân loại, lấy đúng số lượng sản phẩm cần
            return database.SanPhams
                .OrderBy(sanpham => sanpham.TenSanPham)
                .Take(soluong)
                .ToList();
        }


        // GET: CoffeeShop
        public ActionResult Index(string maPhanLoai)
        {
            var dsSanPham = LaySanPham(5);

            return View(dsSanPham);
        }

        public ActionResult LayThucUong()
        {
            var dsThucUong = database.PhanLoais.ToList();
            return PartialView(dsThucUong);
        }

        public ActionResult SPTheoPhanLoai(string id, int? page)
        {
            ViewBag.CategoryId = id;

            int pageSize = 5; // Số sản phẩm trên mỗi trang
            int pageNum = (page ?? 1); // Số trang hiện tại

            // Lấy các sản phẩm theo mã phân loại
            var dsSanPhamTheoPhanLoai = database.SanPhams
                .Where(sanpham => sanpham.MaPhanLoai == id)
                .OrderBy(sanpham => sanpham.TenSanPham)
                .ToPagedList(pageNum, pageSize);

            // Trả về View để render các sản phẩm
            return View(dsSanPhamTheoPhanLoai);
        }

        public ActionResult Details(string id)
        {
            //Lây sản phẩm có mã tương ứng
            var sanpham = database.SanPhams.FirstOrDefault(sp => sp.MaSanPham == id);
            return View(sanpham);
        }
        public ActionResult Products(string id, int? page)
        {

            int pageSize = 5; //Tạo biến cho biết số sản phẩm mỗi trang
            int pageNum = (page ?? 1); //Tạo biến số trang

            //Giả sử cần lấy 24 sản phẩm mới cập nhật
            var dsSanPham = LaySanPham(24);
            return View(dsSanPham.ToPagedList(pageNum, pageSize));


        }
        //public ActionResult Products(int? page)
        //{
        //    int pageSize = 5; //Tạo biến cho biết số sản phẩm mỗi trang
        //    int pageNum = (page ?? 1); //Tạo biến số trang

        //    //Giả sử cần lấy 15 quyển sách mới cập nhật
        //    var dsSanPham = LaySanPham(24);
        //    return View(dsSanPham.ToPagedList(pageNum, pageSize));
        //}
    }
}