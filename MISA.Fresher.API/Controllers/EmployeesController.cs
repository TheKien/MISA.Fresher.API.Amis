using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using MISA.Fresher.Infrastructure.Reponsitory;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static MISA.Fresher.Core.Attributes.MISAAttribute;

namespace MISA.Fresher.API.Controllers
{
    public class EmployeesController : MISABaseController<Employee>
    {
        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;
        public EmployeesController(IEmployeeRepository employeeRepository, IEmployeeService employeeService) : base(employeeRepository, employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Lấy mã nhân viên mới khi thêm mới nhân viên
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreateBy: TTKien(21/12/2021)
        [HttpGet("NewEmployeeCode")]
        public string NewEmployeeCode()
        {
            return _employeeRepository.GetNewEmployeeCode();
        }


        /// <summary>
        /// Tìm kiếm theo mã nhân viên, tên nhân viên và số điện thoại kết hợp phân trang
        /// </summary>
        /// <param name="pageIndex">Số trang hiện tại</param>
        /// <param name="PageSize">Số bản ghi trên 1 trang</param>
        /// <param name="searchText">Gợi ý tìm kiếm</param>
        /// <returns>Danh sách nhân viên tìm kiếm</returns>
        /// CreatedBy: TTKien(24/12/2021)
        [HttpGet("Filter")]
        public IActionResult Filter([FromQuery] int pageIndex, int PageSize, string searchText)
        {
            var res = _employeeRepository.FilterEmployee(pageIndex, PageSize, searchText);
            return Ok(res);
        }

        /// <summary>
        /// Xóa nhiều nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Xoá nhiều nhân viên thành công</returns>
        /// CreatedBy: TTKien(24/12/2021)
        [HttpDelete("DeleteMutilple")]
        public IActionResult DeleteMutilple([FromBody] List<Guid> listEmployeeId)
        {
            var res = _employeeService.DeleteMultipleEmployees(listEmployeeId);
            return Ok(res);
        }

        /// <summary>
        /// api xuất dữ liệu nhân viên thành file excel
        /// </summary>
        /// <returns>File Excel</returns>
        /// CreatedBy: TTKien(25/12/2021)
        [HttpGet("ExportExcel")]
        public IActionResult ExportEmployeesDataToExcel()
        {
            var stream = _employeeRepository.ExportExcel();
            stream.Position = 0;
            string excelName = $"Danh_sach_nhan_vien_{DateTime.Now.ToString("dd-MM-yyy")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
    