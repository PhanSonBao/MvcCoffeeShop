//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcCoffeeShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            this.HoaDons = new HashSet<HoaDon>();
        }
    
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<int> GiaBan { get; set; }
        public string MaNguyenLieu { get; set; }
        public string MaPhanLoai { get; set; }
        public string MoTa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual NguyenLieu NguyenLieu { get; set; }
        public virtual PhanLoai PhanLoai { get; set; }
    }
}
