using DataRepository.DataAccess.BaseData;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helper;
using Common;

namespace Service.BaseBiz
{
    public class BlackListService : BaseService
    {
        public static BlackListEntity GetBlackListEntityById(long cid)
        {
            BlackListEntity result = new BlackListEntity();
            BlackListRepository mr = new BlackListRepository();            
            BlackListInfo info = mr.GetBlackListByKey(cid);
            if (info != null)
            {
                result = TranslateBlackListEntity(info);
            }
            return result;
        }

        private static BlackListInfo TranslateBlackListInfo(BlackListEntity entity)
        {
            BlackListInfo info = new BlackListInfo();
            if (info != null)
            {
                info.BlackID = entity.BlackID;
                info.BlackType = entity.BlackType;
                info.UnionID = entity.UnionID;
                info.UnionName = entity.UnionName;
                info.Status = entity.Status;
                info.SubStatus = entity.SubStatus;
                info.CardCode = entity.CardCode;
                info.Remark = entity.Remark;                
                info.OperatorID = entity.OperatorID;
                info.CreateDate = entity.CreateDate;
                info.ChangeDate = entity.ChangeDate;
            }


            return info;
        }

        private static BlackListEntity TranslateBlackListEntity(BlackListInfo info)
        {
            BlackListEntity entity = new BlackListEntity();
            if (info != null)
            {
                entity.BlackID = info.BlackID;
                entity.BlackType = info.BlackType;
                entity.BlackTypeDesc = StringHelper.GetBlackTypeDesc(info.BlackType);
                entity.UnionID = info.UnionID;
                entity.UnionName = info.UnionName;
                entity.Status = info.Status;
                entity.SubStatus = info.SubStatus;
                entity.CardCode = info.CardCode;
                entity.Remark = info.Remark;
                entity.OperatorID = info.OperatorID;                
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;
            }

            return entity;
        }

        public static bool ModifyBlackList(BlackListEntity entity)
        {
            long result = 0;
            if (entity != null)
            {
                BlackListRepository mr = new BlackListRepository();
                BlackListInfo info = TranslateBlackListInfo(entity);
                if (entity.BlackID > 0)
                {
                    info.ChangeDate = DateTime.Now;
                    mr.ModifyBlackList(info);
                }
                else
                {
                    info.ChangeDate = DateTime.Now;
                    info.CreateDate = DateTime.Now;
                    result = mr.CreateNew(info);
                }

            }
            return result > 0;
        }

        public static List<BlackListEntity> GetBlackListAll()
        {
            List<BlackListEntity> all = new List<BlackListEntity>();
            BlackListRepository mr = new BlackListRepository();
            List<BlackListInfo> miList = mr.GetAllBlackList();//Cache.Get<List<BlackListInfo>>("BlackListALL");
            //if (miList.IsEmpty())
            //{
            //    miList = mr.GetAllBlackList();
            //    Cache.Add("BlackListALL", miList);
            //}
            if (!miList.IsEmpty())
            {
                foreach (BlackListInfo mInfo in miList)
                {
                    BlackListEntity BlackListEntity = TranslateBlackListEntity(mInfo);
                    all.Add(BlackListEntity);
                }
            }

            return all;

        }

        public static int DeleteBlack(long ID)
        {
            BlackListRepository mr = new BlackListRepository();
            int i = mr.DeleteBlack(ID);
            return i;
        }


        #region 分页相关
        public static int GetBlackListCount(int BlackListID)
        {
            return new BlackListRepository().GetBlackListCount(BlackListID);
        }

        public static List<BlackListEntity> GetBlackListInfoPager(PagerInfo pager)
        {
            List<BlackListEntity> all = new List<BlackListEntity>();
            BlackListRepository mr = new BlackListRepository();
            List<BlackListInfo> miList = mr.GetAllBlackListInfoPager(pager);
            foreach (BlackListInfo mInfo in miList)
            {
                BlackListEntity carEntity = TranslateBlackListEntity(mInfo);
                all.Add(carEntity);
            }
            return all;
        }

        public static List<BlackListEntity> GetBlackListInfoByRule(int BlackListID, PagerInfo pager)
        {
            List<BlackListEntity> all = new List<BlackListEntity>();
            BlackListRepository mr = new BlackListRepository();
            List<BlackListInfo> miList = mr.GetBlackListInfoByRule(BlackListID, pager);

            if (!miList.IsEmpty())
            {
                foreach (BlackListInfo mInfo in miList)
                {
                    BlackListEntity storeEntity = TranslateBlackListEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }
        #endregion
    }
}
