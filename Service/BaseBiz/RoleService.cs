using DataRepository.DataAccess.Role;
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
    public class RoleService : BaseService
    {
        private static RoleInfo TranslateRoleInfo(RoleEntity roleEntity)
        {
            RoleInfo roleInfo = new RoleInfo();
            if (roleEntity != null)
            {
                int[] menuids = roleEntity.Menus.Select(t => t.MenuID).ToArray();
                StringBuilder builder = new StringBuilder();
                if (menuids != null && menuids.Length > 0)
                {
                    for (int j = 0; j < menuids.Length; j++)
                    {
                        builder.Append(menuids[j]);
                        builder.Append(",");
                    }
                }
                roleInfo.RoleID = roleEntity.RoleID;
                roleInfo.MenuIDs = builder.ToString().TrimEnd(',');
                roleInfo.RoleName = roleEntity.RoleName;
                roleInfo.Status = roleEntity.Status;
            }


            return roleInfo;
        }

        private static RoleEntity TranslateRoleEntity(RoleInfo roleInfo)
        {
            RoleEntity roleEntity = new RoleEntity();
            if (roleInfo != null)
            {
                roleEntity.RoleID = roleInfo.RoleID;
                roleEntity.RoleName = roleInfo.RoleName;
                roleEntity.Status = roleInfo.Status;
                if (!string.IsNullOrEmpty(roleInfo.MenuIDs))
                {
                    roleEntity.Menus = MenuService.GetMenuByKeys(roleInfo.MenuIDs);
                }
            }


            return roleEntity;
        }

        public static bool ModifyRole(RoleEntity roleEntity)
        {
            int result = 0;
            if (roleEntity != null)
            {
                RoleRepository mr = new RoleRepository();

                RoleInfo RoleInfo = TranslateRoleInfo(roleEntity);


                if (roleEntity.RoleID > 0)
                {
                    result = mr.ModifyRole(RoleInfo);
                }
                else
                {
                    RoleInfo.CreateDate = DateTime.Now;
                    result = mr.CreateNew(RoleInfo);
                }

                List<RoleInfo> miList = mr.GetAllRole();//刷新缓存
                Cache.Add("RoleALL", miList);
            }
            return result > 0;
        }

        public static RoleEntity GetRoleById(int rid)
        {
            RoleEntity result = new RoleEntity();
            RoleRepository mr = new RoleRepository();
            RoleInfo info = mr.GetRoleByKey(rid);
            result = TranslateRoleEntity(info);
            return result;
        }

        public static List<RoleEntity> GetRoleAll()
        {
            List<RoleEntity> all = new List<RoleEntity>();
            RoleRepository mr = new RoleRepository();
            List<RoleInfo> miList = Cache.Get<List<RoleInfo>>("RoleALL");
            if (miList.IsEmpty())
            {
                miList = mr.GetAllRole();
                Cache.Add("RoleALL", miList);
            }
            if (!miList.IsEmpty())
            {
                foreach (RoleInfo mInfo in miList)
                {
                    RoleEntity RoleEntity = TranslateRoleEntity(mInfo);
                    all.Add(RoleEntity);
                }
            }

            return all;

        }

        public static List<RoleEntity> GetRoleByKeys(string ids)
        {
            List<RoleEntity> all = new List<RoleEntity>();
            if (!string.IsNullOrEmpty(ids))
            {
                RoleRepository mr = new RoleRepository();
                List<RoleInfo> miList = mr.GetRoleByKeys(ids);

                if (!miList.IsEmpty())
                {
                    foreach (RoleInfo mInfo in miList)
                    {
                        RoleEntity RoleEntity = TranslateRoleEntity(mInfo);
                        all.Add(RoleEntity);
                    }
                }
            }


            return all;

        }

        public static List<RoleEntity> GetRoleByRule(string name, int status)
        {
            List<RoleEntity> all = new List<RoleEntity>();
            RoleRepository mr = new RoleRepository();
            List<RoleInfo> miList = mr.GetRolesByRule(name, status);

            if (!miList.IsEmpty())
            {
                foreach (RoleInfo mInfo in miList)
                {
                    RoleEntity RoleEntity = TranslateRoleEntity(mInfo);
                    all.Add(RoleEntity);
                }
            }

            return all;

        }

        public static void Remove(int rid)
        {
            RoleRepository mr = new RoleRepository();
            mr.RemoveRole(rid);
            List<RoleInfo> miList = mr.GetAllRole();//刷新缓存
            Cache.Add("RoleALL", miList);
        }
    }
}
