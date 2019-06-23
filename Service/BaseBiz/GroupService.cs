/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：GroupService
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/28/2018 4:24:46 PM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using DataRepository.DataAccess.Group;
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
    public class GroupService : BaseService
    {
        private static GroupInfo TranslateGroupInfo(GroupEntity groupEntity)
        {
            GroupInfo groupInfo = new GroupInfo();
            if (groupEntity != null)
            {
                int[] menuids=groupEntity.Menus.Select(t => t.MenuID).ToArray();
                StringBuilder builder = new StringBuilder();
                if (menuids != null && menuids.Length > 0)
                {
                    for (int j = 0; j < menuids.Length; j++)
                    {
                        builder.Append(menuids[j]);
                        builder.Append(",");
                    }
                }
                groupInfo.GroupID = groupEntity.GroupID;
                groupInfo.MenuIDs = builder.ToString().TrimEnd(',');
                groupInfo.GroupName = groupEntity.GroupName;
                groupInfo.Status = groupEntity.Status;
            }


            return groupInfo;
        }

        private static GroupEntity TranslateGroupEntity(GroupInfo groupInfo)
        {
            GroupEntity groupEntity = new GroupEntity();
            if (groupInfo != null)
            {
                groupEntity.GroupID = groupInfo.GroupID;
                groupEntity.GroupName = groupInfo.GroupName;
                groupEntity.Status = groupInfo.Status;
                if (!string.IsNullOrEmpty(groupInfo.MenuIDs))
                {
                    groupEntity.Menus=MenuService.GetMenuByKeys(groupInfo.MenuIDs);
                }
            }


            return groupEntity;
        }

        public static bool ModifyGroup(GroupEntity groupEntity)
        {
            int result = 0;
            if (groupEntity != null)
            {
                GroupRepository mr = new GroupRepository();

                GroupInfo groupInfo = TranslateGroupInfo(groupEntity);


                if (groupEntity.GroupID > 0)
                {
                    result = mr.ModifyGroup(groupInfo);
                }
                else
                {
                    groupInfo.CreateDate = DateTime.Now;
                    result = mr.CreateNew(groupInfo);
                }

                List<GroupInfo> miList = mr.GetAllGroup();//刷新缓存
                Cache.Add("GroupALL", miList);
            }
            return result > 0;
        }

        public static GroupEntity GetGroupById(long gid)
        {
            GroupEntity result = new GroupEntity();
            GroupRepository mr = new GroupRepository();
            GroupInfo info = mr.GetGroupByKey(gid);
            result = TranslateGroupEntity(info);
            return result;
        }

        public static List<GroupEntity> GetGroupAll()
        {
            List<GroupEntity> all = new List<GroupEntity>();
            GroupRepository mr = new GroupRepository();
            List<GroupInfo> miList = Cache.Get<List<GroupInfo>>("GroupALL");
            if (miList.IsEmpty())
            {
                miList = mr.GetAllGroup();
                Cache.Add("GroupALL", miList);
            }
            if (!miList.IsEmpty())
            {
                foreach (GroupInfo mInfo in miList)
                {
                    GroupEntity GroupEntity = TranslateGroupEntity(mInfo);
                    all.Add(GroupEntity);
                }
            }

            return all;

        }

        public static List<GroupEntity> GetGroupByRule(string name, int status)
        {
            List<GroupEntity> all = new List<GroupEntity>();
            GroupRepository mr = new GroupRepository();
            List<GroupInfo> miList = mr.GetGroupsByRule(name, status);

            if (!miList.IsEmpty())
            {
                foreach (GroupInfo mInfo in miList)
                {
                    GroupEntity GroupEntity = TranslateGroupEntity(mInfo);
                    all.Add(GroupEntity);
                }
            }

            return all;

        }

        public static List<GroupEntity> GetGroupByKeys(string ids)
        {
            List<GroupEntity> all = new List<GroupEntity>();
            GroupRepository mr = new GroupRepository();
            List<GroupInfo> miList = mr.GetGroupByKeys(ids);

            if (!miList.IsEmpty())
            {
                foreach (GroupInfo mInfo in miList)
                {
                    GroupEntity groupEntity = TranslateGroupEntity(mInfo);
                    all.Add(groupEntity);
                }
            }

            return all;
        }

        public static void Remove(long gid)
        {
            GroupRepository mr = new GroupRepository();
            mr.RemoveGroup(gid);
            List<GroupInfo> miList = mr.GetAllGroup();//刷新缓存
            Cache.Add("GroupALL", miList);
        }
    }
}
