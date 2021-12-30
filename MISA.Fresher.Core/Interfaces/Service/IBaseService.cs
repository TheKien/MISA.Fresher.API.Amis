using MISA.Fresher.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Service
{
    public interface IBaseService<MISAEntity>
    {
        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới</param>
        /// <returns>Thêm mới bản ghi thành công</returns>
        /// CreateBy: TTKien(21/12/2021)
        ServiceResult Insert(MISAEntity entity);

        /// <summary>
        /// Cập nhật thông tin 1 bản ghi trong bảng thông qua khoá chính của bản ghi (id)
        /// </summary>
        /// <param name="entity">Thông tin mới cần cập nhật</param>
        /// <param name="entityId">Id để lấy về đối tượng cần cập nhật</param>
        /// <returns>Cập nhật bản ghi thành công</returns>
        /// CreateBy: TTKien(22/10/2021)
        ServiceResult Update(MISAEntity entity, Guid entityId);

        /// <summary>
        /// Xoá 1 bản ghi theo khoá chính
        /// </summary>
        /// <param name="entityId">Khoá chính</param>
        /// <returns>Xoá bản ghi thành công</returns>
        /// CreateBy: TTKien(22/10/2021)
        ServiceResult Delete(string entityId);
    }
}
