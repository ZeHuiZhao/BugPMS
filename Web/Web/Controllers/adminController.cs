using DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class adminController : Controller
    {
  
        Maticsoft.BLL.UserInfo BUserInfo = new Maticsoft.BLL.UserInfo();
        Maticsoft.Model.UserInfo MUserInfo = new Maticsoft.Model.UserInfo();
        getMenu getmenus = new getMenu();
        Maticsoft.BLL.Privilege BPrivilege = new Maticsoft.BLL.Privilege();
        Maticsoft.BLL.Role BRole = new Maticsoft.BLL.Role();
        Maticsoft.Model.Role MRole = new Maticsoft.Model.Role();

        Password_Encrypt_ASC.Password_Encrypt_ASC Setpassword = new Password_Encrypt_ASC.Password_Encrypt_ASC();
        Maticsoft.Model.Privilege MPrivilege = new Maticsoft.Model.Privilege();
        Common.dtTojson CdtTojson = new Common.dtTojson();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult adminManage()
        {
            return View();
        }

     
        /// <summary>
        /// 获取管理员列表
        /// </summary>
        /// <returns></returns>
        public ActionResult adminList(int page = 1, string UserName = "", string TrueName = "")
        {
            string AdminID = "0";
            if (Session["UserInfoID"] != null)
            {
                AdminID = Session["UserInfoID"].ToString();
            }
            int pagecount = 0;
            string strWhere = " ID>0 ";
            if (UserName != "")
            {
                strWhere += " and UserName like '%" + UserName + "%'";
            }
            if (TrueName != "")
            {
                strWhere += " and TrueName like '%" + TrueName + "%'";
            }
            if (AdminID != "0")
            {
                DataSet ds = BUserInfo.GetListByPage(strWhere, "ID", ((page - 1) * 20 + 1), page * 20, out pagecount);//分页获取到数据 
                if (ds == null) { }
                else {
                    List<Maticsoft.Model.UserInfo> userinfoManage = BUserInfo.DataTableToList(ds.Tables[0]);//把datatable转化成List
                    ViewData["userinfoList"] = userinfoManage;
                    ViewData["currentpage"] = page;//当前页码
                    ViewData["pagecount"] = pagecount;
                    return View(userinfoManage);
                }
            }
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// 获得用户权限
        /// </summary>
        /// <returns></returns>
        public ActionResult getPrivilege()
        {
            DataTable dt = BPrivilege.GetList(" ParentID=0 ").Tables[0];//" ParentID=0  and APPName='zhonglika' "
            List<Maticsoft.Model.Privilege> PrivilegesList = BPrivilege.DataTableToList(dt);//把dt转化成List
            ViewData["PrivilegeList"] = PrivilegesList;
            return View(PrivilegesList);
        }

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <returns></returns>
        public ActionResult addadmin()
        {
            getPrivilege();
            return View();
        }


        public ActionResult EditAdmin()
        {
            return View();
        }







        public ActionResult adminPrivilegeManage()
        {
            return View();
        }



        public ActionResult RoleManage()
        {
            return View();
        }

        /// <summary>
        /// 获取管理员列表
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleList(int page = 1, string Name = "")
        {

            List<Maticsoft.Model.Role> userinfoManage = new List<Maticsoft.Model.Role>();
            
            string strWhere = " ID>0  ";// and APPName='zhonglika' ";
            if (Name != "")
            {
                strWhere += " and Name like '%" + Name + "%'";
            }

            DataSet ds = BRole.GetListByPage(strWhere, "ID", ((page - 1) * 20 + 1), page * 20);//分页获取到数据 
            int pagecount = BRole.GetRecordCount(strWhere);
            if (ds == null) { }
            else {
                userinfoManage = BRole.DataTableToList(ds.Tables[0]);//把datatable转化成List
                ViewData["currentpage"] = page;//当前页码
                ViewData["pagecount"] = pagecount;
                
            }
            return View(userinfoManage);
        }


        /// <summary>
        /// 角色权限
        /// </summary>
        /// <returns></returns>
        public ActionResult RolePrivilegeManage()
        {
            DataTable dt = BPrivilege.GetList(" ParentID=0 ").Tables[0];//" ParentID=0  and APPName='zhonglika' "
            List<Maticsoft.Model.Privilege> PrivilegesList = BPrivilege.DataTableToList(dt);//把dt转化成List
            return View(PrivilegesList);
        }

    }
}