using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UserAPI;

namespace MemberPortal.Controllers
{
    public class Login_Controller : Controller
    {
        string baseUrl = "http://localhost:30189/";

       
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            List<UserAPI.User> list = new List<UserAPI.User>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(" api/Users");
                if(Res.IsSuccessStatusCode)
                {
                    var Result = Res.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<User>>(Result);
                }
            }
            bool flag = false;
            foreach(var obj in list)
            {
                if(obj.Email==user.Email && obj.Password==user.Password)
                {
                    flag = true; break;
                }
            }
            if(flag)
            {
                //ViewBag.msg = "Login successful";
                //ModelState.Clear();
                ViewBag.msg = user.Email;
                return View("LoggedIn");
            }
            else
            {
                ViewBag.msg = "Incorrent credentials!";
            }
            return View();
        }

        public IActionResult LoggedIn()
        {
            return View();
        }
    }
}
