using DBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_Error(object sender, EventArgs e)
        {
            ConfigurationHelper config = new ConfigurationHelper();
            string WebUrlPath = config.getConfigName("WebUrl");
            // 在出现未处理的错误时运行的代码
            #region 记录错误日志
            Exception ex = Server.GetLastError().GetBaseException();
            string errortime = "发生时间：" + DateTime.Now.ToString();
            string errorAddress = "发生异常页：" + Request.Url.ToString();
            string errorinfo = "异常信息：" + ex.Message;
            string errorsourse = "错误源：" + ex.Source;
            string errorTrance = "堆栈信息：" + ex.StackTrace;
            string strRole = "没有记录";
            string strUserName = "没有记录";
            Server.ClearError();
            System.IO.StreamWriter writer = null;
            try
            {
                lock (this)
                {
                    //写入日志
                    string Year = DateTime.Now.Year.ToString();
                    string Month = DateTime.Now.Month.ToString();
                    string Day = DateTime.Now.Day.ToString();
                    string Path = string.Empty;
                    string FileName = "错误日志_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                    if (System.IO.Directory.Exists(Server.MapPath(WebUrlPath+"/Error")) == false)//如果不存在就创建Error文件夹
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(WebUrlPath+"/Error"));
                    }
                    Path = Server.MapPath(WebUrlPath+"/Error/") + Year + Month;
                    if (!System.IO.Directory.Exists(Path))
                    {
                        System.IO.Directory.CreateDirectory(Path);
                    }
                    System.IO.FileInfo File = new System.IO.FileInfo(Path + "/" + FileName);
                    writer = new System.IO.StreamWriter(File.FullName, true);
                    //文件不存在则创建，true标示追加。
                    writer.WriteLine("用户IP：" + Request.UserHostAddress);
                    writer.WriteLine(errortime);
                    writer.WriteLine(errorAddress);
                    writer.WriteLine(errorinfo);
                    writer.WriteLine(errorsourse);
                    writer.WriteLine(errorTrance);
                    writer.WriteLine(strUserName);
                    writer.WriteLine(strRole);
                    writer.WriteLine("--------------------------------------------------------------------------------");
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    Response.Write("<script> window.location.href = window.location.href; </ script > "); 
                }
            }
            #endregion
        }

    }
}
