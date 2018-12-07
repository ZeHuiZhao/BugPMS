using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DBUtility
{
    public partial class ConfigurationHelper
    {
        /// <summary>
        /// 二级域名配置文件
        /// </summary>
        /// <returns></returns>
        public string getRealmName()
        {
            return ConfigurationManager.AppSettings["RealmName"].ToString();
        }

        public string getConfigName(string name)
        {
            return ConfigurationManager.AppSettings[name].ToString();
        }
    }
}
