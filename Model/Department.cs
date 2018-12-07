/**  版本信息模板在安装目录下，可自行修改。
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
namespace Maticsoft.Model
{
	/// <summary>
	/// 公司表
	/// </summary>
	[Serializable]
	public partial class Department
	{
		public Department()
		{}
		#region Model
		private int _id;
		private string _name;
		private int? _levels;
		private int? _isactive;
		private int? _display;
		private int? _preid;
		private int _cpid=1;
		private string _dstatus;
		private decimal? _dorder;
		private DateTime? _createdate;
		private string _createuser;
		private int? _parentid;
		private int? _ucdepartmentid;
		/// <summary>
		/// 部门表主键
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 部门名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 等级，0（最高），1，2，3
		/// </summary>
		public int? Levels
		{
			set{ _levels=value;}
			get{return _levels;}
		}
		/// <summary>
		/// 是否可用，0不可用，1可用
		/// </summary>
		public int? IsActive
		{
			set{ _isactive=value;}
			get{return _isactive;}
		}
		/// <summary>
		/// 显示顺序，从1-9的顺序排列
		/// </summary>
		public int? Display
		{
			set{ _display=value;}
			get{return _display;}
		}
		/// <summary>
		/// 父级别ID
		/// </summary>
		public int? PreID
		{
			set{ _preid=value;}
			get{return _preid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CpId
		{
			set{ _cpid=value;}
			get{return _cpid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DStatus
		{
			set{ _dstatus=value;}
			get{return _dstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DOrder
		{
			set{ _dorder=value;}
			get{return _dorder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreateUser
		{
			set{ _createuser=value;}
			get{return _createuser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ParentId
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UcDepartmentId
		{
			set{ _ucdepartmentid=value;}
			get{return _ucdepartmentid;}
		}
		#endregion Model

	}
}

