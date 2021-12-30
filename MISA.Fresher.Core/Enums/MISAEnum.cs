using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Enums
{
    public class MISAEnum
    {
        /// <summary>
        /// Thực thi lấy ra Resources tương ứng với Enum
        /// </summary>
        /// <typeparam name="T">Đối tượng Enum</typeparam>
        /// <param name="misaEnum">Đối tượng Enum</param>
        /// <returns>Resources tương ứng</returns>
        public static string GetEnumTextByEnumName<T>(T misaEnum)
        {
            var enumPropertyName = misaEnum.ToString();
            var enumName = misaEnum.GetType().Name;
            var resourceText = MISA.Fresher.Core.Properties.Resources.ResourceManager.GetString($"Enum_{enumName}_{enumPropertyName}");
            return resourceText;
        }

        /// <summary>
        /// StatusCode để xác định trạng thái
        /// </summary>
        /// createdBy: TTKien(24/12/2021)
        public enum StatusCode
        {
            /// <summary>
            /// Dữ liệu thành công
            /// </summary>
            Success = 200,
            /// <summary>
            /// Thêm bản ghi thành công
            /// </summary>
            Created = 201,
            /// <summary>
            /// Không nội dung
            /// </summary>
            NoContent = 204,
            /// <summary>
            /// Lỗi dữ liệu không hợp lệ
            /// </summary>
            ErrorBadRequest = 400,

            /// <summary>
            /// Lỗi Internal Server
            /// </summary>
            ErrorInternalServer = 500,
        }

        /// <summary>
        /// Giới tính
        /// </summary>
        /// CreatedBy: TTKien(21/12/2021)
        public enum Gender
        {
            /// <summary>
            /// Nữ
            /// </summary>
            Female = 0,
            /// <summary>
            /// Nam
            /// </summary>
            Male = 1,
            /// <summary>
            /// Khác
            /// </summary>
            Other = 2,
        }
    }
}