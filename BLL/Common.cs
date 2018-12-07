using System;
using System.Data;
using System.Collections.Generic;

using Maticsoft.Model;
namespace Maticsoft.BLL
{
    public partial class Common
    {
        public Common()
        { }

        public string GetNameByUserInfoID(int UserInfoID)
        {
            string Name = "";
            BLL.UserInfo BUserInfo = new BLL.UserInfo();
            Model.UserInfo MUserInfo = new Model.UserInfo();
            MUserInfo = BUserInfo.GetModel(UserInfoID);
            if (MUserInfo != null)
            {
                Name = MUserInfo.TrueName;
            }
            return Name;
        }
        public string GetRoleNameByRoleID(string RoleID)
        {
            string Name = "";
            if (RoleID != "")
            {
                BLL.Role BRole = new BLL.Role();
                Model.Role MRole = new Model.Role();
                MRole = BRole.GetModel(int.Parse(RoleID));
                if (MRole != null)
                {
                    Name = MRole.Name;
                }
            }
            return Name;
        }

        public string GetNumByPlanFlag(int ProjectID,string flag)
        {
            string Num = "";
            string className = "";
            Maticsoft.BLL.ProjectTask BProjectTask = new Maticsoft.BLL.ProjectTask();
            string strWhere = " ProjectID=" + ProjectID;
            #region 筛选条件
            if (flag.ToLower() == "new")
            {//新需求
                strWhere += " and ProjectTaskCheckStatusID=1";
                className = "label-danger";
            }
            else if (flag.ToLower() == "hascheck")
            {//已审批
                strWhere += " and ProjectTaskCheckStatusID=2";
                className = "label-info";
            }
            else if (flag.ToLower() == "running")
            {//进行中
                strWhere += " and ProjectTaskCheckStatusID=2";
                strWhere += " and DealStatusID=2";
                className = "label-warning";
            }
            else if (flag.ToLower() == "notbegin")
            {//未开始
                strWhere += " and ProjectTaskCheckStatusID=2";
                strWhere += " and DealStatusID=1";
                className = "label-default";
            }
            else if (flag.ToLower() == "done")
            {//已完成
                strWhere += " and ProjectTaskCheckStatusID=2";
                strWhere += " and DealStatusID=3";
                className = "label-primary";
            }
            else if (flag.ToLower() == "cancel")
            {//已取消
                strWhere += " and ProjectTaskCheckStatusID=2";
                strWhere += " and DealStatusID=4";
                className = "label-default";
            }
            #endregion
            int count = BProjectTask.GetRecordCount(strWhere);
            Num = count.ToString();
            if (count != 0)
            {
                if (flag.ToLower() == "new"|| flag.ToLower() == "hascheck")
                {
                    Num = "<span class='label operate " + className + "'data-operate='scanPlan' data-where='"+ flag.ToLower() + "'>" + Num + "</span>";
                }
                else
                {
                    Num = "<span class='label operate " + className + "'data-operate='editprogress' data-where='"+ flag.ToLower() + "'>" + Num + "</span>";
                }
            }
            return Num;
        }


    }
}
