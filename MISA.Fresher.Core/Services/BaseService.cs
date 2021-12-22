using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.Fresher.Core.MISAAttribute.MISAAttribute;

namespace MISA.Fresher.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        IBaseRepository<MISAEntity> _baseRepository;
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public int? Insert(MISAEntity entity)
        {
            // validate chung - base xử lý:
            var isValid = ValidateObject(entity);
            // validate đặc thù cho từng đối tượng -> các service con tự xử lý:
            if (isValid == true)
            {
                isValid = ValidateObjectCustom(entity);
            }
            if (isValid == true)
            {
                // Thêm mới bản ghi vào database:   
                return _baseRepository.Insert(entity);
            }
            return null;
        }
        /// <summary>
        /// Validate dữ liệu chung
        /// </summary>
        /// <param name="entity">Thông tin đối tượng kiểm tra</param>
        /// <returns>True - Nếu đối tượng thoả mãn các điều kiện, False - Nếu đối tượng không thoá mãn điều kiện</returns>
        /// CreatedBy: TTKien(21/12/2021)
        bool ValidateObject(MISAEntity entity)
        {
            // Kiểm tra tất cả property của đối tượng:
            var properties = typeof(MISAEntity).GetProperties();
            // Duyệt từng property
            foreach (var prop in properties)
            {
                // Lấy tên gốc của property:
                var propNameOrginal = prop.Name;
                // Gắn tên hiển thị bằng tên gốc: 
                var propNameDisplay = propNameOrginal;
                // Lấy giá trị tượng ứng với đối tượng:
                var propValue = prop.GetValue(entity);
                // Lấy về tất cả property đánh dấu bắt buộc nhập:
                var propNotEmptys = prop.GetCustomAttributes(typeof(NotEmpty), true);
                // Lấy về tất cả property có tên tự định nghĩa:
                var propPropertyNames = prop.GetCustomAttributes(typeof(PropertyName), true);
                // Nếu property có tên tự định nghĩa -> lấy tên đặt để hiển thị thông báo cho người dùng khi xảy ra lỗi:
                if (propPropertyNames.Length > 0)
                {
                    // Ghi đè tên hiển thị bằng tên tự định nghĩa
                    propNameDisplay = (propPropertyNames[0] as PropertyName).Name;
                }
                // Xem đối tượng có property là property đánh dấu bắt buộc nhập hay không:
                if (propNotEmptys.Length > 0)
                {
                    // Nếu thông tin bắt buộc nhập thì hiển thị cảnh báo hoặc đánh dấu trạng thái không hợp lệ:
                    if (propValue == null || string.IsNullOrEmpty(propValue.ToString().Trim()))
                    {
                        throw new Exception($"Thong tin {propNameDisplay} khong duoc de trong");
                    }    
                }
            }
            return true;
        }
        /// <summary>
        /// Validate dữ liệu đặc thù của từng đối tượng
        /// </summary>
        /// <param name="entity">Thông tin đối tượng kiểm tra</param>
        /// <returns>True - Nếu đối tượng thoả mãn các điều kiện, False - Nếu đối tượng không thoá mãn điều kiện</returns>
        /// CreatedBy: TTKien(21/12/2021)
        protected virtual bool ValidateObjectCustom(MISAEntity entity)
        {
            return true;
        }
        public int? Update(MISAEntity entity, Guid entityId)
        {
            throw new NotImplementedException();
        }
    }
}
