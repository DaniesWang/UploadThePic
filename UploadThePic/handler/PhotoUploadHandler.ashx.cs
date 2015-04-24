using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace UploadThePic.handler
{
    /// <summary>
    /// PhotoUploadHandler 的摘要说明
    /// </summary>
    public class PhotoUploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            try
            {
                //获取当前Post过来的file集合对象,在这里我只获取了<input type='file' name='fileUp'/>的文件控件
                HttpPostedFile file = context.Request.Files["fileUp"];
                if (file != null)
                {
                    //当前文件上传的目录
                    string path = "/upload/images/" + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MM") + "/";
                    string realPath = context.Server.MapPath(path);
                    if (!Directory.Exists(realPath))
                    {
                        Directory.CreateDirectory(realPath);
                    }
                    //当前文件后缀名
                    string ext = Path.GetExtension(file.FileName).ToLower();
                    //当前待上传的服务端路径
                    string imageUrl = path + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
                    //验证文件类型是否正确
                    if (!ext.Equals(".gif") && !ext.Equals(".jpg") && !ext.Equals(".png") && !ext.Equals(".bmp"))
                    {
                        //这里window.parent.uploadSuccess()是我在前端页面中写好的javascript function,此方法主要用于输出异常和上传成功后的图片地址
                        context.Response.Write("<script>window.parent.uploadSuccess('你上传的文件格式不正确！上传格式有(.gif、.jpg、.png、.bmp)');</script>");
                    }
                    //验证文件的大小
                    else if (file.ContentLength > 1048576)
                    {
                        //这里window.parent.uploadSuccess()是我在前端页面中写好的javascript function,此方法主要用于输出异常和上传成功后的图片地址 
                        context.Response.Write("<script>window.parent.uploadSuccess('你上传的文件不能大于1048576KB!请重新上传！');</script>");
                    }
                    else
                    {
                        //开始上传
                        file.SaveAs(context.Server.MapPath(imageUrl));
                        //这里window.parent.uploadSuccess()是我在前端页面中写好的javascript function,此方法主要用于输出异常和上传成功后的图片地址
                        //如果成功返回的数据是需要返回两个字符串，我在这里使用了|分隔  例： 成功信息|/Test/hello.jpg
                        context.Response.Write("<script>window.parent.uploadSuccess('Upload Success!|" + imageUrl + "');</script>");                    
                    }
                }
                else
                {
                    //上传失败
                    context.Response.Write("<script>window.parent.uploadSuccess('上传发生错误！');</script>");
                }
            }
            catch
            {
                //上传失败 
                context.Response.Write("<script>window.parent.uploadSuccess('上传发生错误！');</script>");
            }
            context.ApplicationInstance.CompleteRequest(); 
            return;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}