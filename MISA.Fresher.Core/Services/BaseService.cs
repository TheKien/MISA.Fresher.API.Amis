using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Exceptions;
using MISA.Fresher.Core.Exeptions;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using MISA.Fresher.Core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MISA.Fresher.Core.Attributes.MISAAttribute;

namespace MISA.Fresher.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        IBaseRepository<MISAEntity> _baseRepository;
        ServiceResult _serviceResult;

        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult() { StatusCode = Enums.MISAEnum.StatusCode.Success };
        }

        public ServiceResult Insert(MISAEntity entity)
        {
            // validate chung - base xử lý:
            var isValid = Validate(entity, Guid.Empty);
            // validate đặc thù cho từng đối tượng -> các service con tự xử lý:
            if (isValid == true)
            {
                isValid = ValidateCustom(entity);
            }
            if (isValid == true)
            {
                _serviceResult.Data = _baseRepository.Insert(entity);
                _serviceResult.Messenger = Resources.HttpCode_201;
                _serviceResult.StatusCode = Enums.MISAEnum.StatusCode.Created;
            }
            return _serviceResult;
        }

        public ServiceResult Update(MISAEntity entity, Guid entityId)
        {
            // validate chung - base xử lý:
            var isValid = Validate(entity, entityId);
            // validate đặc thù cho từng đối tượng -> các service con tự xử lý:
            if (isValid == true)
            {
                isValid = ValidateCustom(entity);
            }
            if (isValid == true)
            {
                _serviceResult.Data = _baseRepository.Update(entity, entityId);
                _serviceResult.Messenger = Resources.HttpCode_200;
                _serviceResult.StatusCode = Enums.MISAEnum.StatusCode.Success;
            }
            return _serviceResult;
        }

        public ServiceResult Delete(string entityId)
        {
            _serviceResult.Data = _baseRepository.Delete(entityId);
            _serviceResult.Messenger = Resources.HttpCode_204;
            _serviceResult.StatusCode = Enums.MISAEnum.StatusCode.NoContent;
            return _serviceResult;

        }

        /// <summary>
        /// Validate dữ liệu chung
        /// </summary>
        /// <param name="entity">Thông tin đối tượng kiểm tra</param>
        /// <returns>True - Nếu đối tượng thoả mãn các điều kiện, False - Nếu đối tượng không thoá mãn ít nhất 1 điều kiện</returns>
        /// CreatedBy: TTKien(21/12/2021)
        bool Validate(MISAEntity entity, Guid entityId)
        {
            var entityOld = new Object();
            // Nếu có id -> lấy về đối tượng cũ cần sửa:
            if (entityId != Guid.Empty)
            {
                entityOld = _baseRepository.GetById(entityId);
            }
            if (entityOld == null)
            {
                var msgErr = Resources.Exceptione_Msg_IsExistEntity;
                throw new MISAResponseNotValidException(msgErr);
            }
            List<string> errMsg = new List<string>();
            // Lấy tất cả property của đối tượng:
            var properties = typeof(MISAEntity).GetProperties();
            // Duyệt từng property:
            foreach (var prop in properties)
            {
                // Lấy tên gốc của property:
                var propNameOrginal = prop.Name;
                // Tên thuộc tính hiển thị trên messenger
                var propNameDisplay = propNameOrginal;
                // Lấy giá trị tượng ứng với đối tượng:
                var propValue = prop.GetValue(entity);
                // Lấy tên tự định nghĩa của property:
                var propPropertyNames = prop.GetCustomAttributes(typeof(PropertyName), true);

                // So sánh giá trị mới và giá trị cũ. Nếu bằng nhau thì bỏ qua validate
                if (entityId != Guid.Empty)
                {
                    // Lấy giá trị cũ tương ứng với đối tượng
                    var propValueOld = prop.GetValue(entityOld);
                    if (propValue == null || propValueOld == null)
                    {
                        continue;
                    }
                    // So sánh
                    if (String.Compare(propValue.ToString().Trim(), propValueOld.ToString().Trim()) == 0)
                    {
                        continue;
                    }
                }

                // Nếu property chỉ để hiển thị hoặc không được update -> bỏ qua validate:
                if (prop.IsDefined(typeof(ReadOnly), true) || prop.IsDefined(typeof(NotUpdated), true))
                {
                    continue;
                }

                // Gắn tên hiển thị
                if (propPropertyNames.Length > 0)
                {
                    propNameDisplay = (propPropertyNames[0] as PropertyName).Name;
                }

                // Nếu property có attibute để đánh dấu bắt buộc (NotEmpty):
                if (prop.IsDefined(typeof(NotEmpty), true))
                {
                    // Nếu thông tin bắt buộc nhập thì hiển thị cảnh báo hoặc đánh dấu trạng thái không hợp lệ:
                    if (propValue == null || string.IsNullOrEmpty(propValue.ToString().Trim()))
                    {
                        errMsg.Add(string.Format(Properties.Resources.Exception_Msg_NotEmpty, propNameDisplay));
                    }
                }

                // Nếu property không null
                if (!(propValue == null || string.IsNullOrEmpty(propValue.ToString().Trim())))
                {
                    // Kiểm tra tồn tại:
                    if (prop.IsDefined(typeof(Unique), true))
                    {
                        // Kiểm tra csdl:
                        bool isExist = _baseRepository.IsExist(entity, prop);
                        // Nếu giá trị đã tồn tại trong csdl:
                        if (isExist)
                        {
                            errMsg.Add(string.Format(Properties.Resources.Exception_Msg_Unique, propNameDisplay, propValue));
                        }
                    }

                    // Kiểm tra định dạng email:
                    if (prop.IsDefined(typeof(Email), true))
                    {
                        Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                                                    RegexOptions.CultureInvariant | RegexOptions.Singleline);
                        bool isValidEmail = regex.IsMatch(propValue.ToString().Trim());
                        if (!isValidEmail)
                        {
                            errMsg.Add(string.Format(Resources.Exception_Msg_Email, propValue.ToString().Trim()));
                        }
                    }
                }
            }
            // Nếu có lỗi ném ra một ngoại lệ MISAResponseNotValidException
            if (errMsg.Count() > 0)
            {
                throw new MISAResponseNotValidException(errMsg);
            }
            return true;
        }

        /// <summary>
        /// Validate dữ liệu đặc thù của từng đối tượng
        /// </summary>
        /// <param name="entity">Thông tin đối tượng kiểm tra</param>
        /// <returns>True - Nếu đối tượng thoả mãn các điều kiện, False - Nếu đối tượng không thoá mãn điều kiện</returns>
        /// CreatedBy: TTKien(21/12/2021)
        protected virtual bool ValidateCustom(MISAEntity entity)
        {
            return true;
        }
    }
}
