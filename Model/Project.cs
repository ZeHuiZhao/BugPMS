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
namespace Maticsoft.Model
{
	/// <summary>
	/// 1
	/// </summary>
	[Serializable]
	public partial class Project
	{
		public Project()
		{}
		#region Model
		private int _id;
		private string _code;
		private string _name;
		private int? _projecttypeid;
		private int? _leaderid;
		private DateTime? _planstarttime;
		private DateTime? _planendtime;
		private DateTime? _realstarttime;
		private DateTime? _realendtime;
		private string _contents;
		private int? _projectstatusid;
		private int? _checkuserid;
		private int? _projectcheckstatus;
		private string _checknote;
		private int? _createuserid;
		private DateTime? _createdate;
		private int? _edituserid;
		private DateTime? _editdate;
		/// <summary>
		/// 项目表主键
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 项目编号
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 项目名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 项目类别ID
		/// </summary>
		public int? ProjectTypeID
		{
			set{ _projecttypeid=value;}
			get{return _projecttypeid;}
		}
		/// <summary>
		/// 项目负责人ID
		/// </summary>
		public int? LeaderID
		{
			set{ _leaderid=value;}
			get{return _leaderid;}
		}
		/// <summary>
		/// 计划开始时间
		/// </summary>
		public DateTime? PlanStartTime
		{
			set{ _planstarttime=value;}
			get{return _planstarttime;}
		}
		/// <summary>
		/// 计划结束时间
		/// </summary>
		public DateTime? PlanEndTime
		{
			set{ _planendtime=value;}
			get{return _planendtime;}
		}
		/// <summary>
		/// 实际开始时间
		/// </summary>
		public DateTime? RealStartTime
		{
			set{ _realstarttime=value;}
			get{return _realstarttime;}
		}
		/// <summary>
		/// 实际结束时间
		/// </summary>
		public DateTime? RealEndTime
		{
			set{ _realendtime=value;}
			get{return _realendtime;}
		}
		/// <summary>
		/// 项目概述
		/// </summary>
		public string Contents
		{
			set{ _contents=value;}
			get{return _contents;}
		}
		/// <summary>
		/// 项目状态，0未开始，1进行中，2已结束，3已延期，4已取消
		/// </summary>
		public int? ProjectStatusID
		{
			set{ _projectstatusid=value;}
			get{return _projectstatusid;}
		}
		/// <summary>
		/// 审核人
		/// </summary>
		public int? CheckUserID
		{
			set{ _checkuserid=value;}
			get{return _checkuserid;}
		}
		/// <summary>
		/// 项目状态：0草稿（已保存），1已提交待审核，2审核通过，3审核未通过
		/// </summary>
		public int? ProjectCheckStatus
		{
			set{ _projectcheckstatus=value;}
			get{return _projectcheckstatus;}
		}
		/// <summary>
		/// 审核意见
		/// </summary>
		public string CheckNote
		{
			set{ _checknote=value;}
			get{return _checknote;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CreateUserID
		{
			set{ _createuserid=value;}
			get{return _createuserid;}
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
		public int? EditUserID
		{
			set{ _edituserid=value;}
			get{return _edituserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditDate
		{
			set{ _editdate=value;}
			get{return _editdate;}
		}
		#endregion Model

	}
}

