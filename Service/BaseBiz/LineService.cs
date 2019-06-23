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
    public class LineService : BaseService
    {
        public static LineEntity GetLineEntityById(long cid)
        {
            LineEntity result = new LineEntity();
            LineRepository mr = new LineRepository();            
            LineInfo info = mr.GetLineByKey(cid);
            if (info != null)
            {
                result = TranslateLineEntity(info);
            }
            return result;
        }

        private static LineInfo TranslateLineInfo(LineEntity entity)
        {
            LineInfo info = new LineInfo();
            if (info != null)
            {
                info.ID = entity.ID;
                info.LineID = entity.LineID;
                info.ReceiverName = entity.ReceiverName;
                info.Address = entity.Address;
                info.Remark = entity.Remark;
                info.OperatorID = entity.OperatorID;
                info.CreateDate = entity.CreateDate;
                info.ChangeDate = entity.ChangeDate;
            }


            return info;
        }

        private static LineEntity TranslateLineEntity(LineInfo info)
        {
            LineEntity entity = new LineEntity();
            if (info != null)
            {

                entity.ID = info.ID;
                entity.LineID = info.LineID;
                entity.ReceiverName = info.ReceiverName;
                entity.Address = info.Address;                
                entity.Remark = info.Remark;
                entity.OperatorID = info.OperatorID;
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;
            }

            return entity;
        }

        public static bool ModifyLine(LineEntity entity)
        {
            long result = 0;
            if (entity != null)
            {
                LineRepository mr = new LineRepository();
                LineInfo info = TranslateLineInfo(entity);
                if (entity.ID > 0)
                {
                    info.ChangeDate = DateTime.Now;
                    mr.ModifyLine(info);
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

        public static List<LineEntity> GetLineAll()
        {
            List<LineEntity> all = new List<LineEntity>();
            LineRepository mr = new LineRepository();
            List<LineInfo> miList = mr.GetAllLine();//Cache.Get<List<LineInfo>>("LineALL");
            //if (miList.IsEmpty())
            //{
            //    miList = mr.GetAllLine();
            //    Cache.Add("LineALL", miList);
            //}
            if (!miList.IsEmpty())
            {
                foreach (LineInfo mInfo in miList)
                {
                    LineEntity LineEntity = TranslateLineEntity(mInfo);
                    all.Add(LineEntity);
                }
            }

            return all;

        }

        public static int Delete(long ID)
        {
            LineRepository mr = new LineRepository();
            int i = mr.Delete(ID);
            return i;
        }


        #region 分页相关
        public static int GetLineCount(int LineID)
        {
            return new LineRepository().GetLineCount(LineID);
        }

        public static List<LineEntity> GetLineInfoPager(PagerInfo pager)
        {
            List<LineEntity> all = new List<LineEntity>();
            LineRepository mr = new LineRepository();
            List<LineInfo> miList = mr.GetAllLineInfoPager(pager);
            foreach (LineInfo mInfo in miList)
            {
                LineEntity carEntity = TranslateLineEntity(mInfo);
                all.Add(carEntity);
            }
            return all;
        }

        public static List<LineEntity> GetLineInfoByRule(int LineID, PagerInfo pager)
        {
            List<LineEntity> all = new List<LineEntity>();
            LineRepository mr = new LineRepository();
            List<LineInfo> miList = mr.GetLineInfoByRule(LineID, pager);

            if (!miList.IsEmpty())
            {
                foreach (LineInfo mInfo in miList)
                {
                    LineEntity storeEntity = TranslateLineEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }
        #endregion
    }
}
