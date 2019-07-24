using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using randompasscode.Models;
using Microsoft.AspNetCore.Http;
namespace randompasscode.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("PasscodeCount") == null)
            {
            HttpContext.Session.SetInt32("PasscodeCount", 0);
            }
            ViewBag.Count = HttpContext.Session.GetInt32("PasscodeCount");
            ViewBag.Word = HttpContext.Session.GetString("Passcode");
            return View();
        }

        [HttpPost("passcode")]
        public IActionResult Passcode()
        {
            int? Count = HttpContext.Session.GetInt32("PasscodeCount");
            Count ++;
            HttpContext.Session.SetInt32("PasscodeCount", Convert.ToInt32(Count));
            string PassCodeLetters = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            Random rand = new Random();
            char[] options = new char[14];
            for(int i = 0; i < 14; i++)
            {
                options[i] = PassCodeLetters[rand.Next(PassCodeLetters.Length)];
            }
            string word = new string(options);
            HttpContext.Session.SetString("Passcode", word);
            return RedirectToAction("Index");
        }

    }
}
