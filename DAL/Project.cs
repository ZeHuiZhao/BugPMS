/**  版本信息模板在安装目录下，可自行修改。
* Project.cs
*
* 功 能： N/A
* 类 名： Project
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/3/10 11:39:12   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:Project
	/// </summary>
	public partial class Project
	{
		public Project()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "Project"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Project");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Project model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Project(");
			strSql.Append("Code,Name,ProjectTypeID,LeaderID,PlanStartTime,PlanEndTime,RealStartTime,RealEndTime,Contents,ProjectStatusID,CheckUserID,ProjectCheckStatus,CheckNote,CreateUserID,CreateDate,EditUserID,EditDate)");
			strSql.Append(" values (");
			strSql.Append("@Code,@Name,@ProjectTypeID,@LeaderID,@PlanStartTime,@PlanEndTime,@RealStartTime,@RealEndTime,@Contents,@ProjectStatusID,@CheckUserID,@ProjectCheckStatus,@CheckNote,@CreateUserID,@CreateDate,@EditUserID,@EditDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectTypeID", SqlDbType.Int,4),
					new SqlParameter("@LeaderID", SqlDbType.Int,4),
					new SqlParameter("@PlanStartTime", SqlDbType.DateTime),
					new SqlParameter("@PlanEndTime", SqlDbType.DateTime),
					new SqlParameter("@RealStartTime", SqlDbType.DateTime),
					new SqlParameter("@RealEndTime", SqlDbType.DateTime),
					new SqlParameter("@Contents", SqlDbType.NVarChar,2000),
					new SqlParameter("@ProjectStatusID", SqlDbType.Int,4),
					new SqlParameter("@CheckUserID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCheckStatus", SqlDbType.Int,4),
					new SqlParameter("@CheckNote", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateUserID", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@EditUserID", SqlDbType.Int,4),
					new SqlParameter("@EditDate", SqlDbType.DateTime)};
			parameters[0].Value = model.Code;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.ProjectTypeID;
			parameters[3].Value = model.LeaderID;
			parameters[4].Value = model.PlanStartTime;
			parameters[5].Value = model.PlanEndTime;
			parameters[6].Value = model.RealStartTime;
			parameters[7].Value = model.RealEndTime;
			parameters[8].Value = model.Contents;
			parameters[9].Value = model.ProjectStatusID;
			parameters[10].Value = model.CheckUserID;
			parameters[11].Value = model.ProjectCheckStatus;
			parameters[12].Value = model.CheckNote;
			parameters[13].Value = model.CreateUserID;
			parameters[14].Value = model.CreateDate;
			parameters[15].Value = model.EditUserID;
			parameters[16].Value = model.EditDate;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Project model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Project set ");
			strSql.Append("Code=@Code,");
			strSql.Append("Name=@Name,");
			strSql.Append("ProjectTypeID=@ProjectTypeID,");
			strSql.Append("LeaderID=@LeaderID,");
			strSql.Append("PlanStartTime=@PlanStartTime,");
			strSql.Append("PlanEndTime=@PlanEndTime,");
			strSql.Append("RealStartTime=@RealStartTime,");
			strSql.Append("RealEndTime=@RealEndTime,");
			strSql.Append("Contents=@Contents,");
			strSql.Append("ProjectStatusID=@ProjectStatusID,");
			strSql.Append("CheckUserID=@CheckUserID,");
			strSql.Append("ProjectCheckStatus=@ProjectCheckStatus,");
			strSql.Append("CheckNote=@CheckNote,");
			strSql.Append("CreateUserID=@CreateUserID,");
			strSql.Append("CreateDate=@CreateDate,");
			strSql.Append("EditUserID=@EditUserID,");
			strSql.Append("EditDate=@EditDate");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectTypeID", SqlDbType.Int,4),
					new SqlParameter("@LeaderID", SqlDbType.Int,4),
					new SqlParameter("@PlanStartTime", SqlDbType.DateTime),
					new SqlParameter("@PlanEndTime", SqlDbType.DateTime),
					new SqlParameter("@RealStartTime", SqlDbType.DateTime),
					new SqlParameter("@RealEndTime", SqlDbType.DateTime),
					new SqlParameter("@Contents", SqlDbType.NVarChar,2000),
					new SqlParameter("@ProjectStatusID", SqlDbType.Int,4),
					new SqlParameter("@CheckUserID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCheckStatus", SqlDbType.Int,4),
					new SqlParameter("@CheckNote", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateUserID", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@EditUserID", SqlDbType.Int,4),
					new SqlParameter("@EditDate", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.Code;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.ProjectTypeID;
			parameters[3].Value = model.LeaderID;
			parameters[4].Value = model.PlanStartTime;
			parameters[5].Value = model.PlanEndTime;
			parameters[6].Value = model.RealStartTime;
			parameters[7].Value = model.RealEndTime;
			parameters[8].Value = model.Contents;
			parameters[9].Value = model.ProjectStatusID;
			parameters[10].Value = model.CheckUserID;
			parameters[11].Value = model.ProjectCheckStatus;
			parameters[12].Value = model.CheckNote;
			parameters[13].Value = model.CreateUserID;
			parameters[14].Value = model.CreateDate;
			parameters[15].Value = model.EditUserID;
			parameters[16].Value = model.EditDate;
			parameters[17].Value = model.ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Project ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Project ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Project GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,Code,Name,ProjectTypeID,LeaderID,PlanStartTime,PlanEndTime,RealStartTime,RealEndTime,Contents,ProjectStatusID,CheckUserID,ProjectCheckStatus,CheckNote,CreateUserID,CreateDate,EditUserID,EditDate from Project ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.Project model=new Maticsoft.Model.Project();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Project DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Project model=new Maticsoft.Model.Project();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["Code"]!=null)
				{
					model.Code=row["Code"].ToString();
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["ProjectTypeID"]!=null && row["ProjectTypeID"].ToString()!="")
				{
					model.ProjectTypeID=int.Parse(row["ProjectTypeID"].ToString());
				}
				if(row["LeaderID"]!=null && row["LeaderID"].ToString()!="")
				{
					model.LeaderID=int.Parse(row["LeaderID"].ToString());
				}
				if(row["PlanStartTime"]!=null && row["PlanStartTime"].ToString()!="")
				{
					model.PlanStartTime=DateTime.Parse(row["PlanStartTime"].ToString());
				}
				if(row["PlanEndTime"]!=null && row["PlanEndTime"].ToString()!="")
				{
					model.PlanEndTime=DateTime.Parse(row["PlanEndTime"].ToString());
				}
				if(row["RealStartTime"]!=null && row["RealStartTime"].ToString()!="")
				{
					model.RealStartTime=DateTime.Parse(row["RealStartTime"].ToString());
				}
				if(row["RealEndTime"]!=null && row["RealEndTime"].ToString()!="")
				{
					model.RealEndTime=DateTime.Parse(row["RealEndTime"].ToString());
				}
				if(row["Contents"]!=null)
				{
					model.Contents=row["Contents"].ToString();
				}
				if(row["ProjectStatusID"]!=null && row["ProjectStatusID"].ToString()!="")
				{
					model.ProjectStatusID=int.Parse(row["ProjectStatusID"].ToString());
				}
				if(row["CheckUserID"]!=null && row["CheckUserID"].ToString()!="")
				{
					model.CheckUserID=int.Parse(row["CheckUserID"].ToString());
				}
				if(row["ProjectCheckStatus"]!=null && row["ProjectCheckStatus"].ToString()!="")
				{
					model.ProjectCheckStatus=int.Parse(row["ProjectCheckStatus"].ToString());
				}
				if(row["CheckNote"]!=null)
				{
					model.CheckNote=row["CheckNote"].ToString();
				}
				if(row["CreateUserID"]!=null && row["CreateUserID"].ToString()!="")
				{
					model.CreateUserID=int.Parse(row["CreateUserID"].ToString());
				}
				if(row["CreateDate"]!=null && row["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(row["CreateDate"].ToString());
				}
				if(row["EditUserID"]!=null && row["EditUserID"].ToString()!="")
				{
					model.EditUserID=int.Parse(row["EditUserID"].ToString());
				}
				if(row["EditDate"]!=null && row["EditDate"].ToString()!="")
				{
					model.EditDate=DateTime.Parse(row["EditDate"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,Code,Name,ProjectTypeID,LeaderID,PlanStartTime,PlanEndTime,RealStartTime,RealEndTime,Contents,ProjectStatusID,CheckUserID,ProjectCheckStatus,CheckNote,CreateUserID,CreateDate,EditUserID,EditDate ");
			strSql.Append(" FROM Project ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ID,Code,Name,ProjectTypeID,LeaderID,PlanStartTime,PlanEndTime,RealStartTime,RealEndTime,Contents,ProjectStatusID,CheckUserID,ProjectCheckStatus,CheckNote,CreateUserID,CreateDate,EditUserID,EditDate ");
			strSql.Append(" FROM Project ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Project ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from Project T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "Project";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

