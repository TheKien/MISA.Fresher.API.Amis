using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Exeptions;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using MISA.Fresher.Core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.Fresher.Core.Enums.MISAEnum;

namespace MISA.Fresher.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        ServiceResult _serviceResult;
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _serviceResult = new ServiceResult() { StatusCode = Enums.MISAEnum.StatusCode.Success };
        }

        public ServiceResult DeleteMultipleEmployees(List<Guid> listEmployeeId)
        {
            _serviceResult.Data = _employeeRepository.DeleteMultipleEmployees(listEmployeeId);
            _serviceResult.Messenger = Resources.HttpCode_204;
            _serviceResult.StatusCode = Enums.MISAEnum.StatusCode.NoContent;
            return _serviceResult;
        }

        /// <summary>
        /// Validate dữ liệu đặc thù của nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên để kiểm tra</param>
        /// <returns>True - Nếu nhân viên thoả mãn các điều kiện, False - Nếu nhân viên không thoá mãn điều kiện</returns>
        /// CreatedBy: TTKien(21/12/2021)
        protected override bool ValidateCustom(Employee employee)
        {
            return true;
        }
    }
}
