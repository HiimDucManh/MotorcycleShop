﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class MOTORBIKEMANAGEMENTEntities : DbContext
{
    public MOTORBIKEMANAGEMENTEntities()
        : base("name=MOTORBIKEMANAGEMENTEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<CHITIETKHUYENMAI> CHITIETKHUYENMAIs { get; set; }

    public virtual DbSet<CHITIETSANPHAM> CHITIETSANPHAMs { get; set; }

    public virtual DbSet<HOADONBT> HOADONBTs { get; set; }

    public virtual DbSet<HOADONMH> HOADONMHs { get; set; }

    public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }

    public virtual DbSet<KHUYENMAI> KHUYENMAIs { get; set; }

    public virtual DbSet<LOAISP> LOAISPs { get; set; }

    public virtual DbSet<MAU> MAUs { get; set; }

    public virtual DbSet<NGUOIDUNG> NGUOIDUNGs { get; set; }

    public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }

    public virtual DbSet<SANPHAM> SANPHAMs { get; set; }

}

}

