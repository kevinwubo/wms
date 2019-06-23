using DataRepository.DataAccess.Brand;
using DataRepository.DataModel;
using Entity.ViewModel;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BrandService
    {
        private static BrandInfo TranslateBrandInfo(BrandEntity brandEntity)
        {
            BrandInfo brandInfo = new BrandInfo();
            if (brandEntity != null)
            {
                brandInfo.BrandID = brandEntity.BrandID;
                brandInfo.BrandName = brandEntity.BrandName;
                brandInfo.BrandNameEN = brandEntity.BrandNameEN;
                brandInfo.Description = brandEntity.Description;
                brandInfo.ImageURL = brandEntity.ImageURL;
                brandInfo.IsUse = brandEntity.IsUse;
            }


            return brandInfo;
        }

        private static BrandEntity TranslateBrandEntity(BrandInfo brandInfo)
        {
            BrandEntity brandEntity = new BrandEntity();
            if (brandInfo != null)
            {
                brandEntity.BrandID = brandInfo.BrandID;
                brandEntity.BrandName = brandInfo.BrandName;
                brandEntity.BrandNameEN = brandInfo.BrandNameEN;
                brandEntity.Description = brandInfo.Description;
                brandEntity.ImageURL = brandInfo.ImageURL;
                brandEntity.IsUse = brandInfo.IsUse;

                AttachmentEntity attachment = new AttachmentEntity();
                if (!string.IsNullOrEmpty(brandEntity.ImageURL))
                {
                    attachment = BaseDataService.GetAttachmentInfoByKyes(brandEntity.ImageURL).FirstOrDefault()??new AttachmentEntity();
                }
                brandEntity.Attachment = attachment;
            }


            return brandEntity;
        }

        public static bool ModifyBrand(BrandEntity brandEntity)
        {
            int result = 0;
            if (brandEntity != null)
            {
                BrandRepository mr = new BrandRepository();

                BrandInfo brandInfo = TranslateBrandInfo(brandEntity);


                if (brandEntity.BrandID > 0)
                {
                    result = mr.ModifyBrand(brandInfo);
                }
                else
                {
                    result = mr.CreateNew(brandInfo);
                }
            }
            return result > 0;
        }

        public static BrandEntity GetBrandById(long bid)
        {
            BrandEntity result = new BrandEntity();
            BrandRepository mr = new BrandRepository();
            BrandInfo info = mr.GetBrandByKey(bid);
            result = TranslateBrandEntity(info);
            return result;
        }

        public static List<BrandEntity> GetBrandAll()
        {
            List<BrandEntity> all = new List<BrandEntity>();
            BrandRepository mr = new BrandRepository();
            List<BrandInfo> miList = mr.GetAllBrand();
            foreach (BrandInfo mInfo in miList)
            {
                BrandEntity brand = TranslateBrandEntity(mInfo);
                all.Add(brand);
            }

            return all;

        }

        public static List<BrandEntity> GetBrandByRule(string name,int isuse)
        {
            List<BrandEntity> all = new List<BrandEntity>();
            BrandRepository mr = new BrandRepository();
            List<BrandInfo> miList = mr.GetBrandByRule(name, isuse);
            foreach (BrandInfo mInfo in miList)
            {
                BrandEntity brand = TranslateBrandEntity(mInfo);
                all.Add(brand);
            }

            return all;

        }


        public static void Remove(long bid)
        {
            BrandRepository mr = new BrandRepository();
            mr.RemoveBrand(bid);
        }
    }
}
