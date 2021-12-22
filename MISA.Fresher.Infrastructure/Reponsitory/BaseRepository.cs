using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.Fresher.Core.MISAAttribute.MISAAttribute;

namespace MISA.Fresher.Infrastructure.Reponsitory
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {
        protected string _connectionString = string.Empty;
        string _className;
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Amis");
            _className = typeof(MISAEntity).Name;
        }

        public virtual IEnumerable<MISAEntity> Get()
        {
            // Khởi tạo kêt nối với Datebase:
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                // Thực thi lấy dữ liệu trong Database:
                var sql = $"SELECT * FROM {_className} ORDER BY CreatedDate DESC";
                var entites = mySqlConnection.Query<MISAEntity>(sql);
                return entites;
            }
        }

        public int Insert(MISAEntity entity)
        {
            // Khởi tạo kêt nối với Datebase:
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                // Thực thi lấy dữ liệu trong Database:
                DynamicParameters parameters = new DynamicParameters();
                var columns = "";
                var columnParams = "";
                // Lấy ra các giá trị của property tương ứng với đối tượng:
                var props = typeof(MISAEntity).GetProperties();
                // Duyệt từng property
                foreach (var prop in props)
                {
                    // Lấy tên gốc của property:
                    var propNameOrginal = prop.Name;
                    // Gắn tên hiển thị bằng tên gốc: 
                    var propNameDisplay = propNameOrginal;
                    // Lấy giá trị tương ứng với đối tượng:
                    var propValue = prop.GetValue(entity);
                    // Lấy giá trị tương ứng với đối tượng:
                    var propType = prop.PropertyType;
                    // Lấy các property chỉ để hiển thị:
                    var propReadOnlys = prop.GetCustomAttributes(typeof(ReadOnly), true);
                    // Lấy các property không được trùng nhau:
                    var propUniques = prop.GetCustomAttributes(typeof(Unique), true);
                    // Lấy về tất cả property có tên tự định nghĩa:
                    var propPropertyNames = prop.GetCustomAttributes(typeof(PropertyName), true);
                    // Nếu property có tên tự định nghĩa -> lấy tên đặt để hiển thị thông báo cho người dùng khi xảy ra lỗi:
                    if (propPropertyNames.Length > 0)
                    {
                        // Ghi đè tên hiển thị bằng tên tự định nghĩa:
                        propNameDisplay = (propPropertyNames[0] as PropertyName).Name;
                    }
                    // Nếu có property không được trùng nhau -> kiểm tra cơ sở dữ liệu, nếu trùng trả về một Exception thông báo cho người dùng:
                    if (propUniques.Length > 0)
                    {
                        // Kiểm tra csdl:
                        var sqlIsUnique = $"SELECT * FROM {_className} WHERE {propNameOrginal} = '{propValue}'";
                        var entityIsUnique = mySqlConnection.Query(sqlIsUnique);
                        // Nếu giá trị đã tồn tại trong csdl:
                        if (entityIsUnique != null)
                        {
                            throw new Exception($"{propNameDisplay} <{propValue}> đã tồn tại");
                        }
                    }
                    // Nếu property chỉ để hiển thị -> bỏ qua property khi thêm mới:
                    if (propReadOnlys.Length > 0)
                    {
                        continue;
                    }
                    // Nếu property là id thì thêm mã guid:
                    if (propNameOrginal == $"{_className}Id" && prop.PropertyType == typeof(Guid))
                    {
                        propValue = Guid.NewGuid();
                    }
                    // Nếu property là created date thì thêm ngày hiện tại:
                    if (propNameOrginal == $"CreatedDate" && prop.PropertyType == typeof(DateTime))
                    {
                        propValue = DateTime.Now;
                    }

                    // Cập nhật chuỗi lệnh thêm mới và thêm các tham số tương ứng:
                    columns += $"{propNameOrginal},";
                    columnParams += $"@{propNameOrginal},";
                    parameters.Add($"@{propNameOrginal}", propValue);
                }
                columns = columns.Substring(0, columns.Length - 1);
                columnParams = columnParams.Substring(0, columnParams.Length - 1);
                var sql = $"INSERT INTO {_className}({columns}) VALUES ({ columnParams })";
                var rowEffects = mySqlConnection.Execute(sql, param: parameters);
                return rowEffects;
            }
        }
        public int Update(MISAEntity entity, Guid entityId)
        {
            throw new NotImplementedException();
        }
    }
}
