using Demo_Application.Enum;
using Demo_Application.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Application.Controllers
{
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("get-list")]
        [TestAttribute(new[] {PermissionEnum.Rights.DELETE})]
        public async Task<IActionResult> GetDataList()
        {
           var _res = new { status = 200, Message = "Sucesss", Data = "Succesfully Authorize.!" };
           return Ok(_res);
        }
    }
}
