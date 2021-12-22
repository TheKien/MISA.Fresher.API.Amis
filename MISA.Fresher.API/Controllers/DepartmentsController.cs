using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.API.Controllers
{
    public class DepartmentsController : MISABaseController<Department>
    {
        public DepartmentsController(IDepartmentRepository departmentRepository, IDepartmentService departmentService) : base(departmentRepository, departmentService)
        {

        }
    }
}
