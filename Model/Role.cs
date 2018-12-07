using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// Role:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Role
	{
		public Role()
		{}
		#region Model
		private int _id;
		private string _name;
		private string _privilegeid;
		private string _appname;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PrivilegeID
		{
			set{ _privilegeid=value;}
			get{return _privilegeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string APPName
		{
			set{ _appname=value;}
			get{return _appname;}
		}
		#endregion Model

	}
}

