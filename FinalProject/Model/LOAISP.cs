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
    
    public partial class LOAISP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAISP()
        {
            this.CHITIETKHUYENMAIs = new HashSet<CHITIETKHUYENMAI>();
            this.HOADONBTs = new HashSet<HOADONBT>();
            this.SANPHAMs = new HashSet<SANPHAM>();
        }
    
        public string MALOAI { get; set; }
        public string TENLOAI { get; set; }
        public byte[] IMG { get; set; }
        public byte[] IMGDV1 { get; set; }
        public byte[] IMGDV2 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETKHUYENMAI> CHITIETKHUYENMAIs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADONBT> HOADONBTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
