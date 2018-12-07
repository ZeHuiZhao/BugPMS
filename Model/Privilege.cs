using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// Privilege:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Privilege
	{
		public Privilege()
		{}
		#region Model
		private int _id;
		private string _name;
		private int? _parentid;
		private int? _displayorder;
		private int? _enable;
		private string _cssstyle;
		private string _href;
		private string _li;
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
		public int? ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DisplayOrder
		{
			set{ _displayorder=value;}
			get{return _displayorder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Enable
		{
			set{ _enable=value;}
			get{return _enable;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CssStyle
		{
			set{ _cssstyle=value;}
			get{return _cssstyle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Href
		{
			set{ _href=value;}
			get{return _href;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string li
		{
			set{ _li=value;}
			get{return _li;}
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

