using DataRepository.DataAccess.BaseData;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helper;
using System.Data;
using Common;
using DataRepository.DataAccess.Goods;

namespace Service.BaseBiz
{
    public class BaseDataService : BaseService
    {
        #region 基础数据导入
        public static List<GoodsEntity> GetImportList(DataSet ds)
        {
            List<GoodsEntity> list = new List<GoodsEntity>();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            GoodsEntity entity = new GoodsEntity();
                            CustomerEntity cEntity = null;
                            List<CustomerEntity> listC = CustomerService.GetCustomerByRule(dr["所属客户"].ToString(), -1);
                            if (listC != null && listC.Count > 0)
                            {
                                cEntity = listC[0];
                            }
                            entity.TypeCode = dr["商品类型"].ToString();
                            entity.CustomerID = cEntity != null ? cEntity.CustomerID : 0;
                            entity.GoodsNo = dr["商品编号"].ToString();
                            entity.GoodsName = dr["商品名称"].ToString();
                            entity.GoodsModel = dr["规格型号"].ToString();
                            entity.Weight = dr["重量"].ToString();
                            entity.Size = dr["尺寸"].ToString();
                            entity.Units = dr["单位"].ToString();
                            entity.SalePrice = string.IsNullOrEmpty(dr["售价"].ToString()) ? 0 : Decimal.Parse(dr["售价"].ToString());
                            entity.Torr = dr["托"].ToString();
                            entity.exDate = dr["保质期"].ToString();
                            entity.exUnits = dr["保质期单位"].ToString();
                            entity.BarCode = dr["商品条形码"].ToString();
                            entity.Remark = dr["备注"].ToString();
                            list.Add(entity);
                        }
                    }
                }
            }
            return list;
        }

        public static int InsertImport(List<GoodsEntity> list)
        {
            GoodsRepository mr = new GoodsRepository();
            int count = 0;
            if (list != null && list.Count > 0)
            {
                foreach (GoodsEntity entity in list)
                {
                    count++;
                    //检测是否存在
                    entity.CreateDate = DateTime.Now;
                    entity.ChangeDate = DateTime.Now;
                    GoodsInfo info = GoodsService.TranslateGoodsInfo(entity);
                    mr.CreateNew(info);
                }
            }
            return count;
        }
        #endregion
        public static void GetAllTest()
        {
            BaseDataRepository mr = new BaseDataRepository();
            DataSet ds= mr.GetAllTest();
            DataTable dt = ds.Tables[0];
            List<Province> proList = GetAllProvince();
            List<City> cList = GetAllCity();
            List<CarrierEntity> carrList = CarrierService.GetCarrierAll();
            List<StorageEntity> storageList = StorageService.GetStorageAll();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ReceiverRepository mrs = new ReceiverRepository();
                    ReceiverInfo info = new ReceiverInfo();
                    List<CustomerEntity> listCus = CustomerService.GetCustomerByRule(dr["客户名称"].ToString(), -1);
                    if (listCus != null && listCus.Count > 0)
                    {
                        info.CustomerID = listCus[0].CustomerID;
                    }
                    info.ReceiverName = dr["门店名称"].ToString();
                    info.ReceiverNo = dr["门店编号"].ToString();
                    info.Address = dr["门店地址"].ToString();
                    Province pInfo = proList.Find(p => p.ProvinceName.Equals(dr["所属省份"].ToString().Replace("省", "").Replace("市", "")));
                    info.ProvinceID = pInfo != null ? pInfo.ProvinceID : 0;
                    info.ReceiverType = "门店";
                    City cInfo = cList.Find(p => p.CityName.Equals(dr["所属市区"].ToString().Replace("市", "")));
                    info.CityID = cInfo != null ? cInfo.CityID : 0;
                    info.Address = dr["门店地址"].ToString();
                    info.OperatorID = 1;
                    info.Remark = dr["门店描述"].ToString();
                    CarrierEntity centity = carrList.Find(p => p.CarrierShortName.Equals(dr["默认承运商简称"].ToString()));
                    info.DefaultCarrierID = centity != null ? centity.CarrierID : 0;
                    StorageEntity sEntity = storageList.Find(p => p.StorageName.Equals(dr["默认出库仓"]));
                    info.DefaultStorageID = sEntity != null ? sEntity.StorageID : 0;
                    info.Status = 1;
                    info.CreateDate = DateTime.Now;
                    info.ChangeDate = DateTime.Now;
                    long id = mrs.CreateNew(info);

                    ContactRepository cr = new ContactRepository();
                    ContactInfo contact = new ContactInfo();
                    if (!string.IsNullOrEmpty(dr["联系人"].ToString()))
                    {
                        contact.ContactName = dr["联系人"].ToString();
                        contact.Mobile = dr["联系人"].ToString();
                        contact.Telephone = dr["联系人"].ToString();
                        contact.Email = "";
                        contact.Remark = "";
                        contact.UnionType = UnionType.Receiver.ToString();//客户
                        contact.UnionID = id;
                        contact.CreateDate = DateTime.Now;
                        contact.ChangeDate = DateTime.Now;
                        cr.CreateNew(contact);
                    }   

                }
            }

        }

        private static BaseDataInfo TranslateBaseDataInfo(BaseDataEntity baseDataEntity)
        {
            BaseDataInfo baseDataInfo = new BaseDataInfo();
            if (baseDataEntity != null)
            {
                baseDataInfo.ID = baseDataEntity.ID;
                baseDataInfo.TypeCode = baseDataEntity.TypeCode ?? "";
                baseDataInfo.PCode = baseDataEntity.PCode??"";
                baseDataInfo.ValueInfo = baseDataEntity.ValueInfo ?? "";
                baseDataInfo.Description = baseDataEntity.Description ?? "";
                baseDataInfo.Status = baseDataEntity.Status;
            }

            return baseDataInfo;
        }

        private static BaseDataEntity TranslateBaseDataEntity(BaseDataInfo baseDataInfo)
        {
            BaseDataEntity baseDataEntity = new BaseDataEntity();
            if (baseDataInfo != null)
            {
                baseDataEntity.ID = baseDataInfo.ID;
                baseDataEntity.TypeCode = baseDataInfo.TypeCode;
                baseDataEntity.PCode = baseDataInfo.PCode;
                baseDataEntity.ValueInfo = baseDataInfo.ValueInfo;
                baseDataEntity.Description = baseDataInfo.Description;
                baseDataEntity.Status = baseDataInfo.Status;
            }

            return baseDataEntity;
        }

        private static AttachmentEntity TranslateAttachmentInfo(AttachmentInfo attachmentInfo)
        {
            AttachmentEntity result = new AttachmentEntity();
            if (attachmentInfo != null)
            {
                result.AttachmentID = attachmentInfo.AttachmentID;
                result.FileName = attachmentInfo.FileName;
                result.FileExtendName = attachmentInfo.FileExtendName;
                result.FilePath = attachmentInfo.FilePath;
                result.UploadDate = attachmentInfo.UploadDate;
                result.FileType = attachmentInfo.FileType;
                result.BusinessType = attachmentInfo.BusinessType;
                result.Channel = attachmentInfo.Channel;
                result.FileSize = attachmentInfo.FileSize;
                result.Remark = attachmentInfo.Remark;
                result.Operator = attachmentInfo.Operator;
            }


            return result;
        }

        private static AttachmentInfo TranslateAttachmentEntity(AttachmentEntity attachmentInfo)
        {
            AttachmentInfo result = new AttachmentInfo();
            if (attachmentInfo != null)
            {
                result.AttachmentID = attachmentInfo.AttachmentID;
                result.FileName = attachmentInfo.FileName;
                result.FileExtendName = attachmentInfo.FileExtendName;
                result.FilePath = attachmentInfo.FilePath;
                result.UploadDate = attachmentInfo.UploadDate;
                result.FileType = attachmentInfo.FileType;
                result.BusinessType = attachmentInfo.BusinessType;
                result.Channel = attachmentInfo.Channel;
                result.FileSize = attachmentInfo.FileSize;
                result.Remark = attachmentInfo.Remark;
                result.Operator = attachmentInfo.Operator;
            }


            return result;
        }

        public static long CreateAttachment(AttachmentEntity attachmentInfo)
        {
            long result=0;
            BaseDataRepository mr = new BaseDataRepository();

            AttachmentInfo attachment = TranslateAttachmentEntity(attachmentInfo);
            attachment.CreateDate = DateTime.Now;
            result = mr.CreateNewAttachment(attachment);

            return result;
        }

        public static List<AttachmentEntity> GetAttachmentInfoByKyes(string ids)
        {
            List<AttachmentEntity> all = new List<AttachmentEntity>();
            BaseDataRepository mr = new BaseDataRepository();
            List<AttachmentInfo> miList = mr.GetAttachments(ids);

            if (!miList.IsEmpty())
            {
                foreach (AttachmentInfo mInfo in miList)
                {
                    AttachmentEntity attachEntity = TranslateAttachmentInfo(mInfo);
                    all.Add(attachEntity);
                }
            }

            return all;
        }

        public static bool ModifyBaseData(BaseDataEntity baseDataEntity)
        {
            int result = 0;
            if (baseDataEntity != null)
            {
                BaseDataRepository mr = new BaseDataRepository();

                BaseDataInfo baseDataInfo = TranslateBaseDataInfo(baseDataEntity);


                if (baseDataEntity.ID > 0)
                {
                    result = mr.ModifyData(baseDataInfo);
                }
                else
                {
                    baseDataInfo.CreateDate = DateTime.Now;
                    result = mr.CreateNew(baseDataInfo);
                }
                List<BaseDataInfo> miList = mr.GetAllData();//刷新缓存
                Cache.Add("BaseDataALL", miList);
            }
            return result > 0;
        }

        public static List<Province> GetAllProvince()
        {
            List<Province> result = Cache.Get<List<Province>>("ProvinceALL")??new List<Province>();

            if (result.IsEmpty())
            {
               BaseDataRepository mr = new BaseDataRepository();
               List<ProvinceInfo> pInfo = mr.GetAllProvince();

               foreach (var item in pInfo)
               {
                   Province p = new Province();
                   p.ProvinceID = item.ProvinceID;
                   p.ProvinceName = item.ProvinceName;
                   result.Add(p);
               }
               Cache.Add("ProvinceALL", result);
            }

            return result;
        }

        public static List<City> GetAllCity()
        {
            List<City> result = Cache.Get<List<City>>("CityALL") ?? new List<City>();
            List<Province> allProvinces = GetAllProvince();
            if (result.IsEmpty())
            {
                BaseDataRepository mr = new BaseDataRepository();
                List<CityInfo> cInfo = mr.GetAllCity();

                foreach (var item in cInfo)
                {
                    City c= new City();
                    c.CityID = item.CityID;
                    c.CityName = item.CityName;
                    c.ProvinceID = item.ProvinceID;
                    c.ProvinceInfo = allProvinces.FirstOrDefault(t => t.ProvinceID == item.ProvinceID) ?? new Province();
                    result.Add(c);
                }
                Cache.Add("CityALL", result);
            }

            return result;
        }

        public static List<City> GetAllHasCity()
        {
            List<City> result = Cache.Get<List<City>>("GetAllHasCity") ?? new List<City>();            
            if (result.IsEmpty())
            {
                BaseDataRepository mr = new BaseDataRepository();
                List<CityInfo> cInfo = mr.GetAllHasCity();
                foreach (var item in cInfo)
                {
                    City c = new City();
                    c.CityID = item.CityID;
                    c.CityName = item.CityName;
                    c.ProvinceID = item.ProvinceID;                    
                    result.Add(c);
                }
                Cache.Add("GetAllHasCity", result);
            }

            return result;
        }

        public static BaseDataEntity GetBaseDataById(int id)
        {
            BaseDataEntity result = new BaseDataEntity();
            BaseDataRepository mr = new BaseDataRepository();
            BaseDataInfo info = mr.GetBaseDataByKey(id);
            result = TranslateBaseDataEntity(info);
            return result;
        }

        public static List<BaseDataEntity> GetBaseDataAll()
        {
            List<BaseDataEntity> all = new List<BaseDataEntity>();
            BaseDataRepository mr = new BaseDataRepository();
            List<BaseDataInfo> miList = mr.GetAllData();//Cache.Get<List<BaseDataInfo>>("BaseDataALLN");
            //if (miList.IsEmpty())
            //{
            //    miList = mr.GetAllData();
            //    Cache.Add("BaseDataALL", miList);
            //}

            if (!miList.IsEmpty())
            {
                foreach (BaseDataInfo mInfo in miList)
                {
                    BaseDataEntity BaseDataEntity = TranslateBaseDataEntity(mInfo);
                    all.Add(BaseDataEntity);
                }
            }

            return all;
        }



        

        public static List<BaseDataEntity> GetBaseDataByType(string tcode)
        {
            List<BaseDataEntity> all = new List<BaseDataEntity>();
            BaseDataRepository mr = new BaseDataRepository();
            List<BaseDataInfo> miList = mr.GetDataByType(tcode);

            if (!miList.IsEmpty())
            {
                foreach (BaseDataInfo mInfo in miList)
                {
                    BaseDataEntity BaseDataEntity = TranslateBaseDataEntity(mInfo);
                    all.Add(BaseDataEntity);
                }
            }

            return all;

        }

        public static List<BaseDataEntity> GetBaseDataByPCode(string pcode)
        {
            List<BaseDataEntity> all = new List<BaseDataEntity>();
            BaseDataRepository mr = new BaseDataRepository();
            List<BaseDataInfo> miList = mr.GetAllDataByPCode(pcode);

            if (!miList.IsEmpty())
            {
                foreach (BaseDataInfo mInfo in miList)
                {
                    BaseDataEntity BaseDataEntity = TranslateBaseDataEntity(mInfo);
                    all.Add(BaseDataEntity);
                }
            }

            return all;

        }

        public static List<BaseDataEntity> GetBaseDataByRule(string desc)
        {
            List<BaseDataEntity> all = new List<BaseDataEntity>();
            BaseDataRepository mr = new BaseDataRepository();
            List<BaseDataInfo> miList = mr.GetBaseDataByRule(desc);

            if (!miList.IsEmpty())
            {
                foreach (BaseDataInfo mInfo in miList)
                {
                    BaseDataEntity BaseDataEntity = TranslateBaseDataEntity(mInfo);
                    all.Add(BaseDataEntity);
                }
            }

            return all;

        }

        public static List<BaseDataEntity> GetAllPayType()
        {
            List<BaseDataEntity> payType = GetBaseDataAll().Where(p => p.PCode == "P00" && p.Status == 1).ToList();
            return payType;
        }


        public static void Remove(int id)
        {
            BaseDataRepository mr = new BaseDataRepository();
            mr.Remove(id);
        }

        public static void AddVerificationCode(VerificationCodeEntity entity)
        {
            BaseDataRepository mr = new BaseDataRepository();
            VerificationCodeInfo info = new VerificationCodeInfo();
            info.Mobile = entity.Mobile;
            info.Email = entity.Email;
            info.VCode = entity.VCode;
            info.Status = entity.Status;
            info.DeadLine = entity.DeadLine;
            mr.AddVerificationCode(info);

        }

        public static VerificationCodeEntity CheckVerificationCode(string telephone, string vcode)
        {
            BaseDataRepository mr = new BaseDataRepository();
            VerificationCodeInfo info=  mr.CheckVerificationCode(telephone, vcode);
            VerificationCodeEntity entity = new VerificationCodeEntity();
            if (info != null)
            {
                entity.Mobile = info.Mobile;
                entity.Email = info.Email;
                entity.VCode = info.VCode;
                entity.Status = info.Status;
                entity.DeadLine = info.DeadLine;
            }
            return entity;
        }
    }
}
