using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ERP.Core.Base.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ERP.Core.Controllers
{
    /// <summary>
    /// 模块名：天气管理
    /// 创建人：zhuwm
    /// 日  期：2020年2月3日
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 世界你好！
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get(string name)
        {
            return "Hello " + name + "!";
        }
    }
}
