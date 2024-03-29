﻿/**  版本信息模板在安装目录下，可自行修改。
* Department.cs
*
* 功 能： N/A
* 类 名： Department
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018/5/14 10:17:38   N/A    初版
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
	/// 数据访问类:Department
	/// </summary>
	public partial class Department
	{
		public Department()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "Department"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Department");
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
		public int Add(Maticsoft.Model.Department model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Department(");
			strSql.Append("Name,Levels,IsActive,Display,PreID,CpId,DStatus,DOrder,CreateDate,CreateUser,ParentId,UcDepartmentId)");
			strSql.Append(" values (");
			strSql.Append("@Name,@Levels,@IsActive,@Display,@PreID,@CpId,@DStatus,@DOrder,@CreateDate,@CreateUser,@ParentId,@UcDepartmentId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Levels", SqlDbType.Int,4),
					new SqlParameter("@IsActive", SqlDbType.Int,4),
					new SqlParameter("@Display", SqlDbType.Int,4),
					new SqlParameter("@PreID", SqlDbType.Int,4),
					new SqlParameter("@CpId", SqlDbType.Int,4),
					new SqlParameter("@DStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@DOrder", SqlDbType.Float,8),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,100),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@UcDepartmentId", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.Levels;
			parameters[2].Value = model.IsActive;
			parameters[3].Value = model.Display;
			parameters[4].Value = model.PreID;
			parameters[5].Value = model.CpId;
			parameters[6].Value = model.DStatus;
			parameters[7].Value = model.DOrder;
			parameters[8].Value = model.CreateDate;
			parameters[9].Value = model.CreateUser;
			parameters[10].Value = model.ParentId;
			parameters[11].Value = model.UcDepartmentId;

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
		public bool Update(Maticsoft.Model.Department model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Department set ");
			strSql.Append("Name=@Name,");
			strSql.Append("Levels=@Levels,");
			strSql.Append("IsActive=@IsActive,");
			strSql.Append("Display=@Display,");
			strSql.Append("PreID=@PreID,");
			strSql.Append("CpId=@CpId,");
			strSql.Append("DStatus=@DStatus,");
			strSql.Append("DOrder=@DOrder,");
			strSql.Append("CreateDate=@CreateDate,");
			strSql.Append("CreateUser=@CreateUser,");
			strSql.Append("ParentId=@ParentId,");
			strSql.Append("UcDepartmentId=@UcDepartmentId");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Levels", SqlDbType.Int,4),
					new SqlParameter("@IsActive", SqlDbType.Int,4),
					new SqlParameter("@Display", SqlDbType.Int,4),
					new SqlParameter("@PreID", SqlDbType.Int,4),
					new SqlParameter("@CpId", SqlDbType.Int,4),
					new SqlParameter("@DStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@DOrder", SqlDbType.Float,8),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,100),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@UcDepartmentId", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.Levels;
			parameters[2].Value = model.IsActive;
			parameters[3].Value = model.Display;
			parameters[4].Value = model.PreID;
			parameters[5].Value = model.CpId;
			parameters[6].Value = model.DStatus;
			parameters[7].Value = model.DOrder;
			parameters[8].Value = model.CreateDate;
			parameters[9].Value = model.CreateUser;
			parameters[10].Value = model.ParentId;
			parameters[11].Value = model.UcDepartmentId;
			parameters[12].Value = model.ID;

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
			strSql.Append("delete from Department ");
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
			strSql.Append("delete from Department ");
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
		public Maticsoft.Model.Department GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,Name,Levels,IsActive,Display,PreID,CpId,DStatus,DOrder,CreateDate,CreateUser,ParentId,UcDepartmentId from Department ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.Department model=new Maticsoft.Model.Department();
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
		public Maticsoft.Model.Department DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Department model=new Maticsoft.Model.Department();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["Levels"]!=null && row["Levels"].ToString()!="")
				{
					model.Levels=int.Parse(row["Levels"].ToString());
				}
				if(row["IsActive"]!=null && row["IsActive"].ToString()!="")
				{
					model.IsActive=int.Parse(row["IsActive"].ToString());
				}
				if(row["Display"]!=null && row["Display"].ToString()!="")
				{
					model.Display=int.Parse(row["Display"].ToString());
				}
				if(row["PreID"]!=null && row["PreID"].ToString()!="")
				{
					model.PreID=int.Parse(row["PreID"].ToString());
				}
				if(row["CpId"]!=null && row["CpId"].ToString()!="")
				{
					model.CpId=int.Parse(row["CpId"].ToString());
				}
				if(row["DStatus"]!=null)
				{
					model.DStatus=row["DStatus"].ToString();
				}
				if(row["DOrder"]!=null && row["DOrder"].ToString()!="")
				{
					model.DOrder=decimal.Parse(row["DOrder"].ToString());
				}
				if(row["CreateDate"]!=null && row["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(row["CreateDate"].ToString());
				}
				if(row["CreateUser"]!=null)
				{
					model.CreateUser=row["CreateUser"].ToString();
				}
				if(row["ParentId"]!=null && row["ParentId"].ToString()!="")
				{
					model.ParentId=int.Parse(row["ParentId"].ToString());
				}
				if(row["UcDepartmentId"]!=null && row["UcDepartmentId"].ToString()!="")
				{
					model.UcDepartmentId=int.Parse(row["UcDepartmentId"].ToString());
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
			strSql.Append("select ID,Name,Levels,IsActive,Display,PreID,CpId,DStatus,DOrder,CreateDate,CreateUser,ParentId,UcDepartmentId ");
			strSql.Append(" FROM Department ");
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
			strSql.Append(" ID,Name,Levels,IsActive,Display,PreID,CpId,DStatus,DOrder,CreateDate,CreateUser,ParentId,UcDepartmentId ");
			strSql.Append(" FROM Department ");
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
			strSql.Append("select count(1) FROM Department ");
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
			strSql.Append(")AS Row, T.*  from Department T ");
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
			parameters[0].Value = "Department";
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

