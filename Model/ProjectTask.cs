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
namespace Maticsoft.Model
{
	/// <summary>
	/// ProjectTask:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProjectTask
	{
		public ProjectTask()
		{}
		#region Model
		private int _id;
		private int? _projectid;
		private int? _createuserid;
		private DateTime? _createdate;
		private int? _dealuserid;
		private int? _dealstatusid;
		private int? _taskno;
		private string _fileaddress;
		private string _contents;
		private DateTime? _expecteddate;
		private DateTime? _planstarttime;
		private DateTime? _planendtime;
		private int? _projecttaskcheckstatusid;
		private DateTime? _checkdate;
		private int? _enable;
		private string _target;
		private string _needsupport;
		private DateTime? _realstarttime;
		private DateTime? _realendtime;
		private int? _mark;
		private string _result;
		private string _reason;
		private string _dealmethods;
		private string _note;
		private int? _edituserid;
		private DateTime? _editdate;
		private string _checknote;
		/// <summary>
		/// 项目计划表主键
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 项目ID
		/// </summary>
		public int? ProjectID
		{
			set{ _projectid=value;}
			get{return _projectid;}
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
		/// 处理人
		/// </summary>
		public int? DealUserID
		{
			set{ _dealuserid=value;}
			get{return _dealuserid;}
		}
		/// <summary>
		/// 完成状态
		/// </summary>
		public int? DealStatusID
		{
			set{ _dealstatusid=value;}
			get{return _dealstatusid;}
		}
		/// <summary>
		/// 步骤序号
		/// </summary>
		public int? TaskNo
		{
			set{ _taskno=value;}
			get{return _taskno;}
		}
		/// <summary>
		/// 附件url
		/// </summary>
		public string FileAddress
		{
			set{ _fileaddress=value;}
			get{return _fileaddress;}
		}
		/// <summary>
		/// 工作内容
		/// </summary>
		public string Contents
		{
			set{ _contents=value;}
			get{return _contents;}
		}
		/// <summary>
		/// 期望完成日期
		/// </summary>
		public DateTime? ExpectedDate
		{
			set{ _expecteddate=value;}
			get{return _expecteddate;}
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
		/// 任务审核状态
		/// </summary>
		public int? ProjectTaskCheckStatusID
		{
			set{ _projecttaskcheckstatusid=value;}
			get{return _projecttaskcheckstatusid;}
		}
		/// <summary>
		/// 审核时间
		/// </summary>
		public DateTime? CheckDate
		{
			set{ _checkdate=value;}
			get{return _checkdate;}
		}
		/// <summary>
		/// 任务状态，0不可用（已删除），1可用
		/// </summary>
		public int? Enable
		{
			set{ _enable=value;}
			get{return _enable;}
		}
		/// <summary>
		/// 目标输出成果
		/// </summary>
		public string Target
		{
			set{ _target=value;}
			get{return _target;}
		}
		/// <summary>
		/// 所需资源与支持
		/// </summary>
		public string NeedSupport
		{
			set{ _needsupport=value;}
			get{return _needsupport;}
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
		/// 标记，1是重要节点，2是结果输出
		/// </summary>
		public int? Mark
		{
			set{ _mark=value;}
			get{return _mark;}
		}
		/// <summary>
		/// 完成情况
		/// </summary>
		public string Result
		{
			set{ _result=value;}
			get{return _result;}
		}
		/// <summary>
		/// 差距及原因分析
		/// </summary>
		public string Reason
		{
			set{ _reason=value;}
			get{return _reason;}
		}
		/// <summary>
		/// 处理方式,改善对策及措施
		/// </summary>
		public string DealMethods
		{
			set{ _dealmethods=value;}
			get{return _dealmethods;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Note
		{
			set{ _note=value;}
			get{return _note;}
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
		/// <summary>
		/// 审核意见
		/// </summary>
		public string CheckNote
		{
			set{ _checknote=value;}
			get{return _checknote;}
		}
		#endregion Model

	}
}

