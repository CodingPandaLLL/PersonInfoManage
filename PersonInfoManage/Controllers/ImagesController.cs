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
using System.Data.SqlClient;
using Redis;
using Microsoft.AspNetCore.Identity;

namespace ImagesController.Controllers
{
    public class ImagesController : Controller
    {

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fileBase"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImage()
        {
            //获取图片路径
            string savePath = "";

            studentsEntities studentsEntities = new studentsEntities();
            var regResult = studentsEntities.register.SqlQuery("select * from register where NAME=@name", new SqlParameter("@name", "IMGPATH")).FirstOrDefault();
            //打印
            Console.Write("*");
            //savePath = RedisHelper.Get<string>("imgPath");
            if (regResult != null)
            {
                //从redis中获取路径
                //RedisHelper.Set("imgPath", regResult.VALUE, DateTime.Parse("2019/12/03"));
                //RedisHelper.Remove(key);

                savePath = regResult.VALUE;
                Console.WriteLine(regResult.VALUE);
            }

            //接受参数
            HttpPostedFileBase fileBase = Request.Files["image"];
            string imgurl = string.Empty;
            int imgId = 0;
            string imgPath = System.IO.Path.GetFileName(fileBase.FileName);
            int index = imgPath.LastIndexOf('.');
            string suffix = imgPath.Substring(index).ToLower();
            string[] imageTypeArr = imgPath.Split('.');
            string imageType = imgPath.Split('.')[imgPath.Split('.').Length - 1];
            if (suffix == ".jpg" || suffix == ".jpeg" || suffix == ".png" || suffix == ".gif" || suffix == ".bmp")
            {
                string pictureName = DateTime.Now.Ticks.ToString() + suffix; //图片名称

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                //保存图片信息到数据库表
                syspic pic = new syspic();
                pic.IMG_NAME = pictureName;
                pic.IMG_PATH = savePath;
                pic.IMG_TYPE = imageType;

                studentsEntities.syspic.Add(pic);
                studentsEntities.Entry<syspic>(pic).State = EntityState.Added;
                studentsEntities.SaveChanges();

                imgurl = "https://" + Request.Url.Authority + "/Files/Images/SlideConfig/" + pictureName;
                imgId = pic.ID;
                fileBase.SaveAs(savePath + pictureName);
            }
            else
            {
                imgurl = "0";
            }
            var result = new
            {
                imgurl = imgurl,
                imgId = imgId
            };
            return Content(JsonConvert.SerializeObject(result));
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
