using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.Fresher.Core.MISAAttribute.MISAAttribute;

namespace MISA.Fresher.Core.Entities
{
    public class Employee
    {
        /// <summary>
        /// Khoá chính của nhân viên
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        [NotEmpty]
        [Unique]
        [PropertyName("Mã nhân viên")]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        [NotEmpty]
        [PropertyName("Tên nhân viên")]
        public string EmployeeName { get; set; }
        /// <summary>
        /// Giới tính
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Ngày sinh
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Số điện thoại
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Số điện thoại cố định
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public string TelephoneNumber { get; set; }
        /// <summary>
        /// Email
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Địa chỉ
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Khoá ngoại phòng ban
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        [NotEmpty]
        [PropertyName("Phòng ban")]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Tên chức vụ
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// Số CMND
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public string IdentityNumber { get; set; }
        /// <summary>
        /// Nơi cấp CMND
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public string IdentityPlace { get; set; }
        /// <summary>
        /// Ngày tạo CMND
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Số tài khoản ngân hàng
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public string BankAccount { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// Chi nhánh ngân hàng
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public string BankBranch { get; set; }
        /// <summary>
        /// Mã phòng ban lấy từ bảng phòng ban
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        [ReadOnly]
        public string DepartmentName { get; }
        /// <summary>
        /// Ngày tạo khi thêm mới nhân viên
        /// CreateBy: TTKien(21/10/2021)
        /// </summary>
        public DateTime CreatedDate { get; set; }
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
