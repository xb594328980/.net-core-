using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace OcelotDownAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcelotController : ControllerBase
    {
        // GET api/ocelot/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await Task.Run(() =>
            {
                return $"This is from {HttpContext.Request.Host.Value}, path: {HttpContext.Request.Path}";
            });
            return Ok(result);
        }
        // GET api/ocelot/aggrWilling
        [HttpGet("aggrWilling")]
        public async Task<IActionResult> AggrWilling(int id)
        {
            var result = await Task.Run(() =>
            {
                ResponseResult response = new ResponseResult()
                { Comment = $"我是Willing，还是多加工资最实际, path: {HttpContext.Request.Path}" };
                return response;
                //return $"我是Willing，还是多加工资最实际, path: {HttpContext.Request.Path}";
            });
            return Ok(result);
        }
        // GET api/ocelot/aggrJack
        [HttpGet("aggrJack")]
        public async Task<IActionResult> AggrJack(int id)
        {
            var result = await Task.Run(() =>
            {
                ResponseResult response = new ResponseResult()
                { Comment = $"我是Jack，我非常珍惜现在的工作机会, path: {HttpContext.Request.Path}" };
                return response;
                //return $"我是Jack，我非常珍惜现在的工作机会, path: {HttpContext.Request.Path}";
            });
            return Ok(result);
        }

        // GET api/ocelot/consulWilling
        [HttpGet("consulWilling")]
        public async Task<IActionResult> ConsulWilling(int id)
        {
            var result = await Task.Run(() =>
            {
                ResponseResult response = new ResponseResult()
                { Comment = $"我是Willing，你可以在Consul那里找到我的信息, host: {HttpContext.Request.Host.Value}, path: {HttpContext.Request.Path}{HttpContext.Request.Headers.Keys.Where(x=>x.Contains("custom")).Count()}" };
                return response;
            });
            return Ok(result);
        }

        // GET api/ocelot/identityWilling
        [HttpGet("identityWilling")]
        //[Authorize]
        public async Task<IActionResult> IdentityWilling(int id)
        {
            var result = await Task.Run(() =>
            {
                ResponseResult response = new ResponseResult()
                { Comment = $"我是Willing，既然你是我公司员工，那我就帮你干活吧, host: {HttpContext.Request.Host.Value}, path: {HttpContext.Request.Path}" };
                return response;
            });
            return Ok(result);
        }
    }

    public class ResponseResult
    {
        public string Comment { get; set; }
    }
}
