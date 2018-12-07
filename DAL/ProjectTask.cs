/**  版本信息模板在安装目录下，可自行修改。
* ProjectTask.cs
*
* 功 能： N/A
* 类 名： ProjectTask
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018/5/14 9:40:22   N/A    初版
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
	/// 数据访问类:ProjectTask
	/// </summary>
	public partial class ProjectTask
	{
		public ProjectTask()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "ProjectTask"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ProjectTask");
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
		public int Add(Maticsoft.Model.ProjectTask model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ProjectTask(");
			strSql.Append("ProjectID,CreateUserID,CreateDate,DealUserID,DealStatusID,TaskNo,FileAddress,Contents,ExpectedDate,PlanStartTime,PlanEndTime,ProjectTaskCheckStatusID,CheckDate,Enable,Target,NeedSupport,RealStartTime,RealEndTime,Mark,Result,Reason,DealMethods,Note,EditUserID,EditDate,CheckNote)");
			strSql.Append(" values (");
			strSql.Append("@ProjectID,@CreateUserID,@CreateDate,@DealUserID,@DealStatusID,@TaskNo,@FileAddress,@Contents,@ExpectedDate,@PlanStartTime,@PlanEndTime,@ProjectTaskCheckStatusID,@CheckDate,@Enable,@Target,@NeedSupport,@RealStartTime,@RealEndTime,@Mark,@Result,@Reason,@DealMethods,@Note,@EditUserID,@EditDate,@CheckNote)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@CreateUserID", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@DealUserID", SqlDbType.Int,4),
					new SqlParameter("@DealStatusID", SqlDbType.Int,4),
					new SqlParameter("@TaskNo", SqlDbType.Int,4),
					new SqlParameter("@FileAddress", SqlDbType.NVarChar,500),
					new SqlParameter("@Contents", SqlDbType.Text),
					new SqlParameter("@ExpectedDate", SqlDbType.DateTime),
					new SqlParameter("@PlanStartTime", SqlDbType.DateTime),
					new SqlParameter("@PlanEndTime", SqlDbType.DateTime),
					new SqlParameter("@ProjectTaskCheckStatusID", SqlDbType.Int,4),
					new SqlParameter("@CheckDate", SqlDbType.DateTime),
					new SqlParameter("@Enable", SqlDbType.Int,4),
					new SqlParameter("@Target", SqlDbType.NVarChar,500),
					new SqlParameter("@NeedSupport", SqlDbType.NVarChar,500),
					new SqlParameter("@RealStartTime", SqlDbType.DateTime),
					new SqlParameter("@RealEndTime", SqlDbType.DateTime),
					new SqlParameter("@Mark", SqlDbType.Int,4),
					new SqlParameter("@Result", SqlDbType.NVarChar,500),
					new SqlParameter("@Reason", SqlDbType.NVarChar,500),
					new SqlParameter("@DealMethods", SqlDbType.NVarChar,500),
					new SqlParameter("@Note", SqlDbType.NVarChar,500),
					new SqlParameter("@EditUserID", SqlDbType.Int,4),
					new SqlParameter("@EditDate", SqlDbType.DateTime),
					new SqlParameter("@CheckNote", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.ProjectID;
			parameters[1].Value = model.CreateUserID;
			parameters[2].Value = model.CreateDate;
			parameters[3].Value = model.DealUserID;
			parameters[4].Value = model.DealStatusID;
			parameters[5].Value = model.TaskNo;
			parameters[6].Value = model.FileAddress;
			parameters[7].Value = model.Contents;
			parameters[8].Value = model.ExpectedDate;
			parameters[9].Value = model.PlanStartTime;
			parameters[10].Value = model.PlanEndTime;
			parameters[11].Value = model.ProjectTaskCheckStatusID;
			parameters[12].Value = model.CheckDate;
			parameters[13].Value = model.Enable;
			parameters[14].Value = model.Target;
			parameters[15].Value = model.NeedSupport;
			parameters[16].Value = model.RealStartTime;
			parameters[17].Value = model.RealEndTime;
			parameters[18].Value = model.Mark;
			parameters[19].Value = model.Result;
			parameters[20].Value = model.Reason;
			parameters[21].Value = model.DealMethods;
			parameters[22].Value = model.Note;
			parameters[23].Value = model.EditUserID;
			parameters[24].Value = model.EditDate;
			parameters[25].Value = model.CheckNote;

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
		public bool Update(Maticsoft.Model.ProjectTask model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ProjectTask set ");
			strSql.Append("ProjectID=@ProjectID,");
			strSql.Append("CreateUserID=@CreateUserID,");
			strSql.Append("CreateDate=@CreateDate,");
			strSql.Append("DealUserID=@DealUserID,");
			strSql.Append("DealStatusID=@DealStatusID,");
			strSql.Append("TaskNo=@TaskNo,");
			strSql.Append("FileAddress=@FileAddress,");
			strSql.Append("Contents=@Contents,");
			strSql.Append("ExpectedDate=@ExpectedDate,");
			strSql.Append("PlanStartTime=@PlanStartTime,");
			strSql.Append("PlanEndTime=@PlanEndTime,");
			strSql.Append("ProjectTaskCheckStatusID=@ProjectTaskCheckStatusID,");
			strSql.Append("CheckDate=@CheckDate,");
			strSql.Append("Enable=@Enable,");
			strSql.Append("Target=@Target,");
			strSql.Append("NeedSupport=@NeedSupport,");
			strSql.Append("RealStartTime=@RealStartTime,");
			strSql.Append("RealEndTime=@RealEndTime,");
			strSql.Append("Mark=@Mark,");
			strSql.Append("Result=@Result,");
			strSql.Append("Reason=@Reason,");
			strSql.Append("DealMethods=@DealMethods,");
			strSql.Append("Note=@Note,");
			strSql.Append("EditUserID=@EditUserID,");
			strSql.Append("EditDate=@EditDate,");
			strSql.Append("CheckNote=@CheckNote");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@CreateUserID", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@DealUserID", SqlDbType.Int,4),
					new SqlParameter("@DealStatusID", SqlDbType.Int,4),
					new SqlParameter("@TaskNo", SqlDbType.Int,4),
					new SqlParameter("@FileAddress", SqlDbType.NVarChar,500),
					new SqlParameter("@Contents", SqlDbType.Text),
					new SqlParameter("@ExpectedDate", SqlDbType.DateTime),
					new SqlParameter("@PlanStartTime", SqlDbType.DateTime),
					new SqlParameter("@PlanEndTime", SqlDbType.DateTime),
					new SqlParameter("@ProjectTaskCheckStatusID", SqlDbType.Int,4),
					new SqlParameter("@CheckDate", SqlDbType.DateTime),
					new SqlParameter("@Enable", SqlDbType.Int,4),
					new SqlParameter("@Target", SqlDbType.NVarChar,500),
					new SqlParameter("@NeedSupport", SqlDbType.NVarChar,500),
					new SqlParameter("@RealStartTime", SqlDbType.DateTime),
					new SqlParameter("@RealEndTime", SqlDbType.DateTime),
					new SqlParameter("@Mark", SqlDbType.Int,4),
					new SqlParameter("@Result", SqlDbType.NVarChar,500),
					new SqlParameter("@Reason", SqlDbType.NVarChar,500),
					new SqlParameter("@DealMethods", SqlDbType.NVarChar,500),
					new SqlParameter("@Note", SqlDbType.NVarChar,500),
					new SqlParameter("@EditUserID", SqlDbType.Int,4),
					new SqlParameter("@EditDate", SqlDbType.DateTime),
					new SqlParameter("@CheckNote", SqlDbType.NVarChar,500),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.ProjectID;
			parameters[1].Value = model.CreateUserID;
			parameters[2].Value = model.CreateDate;
			parameters[3].Value = model.DealUserID;
			parameters[4].Value = model.DealStatusID;
			parameters[5].Value = model.TaskNo;
			parameters[6].Value = model.FileAddress;
			parameters[7].Value = model.Contents;
			parameters[8].Value = model.ExpectedDate;
			parameters[9].Value = model.PlanStartTime;
			parameters[10].Value = model.PlanEndTime;
			parameters[11].Value = model.ProjectTaskCheckStatusID;
			parameters[12].Value = model.CheckDate;
			parameters[13].Value = model.Enable;
			parameters[14].Value = model.Target;
			parameters[15].Value = model.NeedSupport;
			parameters[16].Value = model.RealStartTime;
			parameters[17].Value = model.RealEndTime;
			parameters[18].Value = model.Mark;
			parameters[19].Value = model.Result;
			parameters[20].Value = model.Reason;
			parameters[21].Value = model.DealMethods;
			parameters[22].Value = model.Note;
			parameters[23].Value = model.EditUserID;
			parameters[24].Value = model.EditDate;
			parameters[25].Value = model.CheckNote;
			parameters[26].Value = model.ID;

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
			strSql.Append("delete from ProjectTask ");
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
			strSql.Append("delete from ProjectTask ");
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
		public Maticsoft.Model.ProjectTask GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,ProjectID,CreateUserID,CreateDate,DealUserID,DealStatusID,TaskNo,FileAddress,Contents,ExpectedDate,PlanStartTime,PlanEndTime,ProjectTaskCheckStatusID,CheckDate,Enable,Target,NeedSupport,RealStartTime,RealEndTime,Mark,Result,Reason,DealMethods,Note,EditUserID,EditDate,CheckNote from ProjectTask ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.ProjectTask model=new Maticsoft.Model.ProjectTask();
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
		public Maticsoft.Model.ProjectTask DataRowToModel(DataRow row)
		{
			Maticsoft.Model.ProjectTask model=new Maticsoft.Model.ProjectTask();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["ProjectID"]!=null && row["ProjectID"].ToString()!="")
				{
					model.ProjectID=int.Parse(row["ProjectID"].ToString());
				}
				if(row["CreateUserID"]!=null && row["CreateUserID"].ToString()!="")
				{
					model.CreateUserID=int.Parse(row["CreateUserID"].ToString());
				}
				if(row["CreateDate"]!=null && row["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(row["CreateDate"].ToString());
				}
				if(row["DealUserID"]!=null && row["DealUserID"].ToString()!="")
				{
					model.DealUserID=int.Parse(row["DealUserID"].ToString());
				}
				if(row["DealStatusID"]!=null && row["DealStatusID"].ToString()!="")
				{
					model.DealStatusID=int.Parse(row["DealStatusID"].ToString());
				}
				if(row["TaskNo"]!=null && row["TaskNo"].ToString()!="")
				{
					model.TaskNo=int.Parse(row["TaskNo"].ToString());
				}
				if(row["FileAddress"]!=null)
				{
					model.FileAddress=row["FileAddress"].ToString();
				}
				if(row["Contents"]!=null)
				{
					model.Contents=row["Contents"].ToString();
				}
				if(row["ExpectedDate"]!=null && row["ExpectedDate"].ToString()!="")
				{
					model.ExpectedDate=DateTime.Parse(row["ExpectedDate"].ToString());
				}
				if(row["PlanStartTime"]!=null && row["PlanStartTime"].ToString()!="")
				{
					model.PlanStartTime=DateTime.Parse(row["PlanStartTime"].ToString());
				}
				if(row["PlanEndTime"]!=null && row["PlanEndTime"].ToString()!="")
				{
					model.PlanEndTime=DateTime.Parse(row["PlanEndTime"].ToString());
				}
				if(row["ProjectTaskCheckStatusID"]!=null && row["ProjectTaskCheckStatusID"].ToString()!="")
				{
					model.ProjectTaskCheckStatusID=int.Parse(row["ProjectTaskCheckStatusID"].ToString());
				}
				if(row["CheckDate"]!=null && row["CheckDate"].ToString()!="")
				{
					model.CheckDate=DateTime.Parse(row["CheckDate"].ToString());
				}
				if(row["Enable"]!=null && row["Enable"].ToString()!="")
				{
					model.Enable=int.Parse(row["Enable"].ToString());
				}
				if(row["Target"]!=null)
				{
					model.Target=row["Target"].ToString();
				}
				if(row["NeedSupport"]!=null)
				{
					model.NeedSupport=row["NeedSupport"].ToString();
				}
				if(row["RealStartTime"]!=null && row["RealStartTime"].ToString()!="")
				{
					model.RealStartTime=DateTime.Parse(row["RealStartTime"].ToString());
				}
				if(row["RealEndTime"]!=null && row["RealEndTime"].ToString()!="")
				{
					model.RealEndTime=DateTime.Parse(row["RealEndTime"].ToString());
				}
				if(row["Mark"]!=null && row["Mark"].ToString()!="")
				{
					model.Mark=int.Parse(row["Mark"].ToString());
				}
				if(row["Result"]!=null)
				{
					model.Result=row["Result"].ToString();
				}
				if(row["Reason"]!=null)
				{
					model.Reason=row["Reason"].ToString();
				}
				if(row["DealMethods"]!=null)
				{
					model.DealMethods=row["DealMethods"].ToString();
				}
				if(row["Note"]!=null)
				{
					model.Note=row["Note"].ToString();
				}
				if(row["EditUserID"]!=null && row["EditUserID"].ToString()!="")
				{
					model.EditUserID=int.Parse(row["EditUserID"].ToString());
				}
				if(row["EditDate"]!=null && row["EditDate"].ToString()!="")
				{
					model.EditDate=DateTime.Parse(row["EditDate"].ToString());
				}
				if(row["CheckNote"]!=null)
				{
					model.CheckNote=row["CheckNote"].ToString();
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
			strSql.Append("select ID,ProjectID,CreateUserID,CreateDate,DealUserID,DealStatusID,TaskNo,FileAddress,Contents,ExpectedDate,PlanStartTime,PlanEndTime,ProjectTaskCheckStatusID,CheckDate,Enable,Target,NeedSupport,RealStartTime,RealEndTime,Mark,Result,Reason,DealMethods,Note,EditUserID,EditDate,CheckNote ");
			strSql.Append(" FROM ProjectTask ");
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
			strSql.Append(" ID,ProjectID,CreateUserID,CreateDate,DealUserID,DealStatusID,TaskNo,FileAddress,Contents,ExpectedDate,PlanStartTime,PlanEndTime,ProjectTaskCheckStatusID,CheckDate,Enable,Target,NeedSupport,RealStartTime,RealEndTime,Mark,Result,Reason,DealMethods,Note,EditUserID,EditDate,CheckNote ");
			strSql.Append(" FROM ProjectTask ");
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
			strSql.Append("select count(1) FROM ProjectTask ");
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
			strSql.Append(")AS Row, T.*  from ProjectTask T ");
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
			parameters[0].Value = "ProjectTask";
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

