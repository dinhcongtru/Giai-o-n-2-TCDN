using MISA.WEB09.TCDN2.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB09.TCDN2.Common.Entities.DTO
{
    /// <summary>
    /// Kết quả trả về khi gọi API gặp lỗi
    /// </summary>
    public class ErrorResult
    {
        #region Field
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public MisaErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Message trả về cho dev 
        /// </summary>
        public string DevMsg { get; set; }


        /// <summary>
        /// Message trả về cho người dùng 
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin chi tiết về lỗi
        /// </summary>
        public string MoreInfo { get; set; }

        /// <summary>
        /// ID dùng để trace lỗi khi log lại
        /// </summary>
        public string TraceId { get; set; }
        #endregion
        #region Constructor
        public ErrorResult(string traceId)
        {
            ErrorCode = MisaErrorCode.Exception;
            DevMsg = Resources.DevMsg_Exception;
            UserMsg = Resources.UserMsg_Exception;
            MoreInfo = Resources.MoreInfo_Exception;
            TraceId = traceId;
        }

        public ErrorResult(string devMsg, string userMsg)

        {
            ErrorCode = MisaErrorCode.InvalidData;
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = "";
            TraceId = "";
        }


        #endregion
    }
}
