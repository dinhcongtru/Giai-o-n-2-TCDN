using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB09.TCDN2.Common.Entities.DTO
{
    public class PagingResultVendor
    {
        /// <summary>
        /// Danh sách nhà cung cấp
        /// </summary>
        public List<Vendor> Data { get; set; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public long TotalCount { get; set; }
    }
}
