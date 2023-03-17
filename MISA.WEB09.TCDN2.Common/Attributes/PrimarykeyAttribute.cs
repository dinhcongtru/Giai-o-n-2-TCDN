using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB09.TCDN2.Common
{
    /// <summary>
    /// Attribute kiểm tra xem có null hoặc rỗng hay không
    /// </summary>
    /// Author:DCTRU (18/11/2022)
    public class IsNotNullOrEmptyAttribute : Attribute
    {

        #region Field
        /// <summary>
        /// Thông tin lỗi
        /// </summary>
        public string ErrorMessage { get; set; }
        #endregion

        #region Constructor
        public IsNotNullOrEmptyAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        #endregion
    }

    /// <summary>
    /// Atribute kiếm tra các trường nhập số
    /// </summary>
    /// Author:DCTRU (18/11/2022)
    public class IsNumberAttribute : Attribute
    {
        #region Field
        /// <summary>
        /// Format dữ liệu
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Thông tin lỗi
        /// </summary>
        public string ErrorMessage { get; set; }
        #endregion

        #region Constructor
        public IsNumberAttribute(string format, string errorMessage)
        {
            Format = format;
            ErrorMessage = errorMessage;
        }
        #endregion
    }
    /// <summary>
    /// Atrribute kiểm tra email có đúng định dạng không
    /// </summary>
    /// Author:DCTRU (18/11/2022)
    public class ValidateEmailAttribute : Attribute
    {
        #region Field
        /// <summary>
        /// Format dữ liệu
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Thông tin lỗi
        /// </summary>
        public string ErrorMessage { get; set; }
        #endregion

        #region Constructor
        public ValidateEmailAttribute(string format, string errorMessage)
        {
            Format = format;
            ErrorMessage = errorMessage;
        }
        #endregion
    }

    /// <summary>
    /// Atribute không được phép trùng dữ liệu
    /// </summary>
    /// Author:DCTRU (18/11/2022)
    public class UniqueAttribute : Attribute
    {
        #region Field
        /// <summary>
        /// Thông tin lỗi
        /// </summary>
        public string ErrorMessage { get; set; }
        #endregion


        #region Constructor
        public UniqueAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        #endregion
    }
}
