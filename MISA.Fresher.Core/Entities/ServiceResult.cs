using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.Fresher.Core.Enums.MISAEnum;

namespace MISA.Fresher.Core.Entities
{
    public class ServiceResult
    {
        /// <summary>
        /// Dữ liệu trả về của service
        /// </summary>
        /// CreatedBy: TTKien(24/12/2021)
        public object Data { get; set; }

        /// <summary>
        /// Thông báo kết quả trả về của service
        /// </summary>
        /// CreatedBy: TTKien(24/12/2021)
        public string Messenger { get; set; }

        /// <summary>
        /// Mã Code của service
        /// </summary>
        /// createdBy: TTKien(24/12/2021)
        public StatusCode StatusCode { get; set; }
    }
}
