using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Service
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Xoá nhiều nhân viên
        /// </summary>
        /// <param name="listEmployeeId">Chuỗi danh sách khoá chính</param>
        /// <returns>Số nhân viên xoá thành công</returns>
        /// CreateBy: TTKien(24/12/2021)
        ServiceResult DeleteMultipleEmployees(List<Guid> listEmployeeId);
    }
}
