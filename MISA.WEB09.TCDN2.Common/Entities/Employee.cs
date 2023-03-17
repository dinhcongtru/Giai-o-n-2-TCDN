using MISA.WEB09.TCDN2.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace MISA.WEB09.TCDN2.Common.Entities
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    public class Employee 
    {
        /// <summary>
        /// ID nhân viên
        /// </summary>
        [Key]
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [IsNotNullOrEmpty("Mã nhân viên không được trống.")]
        [Unique("Mã nhân viên đã tồn tại trong hệ thống.")]
        public string EmployeeCode { get; set; }
        
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [IsNotNullOrEmpty("Tên nhân viên không được để trống.")]
        public string EmployeeName { get; set; }
       
        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; } // enum
       
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        [IsNumber("^[0-9]+$","Số Chứng minh nhân dân phải là số.")]
        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
        public string? IdentityNumber { get; set; }
        
        /// <summary>
        /// Vị trí
        /// </summary>
        public string? JobPositionName { get; set; }

        [IsNotNullOrEmpty("Đơn vị không được để trống.")]
        /// <summary>
        /// phòng ban
        /// </summary>
        public Guid DepartmentID { get; set; }

        [IsNumber("^[0-9]+$", "Số điện thoại phải là số.")]
        /// <summary>
        /// Điện thoại di động
        /// </summary>
        public string? PhoneNumber { get; set; }

        [IsNumber("^[0-9]+$", "Điện thoại cố định phải là số.")]
        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        public string? TelephoneNumber { get; set; }

        [ValidateEmail("/^(([^<>()[\\]\\.,;:\\s@\\\"]+(\\.[^<>()[\\]\\.,;:\\s@\\\"]+)*)|(\\\".+\\\"))@(([^<>()[\\]\\.,;:\\s@\\\"]+\\.)+[^<>()[\\]\\.,;:\\s@\\\"]{2,})$/i;", "Email chưa đúng định dạng.")]
        /// <summary>
        /// email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// nơi cấp
        /// </summary>
        public string? IdentitiPlace { get; set; }

        [IsNumber("^[0-9]+$", "Số tài khoản ngân hàng phải là số.")]
        /// <summary>
        /// số tài khoản ngân hàng
        /// </summary>
        public string? BankAccountNumber { get; set; }
       
        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string? BankBranchName { get; set; }
        
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }
       
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }
        
        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentitiDate { get; set; }
        
        /// <summary>
        /// phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }
    }
}
