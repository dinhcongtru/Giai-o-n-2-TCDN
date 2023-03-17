using Dapper;
using MISA.WEB09.TCDN2.Common.Constants;
using MISA.WEB09.TCDN2.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB09.TCDN2.DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        /// <summary>
        /// Hàm mở kết nối vào database
        /// Created by dctru 25/11/2022
        /// </summary>
        /// <returns></returns>
        public MySqlConnection OpenConnection()
        {
            string connectionString = DataBaseContext.ConnectionString;
            return new MySqlConnection(connectionString);
        }

        #region Methods
        /// <summary>
        /// Lấy tất cả danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// CreatedBy : DCTRU (10/11/2022)
        public IEnumerable<T> GetAllRecords()
        {
            string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn2.dctru;Uid=root;Pwd=12345;";
            // Chuẩn bị câu lệnh SQL
            string storedProcedureName = String.Format(Procedure.GET_ALL, typeof(T).Name);

            // Thực hiện gọi vào DB
            var records = new List<T>();

            //Khởi tạo muộn nhất có thể
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                records = (List<T>)mySqlConnection.Query<T>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);
            }

            return records;
        }

        /// <summary>
        /// Lấy thông tin của 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn lấy</param>
        /// <returns>Thông tin của 1 bản ghi theo ID</returns>
        /// CreatedBy : DCTRU (10/11/2022)
        public T GetRecordByID(Guid recordID)
        {
            string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn2.dctru;Uid=root;Pwd=12345;";

            // Chuẩn bị câu lệnh SQL 
            string storedProcedureName = String.Format(Procedure.GET_BY_ID, typeof(T).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}ID", recordID);

            // Thực hiện gọi vào DB
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                var record = mySqlConnection.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return record;
            }

        }

        /// <summary>
        /// Xóa thông tin 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi </param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy : DCTRU(02/11/2022)
        public int DeleteRecordByID(Guid recordID)
        {
            string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn2.dctru;Uid=root;Pwd=12345;";

            //khai báo tên procedure
            string storedProcedureName = String.Format(Procedure.DELETE_BY_ID, typeof(T).Name);

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}ID", recordID);

            // Khoởi tạo kết nối tới DB MySQL(Muộn nhất có thể)
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                //Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                //Xử lý kết quả trả về

                return numberOfRowsAffected;

            }
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="record"></param>Thông tin bản ghi muốn thêm
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy : DCTRU(10/11/2022)
        public int InsertRecord(T record)
        {

            //Khai báo tên procedure
            string storedProcedureName = String.Format(Procedure.INSERT, typeof(T).Name);

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();

            //Lấy ra các properties của class được truyền vào
            var properties = typeof(T).GetProperties();

            var newRecordID = Guid.NewGuid();
            object propValue;

            foreach (var prop in properties)
            {
                var propName = prop.Name;

                // Kiểm tra xem có phải ID không
                var isPrimaryKeyAttribute = Attribute.IsDefined(prop, typeof(KeyAttribute));

                if (isPrimaryKeyAttribute == true)
                {
                    propValue = newRecordID;
                }
                else
                {
                    propValue = prop.GetValue(record);
                }
                parameters.Add($"@{prop.Name}", propValue);
            }
            string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn2.dctru;Uid=root;Pwd=12345;";
            // Khoởi tạo kết nối tới DB MySQL (Muộn nhất có thể)
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                //Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return numberOfRowsAffected;
            }
        }

        /// <summary>
        /// Sửa thông thin một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param> ID của bản ghi
        /// <param name="record"></param> Thông tin bản ghi
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy : DCTRU(10/11/2022)
        public int UpdateRecord(Guid recordID, T record)
        {
            string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn2.dctru;Uid=root;Pwd=12345;";

            //Khai báo tên procedure
            string storedProcedureName = String.Format(Procedure.UPDATE, typeof(T).Name);

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();

            object propValue;

            foreach (var prop in properties)
            {
                // Kiểm tra xem có phải ID không
                var isPrimaryKeyAttribute = Attribute.IsDefined(prop, typeof(KeyAttribute));

                if (isPrimaryKeyAttribute == true)
                {
                    propValue = recordID;
                }
                else
                {
                    propValue = prop.GetValue(record);
                }
                parameters.Add($"@{prop.Name}", propValue);
            }

            // Khoởi tạo kết nối tới DB MySQL (Muộn nhất có thể)
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                //Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return numberOfRowsAffected;
            }
        }

        /// <summary>
        ///Check trùng mã bản ghi khi thêm mới
        /// </summary>
        /// <param name="recordCode"></param>Mã bản ghi muốn kiểm tra
        /// <returns>trả về 0 nếu trùng,1 nếu không trùng</returns>
        /// CreatedBy : DCTRU(10/11/2022)
        public int CheckDuplicateCodeInsert(string recordCode)
        {
            string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn2.dctru;Uid=root;Pwd=12345;";

            //khai báo tên procedure
            string storedProcedureName = String.Format(Procedure.INSERT_CHECK_DAPLICATE, typeof(T).Name);

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}Code", recordCode);
            parameters.Add("isDuplicate", dbType: DbType.String, direction: ParameterDirection.Output);

            // Khoởi tạo kết nối tới DB MySQL (Muộn nhất có thể)
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                //Thực hiện gọi vào DB
                mySqlConnection.QueryFirstOrDefault(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                //Xử lý kết quả trả về
                int isDuplicateCode = parameters.Get<int>("isDuplicate");

                return isDuplicateCode;
            }
        }

        /// <summary>
        ///Check trùng mã bản ghi khi sửa bản ghi
        /// </summary>
        /// <param name="recordID"></param>ID bản ghi muốn kiểm tra trùng mã
        /// <param name="recordCode"></param>Mã bản ghi muốn kiểm tra
        /// <returns>trả về 0 nếu trùng,1 nếu không trùng</returns>
        /// CreatedBy : DCTRU(10/11/2022)
        public int CheckDuplicateCodeUpdate(Guid recordID, string recordCode)
        {
            string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn2.dctru;Uid=root;Pwd=12345;";

            //khai báo tên procedure
            string storedProcedureName = String.Format(Procedure.UPDATE_CHECK_DAPLICATE, typeof(T).Name);

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}ID", recordID);
            parameters.Add($"@{typeof(T).Name}Code", recordCode);
            parameters.Add("isDuplicate", dbType: DbType.String, direction: ParameterDirection.Output);
            // Khoởi tạo kết nối tới DB MySQL (Muộn nhất có thể)
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                //Thực hiện gọi vào DB
                mySqlConnection.QueryFirstOrDefault(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                //Xử lý kết quả trả về
                int isDuplicateCode = parameters.Get<int>("isDuplicate");

                return isDuplicateCode;
            }
        }

       

        #endregion
    }
}
