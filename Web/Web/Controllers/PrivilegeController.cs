using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class PrivilegeController : Controller
    {
        Maticsoft.BLL.Privilege BPrivilege = new Maticsoft.BLL.Privilege();
        Maticsoft.Model.Privilege MPrivilege = new Maticsoft.Model.Privilege();

        // GET: Privilege
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 一级权限
        /// </summary>
        /// <returns></returns>
        public ActionResult FirstPrivilegeManage()
        {
            return View();
        }

        /// <summary>
        /// 一级权限
        /// </summary>
        /// <returns></returns>
        public ActionResult FirstPrivilegeList(int page = 1, string Name = "")
        {
            int pagecount = 0;
            string strWhere = " ParentID=0 ";// and APPName='zhonglika' ";
            if (Name != "")
            {
                strWhere += " and Name like '%" + Name + "%'";
            }
            DataSet ds =BPrivilege .GetListByPage(strWhere, "ID", ((page - 1) * 20 + 1), page * 20, out pagecount);//分页获取到数据 
            if (ds == null) { }
            else {
                List<Maticsoft.Model.Privilege> userinfoManage = BPrivilege.DataTableToList(ds.Tables[0]);//把datatable转化成List
                ViewData["userinfoList"] = userinfoManage;
                ViewData["currentpage"] = page;//当前页码
                ViewData["pagecount"] = pagecount;
                return View(userinfoManage);
            }
            return View();
        }




        /// <summary>
        /// 二级权限
        /// </summary>
        /// <returns></returns>
        public ActionResult TwoPrivilegeManage()
        {
            return View();
        }

        /// <summary>
        /// 二级权限
        /// </summary>
        /// <returns></returns>
        public ActionResult TwoPrivilegeList(int page = 1, string Name = "")
        {
            int pagecount = 0;
            string strWhere = " ParentID!=0 ";// and APPName='zhonglika' ";
            if (Name != "")
            {
                strWhere += " and Name like '%" + Name + "%'";
            }
            DataSet ds = BPrivilege.GetListByPage(strWhere, "ParentID,DisplayOrder", ((page - 1) * 20 + 1), page * 20, out pagecount);//分页获取到数据 
            if (ds == null) { }
            else {
                List<Maticsoft.Model.Privilege> userinfoManage = BPrivilege.DataTableToList(ds.Tables[0]);//把datatable转化成List
                ViewData["userinfoList"] = userinfoManage;
                ViewData["currentpage"] = page;//当前页码
                ViewData["pagecount"] = pagecount;
                return View(userinfoManage);
            }
            return View();
        }




    }
}