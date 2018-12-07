using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Index()
        {
            return View();
        }
        #region 部门列表相关
        //部门维护
        public ActionResult DepartmentList()
        {
            return View();
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentItem(int page = 1, string SearchName = "")
        {
            Maticsoft.BLL.Department BDepartment = new Maticsoft.BLL.Department();
            List<Maticsoft.Model.Department> MDepartmentList = new List<Maticsoft.Model.Department>();
            int pagecount = 0;
            string strWhere = " ID>0  ";
            if (SearchName != "")
            {
                strWhere += " and Name like '%" + SearchName + "%'";
            }
            string strorder = "IsActive Desc,PreID asc,Display asc";
            int pageSize = 20;
            DataSet ds = BDepartment.GetListByPage(strWhere, strorder, ((page - 1) * pageSize + 1), page * pageSize);//分页获取到数据
            pagecount = BDepartment.GetRecordCount(strWhere);
            if (ds != null)
            {
                MDepartmentList = BDepartment.DataTableToList(ds.Tables[0]);//把datatable转化成List
                ViewData["currentpage"] = page;//当前页码
                ViewData["pagecount"] = pagecount;
            }
            return View(MDepartmentList);
        }

        #endregion


        #region 字典表（BaseData）相关
        //部门维护
        public ActionResult DictionaryList()
        {
            return View();
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        public ActionResult DictionaryItem(int page = 1, string SearchName = "")
        {
            Maticsoft.BLL.BaseData BBaseData = new Maticsoft.BLL.BaseData();
            List<Maticsoft.Model.BaseData> MBaseDataList = new List<Maticsoft.Model.BaseData>();
            int pagecount = 0;
            string strWhere = " ID>0  ";
            if (SearchName != "")
            {
                strWhere += " and TableName like '%" + SearchName + "%'";
            }
            string strorder = "IsActive Desc,TableName asc,Display asc";
            int pagesize = 20;
            DataSet ds = BBaseData.GetListByPage(strWhere, strorder, ((page - 1) * pagesize + 1), page * pagesize);//分页获取到数据
            pagecount = BBaseData.GetRecordCount(strWhere);
            if (ds != null)
            {
                MBaseDataList = BBaseData.DataTableToList(ds.Tables[0]);//把datatable转化成List
                ViewData["currentpage"] = page;//当前页码
                ViewData["pagecount"] = pagecount;
            }
            return View(MBaseDataList);
        }

        #endregion
    }
}