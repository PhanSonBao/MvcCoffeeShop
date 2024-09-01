using MvcCoffeeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace MvcCoffeeShop.Controllers
{
    public class NguoiDungController : Controller
    {
        QLQUANCAPHEEntities2 databases = new QLQUANCAPHEEntities2();

        // GET: NguoiDung
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.TenKH))
                    ModelState.AddModelError(string.Empty, "Họ tên không được để trống");
                if (string.IsNullOrEmpty(kh.TenDN))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(kh.MatKhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (kh.MatKhau.Length < 6)
                    ModelState.AddModelError(string.Empty, "Mật khẩu phải có ít nhất 6 ký tự");
                if (string.IsNullOrEmpty(kh.Email))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");

                if (!Regex.IsMatch(kh.Email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
                {
                    ModelState.AddModelError(string.Empty, "Địa chỉ Email không hợp lệ");
                }

                if (string.IsNullOrEmpty(kh.SoDienThoai) || kh.SoDienThoai.Length > 10 || !Regex.IsMatch(kh.SoDienThoai, @"^\d{10}$"))
                {
                    ModelState.AddModelError(string.Empty, "Số điện thoại không hợp lệ");
                }

                if (kh.NgaySinh > DateTime.Now)
                {
                    ModelState.AddModelError(string.Empty, "Ngày sinh không được lớn hơn ngày hiện tại");
                }


                //Kiểm tra tên đăng nhập đã tồn tại hay chưa
                var khachhang = databases.KhachHangs.FirstOrDefault(k => k.TenDN == kh.TenDN);
                if (khachhang != null)
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập đã tồn tại");

                if (ModelState.IsValid)
                {
                    databases.KhachHangs.Add(kh);
                    databases.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("DangNhap");
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.TenDN))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(kh.MatKhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (kh.MatKhau.Length < 6)
                    ModelState.AddModelError(string.Empty, "Mật khẩu phải có ít nhất 6 ký tự");
            }
            if (ModelState.IsValid)
            {
                var khach = databases.KhachHangs.FirstOrDefault(k => k.TenDN == kh.TenDN && k.MatKhau == kh.MatKhau);
                if (khach != null)
                {
                    ViewBag.ThongBao = "Đăng nhập thành công";
                    // Lưu vào session
                    Session["TaiKhoan"] = khach;
                }
                else
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
    }
}