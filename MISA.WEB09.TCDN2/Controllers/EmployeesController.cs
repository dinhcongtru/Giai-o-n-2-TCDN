using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MISA.WEB09.TCDN2.BL;
using MISA.WEB09.TCDN2.Common;
using MISA.WEB09.TCDN2.Common.Entities;
using MISA.WEB09.TCDN2.Common.Entities.DTO;
using MISA.WEB09.TCDN2.Common.Enums;
using MISA.WEB09.TCDN2.DL;
using MySqlConnector;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System;

namespace MISA.WEB09.TCDN2.Controllers
{
    [ApiController]
    public class EmployeesController : BasesController<Employee>
    {
        #region Field

        private IEmployeeBL _employeeBL;

        #endregion

        #region Constructor 

        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #endregion

        #region Method
        /// <summary>
        /// API xuất ra file excel
        /// </summary>
        /// <returns></returns>
        [HttpPost("exportExcel")]
        public IActionResult ExportExcel()
        {
            try
            {
                // Lấy dữ liệu từ database
                var listEmployee = _employeeBL.GetAllRecords();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                if (listEmployee != null)
                {
                    // Khởi tạo vùng nhớ
                    var stream = new MemoryStream();
                    using (var xlPackage = new ExcelPackage(stream))
                    {
                        // Tạo và đặt tên cho sheet
                        var worksheet = xlPackage.Workbook.Worksheets.Add("Sheet1");
                        var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                        namedStyle.Style.Font.UnderLine = true;
                        const int startRow = 4;
                        var row = startRow;
                        var stt = 1;

                        // Khởi tạo title
                        worksheet.Cells["A1"].Value = "DANH SÁCH NHÂN VIÊN";
                        using (var r = worksheet.Cells["A1:J1"])
                        {
                            // Merge hàng A1:I1
                            r.Merge = true;
                            // Cho chữ căn giữa
                            r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                            // Set phông chữ
                            r.Style.Font.SetFromFont("Arial", 16, true);

                        }

                        // Set chiều rộng cột excel
                        worksheet.Column(1).Width = 8;
                        worksheet.Column(2).Width = 15;
                        worksheet.Column(3).Width = 30;
                        worksheet.Column(4).Width = 10;
                        worksheet.Column(5).Width = 15;
                        worksheet.Column(6).Width = 30;
                        worksheet.Column(7).Width = 30;
                        worksheet.Column(8).Width = 15;
                        worksheet.Column(9).Width = 30;
                        worksheet.Column(10).Width = 30;

                        // Gán tên header cho từng cột khi xuất ra file excel
                        worksheet.Cells["A3"].Value = "STT";
                        worksheet.Cells["B3"].Value = "Mã nhân viên";
                        worksheet.Cells["C3"].Value = "Tên nhân viên";
                        worksheet.Cells["D3"].Value = "Giới tính";
                        worksheet.Cells["E3"].Value = "Ngày sinh";
                        worksheet.Cells["F3"].Value = "Chức danh";
                        worksheet.Cells["G3"].Value = "Tên đơn vị";
                        worksheet.Cells["H3"].Value = "Số tài khoản";
                        worksheet.Cells["I3"].Value = "Tên ngân hàng";
                        worksheet.Cells["J3"].Value = "Chi nhánh tài khoản ngân hàng";


                        var modelRows = listEmployee.Count() + 3;
                        string modelRange = "A4:I" + modelRows.ToString();
                        using (var range = worksheet.Cells[modelRange])
                        {
                            // Set phông chữ
                            range.Style.Font.SetFromFont("Arial", 10, false);

                            // Set Border
                            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        }

                        // Lấy range vào tạo format cho range đó ở đây là từ A3 tới I3
                        using (var range = worksheet.Cells["A3:J3"])
                        {
                            range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                            // Set PatternType
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                            // Set Font cho text  trong Range hiện tại
                            range.Style.Font.SetFromFont("Arial", 10, true);
                            // Set Border
                            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        }

                        // Truyền dữ liệu vào excel
                        row = 4;
                        foreach (var employee in listEmployee)
                        {
                            worksheet.Cells[row, 1].Value = stt;
                            worksheet.Cells[row, 2].Value = employee.EmployeeCode;
                            worksheet.Cells[row, 3].Value = employee.EmployeeName;
                            worksheet.Cells[row, 4].Value = employee.Gender == Gender.Male ? "Nam" : employee.Gender == Gender.Female ? "Nữ" : employee.Gender == Gender.Other ? "Khác" : "";
                            worksheet.Cells[row, 5].Value = employee.DateOfBirth;
                            worksheet.Cells[row, 5].Style.Numberformat.Format = "dd/mm/yyyy";
                            worksheet.Cells[row, 6].Value = employee.JobPositionName;
                            worksheet.Cells[row, 7].Value = employee.DepartmentName;
                            worksheet.Cells[row, 8].Value = employee.BankAccountNumber;
                            worksheet.Cells[row, 9].Value = employee.BankName;
                            worksheet.Cells[row, 10].Value = employee.BankBranchName;

                            row++;
                            stt++;
                        }
                        // Lưu spreadsheet mới
                        xlPackage.Save();
                    }

                    stream.Position = 0;
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
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
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API lấy danh sách nhân viên theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="departmentID">ID của phòng ban muốn lọc</param>
        /// <param name="PositionID">ID của vị trí muốn lọc</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="offset">Vị trí của bản ghi bắt đầu lấy</param>
        /// <returns>Danh sách nhân viên và tổng số bản ghi</returns>
        /// CreatedBy : DCTRU (02/11/2022)
        [HttpGet("filter")]
        public IActionResult GetEmployeeByFilterAndPaging(
            [FromQuery] string? keyword,
            [FromQuery] int PageSize = 25,
            [FromQuery] int PageNumber = 1)
        {
            try
            {
                // Khởi tạo kết nối tới DB MYSQL
                string ConnectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn2.dctru;Uid=root;Pwd=12345;";

                //Chuẩn bị câu lệnh sql
                string storeProcedureName = "Proc_employee_GetPading";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();

                parameters.Add("v_Offset", (PageNumber - 1) * PageSize);
                parameters.Add("v_limit", PageSize);
                parameters.Add("v_Soft", "ModifiedDate DESC");

                var lstCondition = new List<string>();
                string searchClause = "";
                if (keyword != null)
                {
                    lstCondition.Add($"EmployeeCode LIKE '%{keyword}%'");
                    lstCondition.Add($"EmployeeName LIKE '%{keyword}%'");
                    lstCondition.Add($"PhoneNumber LIKE '%{keyword}%'");
                }
                if (lstCondition.Count > 0)
                {
                    searchClause = string.Join(" OR ", lstCondition);
                }
                parameters.Add("v_Where", searchClause);

                using (var mysqlConnection = new MySqlConnection(ConnectionString))
                {
                    var multipleResult = mysqlConnection.QueryMultiple(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                    if (multipleResult != null)
                    {
                        var listEmployee = multipleResult.Read<Employee>().ToList();
                        var totalCount = multipleResult.Read<long>().Single();
                        return StatusCode(StatusCodes.Status200OK, new PagingResult()
                        {
                            Data = listEmployee,
                            TotalCount = totalCount,
                        });
                    }
                }
                return StatusCode(StatusCodes.Status400BadRequest, new PagingResult());
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
        /// Lấy mã nhân viên mới nhất
        /// </summary>
        /// <returns>mã nhân viên mới nhất</returns>
        [HttpGet("newCode")]
        public IActionResult GetNewEmployeeCode()
        {

            try
            {
                // Khời tạo kết nối tới DB MySQL
                string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn2.dctru;Uid=root;Pwd=12345;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storedProcedureName = "Proc_emloyee_GetNewCode";

                // Thực hiện gọi vào DB
                var newCode = mySqlConnection.QueryFirstOrDefault(storedProcedureName, commandType: CommandType.StoredProcedure);

                // Xử lý kết quả trả về 
                if (newCode != null)
                {
                    return StatusCode(StatusCodes.Status200OK, newCode);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = MisaErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    MoreInfo = Resources.MoreInfo_Exception,
                    TraceID = HttpContext.TraceIdentifier
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
        /// API xóa nhiều nhân viên theo danh sách ID
        /// </summary>
        /// <param name="listEmployeeID">danh sách ID của các nhân viên muốn xóa</param>
        /// <returns>Status Code 200</returns>
        /// CreatedBy : DCTRU(02/11/2022)
        [HttpPost("deleteBatch")]
        public IActionResult DeleteMultipleEmployee([FromBody] ListEmployeeID listEmployeeID)
        {
            // Khời tạo kết nối tới DB MySQL
            string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn2.dctru;Uid=root;Pwd=12345;";

            MySqlTransaction transaction = null;

            var ids = new List<Guid>();

            foreach (Guid id in listEmployeeID.EmployeeIDs)
            {
                ids.Add(id);
            }

            var str = string.Join("','", ids);

            //Chuẩn bị câu lệnh SQL
            string sql = $" DELETE FROM employee WHERE EmployeeID IN ('{str}');";

            int numberOfRowsAffected = 0;
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                mySqlConnection.Open();
                try
                {
                    transaction = mySqlConnection.BeginTransaction();
                    //Thực hiện gọi vào DB
                    numberOfRowsAffected = mySqlConnection.Execute(sql, transaction: transaction);
                    if (numberOfRowsAffected == listEmployeeID.EmployeeIDs.Count)
                    {
                        transaction.Commit();

                    }
                    else
                    {

                        transaction.Rollback();
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
                finally
                {
                    mySqlConnection.Close();
                }
            }
            //Xử lý kết quả trả về

            //Thành công: Trả về Id nhân viên thêm thành công
            if (numberOfRowsAffected > 0)
            {
                return StatusCode(StatusCodes.Status200OK, listEmployeeID);
            }
            return null;
            //return StatusCode(StatusCodes.Status200OK);
        }
        #endregion
    }
}
