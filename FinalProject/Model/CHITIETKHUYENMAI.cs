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
    
    public partial class CHITIETKHUYENMAI
    {
        public string MAKM { get; set; }
        public string MALOAISPKM { get; set; }
        public Nullable<bool> TRANGTHAI { get; set; }
    
        public virtual KHUYENMAI KHUYENMAI { get; set; }
        public virtual LOAISP LOAISP { get; set; }
    }
}
