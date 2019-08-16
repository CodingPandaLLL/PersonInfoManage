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
using System.Drawing;
using System.Net.Http;
using System.Net;

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

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fileBase"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImage()
        {
            //接受参数
            HttpPostedFileBase fileBase = Request.Files["photo"];
            string imgurl = string.Empty;
            string imgPath = System.IO.Path.GetFileName(fileBase.FileName);
            int index = imgPath.LastIndexOf('.');
            string suffix = imgPath.Substring(index).ToLower();
            string suffix1 = imgPath.Substring(index+1).ToLower();
            if (suffix == ".jpg" || suffix == ".jpeg" || suffix == ".png" || suffix == ".gif" || suffix == ".bmp")
            {
                string pictureName = DateTime.Now.Ticks.ToString() + suffix; //图片名称
                string savePath = Server.MapPath("/Files/Images/SlideConfig/");//幻灯片文件夹
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                //保存图片信息到数据库表
                studentsEntities studentsEntities = new studentsEntities();
                syspic pic = new syspic();
                pic.IMG_NAME = pictureName;
                pic.IMG_PATH = savePath;
                pic.IMG_TYPE = suffix;

                //插入语句
                studentsEntities.syspic.Add(pic);
                studentsEntities.Entry<syspic>(pic).State = EntityState.Added;
                studentsEntities.SaveChanges();

                imgurl = "https://" + Request.Url.Authority + "/Files/Images/SlideConfig/" + pictureName;
                fileBase.SaveAs(savePath + pictureName);
            }
            else
            {
                imgurl = "0";
            }
            var result = new
            {
                imgurl = imgurl
            };
            return Content(JsonConvert.SerializeObject(result));
        }

        /// <summary>
        /// 读取图片文件
        /// </summary>
        /// <param name="path">图片文件路径</param>
        /// <returns>图片文件</returns>
        public Bitmap ReadImageFile(String path)
        {
            string imageDir = "E:\\codeSpace\\c#\\PersonInfoManage\\PersonInfoManage\\Files\\Images\\SlideConfig\\637008722039806440.jpg";
            //string path1 = imageDir + path + 'jpg';
            Bitmap bitmap = null;
            //try
            //{
            FileStream fileStream = new FileStream(imageDir, FileMode.Create);
            Int32 filelength = 0;
            filelength = (int)fileStream.Length;
            Byte[] image = new Byte[filelength];
            fileStream.Read(image, 0, filelength);
            System.Drawing.Image result = System.Drawing.Image.FromStream(fileStream);
            fileStream.Close();
            bitmap = new Bitmap(result);
            //}
            //catch (Exception ex)
            //{
            //  异常输出
            //}

            return bitmap;
            //return bytes;
        }

        public FileResult GetImg()
        {
            //获取参数
            string url = Request.RawUrl;
            string[] paraArr = url.Split('/');
            int imgId = Convert.ToInt32(paraArr[paraArr.Length - 1]);

            //查询语句
            studentsEntities studentsEntities = new studentsEntities();
            syspic syspic = (from c in studentsEntities.syspic where c.ID == imgId select c).FirstOrDefault();
            if (syspic == null)
            {
                return new FilePathResult("~/Files/Images/SlideConfig/1.jpg", "image/jpg");

            }
            else
            {
                string path = syspic.IMG_PATH + syspic.IMG_NAME;
                return new FilePathResult(path, "image/" + syspic.IMG_TYPE);
            }
        }
    }
}
