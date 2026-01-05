using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Health.Web.Models
{
    public class WebApiUrls
    {
        public static string HealthCurrentGetCurrentView
        {
            get
            {
                return GetValue("HealthCurrentGetCurrentView", "HealthCurrent/GetCurrentView");
            }
        }

        public static string HealthHisSearch
        {
            get
            {
                return GetValue("HealthHisSearch", "HealthHis/Search");
            }
        }

        public static string HealthHis2Search
        {
            get
            {
                return GetValue("HealthHis2Search", "HealthHis2/Search");
            }
        }

        public static string HealthTargetCreate
        {
            get
            {
                return GetValue("HealthTargetCreate", "HealthTarget/Create");
            }
        }

        public static string HealthTargetDelete
        {
            get
            {
                return GetValue("HealthTargetDelete", "HealthTarget/Delete");
            }
        }

        public static string HealthTargetUpdate
        {
            get
            {
                return GetValue("HealthTargetUpdate", "HealthTarget/Update");
            }
        }

        public static string HealthTargetSearch
        {
            get
            {
                return GetValue("HealthTargetSearch", "HealthTarget/Search");
            }
        }

        public static string NotificationInfoCreate
        {
            get
            {
                return GetValue("NotificationInfoCreate", "NotificationInfo/Create");
            }
        }

        public static string NotificationInfoDelete
        {
            get
            {
                return GetValue("NotificationInfoDelete", "NotificationInfo/Delete");
            }
        }

        public static string NotificationInfoUpdate
        {
            get
            {
                return GetValue("NotificationInfoUpdate", "NotificationInfo/Update");
            }
        }

        public static string NotificationInfoSearch
        {
            get
            {
                return GetValue("NotificationInfoSearch", "NotificationInfo/Search");
            }
        }

        public static string SystemInfoSearch
        {
            get
            {
                return GetValue("SystemInfoSearch", "SystemInfo/Search");
            }
        }

        public static string SystemInfoCreate
        {
            get
            {
                return GetValue("SystemInfoCreate", "SystemInfo/Create");
            }
        }

        public static string SystemInfoDelete
        {
            get
            {
                return GetValue("SystemInfoDelete", "SystemInfo/Delete");
            }
        }

        public static string SystemInfoUpdate
        {
            get
            {
                return GetValue("SystemInfoUpdate", "SystemInfo/Update");
            }
        }

        public static string SystemInfoUpdateThreshold
        {
            get
            {
                return GetValue("SystemInfoUpdateThreshold", "SystemInfo/UpdateThreshold");
            }
        }

        public static string UserTokenGetCurrentUsers
        {
            get
            {
                return GetValue("UserTokenGetCurrentUsers", "UserToken/GetCurrentUsers");
            }
        }

        public static string VMInfoCreate
        {
            get
            {
                return GetValue("VMInfoCreate", "VMInfo/Create");
            }
        }

        public static string VMInfoDelete
        {
            get
            {
                return GetValue("VMInfoDelete", "VMInfo/Delete");
            }
        }

        public static string VMInfoUpdate
        {
            get
            {
                return GetValue("VMInfoUpdate", "VMInfo/Update");
            }
        }

        public static string VMInfoSearch
        {
            get
            {
                return GetValue("VMInfoSearch", "VMInfo/Search");
            }
        }

        private static string WebApiUrlRoot
        {
            get
            {
                string value = GetValue("WebApiUrlRoot", string.Empty);
                if (value[value.Length - 1] != '/')
                    value += "/";

                return value;
            }
        }

        private static string GetValue(string key, string defaultValue)
        {
            string value = defaultValue;
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
                value = ConfigurationManager.AppSettings[key];

            return value.Contains("://") || value.Contains("~/") ? value : string.Format("{0}{1}", WebApiUrlRoot, value);
        }
    }
}