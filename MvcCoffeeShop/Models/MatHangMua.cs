using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCoffeeShop.Models
{
    public class MatHangMua
    {
        QLQUANCAPHEEntities2 db = new QLQUANCAPHEEntities2();
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string HinhAnh { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public string MaPhanLoai { get; set; }


        //Tính thành tiền = đơn giá * số lượng
        public double ThanhTien()
        {
            return SoLuong * DonGia;
        }
        public MatHangMua(string MaSP)
        {
            this.MaSP = MaSP;

            var sanpham = db.SanPhams.Single(sp => sp.MaSanPham == this.MaSP);
            this.TenSP = sanpham.TenSanPham;
            this.HinhAnh = sanpham.HinhAnh;
            this.DonGia = double.Parse(sanpham.GiaBan.ToString());
            this.SoLuong = 1; // Số lượng mua ban đầu của một mặt hàng là 1 (cho lần click đầu)
        }
    }
}