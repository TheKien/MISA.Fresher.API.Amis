using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Core.Exeptions;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MISABaseController<MISAEntity> : ControllerBase
    {
        IBaseRepository<MISAEntity> _baseRepository;
        IBaseService<MISAEntity> _baseService;
        public MISABaseController(IBaseRepository<MISAEntity> baseRepository, IBaseService<MISAEntity> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }

        /// <summary>
        /// Lầy toàn bộ bản ghi
        /// </summary>
        /// <returns>Toàn bộ bản ghi</returns>
        /// CreateBy: TTKien(21/10/2021)
        [HttpGet]
        public IActionResult Get()
        {
            var entites = _baseRepository.Get();
            return Ok(entites);
        }

        /// <summary>
        /// Lấy 1 bản ghi theo khoá chính
        /// </summary>
        /// <returns>Trả về 1 bản ghi cần lấy</returns>
        /// CreateBy: TTKien(22/10/2021)
        [HttpGet("{entityId}")]
        public IActionResult GetById(Guid entityId)
        {
            var entity = _baseRepository.GetById(entityId);
            return Ok(entity);
        }

        /// <summary>
        /// Thêm 1 bản ghi vào database
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Thêm bản ghi thành công</returns>
        /// CreateBy: TTKien(21/10/2021)
        [HttpPost]
        public IActionResult Insert(MISAEntity entity)
        {
            var serviceResult = _baseService.Insert(entity);
            return Ok(serviceResult);
        }

        /// <summary>
        /// Sửa 1 bản ghi theo khoá chính
        /// </summary>
        /// <param name="entity">Khoá chính của bản ghi cần sửa đổi</param>
        /// <param name="entityId">Đối tượng cần thêm</param>
        /// <returns>Bản ghi được cập nhật thành công</returns>
        /// CreateBy: TTKien(22/10/2021)
        [HttpPut("{entityId}")]
        public IActionResult Update([FromBody] MISAEntity entity, [FromRoute] Guid entityId)
        {
            var serviceResult = _baseService.Update(entity, entityId);
            return Ok(serviceResult);
        }

        /// <summary>
        /// Lấy 1 bản ghi theo khoá chính
        /// </summary>
        /// <returns>Trả về 1 bản ghi cần lấy</returns>
        /// CreateBy: TTKien(22/10/2021)
        [HttpDelete("{entityId}")]
        public IActionResult Delete(string entityId)
        {
            var serviceResult = _baseService.Delete(entityId);
            return Ok(serviceResult);

        }
    }
}
