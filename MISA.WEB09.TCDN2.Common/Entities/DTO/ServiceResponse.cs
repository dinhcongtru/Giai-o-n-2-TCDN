using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB09.TCDN2.Common.Entities.DTO
{
    public class ServiceResponse
    {
        #region Field
        /// <summary>
        /// validate dữ liệu
        /// </summary>
        public static int ValidateRequestData { get; set; }

        /// <summary>
        /// thành công trả về true hoặc false
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object? Data { get; set; }
        #endregion

        #region Constructor
        public ServiceResponse(bool success, object? data)
        {
            Success = success;
            Data = data;
        }

        #endregion
    }
}
