using System.Collections.Generic;

namespace Utility.Model
{
    public class MenuModel
    {
        public class MenuTreeModel
        {
            public string menuIndex { get; set; }

            public IEnumerable<MainMenu> mainMenu { get; set; }

            public IEnumerable<SubMenu> subMenu { get; set; }
        }

        public class MainMenu
        {
            public int ID { get; set; }

            public string Name { get; set; }

            public string Url { get; set; }

            public string Icon { get; set; }

            public string CATEGORY { get; set; }
        }

        public class SubMenu
        {
            public int ID { get; set; }

            public string Name { get; set; }

            public int Level { get; set; }

            public int ParentID { get; set; }

            public string Url { get; set; }

            public string FunctionCode { get; set; }

            public string CATEGORY { get; set; }
        }
    }
}
