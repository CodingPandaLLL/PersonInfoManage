using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Web.Mvc;
using System.Web;
using PersonInfoManage.Models;
using PersonInfoManage.Utils;
using System.Text;

namespace PersonInfoManage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            //接受参数
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            string para = Encoding.UTF8.GetString(b);

            //转换为对象
            if (string.IsNullOrEmpty(para))
            {
                return null;
            }

            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(para);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(UserModel));
            UserModel t = o as UserModel;

            ResponseResult result = new ResponseResult();
            studentsEntities studentsEntities = new studentsEntities();
            //查询语句
            user user = (from c in studentsEntities.user where c.NAME == t.name select c).FirstOrDefault();
            if (user != null)
            {
                if (user.PASSWORD.EndsWith(t.password))
                {
                    result = new ResponseResult() { Result = 1, Message = "登录成功！" };
                }
                else
                {
                    result = new ResponseResult() { Result = 0, Message = "登录失败！" };
                }

            }
            else
            {
                result = new ResponseResult() { Result = 0, Message = "登录失败！" };

            }
            return Content(JsonConvert.SerializeObject(result));

        }

        protected ViewResult GetView(string name)
        {
            return View(string.Format("~/Views/Home/UserInfo.cshtml", name));
        }

        public ActionResult UserInfo()
        {
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);

            studentsEntities studentsEntities = new studentsEntities();
            user user = (from c in studentsEntities.user where c.ID == id select c).FirstOrDefault();

            if (user != null)
            {
                ViewBag.DetailSource = user;
            }

            return GetView("UserInfo");
            //return View();
        }
     
    }

}
