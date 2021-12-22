using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities
{
    public class Department
    {
        /// <summary>
        /// Khoá chính của phòng ban
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Mô tả phong ban
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Ngày tạo khi thêm mới nhân viên
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Tên người thêm mới nhân viên
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa đổi khi cập nhật nhân viên
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Tên người cập nhật thông tin nhân viên  
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
