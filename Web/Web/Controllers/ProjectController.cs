using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Web.Controllers
{
    public class ProjectController : Controller
    {
        #region 初始化
        Maticsoft.BLL.Project BProject = new Maticsoft.BLL.Project();
        Maticsoft.Model.Project MProject = new Maticsoft.Model.Project();
        List<Maticsoft.Model.Project> MProjectList = new List<Maticsoft.Model.Project>();

        Maticsoft.BLL.ProjectTask BProjectTask = new Maticsoft.BLL.ProjectTask();
        Maticsoft.Model.ProjectTask MProjectTask = new Maticsoft.Model.ProjectTask();
        List<Maticsoft.Model.ProjectTask> MProjectTaskList = new List<Maticsoft.Model.ProjectTask>();
        

        Common.dtTojson CdtTojson = new Common.dtTojson();
        #endregion
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }
        #region 新增项目
        public ActionResult ProjectAdd(string id="")
        {
            if (id != "")
            {
                MProject = BProject.GetModel(int.Parse(id));
            }
            return View(MProject);
        }
        #endregion

        #region 项目列表
        public ActionResult ProjectList()
        {
            return View();
        }

        public ActionResult ProjectListDone()
        {
            return View();
        }

        public ActionResult ProjectCustom(string id = "")
        {
            if (id != "")
            {
                MProject = BProject.GetModel(int.Parse(id));
            }
            return View(MProject);
        }

        #region 项目Item
        public ActionResult ProjectItem(int page = 1,
            string ProjectName = "", string ProjectTypeID = "", string ProjectStatusID = "", string ProjectCheckStatusID = "",
            string LeaderName = "", string PlanStartTime = "", string PlanEndTime = "")
        {
            //参数ID是从ProjectList中传过来的
            int pagecount = 0;
            string strWhere = " ID>0  ";
            if (ProjectName != "")
            {
                strWhere += " and (Name like '%" + ProjectName + "%' or Code like '%" + ProjectName + "%' )";
            }
            if (ProjectTypeID != "")
            {
                strWhere += " and ProjectTypeID ='" + ProjectTypeID + "' ";
            }
            if (ProjectStatusID != "")
            {
                strWhere += " and ProjectStatusID ='" + ProjectStatusID + "' ";
            }
            if (ProjectCheckStatusID != "")
            {
                strWhere += " and ProjectCheckStatus ='" + ProjectCheckStatusID + "' ";
            }
            if (LeaderName != "")
            {
                strWhere += " and  LeaderID in( select ID from UserInfo where TrueName like '%" + LeaderName + "%' )";
            }
            if (PlanStartTime != "")
            {
                strWhere += " and Convert(varchar(10),PlanStartTime,120) >='" + PlanStartTime + "' ";
            }
            if (PlanEndTime != "")
            {              
                strWhere += " and Convert(varchar(10),PlanEndTime,120) <='" + PlanEndTime + "' ";
            }
            string strorder = "CreateDate desc";
            int pageSize = 20;
            DataSet ds = BProject.GetListByPage(strWhere, strorder, ((page - 1) * pageSize + 1), page * pageSize);//分页获取到数据
            pagecount = BProject.GetRecordCount(strWhere);
            if (ds != null)
            {
                MProjectList = BProject.DataTableToList(ds.Tables[0]);//把datatable转化成List
                ViewData["currentpage"] = page;//当前页码
                ViewData["pagecount"] = pagecount;
            }
            return View(MProjectList);
        }
        #endregion

        #region 项目计划Item
        public ActionResult ProjectPlanItem( int page = 1,
            string keyName = "", string ProjectID = "", string ProjectTaskCheckStatusID = ""
            ,string CBOnlyMe="",string StartTime="",string EndTime="", string flag = "")
        {
            //参数ID是从ProjectList中传过来的
            int UserinfoID = 0;
            if (Session["UserInfoID"] != null)
            {
                UserinfoID = int.Parse(Session["UserInfoID"].ToString());
            }
            int pagecount = 0;
            int PID = 0;
            int.TryParse(ProjectID, out PID);
            string strWhere = " ProjectID = " + PID;
            if (keyName != "")
            {
                strWhere += " and ([Contents] like '%" + keyName + "%' or [Target] like '%" + keyName + "%' )";
            }
            if (StartTime != "")
            {
                strWhere += " and Convert(varchar(10),CreateDate,120) >='" + StartTime + "' ";
            }
            if (EndTime != "")
            {
                strWhere += " and Convert(varchar(10),CreateDate,120) <='" + EndTime + "' ";
            }
            if (CBOnlyMe == "on")
            {
                strWhere += " and [CreateUserID]='" + UserinfoID + "' ";
            }
            if (ProjectTaskCheckStatusID != "")
            {
                strWhere += " and [ProjectTaskCheckStatusID] ='" + ProjectTaskCheckStatusID + "' ";
            }
            #region 筛选条件
            if (flag.ToLower() == "new")
            {//新需求
                strWhere += " and ProjectTaskCheckStatusID=1";
            }
            else if (flag.ToLower() == "hascheck")
            {//已审批
                strWhere += " and ProjectTaskCheckStatusID=2";
            }
            else if (flag.ToLower() == "running")
            {//进行中
                strWhere += " and ProjectTaskCheckStatusID=2";
                strWhere += " and DealStatusID=2";
            }
            else if (flag.ToLower() == "notbegin")
            {//未开始
                strWhere += " and ProjectTaskCheckStatusID=2";
                strWhere += " and DealStatusID=1";
            }
            else if (flag.ToLower() == "done")
            {//已完成
                strWhere += " and ProjectTaskCheckStatusID=2";
                strWhere += " and DealStatusID=3";
            }
            else if (flag.ToLower() == "cancel")
            {//已取消
                strWhere += " and ProjectTaskCheckStatusID=2";
                strWhere += " and DealStatusID=4";
            }
            #endregion

            string strorder = "CreateDate asc";
            int pageSize = 20;
            DataSet ds = BProjectTask.GetListByPage(strWhere, strorder, ((page - 1) * pageSize + 1), page * pageSize);//分页获取到数据
            pagecount = BProjectTask.GetRecordCount(strWhere);
            if (ds != null)
            {
                MProjectTaskList = BProjectTask.DataTableToList(ds.Tables[0]);//把datatable转化成List
                ViewData["currentpage"] = page;//当前页码
                ViewData["pagecount"] = pagecount;
            }
            return View(MProjectTaskList);
        }
        #endregion

        #endregion

        #region 项目阶段计划
        public ActionResult ProjectPlan(string id = "", string operate = "", string flag = "")
        {
            if (operate == null)
            {
                operate = "";
            }
            ViewData["operate"] = operate;
            if (id != "")
            {
                MProject = BProject.GetModel(int.Parse(id));
            }
            ViewData["flag"] = flag;
            return View(MProject);
        }

        public ActionResult ProjectPlanDone(string id = "", string operate = "", string flag = "")
        {
            if (operate == null)
            {
                operate = "";
            }
            ViewData["operate"] = operate;
            if (id != "")
            {
                MProject = BProject.GetModel(int.Parse(id));
            }
            ViewData["flag"] = flag;
            return View(MProject);
        }

        #region 项目完成计划Item
        public ActionResult ProjectPlanDoneItem(int page = 1,
            string keyName = "", string ProjectID = "", string DealStatusID = "", string CBOnlyMe = "", 
            string StartTime = "", string EndTime = "", string flag = "")
        {
            //参数ID是从ProjectList中传过来的
            int UserinfoID = 0;
            if (Session["UserInfoID"] != null)
            {
                UserinfoID = int.Parse(Session["UserInfoID"].ToString());
            }
            int pagecount = 0;
            int PID = 0;
            int.TryParse(ProjectID, out PID);
            string strWhere = " ProjectID = " + PID;
            strWhere += " and [ProjectTaskCheckStatusID] =2 ";
            if (keyName != "")
            {
                strWhere += " and ([Contents] like '%" + keyName + "%' or [Target] like '%" + keyName + "%' )";
            }
            if (StartTime != "")
            {
                strWhere += " and Convert(varchar(10),CreateDate,120) >='" + StartTime + "' ";
            }
            if (EndTime != "")
            {
                strWhere += " and Convert(varchar(10),CreateDate,120) <='" + EndTime + "' ";
            }
            if (CBOnlyMe == "on")
            {
                strWhere += " and [CreateUserID]='" + UserinfoID + "' ";
            }
            if (DealStatusID != "")
            {
                strWhere += " and [DealStatusID] ='" + DealStatusID + "' ";
            }
            #region 筛选条件
            if (flag.ToLower() == "new")
            {//新需求
                strWhere += " and ProjectTaskCheckStatusID=1";
            }
            else if (flag.ToLower() == "hascheck")
            {//已审批
                strWhere += " and ProjectTaskCheckStatusID=2";
            }
            else if (flag.ToLower() == "running")
            {//进行中
                strWhere += " and ProjectTaskCheckStatusID=2";
                strWhere += " and DealStatusID=2";
            }
            else if (flag.ToLower() == "notbegin")
            {//未开始
                strWhere += " and ProjectTaskCheckStatusID=2";
                strWhere += " and DealStatusID=1";
            }
            else if (flag.ToLower() == "done")
            {//已完成
                strWhere += " and ProjectTaskCheckStatusID=2";
                strWhere += " and DealStatusID=3";
            }
            else if (flag.ToLower() == "cancel")
            {//已取消
                strWhere += " and ProjectTaskCheckStatusID=2";
                strWhere += " and DealStatusID=4";
            }
            #endregion

            string strorder = "CreateDate asc";
            int pageSize = 20;
            DataSet ds = BProjectTask.GetListByPage(strWhere, strorder, ((page - 1) * pageSize + 1), page * pageSize);//分页获取到数据
            pagecount = BProjectTask.GetRecordCount(strWhere);
            if (ds != null)
            {
                MProjectTaskList = BProjectTask.DataTableToList(ds.Tables[0]);//把datatable转化成List
                ViewData["currentpage"] = page;//当前页码
                ViewData["pagecount"] = pagecount;
            }
            return View(MProjectTaskList);
        }
        #endregion
        #endregion

        #region 项目甘特图
        public ActionResult ProjectGantt(string id = "", string operate = "")
        {
            if (operate == null)
            {
                operate = "";
            }
            ViewData["operate"] = operate;
            string operateWhere = "";
            #region
            string LeftJson = "";
            string UserJson = "";
            if (id != "")
            {
                int ProjectID = int.Parse(id);
                MProject = BProject.GetModel(ProjectID);
                string strPlan = @" SELECT pt.ID AS 'data-id', 
    pt.Contents AS Content,pt.[Target],CONVERT(VARCHAR(10),pt.PlanStartTime,120) AS 'PlanStartTime'
   ,CONVERT(VARCHAR(10),pt.PlanEndTime,120) AS 'PlanEndTime',CONVERT(VARCHAR(10),pt.RealStartTime,120) AS 'RealStartTime'
   ,CONVERT(VARCHAR(10),pt.RealEndTime,120) AS 'RealEndTime',pt.Result,pt.Note,
   CONVERT(VARCHAR(10),pt.PlanStartTime,120) AS 'data-StartDate'
   ,CONVERT(VARCHAR(10),pt.PlanEndTime,120) AS 'data-EndDate'
     FROM ProjectTask pt 
    WHERE pt.ProjectID=" + ProjectID+ operateWhere + " and pt.DealStatusID>1 ORDER BY pt.CreateDate asc,pt.ID asc";
                DataTable dt = DBUtility.DbHelperSQL.GetDataTable(strPlan);
                LeftJson=CdtTojson.DataTableToJson(dt, false);
                if (LeftJson != "")
                {
                    LeftJson = "," + LeftJson;
                }
                string strUserList = @"SELECT a.TrueName AS aName,b.TrueName AS bName,c.TrueName AS cName,d.TrueName AS dName FROM ProjectTask pt INNER JOIN Project p
                ON pt.ProjectID=p.ID INNER JOIN UserInfo a ON a.ID=p.LeaderID INNER JOIN UserInfo b ON b.ID=p.CheckUserID
                INNER JOIN UserInfo c ON c.ID=pt.CreateUserID LEFT JOIN UserInfo d ON d.ID=pt.DealUserID 
                WHERE pt.ProjectID='"+ ProjectID + "' and pt.DealStatusID>1 ORDER BY pt.CreateDate asc,pt.ID asc";
                DataTable dtUserList = DBUtility.DbHelperSQL.GetDataTable(strUserList);
                UserJson = CdtTojson.DataTableToJson(dtUserList, false);

            }
            
            ViewData["LeftJson"] = LeftJson;
            ViewData["UserJson"] = UserJson;
            #endregion
            return View(MProject);
        }
        #endregion
        //拼接json
        string GetUser(DataTable dtUser, DataTable dtList)
        {
            string json = "";
            string ProjectTaskID = "";
            int Usercount = dtUser.Rows.Count+1;
            int TaskCount = 0;
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                string json1 = "{", json2 = "{";
                string json3 = ""; 
                #region
                foreach (DataRow dr in dtUser.Rows)
                {                    
                    string userid = dr["userid"].ToString();
                    string TrueName = dr["TrueName"].ToString();
                    string Dept = dr["Dept"].ToString();
                    json1 += "\"Dept" + userid + "\":\"" + Dept + "\",";
                    json2 += "\"Dept" + userid + "\":\"" + TrueName + "\",";
                   
                }
                if (json1.Length > 1)
                {
                    json1 = json1.Remove(json1.Length - 1, 1);
                }
                json1 += "}";
                if (json2.Length > 1)
                {
                    json2 = json2.Remove(json2.Length - 1, 1);
                }

                json2 += "}";
                #endregion

                #region 遍历生成工作内容相关
                for (int i = 0; i < dtList.Rows.Count; i++)
                {
                    string listUserid = dtList.Rows[i]["userid"].ToString();
                    string listTask = dtList.Rows[i]["Task"].ToString();
                    string listProjectTaskID = dtList.Rows[i]["ProjectTaskID"].ToString();

                    if (listUserid != "0")
                    {
                        if (ProjectTaskID == "" || ProjectTaskID != listProjectTaskID)
                        {
                            ProjectTaskID = listProjectTaskID;
                            TaskCount++;
                        }
                    }
                }
                #endregion
                string[,] arrays = new string[1+ TaskCount, Usercount];//用一个数组存放user的清单
                int num = 0;
                arrays[0, 0] = "0";
                foreach (DataRow dr in dtUser.Rows)
                {
                    string userid = dr["userid"].ToString();
                    string TrueName = dr["TrueName"].ToString();
                    string Dept = dr["Dept"].ToString();
                    num++;
                    arrays[0, num] = userid;
                }
                string flagProjectTaskID = "";
                int flagCount = 0;
                for (int i = 0; i < dtList.Rows.Count; i++)
                {
                    string listUserid = dtList.Rows[i]["userid"].ToString();
                    string listTask = dtList.Rows[i]["Task"].ToString();
                    string listProjectTaskID = dtList.Rows[i]["ProjectTaskID"].ToString();

                    if (listUserid != "0")
                    {
                        if (flagProjectTaskID == "" || flagProjectTaskID != listProjectTaskID)
                        {
                            flagProjectTaskID = listProjectTaskID;
                            flagCount++;
                            arrays[flagCount, 0] = listProjectTaskID;
                        }
                    }
                }
                #region 遍历数组，将用户信息存入数组
                for (int i = 1; i < TaskCount + 1; i++)
                {
                    string taskid = arrays[i, 0];

                    foreach (DataRow dr in dtList.Rows)
                    {
                        string listUserid = dr["userid"].ToString();
                        string listTask = dr["Task"].ToString();
                        string listProjectTaskID = dr["ProjectTaskID"].ToString();
                        if (taskid == listProjectTaskID)
                        {//遍历列
                            for (int j = 1; j < Usercount; j++)
                            {
                                string userid = arrays[0, j];
                                if (userid == listUserid)
                                {
                                    arrays[i, j] = listTask;
                                }
                            }
                        }
                    }
                }
                #endregion
                #region 遍历数组，输出结果
                for (int i = 1; i < TaskCount + 1; i++)
                {
                    json3 += ",{";
                    string taskid = arrays[i, 0];
                    for (int j = 1; j < Usercount; j++)
                    {
                        string userid = arrays[0, j];
                        string flag= arrays[i, j];
                        if (j > 1)
                        {
                            json3 += ",";
                        }
                        json3 += "\"Dept" + userid + "\":\"" + flag + "\"";
                    }
                    json3 += "}";
                }
                #endregion

                json = json1 + "," + json2 + json3;
            }
            return json;
        }

    }
}
