using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace DBUtility
{
    public class getMenu
    {
        public string getMenuByLoginID(string UserID,string TrueName)
        {
            ConfigurationHelper config = new ConfigurationHelper();
            DataTable dt = GetFirstMenu(UserID);
            string WebUrlPath = config.getConfigName("WebUrl");
            string privilege = getPrivileges(UserID);//获取当前用户具有的权限<li><a href=\"/admin/ChangePassword\">修改密码</a></li>
            string ErpUrlPath = config.getConfigName("ErpUrl");
            StringBuilder strString = new StringBuilder();
            strString.Append(" <div class=\"nav-header\"> <div class=\"dropdown profile-element center\">");
            strString.Append(" <span><img alt=\"image\" class=\"img-circle\" src=\""+ WebUrlPath + "/img/profile_small.jpg\" /> </span>");//头像
            strString.Append("<a data-toggle=\"dropdown\" class=\"dropdown-toggle\" href=\"#\"><span class=\"clear\"> <span class=\"block m-t-xs\">");
            strString.Append("<strong class=\"font-bold\">"+ TrueName + "</strong></span><span class=\"text-muted text-xs block\">个人中心 <b class=\"caret\"></b></span></span></a>");
            strString.Append("<ul class=\"dropdown-menu animated fadeInRight m-t-xs\"> <li class=\"divider\"></li> ");
            strString.Append("<li><a href=\""+ WebUrlPath + "/Window/LogOut\">注销</a></li>  <li class=\"divider\"></li></ul>");
            strString.Append("</div><div class=\"logo-element\">Server</div> ");
            strString.Append(@"<div class='infont col-md-6 center'>
<a href='" + ErpUrlPath + @"' target='_blank' class='block m-xs p-xs col-md-12 col-sm-12'>
<i class='fa fa-home fa-lg'></i><br>首页</a></div>
<div class='infont col-md-6 center'><a href = '" + WebUrlPath + @"/Window/Login' class='block m-xs p-xs col-md-12'>
<i class='fa fa-mail-reply-all fa-lg'></i><br>退出</a></div></div> <ul class='nav metismenu' id='side-menu'>");
            //上边是顶部菜单导航板块
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string ID = dr["ID"].ToString();
                    string Name = dr["Name"].ToString();
                    string URL = dr["Href"].ToString();
                    if (URL.Contains("/"))
                    {
                        URL = WebUrlPath + URL;
                    }
                    string CssStyle = dr["CssStyle"].ToString();
                    string li = dr["li"].ToString();
                    strString.Append(" <li  data-li='" + li + "'> ");
                    strString.Append(" <a href='" + URL + "'><i class='" + CssStyle + "'></i><span class=\"nav-label\">" + Name + "</span> <span class=\"fa arrow\"></span></a>");
                    strString.Append(" <ul class=\"nav nav-second-level\">");

                    string sql = @"SELECT [ID],[Name],[Href]  FROM  [Privilege] where ParentID='" + ID + "' and ID in " + "(" + privilege + ")" 
                        //+ "  and APPName='zhonglika' "
                        +" and Enable=1 order by DisplayOrder ";
                    DataTable dts = DbHelperSQL.Query(sql).Tables[0];//获取二级权限，返回一个表格
                    foreach (DataRow ds in dts.Rows)
                    {
                        string Names = ds["Name"].ToString();
                        string URLs = ds["Href"].ToString();
                        if (URLs.Contains("/"))
                        {
                            URLs = WebUrlPath + URLs;
                        }
                        strString.Append(" <li><a href='" + URLs + "'>" + Names + "</a></li>");
                    }
                    strString.Append("</ul></li>");
                }
                strString.Append(" </ul>");
            }
            return strString.ToString();
        }



        /// <summary>
        /// 获取具有的权限
        /// 然后根据相关权限获取到具体的权限，返回datatable
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataTable GetFirstMenu(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,TrueName,RoleID,PRIVILEGES,UserName ");
            strSql.Append(" FROM AdminPrivilege ");
            strSql.Append(" where ID=" + UserID);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];//获取用户和用户权限拼接字符串
            if (dt.Rows.Count > 0)
            {
                string privilege = dt.Rows[0]["PRIVILEGES"].ToString();//获取字符串(这时候是二级权限)

                DataTable MenusList = getOneClassPrivileges(privilege);//获取二级权限列表 （然后使用二级列表选出来一级权限）



                return MenusList;//返回一级权限列表
            }
            else {
                DataTable dts = new DataTable();
                return dts;
            }
        }




        /// <summary>
        /// 获取二级生态的ParentID（一级生态的）
        /// </summary>
        /// <param name="privilege"></param>
        /// <returns></returns>
        public DataTable getOneClassPrivileges(string privilege)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" SELECT  [ID],[Name],[ParentID] ,[CssStyle],[Href],[li] ");
            strSql.Append(" FROM Privilege ");
            //strSql.Append(" where  Enable=1 and ID in ( SELECT  distinct [ParentID] FROM Privilege where Enable=1  and APPName='zhonglika' and ID in " + "(" + privilege + ")" + " ) order by DisplayOrder ");
            strSql.Append(" where  Enable=1 and ID in ( SELECT  distinct [ParentID] FROM Privilege where Enable=1 and ID in " + "(" + privilege + ")" + " ) order by DisplayOrder ");
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];//获取用户的权限
            return dt;
        }


        /// <summary>
        /// 参数为管理员的privileges
        /// 获取以及权限列表，并返回DataTable
        /// </summary>
        /// <param name="privilege"></param>
        /// <returns></returns>
        public string getPrivileges(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,TrueName,RoleID,PRIVILEGES,UserName ");
            strSql.Append(" FROM AdminPrivilege ");
            strSql.Append(" where ID=" + UserID);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];//获取用户和用户权限拼接字符串
            string privilege = dt.Rows[0]["PRIVILEGES"].ToString();
            return privilege;
        }


    }
}
