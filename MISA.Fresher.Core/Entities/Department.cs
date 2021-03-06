using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.Fresher.Core.Attributes.MISAAttribute;

namespace MISA.Fresher.Core.Entities
{
    public class Department : BaseEntity
    {
        /// <summary>
        /// Khoá chính của đơn vị
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        [NotUpdated]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        [NotEmpty]
        [PropertyName("Mã đơn vị")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        [NotEmpty]
        [Unique]
        [PropertyName("Tên đơn vị")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Mô tả đơn vị
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        public string Description { get; set; }
        
    }
}
