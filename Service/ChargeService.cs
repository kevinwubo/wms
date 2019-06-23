using DataRepository.DataAccess.Charge;
using DataRepository.DataModel;
using Entity.ViewModel;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helper;

namespace Service
{
    public class ChargeService:BaseService
    {
        private static List<BaseDataEntity> GetPayTypeNames(string payType)
        {
            List<BaseDataEntity> baseList = new List<BaseDataEntity>();
            if (!string.IsNullOrEmpty(payType))
            {
                List<string> payTypes = payType.Split(',').ToList();
                List<BaseDataEntity> allPayType = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "P00").ToList();
                foreach (string item in payTypes)
                {
                    if (allPayType.Exists(t => t.TypeCode == item))
                    {
                        BaseDataEntity baseData = allPayType.FirstOrDefault(t => t.TypeCode == item);
                        baseList.Add(baseData);
                    }
                }
            }

            return baseList;

        }

        private static ChargingBaseInfo TranslateChargingBaseEntity(ChargingBaseEntity chargingBaseEntity)
        {
            ChargingBaseInfo chargeBaseInfo = new ChargingBaseInfo();
            if (chargingBaseEntity != null)
            {
                chargeBaseInfo.ChargeBaseID = chargingBaseEntity.ChargeBaseID;
                chargeBaseInfo.Name = chargingBaseEntity.Name;
                chargeBaseInfo.Code = chargingBaseEntity.Code;
                chargeBaseInfo.ChargeFee = chargingBaseEntity.ChargeFee;
                chargeBaseInfo.ServerFee = chargingBaseEntity.ServerFee;
                chargeBaseInfo.ParkFee = chargingBaseEntity.ParkFee;
                chargeBaseInfo.ChargeNum = chargingBaseEntity.ChargeNum;
                chargeBaseInfo.PayType = chargingBaseEntity.PayType;
                chargeBaseInfo.Address = chargingBaseEntity.Address;
                chargeBaseInfo.Coordinate = chargingBaseEntity.Coordinate;
                chargeBaseInfo.StartTime = chargingBaseEntity.StartTime;
                chargeBaseInfo.EndTime = chargingBaseEntity.EndTime;
                chargeBaseInfo.IsUse = chargingBaseEntity.IsUse;
                chargeBaseInfo.CityID = chargingBaseEntity.CityID;
            }

            return chargeBaseInfo;
        }

        private static ChargingBaseEntity TranslateChargingBaseInfo(ChargingBaseInfo chargeBaseInfo)
        {
            ChargingBaseEntity chargingBaseEntity = new ChargingBaseEntity();
            if (chargeBaseInfo != null)
            {
                chargingBaseEntity.ChargeBaseID = chargeBaseInfo.ChargeBaseID;
                chargingBaseEntity.Name = chargeBaseInfo.Name;
                chargingBaseEntity.Code = chargeBaseInfo.Code;
                chargingBaseEntity.ChargeFee = chargeBaseInfo.ChargeFee;
                chargingBaseEntity.ServerFee = chargeBaseInfo.ServerFee;
                chargingBaseEntity.ParkFee = chargeBaseInfo.ParkFee;
                chargingBaseEntity.ChargeNum = chargeBaseInfo.ChargeNum;
                chargingBaseEntity.PayType = chargeBaseInfo.PayType;
                chargingBaseEntity.Address = chargeBaseInfo.Address;
                chargingBaseEntity.Coordinate = chargeBaseInfo.Coordinate;
                chargingBaseEntity.StartTime = chargeBaseInfo.StartTime;
                chargingBaseEntity.EndTime = chargeBaseInfo.EndTime;
                chargingBaseEntity.IsUse = chargeBaseInfo.IsUse;
                chargingBaseEntity.CityID = chargeBaseInfo.CityID;
                if (!string.IsNullOrEmpty(chargingBaseEntity.PayType))
                {
                    chargingBaseEntity.PayTypeName = GetPayTypeNames(chargingBaseEntity.PayType);
                }

                if (chargingBaseEntity.CityID > 0)
                {
                    City city = BaseDataService.GetAllCity().FirstOrDefault(t => t.CityID == chargingBaseEntity.CityID);
                    chargingBaseEntity.CityInfo = city;
                }
            }

            return chargingBaseEntity;
        }

        private static ChargingPileInfo TranslateChargingPileEntity(ChargingPileEntity chargingPileEntity)
        {
            ChargingPileInfo chargingPileInfo = new ChargingPileInfo();

            if (chargingPileEntity != null)
            {
                chargingPileInfo.ID = chargingPileEntity.ID;
                chargingPileInfo.Code = chargingPileEntity.Code;
                chargingPileInfo.Standard = chargingPileEntity.Standard;
                chargingPileInfo.SOC = chargingPileEntity.SOC;
                chargingPileInfo.Power = chargingPileEntity.Power;
                chargingPileInfo.Electric = chargingPileEntity.Electric;
                chargingPileInfo.CElectric = chargingPileEntity.CElectric;
                chargingPileInfo.Voltage = chargingPileEntity.Voltage;
                chargingPileInfo.CVoltage = chargingPileEntity.CVoltage;
                chargingPileInfo.ChargingBaseID = chargingPileEntity.ChargingBaseID;
                chargingPileInfo.IsUse = chargingPileEntity.IsUse;
            }
            return chargingPileInfo;
        }

        private static ChargingPileEntity TranslateChargingPileInfo(ChargingPileInfo chargingPileInfo)
        {
            ChargingPileEntity chargingPileEntity = new ChargingPileEntity();

            if (chargingPileInfo != null)
            {
                chargingPileEntity.ID = chargingPileInfo.ID;
                chargingPileEntity.Code = chargingPileInfo.Code;
                chargingPileEntity.Standard = chargingPileInfo.Standard;
                chargingPileEntity.SOC = chargingPileInfo.SOC;
                chargingPileEntity.Power = chargingPileInfo.Power;
                chargingPileEntity.Electric = chargingPileInfo.Electric;
                chargingPileEntity.CElectric = chargingPileInfo.CElectric;
                chargingPileEntity.Voltage = chargingPileInfo.Voltage;
                chargingPileEntity.CVoltage = chargingPileInfo.CVoltage;
                chargingPileEntity.ChargingBaseID = chargingPileInfo.ChargingBaseID;
                chargingPileEntity.IsUse = chargingPileInfo.IsUse;

                ChargingBaseEntity chargeBase = GetAllChargingBaseEntity().FirstOrDefault(t => t.IsUse == 1 && t.ChargeBaseID == chargingPileEntity.ChargingBaseID) ?? new ChargingBaseEntity();
                chargingPileEntity.ChargingBase = chargeBase;



            }
            return chargingPileEntity;
        }

        public static List<ChargingPileEntity> GetAllChargingPileEntity()
        {
            List<ChargingPileEntity> all = new List<ChargingPileEntity>();
            ChargeRepository mr = new ChargeRepository();
            List<ChargingPileInfo> miList = Cache.Get<List<ChargingPileInfo>>("ChargingPileALL");
            if (miList.IsEmpty())
            {
                miList = mr.GetAllChargingPileInfo();
                Cache.Add("ChargingPileALL", miList);
            }
            if (!miList.IsEmpty())
            {
                foreach (ChargingPileInfo mInfo in miList)
                {
                    ChargingPileEntity chargingPileEntity = TranslateChargingPileInfo(mInfo);
                    all.Add(chargingPileEntity);
                }
            }

            return all;
        }

        public static List<ChargingBaseEntity> GetAllChargingBaseEntity()
        {
            List<ChargingBaseEntity> all = new List<ChargingBaseEntity>();
            ChargeRepository mr = new ChargeRepository();
            List<ChargingBaseInfo> miList = Cache.Get<List<ChargingBaseInfo>>("ChargingBaseALL");
            if (miList.IsEmpty())
            {
                miList = mr.GetAllChargingBaseInfo();
                Cache.Add("ChargingBaseALL", miList);
            }
            if (!miList.IsEmpty())
            {
                foreach (ChargingBaseInfo mInfo in miList)
                {
                    ChargingBaseEntity chargingBaseEntity = TranslateChargingBaseInfo(mInfo);
                    all.Add(chargingBaseEntity);
                }
            }

            return all;
        }

        public static List<ChargingPileEntity> GetChargingPileListByBaseID(int baseID)
        {
            List<ChargingPileEntity> all = new List<ChargingPileEntity>();
            ChargeRepository mr = new ChargeRepository();
            List<ChargingPileInfo> miList = mr.GetChargingPileListByBaseID(baseID);
            if (!miList.IsEmpty())
            {
                foreach (ChargingPileInfo mInfo in miList)
                {
                    ChargingPileEntity chargingPileEntity = TranslateChargingPileInfo(mInfo);
                    all.Add(chargingPileEntity);
                }
            }

            return all;

        }

        public static ChargingPileEntity GetChargingPileEntityById(long id)
        {
            ChargingPileEntity entity = new ChargingPileEntity();
            ChargingPileInfo info = new ChargeRepository().GetChargingPileById(id);
            if (info != null)
            {
                entity = TranslateChargingPileInfo(info);
            }
            
            return entity;
        }

        public static ChargingBaseEntity GetChargingBaseById(int id)
        {
            ChargingBaseEntity entity = new ChargingBaseEntity();
            ChargingBaseInfo info = new ChargeRepository().GetChargingBaseById(id);
            if (info != null)
            {
                entity = TranslateChargingBaseInfo(info);
            }

            return entity;
        }

        public static bool ModifyChargingPile(ChargingPileEntity entity)
        {
            int result = 0;
            if (entity != null)
            {
                ChargeRepository mr = new ChargeRepository();

                ChargingPileInfo info = TranslateChargingPileEntity(entity);


                if (entity.ID>0)
                {
                    result = mr.ModifyChargingPile(info);
                }
                else
                {
                    info.CreateDate = DateTime.Now;
                    result = mr.CreateNewChargingPile(info);
                    if (result > 0)
                    {
                        ChargingBaseInfo cb = mr.GetChargingBaseById(info.ChargingBaseID);
                        if (cb != null)
                        {
                            int num = cb.ChargeNum + 1;
                            mr.ModifyPileNum(num, info.ChargingBaseID);
                        }
                    }
                }

                List<ChargingPileInfo> miList = mr.GetAllChargingPileInfo();//刷新缓存
                Cache.Add("ChargingPileALL", miList);
            }
            return result > 0;
        }

        public static bool ModifyChargingBase(ChargingBaseEntity entity)
        {
            int result = 0;
            if (entity != null)
            {
                ChargeRepository mr = new ChargeRepository();

                ChargingBaseInfo info = TranslateChargingBaseEntity(entity);


                if (entity.ChargeBaseID>0)
                {
                    result = mr.ModifyChargingBase(info);
                }
                else
                {
                    info.CreateDate = DateTime.Now;
                    result = mr.CreateNewChargingBase(info);
                }

                List<ChargingBaseInfo> miList = mr.GetAllChargingBaseInfo();//刷新缓存
                Cache.Add("ChargingBaseALL", miList);
            }
            return result > 0;
        }


        public static int RemoveChargingBase(int id)
        {
            ChargeRepository mr=new ChargeRepository();
            int r = mr.RemoveChargeBase(id);
            List<ChargingBaseInfo> miList = mr.GetAllChargingBaseInfo();//刷新缓存
            Cache.Add("ChargingBaseALL", miList);      
            return r;
        }

        public static int RemoveChargingPiple(long id)
        {
            ChargeRepository mr = new ChargeRepository();
            int r = mr.RemoveChargePile(id);
            List<ChargingPileInfo> miList = mr.GetAllChargingPileInfo();//刷新缓存
            Cache.Add("ChargingPileALL", miList);     
            return r;
        }

        public static List<ChargingPileEntity> GetChargingPileInfoByRule(string name, int status,int cid)
        {
            List<ChargingPileEntity> list = new List<ChargingPileEntity>();
            List<ChargingPileInfo> info = new ChargeRepository().GetChargingPileInfoRule(name, status, cid);
            if (info != null)
            {
                foreach (ChargingPileInfo item in info)
                {
                    list.Add(TranslateChargingPileInfo(item));
                }
            }

            return list;
        }

        public static List<ChargingBaseEntity> GetChargingBaseInfoByRule(string name, int status)
        {
            List<ChargingBaseEntity> list = new List<ChargingBaseEntity>();
            List<ChargingBaseInfo> info = new ChargeRepository().GetChargingBaseInfoRule(name, status);
            if (info != null)
            {
                foreach (ChargingBaseInfo item in info)
                {
                    list.Add(TranslateChargingBaseInfo(item));
                }
            }

            return list;
        }


        /// <summary>
        ///  1、 不传 返回所有2、 充电桩ID CPid  3、 供应点信息ID ChargeBaseID
        /// </summary>
        /// <param name="ChargeBaseID">供应点信息ID</param>
        /// <returns></returns>
        public static List<ChargingBaseEntity> GetChargingPileInfo(string ChargeBaseID)
        {
            List<ChargingBaseEntity> listBase = new List<ChargingBaseEntity>();

            List<ChargingBaseInfo> listBaseInfo = ChargeRepository.GetChargingBaseInfo("", ChargeBaseID);
            if (listBaseInfo != null && listBaseInfo.Count > 0)
            {
                listBase.Add(TransChargeBase(listBaseInfo[0], true));
            }
            return listBase;
            
        }

        public static List<ChargingBaseEntity> GetChargingBaseInfo(string cityid, string ChargeBaseID)
        {
            List<ChargingBaseEntity> lstCP = null;

            List<ChargingBaseInfo> lstCharging = Cache.Get<List<ChargingBaseInfo>>("GetChargingBaseInfo" + cityid + ChargeBaseID);
            if (lstCharging.IsEmpty())
            {
                lstCharging = ChargeRepository.GetChargingBaseInfo(cityid, ChargeBaseID);
                Cache.Add("GetChargingBaseInfo" + cityid + ChargeBaseID, lstCharging);
            }

            if (lstCharging != null && lstCharging.Count > 0)
            {
                lstCP = new List<ChargingBaseEntity>();
                foreach (ChargingBaseInfo info in lstCharging)
                {                    
                    lstCP.Add(TransChargeBase(info));
                }
            }
            return lstCP;

        }

        public static ChargingBaseEntity TransChargeBase(ChargingBaseInfo info,bool isShowPile=false)
        {
            ChargingBaseEntity entity = new ChargingBaseEntity();
            entity.ChargeBaseID = info.ChargeBaseID;
            entity.Name = info.Name;
            entity.Code = info.Code;
            entity.ChargeFee = info.ChargeFee;
            entity.ParkFee = info.ParkFee;
            entity.ChargeNum = info.ChargeNum;
            entity.PayType = info.PayType;
            entity.StartTime = info.StartTime;
            entity.EndTime = info.EndTime;
            entity.IsUse = info.IsUse;
            Random rd = new Random();
            int R = rd.Next(1, 8);
            entity.imageUrl = FileUrl + "/Images/Charging/" + R+".jpg"; ;
            if (isShowPile)
            {
                entity.chargingPile = new List<ChargingPileEntity>();
                List<ChargingPileInfo> lstCharging = Cache.Get<List<ChargingPileInfo>>("GetChargingPileInfo" + info.ChargeBaseID);
                if (lstCharging.IsEmpty())
                {
                    lstCharging = ChargeRepository.GetChargingPileInfo(info.ChargeBaseID.ToString()); ;
                    Cache.Add("GetChargingPileInfo" + info.ChargeBaseID, lstCharging);
                }

                if (lstCharging != null && lstCharging.Count > 0)
                {                    
                    foreach (ChargingPileInfo info1 in lstCharging)
                    {
                        ChargingPileEntity entityP = new ChargingPileEntity();
                        entityP.ID = info1.ID;
                        entityP.Code = info1.Code;
                        entityP.ChargingBaseID = info1.ChargingBaseID;
                        entityP.Standard = info1.Standard;
                        entityP.SOC = info1.SOC;
                        entityP.Power = info1.Power + "KW";
                        entityP.Electric = info1.Electric;
                        entityP.CElectric = info1.CElectric;
                        entityP.Voltage = info1.Voltage + "V";
                        entityP.CVoltage = info1.CVoltage + "V"; 
                        R = rd.Next(1, 6);
                        entityP.imageUrl = FileUrl + "/Images/Charging/p" + R + ".jpg"; ;
                        entity.chargingPile.Add(entityP);
                    }
                }
            }
            return entity;
        }


    }
}
