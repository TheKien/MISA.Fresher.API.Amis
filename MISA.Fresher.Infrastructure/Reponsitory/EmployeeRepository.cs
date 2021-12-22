using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Infrastructure.Reponsitory
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }
        /// <summary>
        /// Lấy về danh sách nhân viên (thêm tên phòng ban) theo thứ tự ngày tạo gần nhất
        /// </summary>
        /// <returns>Trả về danh sách nhân viên</returns>
        /// CreatedBy: TTKien(21/12/2021)
        public override IEnumerable<Employee> Get()
        {
            // Khởi tạo kêt nối với Datebase:
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                // Thực thi lấy dữ liệu trong Database:
                var sql = $"Proc_GetEmployees";
                var entites = mySqlConnection.Query<Employee>(sql, commandType:System.Data.CommandType.StoredProcedure);
                return entites;
            }
        }
    }
}
