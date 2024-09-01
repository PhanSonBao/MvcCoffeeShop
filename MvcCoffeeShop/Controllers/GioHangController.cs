using MvcCoffeeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCoffeeShop.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        public List<MatHangMua> LayGioHang()
        {
            List<MatHangMua> giohang = Session["GioHang"] as List<MatHangMua>;

            if(giohang == null)
            {
                giohang = new List<MatHangMua>();
                Session["GioHang"] = giohang;
            }
            return giohang;
        }
        public ActionResult ThemSanPhamVaoGio(string MaSP)
        {
            //Lấy giỏ hàng hiện tại
            List<MatHangMua> giohang = LayGioHang();

            //Kiểm tra xem có tồn tại mặt hàng trong giỏ hay chưa
            //Nếu có thì tăng số lượng lên 1, ngược lại thêm vào giỏ
            MatHangMua sanPham = giohang.FirstOrDefault(s => s.MaSP == MaSP);
            if(sanPham == null)
            {
                sanPham = new MatHangMua(MaSP);
                giohang.Add(sanPham);
            }
            else
            {
                sanPham.SoLuong++; // Sản phẩm đã có trong giỏ hàng thì tăng số lượng lên 1
            }
            Session["GioHang"] = giohang;

            return RedirectToAction("Details", "CoffeeShop", new { id = MaSP });
        }
        public int TinhTongSL()
        {
            int tongSL = 0;
            List<MatHangMua> giohang = LayGioHang();
            if(giohang != null)
            {
                tongSL = giohang.Sum(sp => sp.SoLuong);
            }
            return tongSL;
        }
        public double TinhTongTien()
        {
            double TongTien = 0;
            List<MatHangMua> giohang = LayGioHang();
            if(giohang != null)
            {
                TongTien = giohang.Sum(sp => sp.ThanhTien());
            }
            return TongTien;
        }
        public ActionResult HienThiGioHang()
        {
            List<MatHangMua> giohang = LayGioHang();

            //Nếu giỏ hàng trống thì trả về trạng thái ban đầu
            if(giohang == null || giohang.Count == 0)
            {
                return RedirectToAction("Index", "CoffeeShop");
            }
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return View(giohang); //Trả về view hiển thị thông tin giỏ hàng
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();
        }
        public ActionResult XoaMatHang(string MaSanPham)
        {
            List<MatHangMua> giohang = LayGioHang();

            //Lấy sản phẩm trong giỏ hàng
            var sanpham = giohang.FirstOrDefault(s => s.MaSP == MaSanPham);
            if(sanpham != null)
            {
                giohang.RemoveAll(s => s.MaSP == MaSanPham);
                return RedirectToAction("HienThiGioHang"); //Quay về giỏ hàng
            }
            if (giohang.Count == 0) //Quay về trang chủ nếu giỏ hàng trống
                return RedirectToAction("Index", "CoffeeShop");
            return RedirectToAction("HienThiGioHang");
        }
        public ActionResult CapNhatMatHang(string MaSP, int SoLuong)
        {
            List<MatHangMua> giohang = LayGioHang();

            //Lấy sản phẩm trong giỏ hàng
            var sanpham = giohang.FirstOrDefault(s => s.MaSP == MaSP);

            if (sanpham != null)
            {
                //Cập nhật lại số lượng tương ứng và số lượng phải >= 1
                sanpham.SoLuong = SoLuong;
            }
            return RedirectToAction("HienThiGioHang"); //Quay về giỏ hàng
        }
    }
}