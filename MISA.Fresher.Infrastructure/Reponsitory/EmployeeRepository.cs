using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Properties;
using MySqlConnector;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.Fresher.Core.Attributes.MISAAttribute;

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
            // Khởi tạo kêt nối với datebase:
            _dbConnection.Open();

            // Thực thi lấy dữ liệu trong database:
            var sql = $"Proc_GetEmployees";
            var entites = _dbConnection.Query<Employee>(sql, commandType: System.Data.CommandType.StoredProcedure);

            // Đóng kết nối:
            Dispose();

            return entites;

        }

        public string GetNewEmployeeCode()
        {
            // Khởi tạo kêt nối với datebase:
            _dbConnection.Open();

            var sql = $"Proc_GetNewEmployeeCode";
            var newEmployeeCode = _dbConnection.QueryFirstOrDefault<string>(sql, commandType: System.Data.CommandType.StoredProcedure);
            // Nếu chưa có bản ghi nào thì mã nhân viên mới = 'NV-1'
            if (newEmployeeCode == null)
            {
                newEmployeeCode = "NV-1";
            }

            // Đóng kết nối:
            Dispose();

            return newEmployeeCode;

        }

        public int DeleteMultipleEmployees(List<Guid> listEmployeeId)
        {
            var rowAffects = 0;
            // Khởi tạo kết nối database
            _dbConnection.Open();

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    var parameters = new DynamicParameters();
                    var employeeIds = string.Empty;
                    // Duyệt từng id
                    foreach (var id in listEmployeeId)
                    {
                        employeeIds += id.ToString().Trim() + ',';
                    }
                    employeeIds = employeeIds.Substring(0, employeeIds.Length - 1);
                    parameters.Add("@listEmployeeId", employeeIds, DbType.String);
                    rowAffects = _dbConnection.Execute($"Proc_DeleteMultipleEmployees", parameters,
                        transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            // Đóng kết nối:
            Dispose();

            return rowAffects;
        }

        public object FilterEmployee(int pageIndex, int PageSize, string searchText)
        {
            // Khởi tạo kết nối database
            _dbConnection.Open();

            searchText = searchText == null ? string.Empty : searchText;
            var parameters = new DynamicParameters();
            parameters.Add("@m_searchText", searchText);
            parameters.Add("@m_pageIndex", pageIndex);
            parameters.Add("@m_pageSize", PageSize);
            parameters.Add("@m_TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@m_TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var employees = _dbConnection.Query<Employee>("Proc_GetPagingEmployees", param: parameters, commandType: CommandType.StoredProcedure);
            var totalPage = parameters.Get<int>("@m_TotalPage");
            var toatalRecord = parameters.Get<int>("@m_TotalRecord");

            var employeesFilter = new
            {
                TotalPage = totalPage,
                TotalRecord = toatalRecord,
                Data = employees,
            };

            // Đóng kết nối:
            Dispose();

            return employeesFilter;

        }

        public Stream ExportExcel()
        {
            var list = this.Get().ToList();
            MemoryStream stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Truyền vào một stream hoặc Memory Stream để thao tác với file Excel.
            using (var package = new ExcelPackage(stream))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                // Add Sheet vào file Excel
                var workSheet = package.Workbook.Worksheets.Add(Resources.Title_Excel_Employee);

                // lấy các thuộc tính của nhân viên
                var properties = typeof(Employee).GetProperties();
                int col = 1;
                int indexHeader = 2;
                // TABLE HEADER cho file excel
                foreach (var prop in properties)
                {
                    // lấy tên hiển thị đầu tiên của thuộc tính
                    var PropertyNameAttributes = prop.GetCustomAttributes(typeof(ExcelColumnName), true);

                    if (PropertyNameAttributes.Length > 0)
                    {
                        // add vào header của file excel
                        workSheet.Cells[3, indexHeader].Value = (PropertyNameAttributes[0] as ExcelColumnName).Name;
                        // tăng số cột lên 1
                        col++;
                        indexHeader++;
                    }
                }
                // Lấy ra tên cột cuối cùng
                var columnEnd = this.ConvertNumberToLetterExcel(col);

                // TABLE BODY
                // đổ dữ liệu vào excel
                // duyệt các nhân viên
                for (int i = 0; i < list.Count(); i++)
                {
                    int indexColBody = 2; // chỉ số cột
                    // lấy ra STT
                    workSheet.Cells[i + 4, 1].Value = i + 1;

                    // duyệt các thuộc tính để tương tự với phần header
                    for (int j = 1; j < properties.Length; j++)
                    {
                        var propertyNameAttr = properties[j].GetCustomAttributes(typeof(ExcelColumnName), true);

                        // nếu thuộc tính có xuất file: cột +1
                        if (propertyNameAttr.Length > 0)
                        {
                            // xử lí các kiểu dữ liệu datetime
                            if ((propertyNameAttr[0] as ExcelColumnName).Name == Resources.ExcelName_DateOfBirth)
                            {
                                if (properties[j].GetValue(list[i]) != null && !string.IsNullOrEmpty(properties[j].GetValue(list[i]).ToString().Trim()))
                                {
                                    DateTime dt = DateTime.ParseExact(properties[j].GetValue(list[i]).ToString(), "d/M/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                                    workSheet.Cells[i + 4, indexColBody].Value = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                                }
                            }
                            else // các kiểu dữ liệu khác datetime
                            {
                                workSheet.Cells[i + 4, indexColBody].Value = properties[j].GetValue(list[i]);

                            }
                            indexColBody++;
                        }
                        // độ rộng tự động fit với dữ liệu
                        workSheet.Column(j).AutoFit();
                    }
                }

                // Table style
                workSheet.DefaultRowHeight = 20;
                // Set default width cho tất cả column
                workSheet.Cells.AutoFitColumns();
                // Tự động xuống hàng khi text quá dài
                //workSheet.Cells.Style.WrapText = true;
                // Gộp cột
                workSheet.Cells[$"A1:{columnEnd}1"].Merge = true;
                workSheet.Cells[$"A1:{columnEnd}1"].Value = Resources.Title_Excel_Employee;
                workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Size = 16;
                workSheet.Row(1).Style.Font.Name = workSheet.Row(3).Style.Font.Name = Resources.Font_Name_Arial;
                workSheet.Row(1).Style.Font.Bold = workSheet.Row(3).Style.Font.Bold = true;

                workSheet.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                workSheet.Cells[$"A2:{columnEnd}2"].Merge = true;

                workSheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(3).Style.Font.Size = 10;

                workSheet.Cells[$"A3:{columnEnd}3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[$"A3:{columnEnd}3"].Style.Font.Name = Resources.Font_Name_Arial;
                workSheet.Cells[$"A3:{columnEnd}3"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#D8D8D8"));
                workSheet.Cells[$"A3:{columnEnd}{list.Count() + 3}"].Style.Font.Name = Resources.Font_Name_TimeNewRoman;
                // border
                workSheet.Cells[$"A3:{columnEnd}{list.Count() + 3}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A3:{columnEnd}{list.Count() + 3}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A3:{columnEnd}{list.Count() + 3}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A3:{columnEnd}{list.Count() + 3}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[3, 1].Value = Resources.Excel_Index_Number;

                // lưu lại
                package.Save();
                return package.Stream;
            }

            
        }

        /// <summary>
        /// Thực hiện convert chữ số sang tên cột trong excel
        /// </summary>
        /// <param name="col">chữ số</param>
        /// <returns>Ký tự tên cột trong excel</returns>
        /// CreateBy: TTKien(27/12/2021)
        private string ConvertNumberToLetterExcel(int col)
        {
            string columnName = "";

            while (col > 0)
            {
                int modulo = (col - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                col = (col - modulo) / 26;
            }

            return columnName;
        }
    }
}
