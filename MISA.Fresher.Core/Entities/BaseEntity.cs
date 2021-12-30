using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.Fresher.Core.Attributes.MISAAttribute;

namespace MISA.Fresher.Core.Entities
{
    public class BaseEntity
    {
        /// <summary>
        /// Ngày tạo bản ghi
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        [NotUpdated]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Tên người người tạo bản ghi
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        [NotUpdated]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa đổi bản ghi lần gần nhất
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Tên người sửa đổi bản ghi lần gần nhất
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        public string ModifiedBy { get; set; }
    }
}
