using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helper;
using DataRepository.DataAccess.User;
using Common;


namespace Service.BaseBiz
{
    public class UserService : BaseService
    {
        

        private static UserInfo TranslateUserInfo(UserEntity userEntity)
        {
            UserInfo userInfo = new UserInfo();
            if (userEntity != null)
            {
                int[] groupids = userEntity.Groups.Select(t => t.GroupID).ToArray();
                int[] roleids = userEntity.Roles.Select(t => t.RoleID).ToArray();
                StringBuilder groupbuilder = new StringBuilder();
                if (groupids != null && groupids.Length > 0)
                {
                    for (int j = 0; j < groupids.Length; j++)
                    {
                        groupbuilder.Append(groupids[j]);
                        groupbuilder.Append(",");
                    }
                }
                StringBuilder rolebuilder = new StringBuilder();
                if (roleids != null && roleids.Length > 0)
                {
                    for (int j = 0; j < roleids.Length; j++)
                    {
                        rolebuilder.Append(roleids[j]);
                        rolebuilder.Append(",");
                    }
                }
                userInfo.UserID = userEntity.UserID;
                userInfo.UserName = userEntity.UserName;
                userInfo.NickName = userEntity.NickName;
                userInfo.Status = userEntity.Status;
                userInfo.GroupIDs = groupbuilder.ToString().TrimEnd(',');
                userInfo.RoleIDs = rolebuilder.ToString().TrimEnd(',');

            }


            return userInfo;
        }

        private static UserEntity TranslateUserEntity(UserInfo userInfo)
        {
            UserEntity userEntity = new UserEntity();
            if (userInfo != null)
            {
                userEntity.UserID = userInfo.UserID;
                userEntity.UserName = userInfo.UserName;
                userEntity.NickName = userInfo.NickName;
                userEntity.Status = userInfo.Status;
                List<MenuEntity> allMenus = new List<MenuEntity>();
                MenuCompare compare = new MenuCompare();
                if (!string.IsNullOrEmpty(userInfo.RoleIDs))
                {
                    userEntity.Roles = RoleService.GetRoleByKeys(userInfo.RoleIDs);
                    if (userEntity.Roles.Count > 0)
                    {
                        foreach (RoleEntity r in userEntity.Roles)
                        {
                            allMenus = allMenus.Merge(r.Menus, compare);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(userInfo.GroupIDs))
                {
                    userEntity.Groups = GroupService.GetGroupByKeys(userInfo.GroupIDs);
                    if (userEntity.Groups.Count > 0)
                    {
                        foreach (GroupEntity r in userEntity.Groups)
                        {
                            allMenus = allMenus.Merge(r.Menus, compare);
                        }
                    }
                }
                userEntity.Menus = allMenus;
            }


            return userEntity;
        }

        public static bool ModifyUser(UserEntity userEntity)
        {
            int result = 0;
            if (userEntity != null)
            {
                UserRepository mr = new UserRepository();

                UserInfo userInfo = TranslateUserInfo(userEntity);


                if (userEntity.UserID > 0)
                {
                    result = mr.ModifyUser(userInfo);
                }
                else
                {
                    userInfo.CreateDate = DateTime.Now;
                    userInfo.Password = EncryptHelper.MD5Encrypt("123456");
                    result = mr.CreateNew(userInfo);
                }

                List<UserInfo> miList = mr.GetAllUser();//刷新缓存
                Cache.Add("UserALL", miList);
            }
            return result > 0;
        }

        public static UserEntity GetUserById(long uid)
        {
            UserEntity result = new UserEntity();
            UserRepository mr = new UserRepository();
            UserInfo info = mr.GetUserByKey(uid);
            result = TranslateUserEntity(info);
            return result;
        }

        public static List<UserEntity> GetUserAll()
        {
            List<UserEntity> all = new List<UserEntity>();
            UserRepository mr = new UserRepository();
            List<UserInfo> miList = Cache.Get<List<UserInfo>>("UserALL");
            if (miList.IsEmpty())
            {
                miList = mr.GetAllUser();
                Cache.Add("UserALL", miList);
            }
            if (!miList.IsEmpty())
            {
                foreach (UserInfo mInfo in miList)
                {
                    UserEntity userEntity = TranslateUserEntity(mInfo);
                    all.Add(userEntity);
                }
            }

            return all;

        }

        public static List<UserEntity> GetUserByRule(string name, int status)
        {
            List<UserEntity> all = new List<UserEntity>();
            UserRepository mr = new UserRepository();
            List<UserInfo> miList = mr.GetUsersByRule(name, status);

            if (!miList.IsEmpty())
            {
                foreach (UserInfo mInfo in miList)
                {
                    UserEntity userEntity = TranslateUserEntity(mInfo);
                    all.Add(userEntity);
                }
            }

            return all;

        }

        public static void Remove(long uid)
        {
            UserRepository mr = new UserRepository();
            mr.RemoveUser(uid);
            List<UserInfo> miList = mr.GetAllUser();//刷新缓存
            Cache.Add("UserALL", miList);
        }

        public static int ModifyPassword(long uid, string pwd)
        {
            UserRepository mr = new UserRepository();
            return mr.ModifyPassword(uid,pwd);
        }

        public static UserEntity GetLoginUser(string name, string pwd)
        {
            UserEntity result = new UserEntity();
            UserRepository mr = new UserRepository();
            UserInfo info = mr.GetLoginUser(name,pwd);
            result = TranslateUserEntity(info);
            return result;
        }

        public static bool IsExists(string name)
        {
            return  GetUserAll().Exists(t=>t.Status==1 && t.UserName==name);
        }
    }
}
