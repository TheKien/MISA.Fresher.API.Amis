using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {

        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
        }
        /// <summary>
        /// Validate dữ liệu đặc thù của nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên để kiểm tra</param>
        /// <returns>True - Nếu nhân viên thoả mãn các điều kiện, False - Nếu nhân viên không thoá mãn điều kiện</returns>
        /// CreatedBy: TTKien(21/12/2021)
        protected override bool ValidateObjectCustom(Employee employee) {
            return true;
        }
    }
}
