    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.Fresher.Core.Attributes.MISAAttribute;
using static MISA.Fresher.Core.Enums.MISAEnum;

namespace MISA.Fresher.Core.Entities
{
    public class Employee : BaseEntity
    {
        /// <summary>
        /// Khoá chính của nhân viên
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        [NotUpdated]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        [NotEmpty]
        [Unique]
        [PropertyName("Mã nhân viên")]
        [ExcelColumnName("Mã nhân viên")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        [NotEmpty]
        [PropertyName("Tên nhân viên")]
        [ExcelColumnName("Tên nhân viên")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        public Gender Gender { get; set; }

        /// <summary>
        /// Lấy ra tên giới tính (Nam/ Nữ/ Khác)
        /// </summary>
        /// CreateBy: TTKien(22/10/2021)
        [ReadOnly]
        [ExcelColumnName("Giới tính")]
        public string GenderName
        {
            get
            {
                var resourceText = GetEnumTextByEnumName<Gender>(Gender);
                return resourceText;
            }
        }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        [ExcelColumnName("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        public string Address { get; set; }

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        [ExcelColumnName("Chức danh")]
        public string PositionName { get; set; }

        /// <summary>
        /// Khoá ngoại phòng ban
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        [NotEmpty]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban lấy từ bảng phòng ban
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        [ReadOnly]
        [ExcelColumnName("Tên đơn vị")]
        public string DepartmentName { get; }

        /// <summary>
        /// Số CMND
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        [PropertyName("Số CMND")]
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Nơi cấp CMND
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Ngày tạo CMND
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        [ExcelColumnName("Số tài khoản")]
        public string BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        [ExcelColumnName("Tên ngân hàng")]
        public string BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        /// CreateBy: TTKien(21/10/2021)
        public string BankBranch { get; set; }
    }
}
