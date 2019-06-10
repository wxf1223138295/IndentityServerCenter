using System;
using MVCClient.common;
using MVCClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace MVCClient.Controllers
{
    public class ToolsController:Controller
    {
        [HttpGet]
          public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
          public IActionResult Index(BirthModel model)
        {
            var birth=model.birthday.Trim();

            var year = birth.Substring(0, 4);
            var month = birth.Substring(4, 2);
            var day = birth.Substring(6, 2);

            var newtime = year + "-" + month + "-" + day;
            DateTime borntime=DateTime.MinValue;
            var borndate=DateTime.TryParse(newtime,out borntime);
            if (borndate)
            {
                var result = FgFuncAge.GetAge(birth, false);
                ViewData["result"] = result;
            }
            else
            {
                ViewData["result"] = "请传入正确的时间";
            }
          
            return View();
        }

    }
}