//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalProject.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            this.CHITIETSANPHAMs = new HashSet<CHITIETSANPHAM>();
            this.HOADONMHs = new HashSet<HOADONMH>();
        }
    
        public string MASP { get; set; }
        public string TENSP { get; set; }
        public Nullable<decimal> GIAGOC { get; set; }
        public Nullable<decimal> GIABAN { get; set; }
        public string CONGSUAT { get; set; }
        public string MOMEN { get; set; }
        public string DUNGTICH { get; set; }
        public string TRONGLUONG { get; set; }
        public string GHICHU { get; set; }
        public string MALOAISP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETSANPHAM> CHITIETSANPHAMs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADONMH> HOADONMHs { get; set; }
        public virtual LOAISP LOAISP { get; set; }
    }
}
