using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonInfoManage.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using PersonInfoManage.Utils;
using System.Text;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace PersonInfoManage.Controllers
{
    public class PersonController : Controller
    {
        protected ViewResult GetView(string name)
        {
            return View(string.Format("~/Views/Person/PersonList.cshtml", name));
        }

        //获取人员列表
        public ActionResult PersonList()
        {
            studentsEntities studentsEntities = new studentsEntities();

            //查询方法一
            var personResult = (from c in studentsEntities.person where c.ID > 0 select c);

            //查询方法二
            //var sql = "select * from person";
            //var info = ((IObjectContextAdapter)studentsEntities).ObjectContext.CreateQuery<person>(sql);

            //查询方法四
            //var info4 = studentsEntities.Database.SqlQuery<person>(sql);

            //查询方法五
            //object[] obj = new object[1];
            //var info5 = studentsEntities.person.SqlQuery(sql, obj);

            List<person> personList = new List<person>();
            foreach (var item in personResult)
            {
                personList.Add(item);
                Console.WriteLine(item.ID);     //打印ID测试
            }


            //GridRow<DeviceRow> rows = new GridRow<DeviceRow>() { total = pager.TotalCount, rows = models };

            //return Content(personList.ToString()); ;
            if (personList != null)
            {
                //ViewBag.DetailSource = personList.ToString();
                ViewBag.list = Newtonsoft.Json.JsonConvert.SerializeObject(personList);
            }
            return GetView("PersonList");
            //return View();
        }

        //跳转到新增人员信息页面
        public ActionResult GotoAddPerson()
        {
            return View(string.Format("~/Views/Person/AddPerson.cshtml", "AddPerson"));
        }

        public ActionResult GotoEditPerson()
        {
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);

            studentsEntities studentsEntities = new studentsEntities();
            person user = (from c in studentsEntities.person where c.ID == id select c).FirstOrDefault();

            if (user != null)
            {
                ViewBag.DetailSource = user;
            }

            return View(string.Format("~/Views/Person/EditPerson.cshtml", "AddPerson"));
        }

        //新增人员信息
        public ActionResult AddPerson()
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
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(person));
            person t = o as person;

            ResponseResult result = new ResponseResult();
            studentsEntities studentsEntities = new studentsEntities();

            try
            {
                //插入语句
                studentsEntities.person.Add(t);
                studentsEntities.Entry<person>(t).State = EntityState.Added;
                studentsEntities.SaveChanges();
                result = new ResponseResult() { Result = 1, Message = "保存成功！" };
            }
            catch
            {
                result = new ResponseResult() { Result = 0, Message = "保存失败！" };
            }

            return Content(JsonConvert.SerializeObject(result));

        }

        //删除人员信息
        public ActionResult PersonDelete()
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
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(person));
            person t = o as person;

            ResponseResult result = new ResponseResult();
            studentsEntities studentsEntities = new studentsEntities();

            //数据库删除记录
            DbEntityEntry<person> entry = studentsEntities.Entry<person>(t);
            entry.State = System.Data.Entity.EntityState.Deleted;
            int res = studentsEntities.SaveChanges();
            if (res > 0) //删除成功
            {
                result = new ResponseResult() { Result = 1, Message = "删除成功！" };
            }
            else
            {
                result = new ResponseResult() { Result = 0, Message = "删除失败！" };

            }
            return Content(JsonConvert.SerializeObject(result));

        }

        //编辑人员信息
        public ActionResult EditPerson()
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
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(person));
            person t = o as person;

            ResponseResult result = new ResponseResult();
            studentsEntities studentsEntities = new studentsEntities();

            try
            {
                //更新语句
                studentsEntities.person.Add(t);
                studentsEntities.Entry<person>(t).State = EntityState.Modified;
                studentsEntities.SaveChanges();
                result = new ResponseResult() { Result = 1, Message = "保存成功！" };
            }
            catch
            {
                result = new ResponseResult() { Result = 0, Message = "保存失败！" };
            }

            return Content(JsonConvert.SerializeObject(result));

        }
    }
}
