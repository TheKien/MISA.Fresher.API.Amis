using MISA.Fresher.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Infrastructure
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới có dạng NV-X (X là số mã nhất trong database cộng thêm 1)</returns>
        /// CreateBy: TTKien(21/12/2021)
        string GetNewEmployeeCode();

        /// <summary>
        /// Tìm kiếm theo mã nhân viên, tên nhân viên và số điện thoại kết hợp phân trang
        /// </summary>
        /// <param name="pageIndex">Số trang hiện tại</param>
        /// <param name="PageSize">Số bản ghi trên 1 trang</param>
        /// <param name="searchText">gợi ý tìm kiếm</param>
        /// <returns>Danh sách nhân viên tìm theo gợi ý</returns>
        /// CreateBy: TTKien(24/12/2021)
        object FilterEmployee(int pageIndex, int PageSize, string searchText);

        /// <summary>
        /// Xoá nhiều nhân viên
        /// </summary>
        /// <param name="listEmployeeId">Chuỗi danh sách khoá chính</param>
        /// <returns>Số nhân viên xoá thành công</returns>
        /// CreateBy: TTKien(24/12/2021)
        int DeleteMultipleEmployees(List<Guid> listEmployeeId);

        Stream ExportExcel();
    }
}
