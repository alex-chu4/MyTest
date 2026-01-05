using EMIC2.Models;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface;
using EMIC2.Models.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Utility.Cache;
using Utility.Dao;
using Utility.Model;
using static Utility.Model.MenuModel;

namespace Utility.EMIC2
{
    public class Menu
    {
        public static MenuTreeModel GetMenuTree(string menuIndex, string userID, string eocCode, bool bCheckIsONOFF)
        {
            IRepository<REDIS_MENU_TREE> redisRepository = new GenericRepository<REDIS_MENU_TREE>();

            //var UserRoleMenu = menuIndex == "SSO2" ?
            //    UserWithRoleDao.GetSSO2ROLE_MENU(userID) :
            //    UserWithRoleDao.GetEMIC2ROLE_MENU(userID, eocCode);

            var UserRoleMenu =  UserWithRoleDao.GetCOM2ROLE_MENU(userID, eocCode, menuIndex);

            void SetMenuToRedis(string menuJson)
                => new Redis(2).Set($"{menuIndex}-{UserRoleMenu.ROLE_LIST}", UserRoleMenu.MAIN_MENU);

            void SetMenuToDB(string _menuIndex, string _roleList, string _menuTree)
            {
                REDIS_MENU_TREE tmp =
                    redisRepository
                    .Get(x => x.ROLE_LIST == _roleList && x.MENU_INDEX == _menuIndex);

                if (tmp != null)
                {
                    //update
                    if (tmp.MENU_TREE.Equals(_menuTree) == false)
                    {
                        tmp.MENU_TREE = _menuTree;
                        redisRepository.Update(tmp, tmp.ROLE_LIST, tmp.MENU_INDEX);
                    }
                }
                else
                {
                    redisRepository.Create(
                        new REDIS_MENU_TREE
                        {
                            MENU_INDEX = _menuIndex,
                            ROLE_LIST = _roleList,
                            MENU_TREE = _menuTree
                        });
                }
            }

            MenuTreeModel sso2MenuTreeModel = new MenuTreeModel();
            if (!string.IsNullOrEmpty(UserRoleMenu.MAIN_MENU))
            {
                sso2MenuTreeModel = JsonConvert.DeserializeObject<MenuTreeModel>(UserRoleMenu.MAIN_MENU);
            }

            return sso2MenuTreeModel;
        }

        //public static MenuTreeModel GetMenuTree(string menuIndex, string roles)
        //{
        //    MenuTreeModel sso2MenuTreeModel = new MenuTreeModel();

        //    if (!string.IsNullOrEmpty(menuIndex) && !string.IsNullOrEmpty(roles))
        //    {
        //        string jsonMenu = string.Empty;

        //        try
        //        {
        //            var redis = new Redis(2);

        //            jsonMenu = redis.Get(menuIndex + "-" + roles);
        //        }
        //        catch
        //        {

        //        }

        //        if (string.IsNullOrEmpty(jsonMenu))
        //        {
        //            IRepository<REDIS_MENU_TREE> repository = new GenericRepository<REDIS_MENU_TREE>();

        //            jsonMenu = repository.Get(x => x.ROLE_LIST == menuIndex + "-" + roles)?.MENU_TREE;
        //        }

        //        if (!string.IsNullOrEmpty(jsonMenu))
        //        {
        //            sso2MenuTreeModel = JsonConvert.DeserializeObject<MenuTreeModel>(jsonMenu);
        //        }
        //    }

        //    return sso2MenuTreeModel;
        //}

        //public static string SetMenuTree(string menuIndex, string userID, string eocCode, bool bCheckIsONOFF)
        //{

        //    IRepository<REDIS_MENU_TREE> redisRepository = new GenericRepository<REDIS_MENU_TREE>();

        //    var UserRoleMenu = menuIndex == "SSO2" ?
        //        UserWithRoleDao.GetSSO2ROLE_MENU(userID) :
        //        UserWithRoleDao.GetEMIC2ROLE_MENU(userID, eocCode);

        //    void SetMenuToRedis(string menuJson)
        //        => new Redis(2).Set($"{menuIndex}-{UserRoleMenu.ROLE_LIST}", UserRoleMenu.MAIN_MENU);

        //    void SetMenuToDB(string _menuIndex, string _roleList, string _menuTree)
        //    {
        //        REDIS_MENU_TREE tmp =
        //            redisRepository
        //            .Get(x => x.ROLE_LIST == _roleList && x.MENU_INDEX == _menuIndex);

        //        if (tmp != null)
        //        {
        //            //update
        //            if (tmp.MENU_TREE.Equals(_menuTree) == false)
        //            {
        //                tmp.MENU_TREE = _menuTree;
        //                redisRepository.Update(tmp, tmp.ROLE_LIST, tmp.MENU_INDEX);
        //            }
        //        }
        //        else
        //        {
        //            redisRepository.Create(
        //                new REDIS_MENU_TREE {
        //                    MENU_INDEX = _menuIndex,
        //                    ROLE_LIST = _roleList,
        //                    MENU_TREE = _menuTree
        //                });
        //        }
        //    }

        //    // Menu 寫入 Redis
        //    SetMenuToRedis(UserRoleMenu.MAIN_MENU);
        //    // Menu 寫入 DataBase
        //    SetMenuToDB(menuIndex, $"{menuIndex}-{UserRoleMenu.ROLE_LIST}", UserRoleMenu.MAIN_MENU);

        //    return UserRoleMenu.ROLE_LIST ?? null;
        //}

        #region 建立MenuTree 舊版邏輯
        //public static string SetMenuTree(string menuIndex, string userID, string eocCode, bool bCheckIsONOFF)
        //{
        //    IRepository<EMP2_MENU_TREE> emp2Repository = new GenericRepository<EMP2_MENU_TREE>();
        //    IRepository<REDIS_MENU_TREE> redisRepository = new GenericRepository<REDIS_MENU_TREE>();

        //    // 取得該menuIndex的 Menu Tree 組合
        //    MenuTreeDao menuTree = new MenuTreeDao();
        //    MenuTreeModel tmpMenuTreeModel = new MenuTreeModel();
        //    MenuTreeModel menu = new MenuTreeModel();
        //    menu.mainMenu = menuTree.GetParentList(menuIndex, bCheckIsONOFF).ToList();
        //    menu.subMenu = menuTree.GetSubList(menuIndex, bCheckIsONOFF).ToList();
        //    menu.menuIndex = menuIndex;

        //    if (menu.subMenu.Count() == 0)
        //    {
        //        return string.Empty;
        //    }

        //    RoleAndFunctionDao roleAndFunction = new RoleAndFunctionDao();
        //    RoleFunctionModel rfModel = roleAndFunction.getRolsFuncsByUser(userID).FirstOrDefault();
        //    rfModel.Roles = menuIndex == "SSO2" ? UserWithRoleDao.GetSSO2RoleList(userID) : UserWithRoleDao.GetEMIC2RoleList(userID, eocCode);
        //    REDIS_MENU_TREE dbModel = new REDIS_MENU_TREE();
        //    if (rfModel != null && string.IsNullOrEmpty(rfModel.Functions) == false)
        //    {
        //        MenuTreeModel menuVM = new MenuTreeModel();
        //        List<MainMenu> setMainMenu = new List<MainMenu>();
        //        List<SubMenu> setSubMenu = new List<SubMenu>();

        //        // 取得 function array
        //        string[] funcArray = rfModel.Functions.Split(',');

        //        // 取得全部 Menu
        //        IEnumerable<MainMenu> mainMenu = menu.mainMenu;
        //        IEnumerable<SubMenu> subMenu = menu.subMenu;

        //        // Menu Tree 有資料才組選單
        //        if (subMenu.Count() > 0)
        //        {
        //            // 組出子選單 & 結點
        //            //foreach (var subItem in subMenu)
        //            //{
        //            //    if (string.IsNullOrEmpty(subItem.FunctionCode))
        //            //    {
        //            //        if (subMenu.Any(x => x.ParentID == subItem.ID && funcArray.Contains(x.FunctionCode) && x.Url != ""))
        //            //        {
        //            //            setSubMenu.Add(subItem);
        //            //        }
        //            //    }
        //            //    else
        //            //    {
        //            //        if (!string.IsNullOrEmpty(subItem.Url))
        //            //        {
        //            //            setSubMenu.Add(subItem);
        //            //        }
        //            //    }
        //            //}

        //            // 組出子選單 & 結點
        //            var allSubFunction = subMenu.Select(s => s.FunctionCode);

        //            var IntersectFunction = funcArray.Intersect(allSubFunction);

        //            foreach (var subItem in subMenu)
        //            {
        //                if (string.IsNullOrEmpty(subItem.FunctionCode))
        //                {
        //                    if (subMenu.Any(x => x.ParentID == subItem.ID && IntersectFunction.Contains(x.FunctionCode) && x.Url != ""))
        //                    {
        //                        setSubMenu.Add(subItem);
        //                    }
        //                }
        //                else
        //                {
        //                    if (subMenu.Any(x => IntersectFunction.Contains(x.FunctionCode)))
        //                    {
        //                        setSubMenu.Add(subItem);
        //                    }
        //                }
        //            }

        //            // 組出主選單
        //            foreach (var mainItem in mainMenu)
        //            {
        //                if (setSubMenu.Any(x => x.ParentID == mainItem.ID) || mainItem.CATEGORY == "F")
        //                {
        //                    setMainMenu.Add(mainItem);
        //                }
        //            }

        //            // Redis Menu
        //            menuVM.menuIndex = menuIndex;
        //            menuVM.mainMenu = setMainMenu;
        //            menuVM.subMenu = setSubMenu;

        //            var menuJsonStr = JsonConvert.SerializeObject(menuVM);

        //            //-----------------------------------------------------------------
        //            var redis = new Redis(2);
        //            redis.Set(menuIndex + "-" + rfModel.Roles, menuJsonStr, 0);

        //            //-----------------------------------------------------------------

        //            dbModel.ROLE_LIST = menuIndex + "-" + rfModel.Roles;
        //            dbModel.MENU_TREE = menuJsonStr;
        //            dbModel.MENU_INDEX = menuIndex;
        //            REDIS_MENU_TREE tmp = redisRepository.Get(x => x.ROLE_LIST == dbModel.ROLE_LIST && x.MENU_INDEX == dbModel.MENU_INDEX);
        //            if (tmp != null)
        //            {
        //                //update
        //                if (tmp.MENU_TREE.Equals(dbModel.MENU_TREE) == false)
        //                {
        //                    tmp.MENU_TREE = dbModel.MENU_TREE;
        //                    bool bUpdate = redisRepository.Update(tmp, tmp.ROLE_LIST, tmp.MENU_INDEX);
        //                }
        //            }
        //            else
        //            {
        //                bool bCreate = redisRepository.Create(dbModel);
        //            }

        //        }
        //    }
        //    return rfModel == null ? string.Empty : rfModel.Roles;
        //}
        #endregion

    }
}
