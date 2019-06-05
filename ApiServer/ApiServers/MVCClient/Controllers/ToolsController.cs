using Microsoft.AspNetCore.Mvc;
using MVCClient.common;
using MVCClient.Models.Input;

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
            var birth=model.birthday;
            var result=FgFuncAge.GetAge(birth,false);
            ViewData["result"]=result;
            return View();
        }

    }
}