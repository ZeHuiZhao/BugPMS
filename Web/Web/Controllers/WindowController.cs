using DBUtility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class WindowController : Controller
    {
        Password_Encrypt_ASC.Password_Encrypt_ASC Setpassword = new Password_Encrypt_ASC.Password_Encrypt_ASC();
        Maticsoft.BLL.UserInfo BUserInfo = new Maticsoft.BLL.UserInfo();
        Maticsoft.Model.UserInfo MUserInfo = new Maticsoft.Model.UserInfo();
        Maticsoft.BLL.Company BCompany = new Maticsoft.BLL.Company();
        Maticsoft.BLL.Department BDepartment = new Maticsoft.BLL.Department();
        Maticsoft.Model.Company MCompany = new Maticsoft.Model.Company();
        Maticsoft.Model.Department MDepartment = new Maticsoft.Model.Department();
        // GET: Window

        /// <summary>
        /// 用户登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            string username = Request["username"];//用户名
            string password = Request["password"];//密码 
            string url = "";
            if (Request.QueryString["ID"] != null)
            {
                url = Request.QueryString["ID"].ToString();//获取这个参数  //url = HttpUtility.UrlDecode(url);
            }
            int flag = -1;
            string[] array;
            if (!string.IsNullOrEmpty(url))
            {
                url = HttpUtility.UrlEncode(url, System.Text.Encoding.UTF8);
                url = Setpassword.get_password_ASC(HttpUtility.UrlDecode(url));
                array = url.Split('/');//分开，分别验证（验证时间是否允许）
                DateTime dtCheckTime = DateTime.ParseExact(array[3], "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                if (dtCheckTime.Date != DateTime.Now.Date)
                {
                    flag = 0;
                }
                else if (dtCheckTime.Date == DateTime.Now.Date)
                {
                    flag = 1;
                }
            }
            if (flag == -1)
            {
                if (username == null)//用户名为空
                {
                    ViewData["flag"] = "0";
                    return View();
                }
                else if (password == null)//密码为空
                {
                    ViewData["flag"] = "2";
                    return View();
                }
                else if (username.ToString() == "")
                {
                    ViewData["flag"] = "0";
                    return View();
                }
                else if (password.ToString() == "")
                {
                    ViewData["flag"] = "2";
                    return View();
                }
                password = Setpassword.set_password_ASC(password);//对密码进行加密判断
            }
            #region

            #region
            if (flag == -1)
            {
                #region 用户登录
                string sql = @"SELECT  [ID],[ParentID],[UserName],[PrivilegeID],[TrueName],[RoleID],[Status] 
                              FROM  [UserInfo]  WHERE UserName = @username AND [Password] = @password  AND [Status] = 1";
                SqlParameter[] paras = {
                                      new SqlParameter("@username",username ),
                                      new SqlParameter("@password",password )
                                      };
                DataTable dt = DbHelperSQL.GetDataSet(sql, paras).Tables[0];
                if (dt.Rows.Count < 1)
                {
                    ViewData["flag"] = "3";
                    return View();
                }
                else
                {
                    string name = dt.Rows[0]["TrueName"].ToString();
                    string ID = dt.Rows[0]["ID"].ToString();
                    string RoleID = dt.Rows[0]["RoleID"].ToString();
                    HttpContext.Session.Add("UserInfoID", ID);
                    HttpContext.Session.Add("RoleID", RoleID);
                    HttpContext.Session.Add("TrueName", name);
                    HttpContext.Session.Timeout = 40;
                    ViewData["flag"] = "1";
                    return RedirectToAction("Index", "Window");
                }
                #endregion
            }
            else if (flag == 1)
            {//ERP
                array = url.Split('/');
                DataTable dt = BUserInfo.GetList("UnionID='" + array[0] + "'").Tables[0];//获取数据，判断是否存在
                if (array[0].Length > 0)
                {
                    #region 验证用户登录
                    if (dt.Rows.Count > 0)
                    {
                        string name = dt.Rows[0]["TrueName"].ToString();
                        string ID = dt.Rows[0]["ID"].ToString();
                        string RoleID = dt.Rows[0]["RoleID"].ToString();
                        Session.Add("UserInfoID", ID);
                        Session.Add("TrueName", name);
                        Session.Add("RoleID", RoleID);
                        Session.Timeout = 40;
                        return RedirectToAction("Index", "Window");
                    }
                    else
                    {
                        MUserInfo.ParentID = 1;//默认上级是超级管理员
                        MUserInfo.Password = "";// password.set_password_ASC("888888");
                        MUserInfo.PrivilegeID = "17";//默认权限
                        MUserInfo.RoleID = 3;
                        MUserInfo.status = Convert.ToInt32(1);
                        MUserInfo.Time = System.DateTime.Now;
                        MUserInfo.TrueName = array[2];//姓名
                        MUserInfo.UnionID = array[0];//用户编号
                        MUserInfo.UserFrom = "ERP-User";
                        MUserInfo.UserName = array[1];//用户昵称，就是手机号
                        int UserInfoID = BUserInfo.Add(MUserInfo);
                        if (UserInfoID > 0)
                        {
                            Session.Add("UserInfoID", UserInfoID);
                            Session.Add("RoleID", 3);//默认都是反馈人员
                            Session.Add("TrueName", MUserInfo.TrueName);
                            Session.Timeout = 40;
                            return RedirectToAction("Index", "Window");
                        }
                        else
                        {
                            return RedirectToAction("Login", "Window");
                        }

                    }
                    #endregion
                }
                else
                {
                    ViewData["flag"] = "-1";
                    return View();
                }
            }
            else //if(flag==0)
            {
                ViewData["flag"] = "-1";
                return View();
            }
            #endregion

            #endregion


        }

        /// <summary>
        /// 用户中心登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult UserCenterLogin()
        {
            ConfigurationHelper config = new ConfigurationHelper();
            string WebUrl = config.getConfigName("WebUrl");

            string token = Request.QueryString["token"]?.ToString();
            DataTable dt = null;
            if (!string.IsNullOrEmpty(token))
            {
                token = HttpUtility.UrlEncode(token, System.Text.Encoding.UTF8);
                token = Setpassword.get_password_ASC(HttpUtility.UrlDecode(token));
                dt = JsonConvert.DeserializeObject(token, typeof(DataTable)) as DataTable;
            }

            if (dt != null && dt.Rows.Count > 0)//表示token能解析出对象
            {
                DateTime time = Convert.ToDateTime(dt.Rows[0]["CreateDate"]);
                if (time.Date == DateTime.Now.Date)
                {
                    DataTable userTable = BUserInfo.GetList("UrGuid='" + dt.Rows[0]["UserGuid"].ToString() + "'").Tables[0];
                    //处理dt
                    if (userTable.Rows.Count > 0)//如果存在这个guid 表示用户存在
                    {
                        //存在还要判断数据是否有被修改
                        int companyId = Convert.ToInt32(dt.Rows[0]["CompanyID"]);
                        string companyName = dt.Rows[0]["CompanyName"].ToString();
                        //先开始判断公司
                        MCompany = BCompany.GetModel(companyId);
                        if (MCompany == null)
                        {
                            //创建公司
                            MCompany = new Maticsoft.Model.Company() { Id = companyId, Name = companyName };
                            BCompany.Add(MCompany);
                        }
                        else//表示存在公司
                        {
                            if (MCompany.Name != companyName)//表示数据更改
                            {
                                string sql = "update company set Name=@Name where Id=@Id";
                                SqlParameter[] paras = {
                                      new SqlParameter("@Name",companyName ),
                                      new SqlParameter("@Id",companyId )
                                      };
                                DbHelperSQL.ExecuteSql(sql, paras);
                            }
                        }

                        int departmentId = Convert.ToInt32(dt.Rows[0]["DepartmentID"]);
                        int newDepartmentId = 0;
                        string departmentName = dt.Rows[0]["DepartmentName"].ToString();
                        DataTable departmentTable = BDepartment.GetList("UcDepartmentId = " + departmentId).Tables[0];
                        if (departmentTable.Rows.Count > 0)//表示已经存在部门
                        {
                            newDepartmentId = Convert.ToInt32(departmentTable.Rows[0]["ID"]);
                            if (departmentTable.Rows[0]["Name"].ToString() != departmentName || departmentTable.Rows[0]["CpId"].ToString() != companyId.ToString())//表示部门名称更改，更新部门名称
                            {
                                string sql = "update Department set UcDepartmentId=@UcDepartmentId,Name=@Name,CpId=@CpId where Id=@Id";
                                SqlParameter[] paras = {
                                      new SqlParameter("@UcDepartmentId",departmentId ),
                                      new SqlParameter("@CpId",companyId ),
                                      new SqlParameter("@Name",departmentName ),
                                      new SqlParameter("@Id",newDepartmentId)
                                      };
                                DbHelperSQL.ExecuteSql(sql, paras);
                            }
                        }
                        else
                        {
                            //不存在部门，新增
                            MDepartment = new Maticsoft.Model.Department() { CpId = companyId, UcDepartmentId = departmentId, Name = departmentName };
                            newDepartmentId = BDepartment.Add(MDepartment);
                        }

                        //处理用户

                        if (userTable.Rows[0]["UserName"].ToString() != dt.Rows[0]["UserPhone"].ToString() || dt.Rows[0]["UserName"].ToString() != userTable.Rows[0]["TrueName"].ToString() || Convert.ToInt32(userTable.Rows[0]["DepartmentId"]) != newDepartmentId || userTable.Rows[0]["HeadImage"].ToString() != dt.Rows[0]["UserHeadImage"].ToString())
                        {
                            //表示用户信息有被更改
                            string sql = "update Userinfo set UserName=@UserName,TrueName=@TrueName,DepartmentId=@DepartmentId,HeadImage=@HeadImage where Id=@Id";
                            SqlParameter[] paras = {
                                      new SqlParameter("@UserName",dt.Rows[0]["UserPhone"].ToString()),
                                      new SqlParameter("@TrueName",dt.Rows[0]["UserName"].ToString()),
                                      new SqlParameter("@HeadImage",dt.Rows[0]["UserHeadImage"].ToString()),
                                      new SqlParameter("@DepartmentId",newDepartmentId),
                                      new SqlParameter("@Id",Convert.ToInt32(userTable.Rows[0]["ID"]))
                                      };
                            DbHelperSQL.ExecuteSql(sql, paras);
                        }

                        string name = userTable.Rows[0]["TrueName"].ToString();
                        string ID = userTable.Rows[0]["ID"].ToString();
                        string RoleID = userTable.Rows[0]["RoleID"].ToString();
                        Session.Add("UserInfoID", ID);
                        Session.Add("TrueName", name);
                        Session.Add("RoleID", RoleID);
                        Session.Timeout = 40;
                        return RedirectToAction("Index", "Window");


                        //string ID = userTable.Rows[0]["ID"].ToString();
                        //Session.Add("UserInfoID", ID);
                        //Session.Add("RoleID", 3);//默认都是反馈人员
                        //Session.Add("TrueName", MUserInfo.TrueName);
                        //Session.Timeout = 40;
                        //return RedirectToAction("Index", "Window");
                    }
                    else //表示不存在这个用户
                    {
                        int companyId = Convert.ToInt32(dt.Rows[0]["CompanyID"]);
                        string companyName = dt.Rows[0]["CompanyName"].ToString();
                        //先开始判断公司
                        MCompany = BCompany.GetModel(companyId);
                        if (MCompany == null)
                        {
                            //创建公司
                            MCompany = new Maticsoft.Model.Company() { Id = companyId, Name = companyName };
                            BCompany.Add(MCompany);
                        }
                        else//表示存在公司
                        {
                            if (MCompany.Name != companyName)//表示数据更改
                            {
                                string sql = "update company set Name=@Name where Id=@Id";
                                SqlParameter[] paras = {
                                      new SqlParameter("@Name",companyName ),
                                      new SqlParameter("@Id",companyId )
                                      };
                                DbHelperSQL.ExecuteSql(sql, paras);
                            }
                        }
                        int departmentId = Convert.ToInt32(dt.Rows[0]["DepartmentID"]);
                        int newDepartmentId = 0;
                        string departmentName = dt.Rows[0]["DepartmentName"].ToString();
                        DataTable departmentTable = BDepartment.GetList("UcDepartmentId = " + departmentId).Tables[0];
                        if (departmentTable.Rows.Count > 0)//表示已经存在部门
                        {
                            newDepartmentId = Convert.ToInt32(departmentTable.Rows[0]["ID"]);
                            if (departmentTable.Rows[0]["Name"].ToString() != departmentName || departmentTable.Rows[0]["CpId"].ToString() != companyId.ToString())//表示部门名称更改，更新部门名称
                            {
                                string sql = "update Department set UcDepartmentId=@UcDepartmentId,Name=@Name,CpId=@CpId where Id=@Id";
                                SqlParameter[] paras = {
                                      new SqlParameter("@UcDepartmentId",departmentId ),
                                      new SqlParameter("@Name",departmentName ),
                                      new SqlParameter("@CpId",companyId ),
                                      new SqlParameter("@Id",newDepartmentId)
                                      };
                                DbHelperSQL.ExecuteSql(sql, paras);
                            }
                        }
                        else
                        {
                            //不存在部门，新增
                            MDepartment = new Maticsoft.Model.Department() { CpId = companyId, UcDepartmentId = departmentId, Name = departmentName };
                            newDepartmentId = BDepartment.Add(MDepartment);
                        }
                        //创建用户
                        MUserInfo.ParentID = 1;//默认上级是超级管理员
                        MUserInfo.Password = "";// password.set_password_ASC("888888");
                        MUserInfo.PrivilegeID = "17";//默认权限
                        MUserInfo.RoleID = 3;
                        MUserInfo.status = Convert.ToInt32(1);
                        MUserInfo.Time = System.DateTime.Now;
                        MUserInfo.UserFrom = "ERP-User";
                        MUserInfo.DepartmentID = newDepartmentId;
                        MUserInfo.TrueName = dt.Rows[0]["UserName"].ToString();//用户真实姓名
                        MUserInfo.UserName = dt.Rows[0]["UserPhone"].ToString();//用户昵称，就是手机号
                        MUserInfo.UrGuid = dt.Rows[0]["UserGuid"].ToString();
                        MUserInfo.HeadImage = dt.Rows[0]["UserHeadImage"].ToString();
                        int check = BUserInfo.Add(MUserInfo);
                        if (check > 0)
                        {
                            Session.Add("UserInfoID", check);
                            Session.Add("RoleID", 3);//默认都是反馈人员
                            Session.Add("TrueName", MUserInfo.TrueName);
                            Session.Timeout = 40;
                            return RedirectToAction("Index", "Window");
                        }
                        else
                        {
                            return RedirectToAction("Login", "Window");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Window");
                }
            }
            else
            {
                return RedirectToAction("Login", "Window");
            }
        }


        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Window");
        }

    }
}