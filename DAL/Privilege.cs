using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using  DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:Privilege
	/// </summary>
	public partial class Privilege
	{
		public Privilege()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "Privilege"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Privilege");
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
		public int Add(Maticsoft.Model.Privilege model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Privilege(");
			strSql.Append("Name,ParentID,DisplayOrder,Enable,CssStyle,Href,li,APPName)");
			strSql.Append(" values (");
			strSql.Append("@Name,@ParentID,@DisplayOrder,@Enable,@CssStyle,@Href,@li,@APPName)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@DisplayOrder", SqlDbType.Int,4),
					new SqlParameter("@Enable", SqlDbType.Int,4),
					new SqlParameter("@CssStyle", SqlDbType.NVarChar,100),
					new SqlParameter("@Href", SqlDbType.NVarChar,100),
					new SqlParameter("@li", SqlDbType.NVarChar,100),
					new SqlParameter("@APPName", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.ParentID;
			parameters[2].Value = model.DisplayOrder;
			parameters[3].Value = model.Enable;
			parameters[4].Value = model.CssStyle;
			parameters[5].Value = model.Href;
			parameters[6].Value = model.li;
			parameters[7].Value = model.APPName;

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
		public bool Update(Maticsoft.Model.Privilege model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Privilege set ");
			strSql.Append("Name=@Name,");
			strSql.Append("ParentID=@ParentID,");
			strSql.Append("DisplayOrder=@DisplayOrder,");
			strSql.Append("Enable=@Enable,");
			strSql.Append("CssStyle=@CssStyle,");
			strSql.Append("Href=@Href,");
			strSql.Append("li=@li,");
			strSql.Append("APPName=@APPName");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@DisplayOrder", SqlDbType.Int,4),
					new SqlParameter("@Enable", SqlDbType.Int,4),
					new SqlParameter("@CssStyle", SqlDbType.NVarChar,100),
					new SqlParameter("@Href", SqlDbType.NVarChar,100),
					new SqlParameter("@li", SqlDbType.NVarChar,100),
					new SqlParameter("@APPName", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.ParentID;
			parameters[2].Value = model.DisplayOrder;
			parameters[3].Value = model.Enable;
			parameters[4].Value = model.CssStyle;
			parameters[5].Value = model.Href;
			parameters[6].Value = model.li;
			parameters[7].Value = model.APPName;
			parameters[8].Value = model.ID;

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
			strSql.Append("delete from Privilege ");
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
			strSql.Append("delete from Privilege ");
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
		public Maticsoft.Model.Privilege GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,Name,ParentID,DisplayOrder,Enable,CssStyle,Href,li,APPName from Privilege ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.Privilege model=new Maticsoft.Model.Privilege();
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
		public Maticsoft.Model.Privilege DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Privilege model=new Maticsoft.Model.Privilege();
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
				if(row["ParentID"]!=null && row["ParentID"].ToString()!="")
				{
					model.ParentID=int.Parse(row["ParentID"].ToString());
				}
				if(row["DisplayOrder"]!=null && row["DisplayOrder"].ToString()!="")
				{
					model.DisplayOrder=int.Parse(row["DisplayOrder"].ToString());
				}
				if(row["Enable"]!=null && row["Enable"].ToString()!="")
				{
					model.Enable=int.Parse(row["Enable"].ToString());
				}
				if(row["CssStyle"]!=null)
				{
					model.CssStyle=row["CssStyle"].ToString();
				}
				if(row["Href"]!=null)
				{
					model.Href=row["Href"].ToString();
				}
				if(row["li"]!=null)
				{
					model.li=row["li"].ToString();
				}
				if(row["APPName"]!=null)
				{
					model.APPName=row["APPName"].ToString();
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
			strSql.Append("select ID,Name,ParentID,DisplayOrder,Enable,CssStyle,Href,li,APPName ");
			strSql.Append(" FROM Privilege ");
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
			strSql.Append(" ID,Name,ParentID,DisplayOrder,Enable,CssStyle,Href,li,APPName ");
			strSql.Append(" FROM Privilege ");
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
			strSql.Append("select count(1) FROM Privilege ");
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
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex,out int pagecount)
		{
			StringBuilder strSql=new StringBuilder();
            const string tablename = "Privilege";
            string strSqlCount = "select count(1) from " + tablename;

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
			strSql.Append(")AS Row, T.*  from Privilege T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
                strSqlCount += " WHERE " + strWhere;
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    pagecount = Convert.ToInt32(DbHelperSQL.GetSingle(strSqlCount));
                    return ds;
                }
            }
            pagecount = 0;
            return null;
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
			parameters[0].Value = "Privilege";
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

