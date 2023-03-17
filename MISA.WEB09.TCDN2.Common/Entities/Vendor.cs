using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB09.TCDN2.Common.Entities
{
    public class Vendor
    {
        /// <summary>
        /// ID nhà cung cấp
        /// </summary>
        [Key]
        public Guid VendorID { get; set; }

        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        [IsNotNullOrEmpty("Mã nhà cung cấp không được trống.")]
        [Unique("Mã nhà cung cấp đã tồn tại trong hệ thống.")]
        public string VendorCode { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        [IsNotNullOrEmpty("Tên nhà cung cấp không được để trống.")]
        public string VendorName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// diễn giải
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// CÔNG Nợ
        /// </summary>
        [IsNumber("^[0-9]+$","Số công nợ phải là số.")]
        public int? Debt { get; set; }

        /// <summary>
        /// ID nhân viên
        /// </summary>
        public Guid? EmployeeID { get; set; }

        /// <summary>
        /// mã nhân viên
        /// </summary>
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// tên nhân viên
        /// </summary>
        public string? EmployeeName { get; set; }

        /// <summary>
        /// tài khoản ngân hàng
        /// </summary>
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// chi nhánh ngân hàng
        /// </summary>
        public string? BankBranchName { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [IsNumber("^[0-9]+$", "Số điện thoại phải là số.")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string? TaxCode { get; set; }

        /// <summary>
        /// địa chỉ website
        /// </summary>
        public string? Website { get; set; }

        /// <summary>
        /// số chứng minh nhân dân
        /// </summary>
        [IsNumber("^[0-9]+$","Số chứng minh nhân dân phải là số.")]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Điều khoản thanh toán
        /// </summary>
        public string? Rule { get; set; }

        /// <summary>
        /// số ngày được nợ
        /// </summary>
        [IsNumber("^[0-9]+$","Số ngày được nợ phải là số.")]
        public int? DayOwed { get; set; }

        /// <summary>
        /// Số nợ tối đa
        /// </summary>
        [IsNumber("^[0-9]+$", "Số nợ tối đa phải là số.")]
        public int? MaximumDebt { get; set; }

        /// <summary>
        /// Tài khoản công nợ phải trả
        /// </summary>
        public string? AcountPayable { get; set; }

        /// <summary>
        /// Xưng hô
        /// </summary>
        public string? Vocative { get; set; }

        /// <summary>
        /// Người liên hệ
        /// </summary>
        public string? Contact { get; set; }

        /// <summary>
        /// Điện thoại liên hệ
        /// </summary>
        [IsNumber("^[0-9]+$","Số điện thoại liên hệ phải là số.")]
        public string? ContactPhone { get; set; }

        /// <summary>
        /// Email liên hệ
        /// </summary>
        [ValidateEmail("/^(([^<>()[\\]\\.,;:\\s@\\\"]+(\\.[^<>()[\\]\\.,;:\\s@\\\"]+)*)|(\\\".+\\\"))@(([^<>()[\\]\\.,;:\\s@\\\"]+\\.)+[^<>()[\\]\\.,;:\\s@\\\"]{2,})$/i;", "Email chưa đúng định dạng.")]
        public string? ContactEmail { get; set; }

        /// <summary>
        /// Người đại diện theo pháp luật
        /// </summary>
        public string? AgentLaw { get; set; }

        /// <summary>
        /// Tên Nhóm nhà cung cấp
        /// </summary>
        public string? GroupVendorName { get; set; }

        /// <summary>
        /// ID Nhóm nhà cung cấp
        /// </summary>
        public Guid? GroupVendorID { get; set; }

        /// <summary>
        /// MÃNhóm nhà cung cấp
        /// </summary>
        public String? GroupVendorCode{ get; set; }
    }
}
