using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var entites = _baseRepository.Get();
                return Ok(entites);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Co loi xay ra",
                    data = DBNull.Value,
                    moreInfo = ""
                };
                return StatusCode(500, result);
            }
        }
        /// <summary>
        /// Thêm 1 bản ghi vào database
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Thêm bản ghi thành công</returns>
        [HttpPost]
        public IActionResult Insert(MISAEntity entity)
        {
            try
            {
                var res = _baseService.Insert(entity);
                return CreatedAtAction("Insert", res);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Co loi xay ra",
                    data = DBNull.Value,
                    moreInfo = ""
                };
                return StatusCode(500, result);
            }
        }
    }
}
