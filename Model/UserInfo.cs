/**  版本信息模板在安装目录下，可自行修改。
* UserInfo.cs
*
* 功 能： N/A
* 类 名： UserInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018/5/14 10:17:39   N/A    初版
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
	public partial class UserInfo
	{
		public UserInfo()
		{}
		#region Model
		private int _id;
		private int? _parentid;
		private string _username;
		private string _password;
		private string _privilegeid;
		private string _truename;
		private int? _roleid;
		private DateTime? _time;
		private int? _status;
		private string _unionid;
		private string _userfrom;
		private int? _departmentid;
		private string _urguid;
		private string _headimage;
		/// <summary>
		/// 管理员—表主键
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 添加人ID
		/// </summary>
		public int? ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 权限ID
		/// </summary>
		public string PrivilegeID
		{
			set{ _privilegeid=value;}
			get{return _privilegeid;}
		}
		/// <summary>
		/// 真实姓名
		/// </summary>
		public string TrueName
		{
			set{ _truename=value;}
			get{return _truename;}
		}
		/// <summary>
		/// 角色ID
		/// </summary>
		public int? RoleID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime? Time
		{
			set{ _time=value;}
			get{return _time;}
		}
		/// <summary>
		/// 状态：0禁用;1可用
		/// </summary>
		public int? status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// ERP中的用户ID（预留）
		/// </summary>
		public string UnionID
		{
			set{ _unionid=value;}
			get{return _unionid;}
		}
		/// <summary>
		/// 用户来源
		/// </summary>
		public string UserFrom
		{
			set{ _userfrom=value;}
			get{return _userfrom;}
		}
		/// <summary>
		/// 部门表ID
		/// </summary>
		public int? DepartmentID
		{
			set{ _departmentid=value;}
			get{return _departmentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UrGuid
		{
			set{ _urguid=value;}
			get{return _urguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HeadImage
		{
			set{ _headimage=value;}
			get{return _headimage;}
		}
		#endregion Model

	}
}

