using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB09.TCDN2.BL;
using MISA.WEB09.TCDN2.BL.BaseBL;
using MISA.WEB09.TCDN2.Common.Entities;
using MISA.WEB09.TCDN2.Common.Enums;
using MISA.WEB09.TCDN2.Common;
using Dapper;
using MISA.WEB09.TCDN2.Common.Entities.DTO;
using MySqlConnector;
using System.Data;
using System.ComponentModel;
using OfficeOpenXml;
using LicenseContext = System.ComponentModel.LicenseContext;

namespace MISA.WEB09.TCDN2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor 

        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }
        #endregion

        #region Base API
        /// <summary>
        /// API lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// CreatedBy : DCTRU (02/11/2022)
        [HttpGet]
        public IActionResult GetAllRecords()
        {
            try
            {
                //Kết quả trả về
                var res = _baseBL.GetAllRecords();

                //Xử lý kết quả trả về
                if (res != null)
                {
                    return StatusCode(StatusCodes.Status200OK, res);
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = MisaErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    MoreInfo = Resources.MoreInfo_Exception,
                    TraceID = HttpContext.TraceIdentifier
                });

            }
        }

        /// <summary>
        /// API lấy thông tin 1 bản ghi theo ID
        /// </summary>
        /// <returns>Thông tin bản ghi muốn lấy</returns>
        /// CreatedBy: DCTRU (02/11/2022)
        [HttpGet("{recordID}")]
        public IActionResult GetRecordByID([FromRoute] Guid recordID)
        {
            try
            {
                //Kết quả trả về
                ServiceResponse res = _baseBL.GetRecordByID(recordID);

                //Xử lý kết quả trả về
                if (res.Success == true)
                {
                    return StatusCode(StatusCodes.Status200OK, res.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = MisaErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    MoreInfo = Resources.MoreInfo_Exception,
                    TraceID = HttpContext.TraceIdentifier
                });
            }
        }
        /// <summary>
        /// API thêm mới 1 nhân viên
        /// </summary>
        /// <param name="record"> Đối tượng nhân viên cần thêm mới</param>
        /// <returns> ID của nhân viên vừa thêm mới</returns>
        /// CreatedBy: DCTRU (02/11/2022)
        [HttpPost]
        public IActionResult InsertRecord([FromBody] T record)
        {
            try
            {
                
                //Kết quả trả về
                ServiceResponse res = _baseBL.InsertRecord(record);

                //Xử lý kết quả  trả về
                if (res.Success == false)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult((string)res.Data, (string)res.Data));

                }
                else
                {
                    int numberOfRowsAffected = (int)res.Data;
                    if (numberOfRowsAffected > 0)
                    {
                        return StatusCode(StatusCodes.Status201Created, 1);
                    }

                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(HttpContext.TraceIdentifier));

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(HttpContext.TraceIdentifier));

            }
        }

        /// <summary>
        /// API sửa thông tin bản ghi theo ID
        /// </summary>
        /// <param name="employeeID"> ID nhân viên muốn sửa</param>
        /// <param name="employee">Đối tượng nhân viên muốn sửa</param>
        /// <returns>ID nhân viên vừa sửa</returns>
        /// CreatedBy: DCTRU (02/11/2022)
        [HttpPut("{recordID}")]
        public IActionResult UpdateRecord(
            [FromRoute] Guid recordID,
            [FromBody] T record)

        {
            try
            {
                //Kết quả trả về
                ServiceResponse res = _baseBL.UpdateRecord(recordID, record);

                //Xử lý kết quả trả 
                if (res.Success == false)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult((string)res.Data, (string)res.Data));

                }
                else
                {
                    int numberOfRowsAffected = (int)res.Data;
                    if (numberOfRowsAffected > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, 1);
                    }

                    return StatusCode(StatusCodes.Status204NoContent);



                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = MisaErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    MoreInfo = Resources.MoreInfo_Exception,
                    TraceID = HttpContext.TraceIdentifier
                });

            }
        }

        /// <summary>
        /// API xóa 1 bản ghi
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn xóa</param>
        /// <returns> ID bản ghi vừa xóa</returns>
        /// CreatedBy: DCTRU (02/11/2022)
        [HttpDelete("{recordID}")]
        public IActionResult DeleteRecord([FromRoute] Guid recordID)
        {
            try
            {
                //Kết quả trả về
                ServiceResponse res = _baseBL.DeleteRecordByID(recordID);

                //Xử lý kết quả trả về
                if (res.Success == true)
                {
                    return StatusCode(StatusCodes.Status200OK, res.Data);
                }

                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = MisaErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    MoreInfo = Resources.MoreInfo_Exception,
                    TraceID = HttpContext.TraceIdentifier
                });

            }
        }    
        #endregion
    }
}
