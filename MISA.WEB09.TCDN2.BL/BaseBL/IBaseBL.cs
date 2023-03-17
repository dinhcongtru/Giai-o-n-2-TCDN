using MISA.WEB09.TCDN2.Common.Entities;
using MISA.WEB09.TCDN2.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB09.TCDN2.BL.BaseBL
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// Lấy tất cả danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// CreatedBy : DCTRU (10/11/2022)
        public IEnumerable<T> GetAllRecords();

        /// <summary>
        /// Lấy thông tin của 1 bản ghi
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn lấy</param>
        /// <returns>Thông tin của 1 bản ghi theo ID</returns>
        /// CreatedBy : DCTRU (10/11/2022)
        public ServiceResponse GetRecordByID(Guid recordID);

        /// <summary>
        /// Xóa thông tin 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi </param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy : DCTRU (10/11/2022)
        public ServiceResponse DeleteRecordByID(Guid recordID);

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="record"></param>Thông tin bản ghi muốn thêm
        /// <returns>Trả về lỗi nếu validate false hoặc trả về số bản ghi bị ảnh hưởng trong database</returns>
        /// CreatedBy : DCTRU (10/11/2022)
        public ServiceResponse InsertRecord(T record);

        /// <summary>
        /// Sửa thông thin một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param> ID của bản ghi
        /// <param name="record"></param> Thông tin bản ghi
        /// <returns>Trả về lỗi nếu validate false hoặc trả về số bản ghi bị ảnh hưởng trong database</returns>
        /// CreatedBy : DCTRU (10/11/2022)
        public ServiceResponse UpdateRecord(Guid recordID, T record);

        /// <summary>
        /// lấy danh sách bản ghi theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="offset">Vị trí của bản ghi bắt đầu lấy</param>
        /// <returns>Danh sách bản ghi và phân trang
        //public IEnumerable<T> GetRecordByFilterAndPaging(string? keyword, int PageSize = 25, int PageNumber = 1);
    }
}
