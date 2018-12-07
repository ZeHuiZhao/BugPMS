/**  版本信息模板在安装目录下，可自行修改。
* Company.cs
*
* 功 能： N/A
* 类 名： Company
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
	public partial class Company
	{
		public Company()
		{}
		#region Model
		private int _id;
		private string _name;
		private string _cstatus;
		private decimal? _corder;
		private DateTime? _createdate;
		private string _createuser;
		private int? _parentid;
		/// <summary>
		/// 公司ID(主键)
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 公司名字
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 状态[1=生效,0=失效]
		/// </summary>
		public string CStatus
		{
			set{ _cstatus=value;}
			get{return _cstatus;}
		}
		/// <summary>
		/// 排序
		/// </summary>
		public decimal? COrder
		{
			set{ _corder=value;}
			get{return _corder;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 创建人
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
		#endregion Model

	}
}

