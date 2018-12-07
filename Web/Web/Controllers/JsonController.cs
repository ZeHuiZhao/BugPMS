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
    public class JsonController : Controller
    {
        ConfigurationHelper config = new ConfigurationHelper();
        
        Maticsoft.BLL.UserInfo BUserInfo = new Maticsoft.BLL.UserInfo();
        Maticsoft.Model.UserInfo MUserInfo = new Maticsoft.Model.UserInfo();
        List<Maticsoft.Model.UserInfo> MUserInfoList = new List<Maticsoft.Model.UserInfo>();
        getMenu getmenus = new getMenu();
        Maticsoft.BLL.Privilege BPrivilege = new Maticsoft.BLL.Privilege();
        Maticsoft.BLL.Role BRole = new Maticsoft.BLL.Role();
        Maticsoft.Model.Role MRole = new Maticsoft.Model.Role();

        Password_Encrypt_ASC.Password_Encrypt_ASC Setpassword = new Password_Encrypt_ASC.Password_Encrypt_ASC();
        Maticsoft.Model.Privilege MPrivilege = new Maticsoft.Model.Privilege();
        Common.dtTojson CdtTojson = new Common.dtTojson();

        Maticsoft.BLL.OperateRecord BOperateRecord = new Maticsoft.BLL.OperateRecord();
        Maticsoft.Model.OperateRecord MOperateRecord = new Maticsoft.Model.OperateRecord();
        // GET: Json
        public ActionResult Index()
        {
            return View();
        }
        #region 获取session

        public ActionResult GetSessionName()
        {
            string result = "";
            if (Session["UserInfoID"] != null)
            {
                result = "success";
            }
            return Json(new { Result = result}, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region   用户、角色及权限相关接口
        /// <summary>
        /// 根据用户的ID，重置密码
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult ResertUserInfoPassword(string ID)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            MUserInfo = BUserInfo.GetModel(Convert.ToInt32(ID));//获取对象实体
            string password = Setpassword.set_password_ASC("123456");//重置为123456
            MUserInfo.Password = password;
            bool check = BUserInfo.Update(MUserInfo);
            if (check)//如果修改成功
            {
                message = "密码重置成功";
                result = "success";
                address = "/admin/adminManage";
            }
            else
            {
                message = "系统异常请稍后再试";
                result = "error";
                address = "/admin/adminManage";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldpassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="checkPassword">验证密码</param>
        /// <returns></returns>
        public ActionResult ChangePassword(string oldpassword, string newPassword, string checkPassword)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            string AdminID = "0";
            if (Session["UserInfoID"] != null)
            {
                AdminID = Session["UserInfoID"].ToString();
            }
            ViewData["menu"] = getmenus.getMenuByLoginID(AdminID, Session["TrueName"].ToString());
            #region 当输入内容都不为空时，修改密码
            if (oldpassword != null && newPassword != null && checkPassword != null)
            {
                string OldPassword = Setpassword.set_password_ASC(oldpassword);//原密码
                DataSet ds = BUserInfo.GetList("ID=" + AdminID);
                DataTable dt = ds.Tables[0];
                if (OldPassword != dt.Rows[0]["Password"].ToString())
                {
                    message = "原密码错误";
                    result = "errorPassword";
                    address = "#";
                }
                else if (newPassword != checkPassword)
                {
                    message = "两次输入的密码不一致";
                    result = "false";
                    address = "#";
                }
                else
                {
                    string password = Setpassword.set_password_ASC(newPassword);
                    MUserInfo = BUserInfo.GetModel(Convert.ToInt32(AdminID));
                    MUserInfo.Password = password;
                    bool check = BUserInfo.Update(MUserInfo);
                    if (check)//修改成功
                    {
                        message = "修改成功,请重新登录";
                        result = "success";
                        address = "/Window/LogOut";
                    }
                    else
                    {
                        message = "修改失败";
                        result = "fail";
                        address = "#";
                    }
                }
                string WebUrlPath = config.getConfigName("WebUrl");
                address = WebUrlPath + address;
                return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string WebUrlPath = config.getConfigName("WebUrl");
                address = WebUrlPath + address;
                return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
            }
            #endregion
        }

        /// <summary>
        /// 添加后台管理员
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="roleID">用户角色</param>
        /// <param name="TrueName">真实姓名</param>
        /// <param name="Department">部门</param>
        /// <returns></returns>
        public JsonResult addadmins(string username, string password, string roleID, string TrueName,string DepartmentID)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            DataTable dt = BUserInfo.GetList("UserName='" + username + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                message = "用户已存在";
                result = "errorcheck";
                address = "/admin/adminManage";
            }
            else
            {
                if (username != null && password != null && roleID != null && TrueName != null)
                {

                    MUserInfo.ParentID = Convert.ToInt32(Session["UserInfoID"].ToString());
                    MUserInfo.Password = Setpassword.set_password_ASC(password);
                    MUserInfo.UserName = username;
                    MUserInfo.RoleID = Convert.ToInt32(roleID);
                    MUserInfo.PrivilegeID = "3";
                    MUserInfo.status = 1;
                    MUserInfo.Time = System.DateTime.Now;
                    MUserInfo.TrueName = TrueName;
                    MUserInfo.DepartmentID= Convert.ToInt32(DepartmentID);
                    int id = BUserInfo.Add(MUserInfo);//添加用户
                    if (id > 0)
                    {
                        message = "添加成功";
                        result = "success";
                        address = "/admin/adminManage";
                    }
                    else
                    {
                        message = "添加失败";
                        result = "error";
                        address = "/admin/adminManage";
                    }
                }
                else
                {
                    message = "参数不合法";
                    result = "false";
                    address = "/admin/adminManage";
                }
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 根据ID获取用户的基本数据
        /// 用来进行编辑使用
        /// </summary>
        public string getUserInfoMessage(string ID)
        {
            DataTable dt = BUserInfo.GetList("ID=" + ID + "").Tables[0];
            string jsonUserMessage = CdtTojson.DataTableToJson(dt);
            return jsonUserMessage;
        }



        /// <summary>
        /// 修改管理员基本信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="roleID">角色</param>
        /// <param name="TrueName">真实姓名</param>
        /// <returns></returns>
        public JsonResult updateadmins(string ID, string roleID, string TrueName,string DepartmentID)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            MUserInfo = BUserInfo.GetModel(Convert.ToInt32(ID));
            MUserInfo.TrueName = TrueName;
            MUserInfo.RoleID = Convert.ToInt32(roleID);
            MUserInfo.DepartmentID = Convert.ToInt32(DepartmentID);
            bool check = BUserInfo.Update(MUserInfo);
            if (check)
            {
                message = "修改成功";
                result = "success";
                address = "/admin/adminManage";
            }
            else
            {
                message = "修改失败";
                result = "error";
                address = "/admin/adminManage";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }




        /// <summary>
        /// 重置用户的状态，如果是1改为0，如果是0改为1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ResertUserInfostatus(string id)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            try
            {
                MUserInfo = BUserInfo.GetModel(Convert.ToInt32(id));//获取对象实体
                if (MUserInfo.status == 1)
                {
                    MUserInfo.status = 0;
                }
                else
                {
                    MUserInfo.status = 1;
                }
                bool check = BUserInfo.Update(MUserInfo);
                if (check)//如果修改成功
                {
                    message = "审核成功";
                    result = "success";
                    address = "/admin/adminManage";
                }
                else
                {
                    message = "网络繁忙请稍后再试";
                    result = "error";
                    address = "/admin/adminManage";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
                address = "/admin/adminManage";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 获取权限，转换成json字符串:待优化
        /// </summary>
        /// <returns></returns>
        public string GetPrivilege()
        {
            StringBuilder json = new StringBuilder();//json字符串
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Name,ParentID ");
            strSql.Append(" FROM Privilege WHERE ParentID=0 AND Enable=1 ORDER BY  DisplayOrder  ");
            //strSql.Append(" FROM Privilege WHERE ParentID=0 AND Enable=1  and APPName='zhonglika' ORDER BY  DisplayOrder  ");
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            int i = 0;
            json.Append("[");
            foreach (DataRow dr in dt.Rows)
            {
                if (i == 0)
                {
                    DataTable privilege = DbHelperSQL.Query(@"select ID,Name FROM Privilege WHERE ParentID=" + dr["ID"].ToString()
                        + " AND Enable=1 ORDER BY  DisplayOrder ").Tables[0];
                    //+ " AND Enable=1  and APPName='zhonglika' ORDER BY  DisplayOrder ").Tables[0];
                    string jjson = CdtTojson.DataTableToJsonNo(privilege);
                    json.Append("{");
                    json.Append("\"ID\"");
                    json.Append(":");
                    json.Append("\"" + dr["ID"].ToString() + "\"");//ID值
                    json.Append(",");
                    json.Append("\"Name\"");//Name
                    json.Append(":");
                    json.Append("\"" + dr["Name"].ToString() + "\"");//ID值
                    json.Append(",");
                    //json.Append("child");
                    json.Append("\"child\"");
                    json.Append(":");
                    json.Append(jjson);//ID值 
                    json.Append("}");
                }
                else
                {
                    DataTable privilege = DbHelperSQL.Query("select ID,Name FROM Privilege WHERE ParentID=" + dr["ID"].ToString()
                    //+ " AND Enable=1  and APPName='zhonglika' ORDER BY  DisplayOrder ").Tables[0];
                    + " AND Enable=1  ORDER BY  DisplayOrder ").Tables[0];
                    string jjson = CdtTojson.DataTableToJsonNo(privilege);
                    json.Append(",{");
                    json.Append("\"ID\"");
                    json.Append(":");
                    json.Append("\"" + dr["ID"].ToString() + "\"");//ID值
                    json.Append(",");
                    json.Append("\"Name\"");//Name
                    json.Append(":");
                    json.Append("\"" + dr["Name"].ToString() + "\"");//ID值
                    json.Append(",");
                    //json.Append("child");
                    json.Append("\"child\"");
                    json.Append(":");
                    json.Append(jjson);//ID值 
                    json.Append("}");
                }
                i++;
            }
            json.Append("]");
            return json.ToString();
        }


        /// <summary>
        /// 获取管理员，用来设置权限
        /// </summary>
        /// <returns></returns>
        public string getUserInfo()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,UserName  FROM UserInfo ");
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            string json = CdtTojson.DataTableToJson(dt);
            return json;
        }


        /// <summary>
        /// 根据ID获取权限值
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string getUserInfoPrivilege(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PrivilegeID  FROM UserInfo Where ID=" + ID + " ");
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            /*  string json = CdtTojson.DataTableToJson(dt)*/
            return dt.Rows[0]["PrivilegeID"].ToString();
        }



        /// <summary>
        /// 重置用户的权限
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PrivilegeID"></param>
        /// <returns></returns>
        public ActionResult ResertUserInfoPrivilege(string ID, string PrivilegeID)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            try
            {
                MUserInfo = BUserInfo.GetModel(Convert.ToInt32(ID));
                MUserInfo.PrivilegeID = PrivilegeID;
                bool check = BUserInfo.Update(MUserInfo);
                if (check)//如果修改成功
                {
                    message = "设置成功";
                    result = "success";
                    address = "/admin/adminPrivilegeManage";
                }
                else
                {
                    message = "设置失败";
                    result = "false";
                    address = "/admin/adminPrivilegeManage";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
                address = "/admin/adminPrivilegeManage";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ActionResult addRole(string Name)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            try
            {
                MRole.Name = Name;
                MRole.APPName = "";
                int check = BRole.Add(MRole);
                if (check > 0)//如果修改成功
                {
                    message = "添加成功";
                    result = "success";
                    address = "/admin/RoleManage";
                }
                else
                {
                    message = "添加失败";
                    result = "false";
                    address = "/admin/RoleManage";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
                address = "/admin/RoleManage";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 修改角色（部门）
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name">名称</param>
        /// <returns></returns>
        public ActionResult UpdateRole(string ID, string Name)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            try
            {

                MRole = BRole.GetModel(Convert.ToInt32(ID));
                MRole.Name = Name;
                bool check = BRole.Update(MRole);
                if (check)//如果修改成功
                {
                    message = "修改成功";
                    result = "success";
                    address = "/admin/RoleManage";
                }
                else
                {
                    message = "修改失败";
                    result = "false";
                    address = "/admin/RoleManage";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
                address = "/admin/RoleManage";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 角色ID,角色权限
        /// 设置角色的权限
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="privilege"></param>
        /// <returns></returns>
        public ActionResult ResetRolePrivilege(string ID, string privilege)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            try
            {

                MRole = BRole.GetModel(Convert.ToInt32(ID));
                MRole.PrivilegeID = privilege;
                bool check = BRole.Update(MRole);
                if (check)//如果修改成功
                {
                    message = "修改成功";
                    result = "success";
                    address = "/admin/RolePrivilegeManage";
                }
                else
                {
                    message = "修改失败";
                    result = "false";
                    address = "/admin/RolePrivilegeManage";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
                address = "/admin/RolePrivilegeManage";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取角色对应的基本信息
        /// 用来进行修改,设置权限
        /// </summary>
        /// <returns></returns>
        public string getRole(string ID)
        {
            DataTable dt = BRole.GetList("ID=" + ID + "").Tables[0];
            string json = CdtTojson.DataTableToJson(dt);
            return json;
        }



        /// <summary>
        /// 获取角色的基本数据
        /// json类型，用来设置角色权限
        /// </summary>
        /// <returns></returns>
        public string getRoleList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Name ");
            strSql.Append(" FROM Role ");
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            string json = CdtTojson.DataTableToJson(dt);
            return json;
        }

        /// 获取部门的基本数据
        /// json类型，用来设置部门权限
        /// </summary>
        /// <returns></returns>
        public string getDepartmentList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Name ");
            strSql.Append(" FROM Department where IsActive=1 order by Display desc ");
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            string json = CdtTojson.DataTableToJson(dt);
            return json;
        }

        /// <summary>
        /// 根据拼接的ID值，
        /// 删除角色
        /// </summary>
        /// <param name="IDS"></param>
        /// <returns></returns>
        public ActionResult deleteRole(string IDS)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            try
            {
                bool check = BRole.DeleteList(IDS);
                if (check)//如果修改成功
                {
                    message = "删除成功";
                    result = "success";
                    address = "/admin/RoleManage";
                }
                else
                {
                    message = "删除失败";
                    result = "false";
                    address = "/admin/RoleManage";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
                address = "/admin/RoleManage";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Privilege 权限相关

        /// <summary>
        /// 添加一级权限
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="DisplayOrder"></param>
        /// <param name="CssStyle"></param>
        /// <returns></returns>
        public JsonResult AddFirstPrivilege(string Name, string DisplayOrder, string CssStyle)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            try
            {
                MPrivilege.CssStyle = CssStyle;
                MPrivilege.Name = Name;
                MPrivilege.DisplayOrder = Convert.ToInt32(DisplayOrder);
                MPrivilege.ParentID = 0;
                MPrivilege.Enable = 0;
                MPrivilege.Href = "#";
                MPrivilege.APPName = "";
                int check = BPrivilege.Add(MPrivilege);
                if (check > 0)
                {
                    message = "添加成功";
                    result = "success";
                    address = "/Privilege/FirstPrivilegeManage";
                }
                else
                {
                    message = "添加失败";
                    result = "false";
                    address = "/Privilege/FirstPrivilegeManage";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
                address = "/Privilege/FirstPrivilegeManage";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 获取一级菜单
        /// 用来添加二级菜单时候使用
        /// </summary>
        /// <returns></returns>
        public string getFirstPrivilege()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Name ");
            strSql.Append(" FROM Privilege where Enable=1 and ParentID=0 ");
            //strSql.Append(" FROM Privilege where Enable=1 and APPName='zhonglika' and ParentID=0 ");
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            string json = CdtTojson.DataTableToJson(dt);
            return json;
        }


        /// <summary>
        /// 添加二级菜单
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="DisplayOrder">排序</param>
        /// <param name="ParentID">父级ID</param>
        /// <param name="Href">链接</param>
        /// <returns></returns>
        public JsonResult AddTwoPrivilege(string Name, string DisplayOrder, string ParentID, string Href)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            try
            {
                MPrivilege.Name = Name;
                MPrivilege.DisplayOrder = Convert.ToInt32(DisplayOrder);
                MPrivilege.ParentID = Convert.ToInt32(ParentID);
                MPrivilege.Href = Href;
                MPrivilege.Enable = 0;
                MPrivilege.APPName = "";
                int check = BPrivilege.Add(MPrivilege);
                if (check > 0)
                {
                    message = "添加成功";
                    result = "success";
                    address = "/Privilege/TwoPrivilegeManage";
                }
                else
                {
                    message = "添加失败";
                    result = "false";
                    address = "/Privilege/TwoPrivilegeManage";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
                address = "/Privilege/TwoPrivilegeManage";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 审核一级菜单
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public JsonResult ResetFirstPrivilegestatus(string ID)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            try
            {
                MPrivilege = BPrivilege.GetModel(Convert.ToInt32(ID));
                if (MPrivilege.Enable == 1)
                { MPrivilege.Enable = 0; }
                else { MPrivilege.Enable = 1; }
                bool check = BPrivilege.Update(MPrivilege);
                if (check)
                {
                    message = "设置成功";
                    result = "success";
                    address = "/Privilege/FirstPrivilegeManage";
                }
                else
                {
                    message = "设置失败";
                    result = "false";
                    address = "/Privilege/FirstPrivilegeManage";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
                address = "/Privilege/FirstPrivilegeManage";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 审核二级菜单
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public JsonResult ResetTwoPrivilegestatus(string ID)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            try
            {
                MPrivilege = BPrivilege.GetModel(Convert.ToInt32(ID));
                if (MPrivilege.Enable == 1)
                { MPrivilege.Enable = 0; }
                else { MPrivilege.Enable = 1; }
                bool check = BPrivilege.Update(MPrivilege);
                if (check)
                {
                    message = "设置成功";
                    result = "success";
                    address = "/Privilege/TwoPrivilegeManage";
                }
                else
                {
                    message = "设置失败";
                    result = "false";
                    address = "/Privilege/TwoPrivilegeManage";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
                address = "/Privilege/TwoPrivilegeManage";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取基本信息
        /// 用来修改信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetMessage(string ID)
        {
            DataTable dt = BPrivilege.GetList("ID=" + ID + "").Tables[0];
            string json = CdtTojson.DataTableToJson(dt);
            return json;
        }

        /// <summary>
        /// 修改一级菜单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="DisplayOrder"></param>
        /// <param name="CssStyle"></param>
        /// <returns></returns>
        public JsonResult updateFirstPrivilege(string ID, string Name, string DisplayOrder, string CssStyle)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            try
            {
                MPrivilege = BPrivilege.GetModel(Convert.ToInt32(ID));
                MPrivilege.CssStyle = CssStyle;
                MPrivilege.Name = Name;
                MPrivilege.DisplayOrder = Convert.ToInt32(DisplayOrder);
                bool check = BPrivilege.Update(MPrivilege);
                if (check)
                {
                    message = "修改成功";
                    result = "success";
                    address = "/Privilege/FirstPrivilegeManage";
                }
                else
                {
                    message = "修改失败";
                    result = "false";
                    address = "/Privilege/FirstPrivilegeManage";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
                address = "/Privilege/FirstPrivilegeManage";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改二级菜单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="DisplayOrder"></param>
        /// <param name="ParentID"></param>
        /// <param name="Href"></param>
        /// <returns></returns>
        public JsonResult UpdateTwoPrivilege(string ID, string Name, string DisplayOrder, string ParentID, string Href)
        {
            string message = string.Empty;
            string address = string.Empty;
            string result = string.Empty;
            try
            {
                MPrivilege = BPrivilege.GetModel(Convert.ToInt32(ID));
                MPrivilege.Name = Name;
                MPrivilege.DisplayOrder = Convert.ToInt32(DisplayOrder);
                MPrivilege.ParentID = Convert.ToInt32(ParentID);
                MPrivilege.Href = Href;

                bool check = BPrivilege.Update(MPrivilege);
                if (check)
                {
                    message = "设置成功";
                    result = "success";
                    address = "/Privilege/TwoPrivilegeManage";
                }
                else
                {
                    message = "设置失败";
                    result = "false";
                    address = "/Privilege/TwoPrivilegeManage";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
                address = "/Privilege/TwoPrivilegeManage";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region 部门相关
        Maticsoft.BLL.Department BDepartment = new Maticsoft.BLL.Department();
        Maticsoft.Model.Department MDepartment = new Maticsoft.Model.Department();

        #region 删除部门
        /// <summary>
        /// 根据拼接的ID值，
        /// 删除部门
        /// </summary>
        /// <param name="IDS"></param>
        /// <returns></returns>
        public ActionResult DelDepartment(string IDs)
        {
            string message = string.Empty;
            string address = "/Data/DepartmentList";
            string result = string.Empty;
            try
            {
                bool check = BDepartment.DeleteList(IDs);
                if (check)//如果修改成功
                {
                    message = "删除成功";
                    result = "success";
                }
                else
                {
                    message = "删除失败";
                    result = "false";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取部门的基本信息
        /// <summary>
        /// 获取部门的基本信息
        /// 用来进行修改
        /// </summary>
        /// <returns></returns>
        public string getDepartment(string ID)
        {
            DataTable dt = BDepartment.GetList("ID=" + ID + "").Tables[0];
            string json = CdtTojson.DataTableToJson(dt);
            return json;
        }
        #endregion

        #region 新增和修改部门信息

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ActionResult addDepartment(string Name,string Levels,string IsActive,string Display,string PreID)
        {
            string message = string.Empty;
            string address = "/Data/DepartmentList";
            string result = string.Empty;
            try
            {
                MDepartment.Name = Name;
                MDepartment.Levels = int.Parse(Levels);
                MDepartment.IsActive = int.Parse(IsActive);
                MDepartment.Display = int.Parse(Display);
                MDepartment.PreID = int.Parse(PreID);
                int check = BDepartment.Add(MDepartment);
                if (check > 0)//如果修改成功
                {
                    message = "添加成功";
                    result = "success";
                }
                else
                {
                    message = "添加失败";
                    result = "false";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 修改角色（部门）
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name">名称</param>
        /// <returns></returns>
        public ActionResult UpdateDepartment(string ID, string Name, string Levels, string IsActive, string Display, string PreID)
        {
            string message = string.Empty;
            string address = "/Data/DepartmentList";
            string result = string.Empty;
            try
            {

                MDepartment.ID = Convert.ToInt32(ID);
                MDepartment.Name = Name;
                MDepartment.Levels = int.Parse(Levels);
                MDepartment.IsActive = int.Parse(IsActive);
                MDepartment.Display = int.Parse(Display);
                MDepartment.PreID = int.Parse(PreID);
                bool check = BDepartment.Update(MDepartment);
                if (check)//如果修改成功
                {
                    message = "修改成功";
                    result = "success";
                }
                else
                {
                    message = "修改失败";
                    result = "false";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region 字典表维护
        Maticsoft.BLL.BaseData BBaseData = new Maticsoft.BLL.BaseData();
        Maticsoft.Model.BaseData MBaseData = new Maticsoft.Model.BaseData();

        #region 删除字典
        /// <summary>
        /// 根据拼接的ID值，
        /// 删除字典
        /// </summary>
        /// <param name="IDS"></param>
        /// <returns></returns>
        public ActionResult DelDictionary(string IDs)
        {
            string message = string.Empty;
            string address = "/Data/DictionaryList";
            string result = string.Empty;
            try
            {
                bool check = BBaseData.DeleteList(IDs);
                if (check)//如果修改成功
                {
                    message = "删除成功";
                    result = "success";
                }
                else
                {
                    message = "删除失败";
                    result = "false";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取字典的基本信息
        /// <summary>
        /// 获取字典的基本信息
        /// 用来进行修改
        /// </summary>
        /// <returns></returns>
        public string getDictionary(string ID)
        {
            DataTable dt = BBaseData.GetList("ID=" + ID + "").Tables[0];
            string json = CdtTojson.DataTableToJson(dt);
            return json;
        }

        /// <summary>
        /// 根据TableName获取字典的基本数据
        /// </summary>
        /// <param name="TableName">表名称</param>
        /// <returns></returns>
        public string getBaseData(string TableName,string strWhere="")
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [KeyID],[Name],[ClassName]  FROM [BaseData] WHERE IsActive=1 AND TableName='" + TableName + "' " + strWhere + " ORDER BY Display ");
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            string json = CdtTojson.DataTableToJson(dt);
            return json;
        }
        #endregion

        #region 新增和修改部门信息

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ActionResult addDictionary(string TableName, string Name, string KeyID, string IsActive, string Display)
        {
            string message = string.Empty;
            string address = "/Data/DictionaryList"; 
            string result = string.Empty;
            try
            {
                MBaseData.TableName = TableName;
                MBaseData.KeyID = int.Parse(KeyID);
                MBaseData.Name = Name;
                MBaseData.IsActive = int.Parse(IsActive);
                MBaseData.Display = int.Parse(Display);
                int check = BBaseData.Add(MBaseData);
                if (check > 0)//如果修改成功
                {
                    message = "添加成功";
                    result = "success";
                }
                else
                {
                    message = "添加失败";
                    result = "false";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 修改角色（部门）
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name">名称</param>
        /// <returns></returns>
        public ActionResult UpdateDictionary(string ID, string TableName, string Name, string KeyID, string IsActive, string Display)
        {
            string message = string.Empty;
            string address = "/Data/DictionaryList";
            string result = string.Empty;
            try
            {
                MBaseData = BBaseData.GetModel(Convert.ToInt32(ID));
                if (MBaseData != null)
                {
                    MBaseData.TableName = TableName;
                    MBaseData.KeyID = int.Parse(KeyID);
                    MBaseData.Name = Name;
                    MBaseData.IsActive = int.Parse(IsActive);
                    MBaseData.Display = int.Parse(Display);
                    bool check = BBaseData.Update(MBaseData);
                    if (check)//如果修改成功
                    {
                        message = "修改成功";
                        result = "success";
                    }
                    else
                    {
                        message = "修改失败";
                        result = "false";
                    }
                }
                else
                {
                    message = "参数非法";
                    result = "false";
                }
            }
            catch
            {
                message = "系统异常";
                result = "error";
            }
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #endregion

        #region 获取老师信息
        /// <summary>
        /// 获取老师信息
        /// </summary>
        /// <returns></returns>
        public string getUserList(string strWhere = "")
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID] ,[TrueName] FROM [UserInfo] where [status]=1 " + strWhere);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            string json = CdtTojson.DataTableToJson(dt);
            return json;
        }
        #endregion

        #region 项目相关
        Maticsoft.BLL.Project BProject = new Maticsoft.BLL.Project();
        Maticsoft.Model.Project MProject = new Maticsoft.Model.Project();
        
        

        #region 新增项目
        public ActionResult AddProject(string Name,string ProjectTypeID, string LeaderID, string CheckUserID, string PlanStartTime, string PlanEndTime, string Contents,string ID="",string Code="")
        {
            #region
            string message = string.Empty;
            string address = "/Project/ProjectList";
            string result = string.Empty;
            try
            {
                #region
                if (ID != "")
                {
                    MProject = BProject.GetModel(int.Parse(ID));
                }
                MProject.Name = Name;
                MProject.ProjectTypeID = int.Parse(ProjectTypeID);
                MProject.CheckUserID= int.Parse(CheckUserID);
                MProject.PlanStartTime = Convert.ToDateTime(PlanStartTime);
                MProject.PlanEndTime = Convert.ToDateTime(PlanEndTime);
                MProject.LeaderID = int.Parse(LeaderID);// int.Parse(Session["UserInfoID"].ToString());//
                MProject.Contents = Contents;
                if (ID == "")//说明是添加
                {
                   
                    MProject.Code = getProjectCode();
                    MProject.CreateUserID = int.Parse(Session["UserInfoID"].ToString());
                    MProject.CreateDate = System.DateTime.Now;

                    //MProject.RealStartTime =Convert.ToDateTime("1970-01-01");
                    //MProject.RealEndTime = Convert.ToDateTime("1970-01-01");
                    MProject.CheckNote = "";

                    MProject.ProjectStatusID = 1;
                    MProject.ProjectCheckStatus = 5;//审核状态为5，初始值，仅此一次
                    MProject.EditUserID = 0;

                    int MID = BProject.Add(MProject);
                    if (MID > 0)
                    {
                        AddOperateRecord("添加本项目", "Project", MID);
                        message = "新增成功";
                        result = "success";
                    }
                    else
                    {
                        message = "新增失败";
                        result = "false";
                    }
                }
                else
                {//修改项目内容
                    MProject.EditUserID = int.Parse(Session["UserInfoID"].ToString());
                    MProject.EditDate = System.DateTime.Now;
                    bool res = BProject.Update(MProject);
                    if (res)
                    {
                        AddOperateRecord("修改了项目内容", "Project", MProject.ID);
                        message = "修改成功";
                        result = "success";
                    }
                    else
                    {
                        message = "修改失败";
                        result = "false";
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                message = "系统异常，请返回重试";
                result = "error";
            }
            #endregion
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 删除项目
        public ActionResult ProjectDel(string ProjectID)
        {
            #region
            string message = "";
            string address = "/Project/ProjectList";
            string result = "error";
            try
            {
                #region
                int PID = 0;
                if (!String.IsNullOrEmpty(ProjectID) && int.TryParse(ProjectID, out PID))
                {
                    MProject = BProject.GetModel(PID);
                    bool res = BProject.Delete(PID);
                    if (res)
                    {
                        AddOperateRecord("删除了项目："+ MProject.Name, "Project", PID);
                        
                        message = "删除成功";
                        result = "success";
                    }
                    else
                    {
                        message = "删除失败，请返回重试";
                        result = "error";
                    }
                }
                else
                {
                    message = "参数非法";
                    result = "error";
                }
                #endregion
            }
            catch (Exception ex)
            {
                message = "系统异常，请返回重试";
                result = "error";
            }
            #endregion
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 修改项目状态
        public ActionResult updateProjectStatus(string ProjectID, string StatusID)
        {
            #region
            string message = "";
            string address = "/Project/ProjectList";
            string result = "error";
            try
            {
                #region
                string res = ChangeProjectStatus(ProjectID,StatusID);
                if(res== "empty")
                {
                    message = "参数不完整，请填写完整";
                    result = "error";
                }
                else if (res == "warning")
                {
                    message = "参数非法";
                    result = "error";
                }
                else if (res == "error")
                {
                    message = "修改失败，请返回重试";
                    result = "error";
                }
                else if (res == "success")
                {
                    message = "修改成功";
                    result = "success";
                }
                #endregion
            }
            catch (Exception ex)
            {
                message = "系统异常，请返回重试";
                result = "error";
            }
            #endregion
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }
        string ChangeProjectStatus(string ProjectID, string StatusID,string flag="")
        {
            string result = "";
            if (!String.IsNullOrEmpty(ProjectID) && !String.IsNullOrEmpty(StatusID))
            {
                MProject = BProject.GetModel(int.Parse(ProjectID));

                if (MProject != null)
                {
                    if (flag == "check")
                    {
                        MProject.ProjectCheckStatus = int.Parse(StatusID);
                    }
                    else
                    {
                        MProject.ProjectStatusID = int.Parse(StatusID);
                    }
                    MProject.EditUserID = int.Parse(Session["UserInfoID"].ToString());
                    MProject.EditDate = System.DateTime.Now;
                    bool res = BProject.Update(MProject);
                    if (res)
                    {
                        if (flag == "check")
                        {
                            AddOperateRecord("修改了项目审核状态，状态值改为：" + StatusID, "Project", MProject.ID);
                        }
                        else
                        {
                            AddOperateRecord("修改了项目状态，状态值改为：" + StatusID, "Project", MProject.ID);
                        }
                        result = "success";
                    }
                    else
                    {//修改失败，请返回重试"
                        result = "error";
                    }
                }
                else
                {//参数非法
                    result = "warning";
                }
            }
            else
            {//参数不完整，请填写完整"
                result = "empty";
            }
            return result;
        }
        #endregion

        #region 修改项目审核状态
        public ActionResult CheckPlan(string operate, string checkNote, string strPlanid,string ProjectID)
        {
            #region
            string message = "";
            string address = "/Project/ProjectList";
            string result = "error";
            try
            {
                #region
                if (!String.IsNullOrEmpty(strPlanid) && !String.IsNullOrEmpty(operate))
                {
                    strPlanid = strPlanid.Trim();
                    strPlanid = strPlanid.Remove(strPlanid.Length-1, 1);
                    int ProjectTaskCheckStatusID = 0;
                    if (operate == "checkyes")
                    {
                        ProjectTaskCheckStatusID = 2;
                    }
                    else if (operate == "checkno")
                    {
                        ProjectTaskCheckStatusID = 3;
                    }
                    string sql = "UPDATE [ProjectTask] SET ProjectTaskCheckStatusID= "+ ProjectTaskCheckStatusID 
                        + ",CheckDate=GETDATE(),CheckNote='"+ checkNote 
                        + "' WHERE ID IN ("+ strPlanid + ")";
                    int num= DbHelperSQL.ExecuteSql(sql);
                    if (num > 0)
                    {
                        AddOperateRecord("审核了项目计划:计划编号为："+ strPlanid, "Project",int.Parse(ProjectID));
                        message = "审核成功【"+num+"】条";
                        result = "success";
                    }
                    else
                    {
                        message = "审核失败";
                        result = "error";
                    }
                }
                else
                {
                    message = "参数不完整，请填写完整";
                    result = "error";
                }
                #endregion
            }
            catch (Exception ex)
            {
                message = "系统异常，请返回重试";
                result = "error";
            }
            #endregion
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 提交审核
        public ActionResult ProjectSubmit(string ProjectID)
        {
            #region
            string message = "";
            string address = "/Project/ProjectList";
            string result = "error";
            try
            {
                #region
                int PID = 0;
                if (!String.IsNullOrEmpty(ProjectID) && int.TryParse(ProjectID, out PID))
                {
                    MProject = BProject.GetModel(PID);
                    MProjectTaskList = BProjectTask.GetModelList(" ProjectID='" + PID + "' ");
                    MProject.ProjectCheckStatus = 2;//待审核
                    if (MProjectTaskList != null && MProjectTaskList.Count > 0)
                    {
                        #region
                        bool res = BProject.Update(MProject);
                        if (res)
                        {
                            AddOperateRecord("提交了项目", "Project", PID);
                            #region 修改任务状态(除了已通过的，都改为未审核的)
                            string sql = @"  UPDATE ProjectTask SET ProjectTaskCheckStatusID=1,DealStatusID=1,CheckDate=null,CheckNote='' WHERE 
                            ProjectID='" + PID + @"'  AND ProjectTaskCheckStatusID!=2";
                            DbHelperSQL.ExecuteSql(sql);
                            #endregion
                            message = "提交成功";
                            result = "success";
                        }
                        else
                        {
                            message = "提交失败，请返回重试";
                            result = "error";
                        }
                        #endregion
                    }
                    else
                    {
                        message = "未填写计划，无法提交审核";
                        result = "error";
                    }
                }
                else
                {
                    message = "参数非法";
                    result = "error";
                }
                #endregion
            }
            catch (Exception ex)
            {
                message = "系统异常，请返回重试";
                result = "error";
            }
            #endregion
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 生成项目编号
        string getProjectCode()
        {
            #region
            string Code = "";
            int num = 1;
            string sql = " SELECT MAX(SUBSTRING(Code,6,4)) AS Code  FROM Project where SUBSTRING(Code,1,4)='" + System.DateTime.Now.Year + "' ";
            object obj = DBUtility.DbHelperSQL.GetSingle(sql);
            if (obj != null)
            {
                num += int.Parse(obj.ToString());
            }
            Code = num.ToString().PadLeft(4, '0');
            return System.DateTime.Now.Year + "P" + Code;
            #endregion
        }
        #endregion


        #region 项目阶段和计划

        Maticsoft.BLL.ProjectTask BProjectTask = new Maticsoft.BLL.ProjectTask();
        Maticsoft.Model.ProjectTask MProjectTask = new Maticsoft.Model.ProjectTask();
        List<Maticsoft.Model.ProjectTask> MProjectTaskList = new List<Maticsoft.Model.ProjectTask>();
        

        #region 项目阶段
        #region 项目阶段新增
        [HttpPost]
        public ActionResult ProjectPlanAdd(string ProjectID, 
                string PlanName,string ExpectedDate, string PlanTarget)
        {
            #region
            string message = string.Empty;
            string address = "/Project/ProjectPlan/" + ProjectID;
            string result = string.Empty;
            int PID = 0;
            int.TryParse(ProjectID, out PID);
            string Imgurl = "";
            var File= Request.Files["FilePlan"];
            if (File != null)
            {
                Imgurl = getImgUrlByFile(File);
            }
            if (ProjectID != "" && PlanName != "")
            {
                if (PID > 0)
                {
                    MProjectTask.ProjectID = PID;
                    MProjectTask.TaskNo = 0;
                    if (ExpectedDate != "")
                    {
                        MProjectTask.ExpectedDate = Convert.ToDateTime(ExpectedDate);
                    }
                    MProjectTask.Contents = PlanName;
                    MProjectTask.Enable = 1;
                    MProjectTask.Target = PlanTarget;
                    MProjectTask.CreateDate = DateTime.Now;
                    if (Imgurl != "")
                    {
                        MProjectTask.FileAddress = Imgurl;
                    }
                    MProjectTask.CreateUserID = int.Parse(Session["UserInfoID"].ToString());
                    MProjectTask.ProjectTaskCheckStatusID = 4;
                    int TaskID = BProjectTask.Add(MProjectTask);

                    #region 修改项目审核状态,每次修改都改为：草稿
                    ChangeProjectStatus(ProjectID, "1","check");
                    #endregion
                    message = "添加成功";
                    result = "success";
                }
                else
                {
                    message = "请填写内容";
                    result = "error";
                }
            }
            else
            {
                message = "参数不完整，请填写完整";
                result = "error";
            }
            #endregion
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 保存图片
        string getImgUrlByFile(HttpPostedFileBase File)
        {
            string ImgUrl = "";
            string filename = File.FileName.ToString();
            string houzhui = filename.Substring(filename.LastIndexOf("."));

            string strfile = "/uploadfile/File/";//path表示文件夹名称，拼接的时间

            string ServerFile = Server.MapPath(".."+strfile);

            string time = DateTime.Now.ToString("yyyymmddHHMM");
            try
            {
                if (!System.IO.Directory.Exists(ServerFile))
                {
                    System.IO.Directory.CreateDirectory(ServerFile);//创建文件夹
                }
                string fileHouzhui = time + filename;
                File.SaveAs(ServerFile + fileHouzhui);
                ImgUrl = strfile + fileHouzhui;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ImgUrl;
        }
        #endregion
        
        #region
        public ActionResult ProjectPlanChange(string ProjectID,
             string PlanID, string ExpectedDate, string PlanName,string PlanTarget)
        {
            #region
            string message = string.Empty;
            string address = "/Project/ProjectPlan/" + ProjectID;
            string result = string.Empty;
            int PID = 0;
            int.TryParse(PlanID, out PID);
            string Imgurl = "";
            var File = Request.Files["FilePlan"];
            if (File != null)
            {
                Imgurl = getImgUrlByFile(File);
            }
            if (ProjectID != "" && PlanName != "" && PlanID != "")
            {
                if (PID > 0)
                {
                    MProjectTask = BProjectTask.GetModel(PID);
                    MProjectTask.ProjectID = int.Parse(ProjectID);
                    if (ExpectedDate != "")
                    {
                        MProjectTask.ExpectedDate = Convert.ToDateTime(ExpectedDate);
                    }
                    if (Imgurl != "")
                    {
                        MProjectTask.FileAddress = Imgurl;
                    }
                    MProjectTask.Contents = PlanName;
                    MProjectTask.Enable = 1;
                    MProjectTask.Target = PlanTarget;
                    MProjectTask.EditDate = DateTime.Now;
                    MProjectTask.EditUserID = int.Parse(Session["UserInfoID"].ToString());
                    MProjectTask.ProjectTaskCheckStatusID = 4;//修改为未提交状态。
                    int TaskID = MProjectTask.ID;
                    bool res = BProjectTask.Update(MProjectTask);
                    if (res)
                    {
                        //更新项目状态为未审核
                        ChangeProjectStatus(ProjectID, "1", "check");

                        message = "修改成功";
                        result = "success";
                    }
                    else
                    {
                        message = "修改失败";
                        result = "error";
                    }
                }
                else
                {
                    message = "请填写内容";
                    result = "error";
                }
            }
            else
            {
                message = "参数不完整，请填写完整";
                result = "error";
            }
            #endregion
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 修改计划
        public ActionResult ProjectStageAndPlanChange(string ProjectID,
             string PlanID, string PlanName, string PlanStartTime, string PlanEndTime, string PlanTarget,
             string NeedSupport, string strUserAdd, string strUserDel, string strUserNochange,
             string strCustomAdd, string strCustomDel, string strCustomNochange)
        {
            #region
            string message = string.Empty;
            string address = "/Project/ProjectPlan/" + ProjectID;
            string result = string.Empty;
            int PID = 0;
            int.TryParse(PlanID, out PID);
            if (ProjectID != "" && PlanName != "" && PlanID != ""
                && PlanStartTime != "" && PlanEndTime != "")
            {
                if (PID > 0)
                {
                    MProjectTask = BProjectTask.GetModel(PID);
                    MProjectTask.ProjectID = PID;
                    MProjectTask.Contents = PlanName;
                    MProjectTask.PlanStartTime = Convert.ToDateTime(PlanStartTime);
                    MProjectTask.PlanEndTime = Convert.ToDateTime(PlanEndTime);
                    MProjectTask.Enable = 1;
                    MProjectTask.Target = PlanTarget;
                    MProjectTask.NeedSupport = NeedSupport;
                    MProjectTask.EditDate = DateTime.Now;
                    MProjectTask.EditUserID = int.Parse(Session["UserInfoID"].ToString());
                    MProjectTask.ProjectTaskCheckStatusID = 4;//修改为未提交状态。
                    int TaskID = MProjectTask.ID;
                    bool res = BProjectTask.Update(MProjectTask);
                    if (res)
                    {
                        //更新项目状态为未审核
                        ChangeProjectStatus(ProjectID, "1","check");
                        

                        message = "修改成功";
                        result = "success";
                    }
                    else
                    {
                        message = "修改失败";
                        result = "error";
                    }
                }
                else
                {
                    message = "请填写阶段名称";
                    result = "error";
                }
            }
            else
            {
                message = "参数不完整，请填写完整";
                result = "error";
            }
            #endregion
            string WebUrlPath = config.getConfigName("WebUrl");
            address = WebUrlPath + address;
            return Json(new { Result = result, Message = message, Address = address }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 删除计划，如果本阶段计划只有一个，那么关联删除
        public ActionResult ProjectTaskDel(string PlanID)
        {
            #region
            string message = string.Empty;
            string result = string.Empty;
            int PID = 0;
            if (PlanID != ""&&int.TryParse(PlanID, out PID))
            {
                MProjectTask = BProjectTask.GetModel(PID);
                bool res = BProjectTask.Delete(PID);
                if (res)
                {
                    message = "删除成功";
                    result = "success";
                }
                else
                {
                    message = "删除失败";
                    result = "error";
                }             
            }
            else
            {
                message = "未知错误，请返回重试";
                result = "error";
            }
            #endregion
            return Json(new { Result = result, Message = message, PlanID= PlanID }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 分配计划
        public ActionResult ProjectTaskGive(string PlanID, string PlanStartTime, string PlanEndTime, string DealUserID)
        {
            #region
            string message = string.Empty;
            string result = string.Empty;
            int PID = 0;
            int.TryParse(PlanID, out PID);
            if (PID >0 && PlanStartTime != "")
            {
                MProjectTask = BProjectTask.GetModel(PID);
                if (PlanStartTime != null && PlanStartTime != "")
                {
                    MProjectTask.PlanStartTime = Convert.ToDateTime(PlanStartTime); ;
                }
                else
                {
                    MProjectTask.PlanStartTime = null;
                }
                if (PlanEndTime != null && PlanEndTime != "")
                {
                    MProjectTask.PlanEndTime = Convert.ToDateTime(PlanEndTime); ;
                }
                else
                {
                    MProjectTask.PlanEndTime = null;
                }
                MProjectTask.DealUserID = int.Parse(DealUserID);
                MProjectTask.DealStatusID = 2;
                MProjectTask.EditDate = DateTime.Now;
                MProjectTask.EditUserID = int.Parse(Session["UserInfoID"].ToString());
                int TaskID = MProjectTask.ID;
                bool res = BProjectTask.Update(MProjectTask);
                if (res)
                {
                    message = "提交成功";
                    result = "success";
                }
                else
                {
                    message = "提交失败";
                    result = "error";
                }
            }
            else
            {
                message = "参数不完整，请填写完整";
                result = "error";
            }
            #endregion
            return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 完成计划
        public ActionResult ProjectTaskDone(string PlanID, string RealStartTime, string RealEndTime, string Result, string Note,string DealStatusID)
        {
            #region
            string message = string.Empty;
            string result = string.Empty;
            int PID = 0;
            int DSID = 0;
            
            int.TryParse(PlanID, out PID);
            int.TryParse(DealStatusID, out DSID);
            if (PID > 0 && DSID > 0 && RealStartTime != "")
            {
                MProjectTask = BProjectTask.GetModel(PID);
                if (RealStartTime != null && RealStartTime != "")
                {
                    MProjectTask.RealStartTime = Convert.ToDateTime(RealStartTime); ;
                }
                else
                {
                    MProjectTask.RealStartTime = null;
                }
                if (RealEndTime != null && RealEndTime != "")
                {
                    MProjectTask.RealEndTime = Convert.ToDateTime(RealEndTime); ;
                }
                else
                {
                    MProjectTask.RealEndTime = null;
                }
                MProjectTask.DealStatusID = DSID;
                MProjectTask.Note = Note;
                MProjectTask.Result = Result;
                MProjectTask.EditDate = DateTime.Now;
                MProjectTask.EditUserID = int.Parse(Session["UserInfoID"].ToString());
                int TaskID = MProjectTask.ID;
                bool res = BProjectTask.Update(MProjectTask);
                if (res)
                {
                    message = "提交成功";
                    result = "success";
                }
                else
                {
                    message = "提交失败";
                    result = "error";
                }
            }
            else
            {
                message = "参数不完整，请填写完整";
                result = "error";
            }
            #endregion
            return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #endregion

        #endregion

        #region 新增操作记录
        void AddOperateRecord(string Contents,string TableName,int KeyID=0)
        {
            string UserName = "";
            if (Session["TrueName"] != null)
            {
                UserName = "[" + Session["UserInfoID"].ToString() + "]" + Session["TrueName"].ToString();
            }
            MOperateRecord.TableName = TableName;
            MOperateRecord.Time = System.DateTime.Now;
            MOperateRecord.Contents = UserName+"　 "+Contents;
            MOperateRecord.KeyID = KeyID;
            BOperateRecord.Add(MOperateRecord);
        }
        #endregion

    }
}