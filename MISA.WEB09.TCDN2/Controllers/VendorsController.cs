using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB09.TCDN2.BL.BaseBL;
using MISA.WEB09.TCDN2.Common;
using MISA.WEB09.TCDN2.Common.Entities;
using MISA.WEB09.TCDN2.Common.Entities.DTO;
using MISA.WEB09.TCDN2.Common.Enums;
using MySqlConnector;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Data;
using MISA.WEB09.TCDN2.BL;
using System.Resources;

namespace MISA.WEB09.TCDN2.Controllers
{
    
    [ApiController]
    public class VendorsController : BasesController<Vendor>
    {
        #region field

        private IVendorBL _vendorBL;

        #endregion

        #region Constructor

        public VendorsController(IVendorBL vendorBL) : base(vendorBL)
        {
           
            _vendorBL = vendorBL;
        }   

        #endregion

        #region Method

        /// <summary>
        /// Lấy mã nhà cung cấp mới nhất
        /// </summary>
        /// <returns>mã nhà cung cấp mới nhất</returns>
        [HttpGet("newCode")]
        public IActionResult GetNewVendorCode()
        {

            try
            {
                // Khời tạo kết nối tới DB MySQL
                string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn2.dctru;Uid=root;Pwd=12345;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storedProcedureName = "Proc_vendor_GetNewCode";

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
        /// API lấy danh sách nhà cung cấp theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="offset">Vị trí của bản ghi bắt đầu lấy</param>
        /// <returns>Danh sách nhà cung cấp và tổng số bản ghi</returns>
        /// CreatedBy : DCTRU (02/11/2022)
        [HttpGet("filter")]
        public IActionResult GetVendorByFilterAndPaging(
            [FromQuery] string? keyword,
            [FromQuery] int PageSize = 25,
            [FromQuery] int PageNumber = 1)
        {
            try
            {
                // Khởi tạo kết nối tới DB MYSQL
                string ConnectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn2.dctru;Uid=root;Pwd=12345;";

                //Chuẩn bị câu lệnh sql
                string storeProcedureName = "Proc_vendor_GetPading";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();

                parameters.Add("v_Offset", (PageNumber - 1) * PageSize);
                parameters.Add("v_Limit", PageSize);
                parameters.Add("v_Soft", "ModifiedDate DESC");

                var lstCondition = new List<string>();
                string searchClause = "";
                if (keyword != null)
                {
                    lstCondition.Add($"VendorCode LIKE '%{keyword}%'");
                    lstCondition.Add($"VendorName LIKE '%{keyword}%'");
                    /*lstCondition.Add($"PhoneNumber LIKE '%{keyword}%'");*/
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
                        var listVendor = multipleResult.Read<Vendor>().ToList();
                        var totalCount = multipleResult.Read<long>().Single();
                        return StatusCode(StatusCodes.Status200OK, new PagingResultVendor()
                        {
                            Data = listVendor,
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
        /// API xóa nhiều nhà cung cấp theo danh sách ID
        /// </summary>
        /// <param name="listVendorID">danh sách ID của các nhà cung cấp muốn xóa</param>
        /// <returns>Status Code 200</returns>
        /// CreatedBy : DCTRU(02/11/2022)
        [HttpPost("deleteBatch")]
        public IActionResult DeleteMultipleVendor([FromBody] ListVendorIDs listVendorID)
        {
            // Khời tạo kết nối tới DB MySQL
            string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn2.dctru;Uid=root;Pwd=12345;";

            MySqlTransaction transaction = null;

            var ids = new List<Guid>();

            foreach (Guid id in listVendorID.VendorIDs)
            {
                ids.Add(id);
            }

            var str = string.Join("','", ids);

            //Chuẩn bị câu lệnh SQL
            string sql = $" DELETE FROM vendor WHERE VendorID IN ('{str}');";

            int numberOfRowsAffected = 0;
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                mySqlConnection.Open();
                try
                {
                    transaction = mySqlConnection.BeginTransaction();
                    //Thực hiện gọi vào DB
                    numberOfRowsAffected = mySqlConnection.Execute(sql, transaction: transaction);
                    if (numberOfRowsAffected == listVendorID.VendorIDs.Count)
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

            //Thành công: Trả về Id nhà cung cấp thêm thành công
            if (numberOfRowsAffected > 0)
            {
                return StatusCode(StatusCodes.Status200OK, listVendorID);
            }
            return null;
            //return StatusCode(StatusCodes.Status200OK);
        }


        /// API xuất ra file excel
        /// </summary>
        /// <returns></returns>
        [HttpPost("exportToExcel")]
        public IActionResult ExportToExcel()
        {
            try
            {
                // Lấy dữ liệu từ database
                var listVendor = _vendorBL.GetAllRecords();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                if (listVendor != null)
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
                        worksheet.Cells["A1"].Value = "DANH SÁCH NHÀ CUNG CẤP";
                        using (var r = worksheet.Cells["A1:AA1"])
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
                        worksheet.Column(2).Width = 20;
                        worksheet.Column(3).Width = 15;
                        worksheet.Column(4).Width = 20;
                        worksheet.Column(5).Width = 20;
                        worksheet.Column(6).Width = 30;
                        worksheet.Column(7).Width = 10;
                        worksheet.Column(8).Width = 15;
                        worksheet.Column(9).Width = 15;
                        worksheet.Column(10).Width = 10;
                        worksheet.Column(11).Width = 10;
                        worksheet.Column(12).Width = 15;
                        worksheet.Column(13).Width = 15;
                        worksheet.Column(14).Width = 15;
                        worksheet.Column(15).Width = 15;
                        worksheet.Column(16).Width = 15;
                        worksheet.Column(17).Width = 15;
                        worksheet.Column(18).Width = 15;
                        worksheet.Column(19).Width = 15;
                        worksheet.Column(20).Width = 15;
                        worksheet.Column(21).Width = 15;
                        worksheet.Column(22).Width = 30;
                        worksheet.Column(23).Width = 15;
                        worksheet.Column(24).Width = 15;
                        worksheet.Column(25).Width = 15;
                        worksheet.Column(26).Width = 15;
                        worksheet.Column(27).Width = 15;

                        // Gán tên header cho từng cột khi xuất ra file excel
                        worksheet.Cells["A3"].Value = "STT";
                        worksheet.Cells["B3"].Value = "Mã nhà cung cấp";
                        worksheet.Cells["C3"].Value = "Nhóm KH, NCC";
                        worksheet.Cells["D3"].Value = "Tên nhà cung cấp";
                        worksheet.Cells["E3"].Value = "Địa chỉ";
                        worksheet.Cells["F3"].Value = "Diễn giải";
                        worksheet.Cells["G3"].Value = "Công nợ";
                        worksheet.Cells["H3"].Value = "Mã số thuế";
                        worksheet.Cells["I3"].Value = "Điện thoại";
                        worksheet.Cells["J3"].Value = "Website";
                        worksheet.Cells["K3"].Value = "Số CMND";
                        worksheet.Cells["L3"].Value = "Ngày cấp";
                        worksheet.Cells["M3"].Value = "Nơi cấp";
                        worksheet.Cells["N3"].Value = "Điều khoản thanh toán";
                        worksheet.Cells["O3"].Value = "Số ngày được nợ";
                        worksheet.Cells["P3"].Value = "Số nợ tối đa";
                        worksheet.Cells["Q3"].Value = "Tài khoản công nợ phải trả";
                        worksheet.Cells["R3"].Value = "Nhân viên";
                        worksheet.Cells["S3"].Value = "Tên nhân viên";
                        worksheet.Cells["T3"].Value = "Tài khoản ngân hàng";
                        worksheet.Cells["U3"].Value = "Tên ngân hàng";
                        worksheet.Cells["V3"].Value = "Chi nhánh tài khoản ngân hàng";
                        worksheet.Cells["W3"].Value = "Xưng hô";
                        worksheet.Cells["X3"].Value = "Người liên hệ";
                        worksheet.Cells["Y3"].Value = "Số điện thoại NLH";
                        worksheet.Cells["Z3"].Value = "Email NLH";
                        worksheet.Cells["AA3"].Value = "Người đại diện PL";


                        var modelRows = listVendor.Count() + 3;
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
                        using (var range = worksheet.Cells["A3:AA3"])
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
                        foreach (var vendor in listVendor)
                        {
                            worksheet.Cells[row, 1].Value = stt;
                            worksheet.Cells[row, 2].Value = vendor.VendorCode;
                            worksheet.Cells[row, 3].Value = vendor.GroupVendorCode;
                            worksheet.Cells[row, 4].Value = vendor.VendorName;
                            worksheet.Cells[row, 5].Value = vendor.Address;
                            worksheet.Cells[row, 6].Value = vendor.Description;
                            worksheet.Cells[row, 7].Value = vendor.Debt;
                            worksheet.Cells[row, 8].Value = vendor.TaxCode;
                            worksheet.Cells[row, 9].Value = vendor.PhoneNumber;
                            worksheet.Cells[row, 10].Value = vendor.Website;
                            worksheet.Cells[row, 11].Value = vendor.IdentityNumber;
                            worksheet.Cells[row, 12].Value = vendor.IdentityDate;
                            worksheet.Cells[row, 13].Value = vendor.IdentityPlace;
                            worksheet.Cells[row, 14].Value = vendor.Rule;
                            worksheet.Cells[row, 15].Value = vendor.DayOwed;
                            worksheet.Cells[row, 16].Value = vendor.MaximumDebt;
                            worksheet.Cells[row, 17].Value = vendor.AcountPayable;
                            worksheet.Cells[row, 18].Value = vendor.EmployeeCode;
                            worksheet.Cells[row, 19].Value = vendor.EmployeeName;
                            worksheet.Cells[row, 20].Value = vendor.BankAccountNumber;
                            worksheet.Cells[row, 21].Value = vendor.BankName;
                            worksheet.Cells[row, 22].Value = vendor.BankBranchName;
                            worksheet.Cells[row, 23].Value = vendor.Vocative;
                            worksheet.Cells[row, 24].Value = vendor.Contact;
                            worksheet.Cells[row, 25].Value = vendor.ContactPhone;
                            worksheet.Cells[row, 26].Value = vendor.ContactEmail;
                            worksheet.Cells[row, 27].Value = vendor.AgentLaw;

                            row++;
                            stt++;
                        }
                        // Lưu spreadsheet mới
                        xlPackage.Save();
                    }

                    stream.Position = 0;
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "nhacungcap.xlsx");
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

        #endregion
    }
}
