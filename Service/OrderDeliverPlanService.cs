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

namespace Service
{
    public class OrderDeliverPlanService
    {
        public static OrderDeliverPlanEntity GetOrderDeliverPlanEntityById(long cid)
        {
            OrderDeliverPlanEntity result = new OrderDeliverPlanEntity();
            OrderDeliverPlanRepository mr = new OrderDeliverPlanRepository();
            OrderDeliverPlanInfo info = mr.GetOrderDeliverPlanByKey(cid);
            if (info != null)
            {
                result = TranslateOrderDeliverPlanEntity(info);
            }
            return result;
        }

        private static OrderDeliverPlanInfo TranslateOrderDeliverPlanInfo(OrderDeliverPlanEntity entity)
        {
            OrderDeliverPlanInfo info = new OrderDeliverPlanInfo();
            if (info != null)
            {
                info.PlanID = entity.PlanID;
                info.OrderIDS = entity.OrderIDS;
                info.DeliveryNo = entity.DeliveryNo;
                info.CarrierName = entity.CarrierName;
                info.CarrierID = entity.CarrierID;
                info.Temp = entity.Temp;
                info.DeliveryType = entity.DeliveryType;
                info.DriverName = entity.DriverName;
                info.DriverTelephone = entity.DriverTelephone;
                info.CarModel = entity.CarModel;
                info.CarNo = entity.CarNo;
                info.OilCardNo = entity.OilCardNo;
                info.OilCardBalance = entity.OilCardBalance;
                info.GPSNo = entity.GPSNo;
                info.NeedTicket = entity.NeedTicket;
                info.DeliverDate = entity.DeliverDate;
                info.Remark = entity.Remark;
                info.OperatorID = entity.OperatorID;
                info.CreateDate = entity.CreateDate;
                info.ChangeDate = entity.ChangeDate;
            }


            return info;
        }

        private static OrderDeliverPlanEntity TranslateOrderDeliverPlanEntity(OrderDeliverPlanInfo info)
        {
            OrderDeliverPlanEntity entity = new OrderDeliverPlanEntity();
            if (info != null)
            {

                entity.PlanID = info.PlanID;
                entity.OrderIDS = info.OrderIDS;
                entity.CarrierName = info.CarrierName;
                entity.CarrierID = info.CarrierID;
                entity.Temp = info.Temp;
                entity.DeliveryType = info.DeliveryType;
                entity.DriverName = info.DriverName;
                entity.DriverTelephone = info.DriverTelephone;
                entity.CarModel = info.CarModel;
                entity.CarNo = info.CarNo;
                entity.OilCardNo = info.OilCardNo;
                entity.OilCardBalance = info.OilCardBalance;
                entity.GPSNo = info.GPSNo;
                entity.NeedTicket = info.NeedTicket;
                entity.DeliverDate = info.DeliverDate;
                if (entity.DeliverDate != null)
                {
                    entity.formatDeliverDate = info.DeliverDate.ToString("yyyy-MM-dd");
                }
                entity.Remark = info.Remark;
                entity.OperatorID = info.OperatorID;
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;
            }

            return entity;
        }

        public static int ModifyOrderDeliverPlan(OrderDeliverPlanEntity entity)
        {
            int result = entity.PlanID;
            if (entity != null)
            {
                OrderDeliverPlanRepository mr = new OrderDeliverPlanRepository();
                OrderDeliverPlanInfo info = TranslateOrderDeliverPlanInfo(entity);
                if (entity.PlanID > 0)
                {
                    info.ChangeDate = DateTime.Now;
                    mr.ModifyOrderDeliverPlan(info);
                }
                else
                {
                    info.ChangeDate = DateTime.Now;
                    info.CreateDate = DateTime.Now;
                    result = mr.CreateNew(info);
                }

            }
            return result;
        }

        public static List<OrderDeliverPlanEntity> GetOrderDeliverPlanAll(string orderids)
        {
            List<OrderDeliverPlanEntity> all = new List<OrderDeliverPlanEntity>();
            OrderDeliverPlanRepository mr = new OrderDeliverPlanRepository();
            List<OrderDeliverPlanInfo> miList = mr.GetAllOrderDeliverPlan(orderids);//Cache.Get<List<OrderDeliverPlanInfo>>("OrderDeliverPlanALL");
            //if (miList.IsEmpty())
            //{
            //    miList = mr.GetAllOrderDeliverPlan();
            //    Cache.Add("OrderDeliverPlanALL", miList);
            //}
            if (!miList.IsEmpty())
            {
                foreach (OrderDeliverPlanInfo mInfo in miList)
                {
                    OrderDeliverPlanEntity OrderDeliverPlanEntity = TranslateOrderDeliverPlanEntity(mInfo);
                    all.Add(OrderDeliverPlanEntity);
                }
            }

            return all;

        }

        public static int Delete(long ID)
        {
            OrderDeliverPlanRepository mr = new OrderDeliverPlanRepository();
            int i = mr.Delete(ID);
            return i;
        }


        #region 分页相关
        public static int GetOrderDeliverPlanCount(int carrierID, string begindate, string enddate)
        {
            return new OrderDeliverPlanRepository().GetOrderDeliverPlanCount(carrierID, begindate, enddate);
        }

        public static List<OrderDeliverPlanEntity> GetOrderDeliverPlanInfoPager(PagerInfo pager)
        {
            List<OrderDeliverPlanEntity> all = new List<OrderDeliverPlanEntity>();
            OrderDeliverPlanRepository mr = new OrderDeliverPlanRepository();
            List<OrderDeliverPlanInfo> miList = mr.GetAllOrderDeliverPlanInfoPager(pager);
            foreach (OrderDeliverPlanInfo mInfo in miList)
            {
                OrderDeliverPlanEntity carEntity = TranslateOrderDeliverPlanEntity(mInfo);
                all.Add(carEntity);
            }
            return all;
        }

        public static List<OrderDeliverPlanEntity> GetOrderDeliverPlanInfoByRule(int carrierID, string begindate, string enddate, PagerInfo pager)
        {
            List<OrderDeliverPlanEntity> all = new List<OrderDeliverPlanEntity>();
            OrderDeliverPlanRepository mr = new OrderDeliverPlanRepository();
            List<OrderDeliverPlanInfo> miList = mr.GetOrderDeliverPlanInfoByRule(carrierID, begindate, enddate, pager);

            if (!miList.IsEmpty())
            {
                foreach (OrderDeliverPlanInfo mInfo in miList)
                {
                    OrderDeliverPlanEntity storeEntity = TranslateOrderDeliverPlanEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }
        #endregion
    }
}
