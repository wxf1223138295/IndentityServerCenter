using ToolClient.common;
using ToolClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace ToolClient.Controllers
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
            var birth=model.birthday;
            var result=FgFuncAge.GetAge(birth,false);
            ViewData["result"]=result;
            return View();
        }

    }
}