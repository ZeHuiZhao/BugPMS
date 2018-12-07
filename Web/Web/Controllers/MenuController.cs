using DBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class MenuController : Controller
    {
        getMenu getmenus = new getMenu();
        // GET: Menu
        public ActionResult Menu()
        {
            try
            {
                string AdminID = "";
                if (Session["UserInfoID"] == null)
                {
                    ConfigurationHelper config = new ConfigurationHelper();
                    string WebUrlPath = config.getConfigName("WebUrl");
                    Response.Redirect(WebUrlPath + "/Window/Login");
                    return View();
                }
                else
                {
                    AdminID = Session["UserInfoID"].ToString();
                }
                ViewData["menu"] = getmenus.getMenuByLoginID(AdminID, Session["TrueName"].ToString());
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}