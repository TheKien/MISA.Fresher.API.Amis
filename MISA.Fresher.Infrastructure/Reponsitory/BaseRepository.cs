using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static MISA.Fresher.Core.Attributes.MISAAttribute;

namespace MISA.Fresher.Infrastructure.Reponsitory
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// chuỗi kết nối với cơ sở dữ liệu
        /// </summary>
        protected string _connectionString = string.Empty;
        protected IDbConnection _dbConnection = null;
        /// <summary>
        /// Tên entity
        /// </summary>
        string _className;

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Amis");
            _dbConnection = new MySqlConnection(_connectionString);
            _className = typeof(MISAEntity).Name;
        }

        public virtual IEnumerable<MISAEntity> Get()
        {
            // Khởi tạo kêt nối với datebase:
            _dbConnection.Open();

            var sql = $"SELECT * FROM { _className } ORDER BY CreatedDate DESC";
            var entites = _dbConnection.Query<MISAEntity>(sql);

            //Đóng kết nối:
            Dispose();

            return entites;
        }

        public MISAEntity GetById(Guid entityId)
        {
            // Khởi tạo kết nối với database:
            _dbConnection.Open();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{_className}Id", entityId);
            var sql = $"SELECT * FROM { _className } WHERE { _className }Id = @{ _className }Id";
            var entity = _dbConnection.QueryFirstOrDefault<MISAEntity>(sql, parameters);

            //Đóng kết nối:
            Dispose();
            return entity;

        }

        public int Insert(MISAEntity entity)
        {
            // Khởi tạo kết nối với database:
            _dbConnection.Open();

            var rowEffects = 0;
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
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
                        // Lấy tên của property:
                        var propName = prop.Name;
                        // Lấy giá trị tương ứng với đối tượng:
                        var propValue = prop.GetValue(entity);
                        // Lấy kiểu giá trị của property:
                        var propType = prop.PropertyType;
                        // Nếu property chỉ để hiển thị -> bỏ qua property khi thêm mới:
                        if (prop.IsDefined(typeof(ReadOnly), true))
                        {
                            continue;
                        }
                        // Nếu property là id thì thêm mã guid:
                        if (propName == $"{_className}Id" && propType == typeof(Guid))
                        {
                            propValue = Guid.NewGuid();
                        }
                        // Nếu property là created date thì thêm ngày hiện tại:
                        if ((propName == "CreatedDate" || propName == "ModifiedDate") && propType == typeof(DateTime))
                        {
                            propValue = DateTime.Now;
                        }
                        // Cập nhật chuỗi lệnh thêm mới và thêm các tham số tương ứng:
                        columns += $"{propName},";
                        columnParams += $"@{propName},";
                        parameters.Add($"@{propName}", propValue);
                    }
                    columns = columns.Substring(0, columns.Length - 1);
                        columnParams = columnParams.Substring(0, columnParams.Length - 1);
                    var sql = $"INSERT INTO { _className }({ columns }) VALUES ({ columnParams })";
                    rowEffects = _dbConnection.Execute(sql, param: parameters, transaction: transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            //Đóng kết nối:
            Dispose();

            return rowEffects;

        }

        public int Update(MISAEntity entity, Guid entityId)
        {
            // Khởi tạo kết nối với database:
            _dbConnection.Open();

            var rowEffects = 0;
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    // Thực thi lấy dữ liệu trong Database:
                    DynamicParameters parameters = new DynamicParameters();
                    var propertys = "";
                    // Lấy ra các giá trị của property tương ứng với đối tượng:
                    var props = typeof(MISAEntity).GetProperties();
                    // Duyệt từng property
                    foreach (var prop in props)
                    {
                        // Lấy tên của property:
                        var propName = prop.Name;
                        // Lấy giá trị tương ứng với đối tượng:
                        var propValue = prop.GetValue(entity);
                        // Lấy kiểu giá trị của property:
                        var propType = prop.PropertyType;
                        // Nếu property chỉ để hiển thị hoặc không được update -> bỏ qua khi update:
                        if (prop.IsDefined(typeof(ReadOnly), true) || prop.IsDefined(typeof(NotUpdated), true))
                        {
                            continue;
                        }
                        // Cập nhật thời gian sửa bằng ngày hiện tại:
                        if (propName == "ModifiedDate" && propType == typeof(DateTime))
                        {
                            propValue = DateTime.Now;
                        }
                        propertys += $"{ propName } = @{ propName },";
                        parameters.Add($"@{propName}", propValue);
                    }
                    propertys = propertys.Substring(0, propertys.Length - 1);
                    var sql = $"UPDATE { _className }  SET { propertys } WHERE { _className }Id = '{ entityId }'";
                    rowEffects = _dbConnection.Execute(sql, param: parameters, transaction: transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            //Đóng kết nối:
            Dispose();

            return rowEffects;
        }

        public int Delete(string entityId)
        {
            // Khởi tạo kết nối với database:
            _dbConnection.Open();

            var rowEffects = 0;
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add($"@{_className}Id", entityId);
                    var sql = $"DELETE FROM { _className } WHERE { _className }Id = @{ _className }Id";
                    rowEffects = _dbConnection.Execute(sql, parameters, transaction: transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            //Đóng kết nối
            Dispose();

            return rowEffects;
        }

        public bool IsExist(MISAEntity entity, PropertyInfo property)
        {
            // Khởi tạo kết nối với database:
            _dbConnection.Open();

            // lấy tên property:
            var propertyName = property.Name;
            // lấy giá trị property
            var propertyValue = property.GetValue(entity);

            var param = new DynamicParameters();
            param.Add($"@{propertyName}", propertyValue);
            var sql = $"SELECT * FROM { _className } WHERE { propertyName } = '{ propertyValue }'";
            var entityExist = _dbConnection.Query(sql, param: param).FirstOrDefault();
            if (entityExist != null)
            {
                //Đóng kết nối
                Dispose();
                return true;
            }

            //Đóng kết nối
            Dispose();

            return false;

        }


        /// <summary>
        /// Hàm tắt kết nối với DB khi để tiết kiệm tại tài nguyên
        /// </summary>
        /// createdBy: TTKien(24/12/2021)
        protected void Dispose()
        {
            if (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
        }
    }
}
