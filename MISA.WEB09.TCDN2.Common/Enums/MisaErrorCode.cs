using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB09.TCDN2.Common.Enums
{
    /// <summary>
    /// Enum sử dụng các lỗi xẩy ra khi gọi API
    /// </summary>
    public enum MisaErrorCode
    {
        /// <summary>
        /// lỗi gặp exception
        /// </summary>
        Exception = 1,

        /// <summary>
        /// lỗi trùng mã
        /// </summary>
        DuplicateCode = 2,

        /// <summary>
        /// lỗi dữ liệu đầu vào không hợp lệ
        /// </summary>
        InvalidData = 3,
    }
}
