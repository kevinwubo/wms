/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：MenuService
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/28/2018 9:43:25 AM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using DataRepository.DataAccess.Menu;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helper;

namespace Service.BaseBiz
{
    public class MenuService : BaseService
    {
        private static MenuInfo TranslateMenuInfo(MenuEntity menuEntity)
        {
            MenuInfo menuInfo = new MenuInfo();
            if (menuEntity != null)
            {
                menuInfo.MenuID = menuEntity.MenuID;
                menuInfo.MenuName = menuEntity.MenuName;
                menuInfo.PreFlag = menuEntity.PreFlag ?? "";
                menuInfo.SufFlag = menuEntity.SufFlag ?? "";
                menuInfo.Remark = menuEntity.Remark ?? "";
                menuInfo.URL = menuEntity.URL;
                menuInfo.Status = menuEntity.Status;
                menuInfo.GroupCode = menuEntity.GroupCode;
                
            }


            return menuInfo;
        }

        private static MenuEntity TranslateMenuEntity(MenuInfo menuInfo)
        {
            MenuEntity menuEntity = new MenuEntity();
            if (menuEntity != null)
            {
                menuEntity.MenuID = menuInfo.MenuID;
                menuEntity.MenuName = menuInfo.MenuName;
                menuEntity.PreFlag = menuInfo.PreFlag ?? "";
                menuEntity.SufFlag = menuInfo.SufFlag ?? "";
                menuEntity.Remark = menuInfo.Remark ?? "";
                menuEntity.URL = menuInfo.URL;
                menuEntity.Status = menuInfo.Status;
                menuEntity.GroupCode = menuInfo.GroupCode;
                if (!string.IsNullOrEmpty(menuEntity.GroupCode))
                {
                    menuEntity.BaseData = BaseDataService.GetBaseDataByType(menuEntity.GroupCode).FirstOrDefault() ?? new BaseDataEntity();
                }
                else
                {
                    menuEntity.BaseData = new BaseDataEntity();
                }
            }


            return menuEntity;
        }

        public static bool ModifyMenu(MenuEntity menuEntity)
        {
            int result = 0;
            if (menuEntity != null)
            {
                MenuRepository mr = new MenuRepository();

                MenuInfo menuInfo = TranslateMenuInfo(menuEntity);


                if (menuEntity.MenuID > 0)
                {
                    result = mr.ModifyMenu(menuInfo);
                }
                else
                {
                    menuInfo.CreateDate = DateTime.Now;
                    result = mr.CreateNew(menuInfo);
                }

                List<MenuInfo> miList = mr.GetAllMenu();//刷新缓存
                Cache.Add("MenuALL", miList);
            }
            return result>0;
        }

        public static MenuEntity GetMenuById(long mid)
        {
            MenuEntity result = new MenuEntity();
            MenuRepository mr = new MenuRepository();
            MenuInfo info = mr.GetMenuByKey(mid);
            result = TranslateMenuEntity(info);
            return result;
        }

        public static List<MenuEntity> GetMenuAll()
        {
            List<MenuEntity> all = new List<MenuEntity>();
            MenuRepository mr = new MenuRepository();
            List<MenuInfo> miList = Cache.Get<List<MenuInfo>>("MenuALL");
            if (miList.IsEmpty())
            {
                miList = mr.GetAllMenu();
                Cache.Add("MenuALL", miList);
            }

            if (!miList.IsEmpty())
            {
                foreach (MenuInfo mInfo in miList)
                {
                    MenuEntity menuEntity = TranslateMenuEntity(mInfo);
                    all.Add(menuEntity);
                }
            }

            return all;

        }

        public static List<MenuEntity> GetMenuByRule(string name,int status)
        {
            List<MenuEntity> all = new List<MenuEntity>();
            MenuRepository mr = new MenuRepository();
            List<MenuInfo> miList = mr.GetMenusByRule(name,status);

            if (!miList.IsEmpty())
            {
                foreach (MenuInfo mInfo in miList)
                {
                    MenuEntity menuEntity = TranslateMenuEntity(mInfo);
                    all.Add(menuEntity);
                }
            }

            return all;

        }

        public static List<MenuEntity> GetMenuByKeys(string ids)
        {
            List<MenuEntity> all = new List<MenuEntity>();
            MenuRepository mr = new MenuRepository();
            List<MenuInfo> miList = mr.GetMenuByKeys(ids);

            if (!miList.IsEmpty())
            {
                foreach (MenuInfo mInfo in miList)
                {
                    MenuEntity menuEntity = TranslateMenuEntity(mInfo);
                    all.Add(menuEntity);
                }
            }

            return all;
        }

        public static void Remove(long mid)
        {
            MenuRepository mr = new MenuRepository();
            mr.RemoveMenu(mid);
            List<MenuInfo> miList = mr.GetAllMenu();//刷新缓存
            Cache.Add("MenuALL", miList);
        }
    }
}
