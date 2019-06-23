using DataRepository.DataAccess.News;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helper;
using Service.BaseBiz;

namespace Service
{
    public class NewsService : BaseService
    {
        public static bool ModifiyNews(NewsEntity entity)
        {
            int result = 0;
            if (entity != null)
            {
                NewsRepository mr = new NewsRepository();
                NewsInfo newInfo = TranslateNewsInfo(entity);
                if (entity.ID > 0)
                {
                    result = mr.UpdateNew(newInfo);
                }
                else
                {
                    result = mr.InsertNew(newInfo);
                }

                //List<StoreInfo> miList = mr.InsertNew();//刷新缓存
                //Cache.Add("StoreALL", miList);
            }
            return result > 0;
        }

        public static List<NewsEntity> GetNews(string title,int status)
        {
            List<NewsEntity> lstNews = new List<NewsEntity>();
            NewsRepository mr = new NewsRepository();
            List<NewsInfo> lstInfo = mr.GetNews(title, status);
            if (lstInfo != null && lstInfo.Count > 0)
            {
                foreach (NewsInfo info in lstInfo)
                {
                    lstNews.Add(TranslateNewsEntity(info,false));
                }
            }
            return lstNews;
        }

        public static List<NewsEntity> GetCountNews(int count,bool isApi)
        {
            List<NewsEntity> lstNews = new List<NewsEntity>();
            NewsRepository mr = new NewsRepository();

            List<NewsInfo> lstInfo = Cache.Get<List<NewsInfo>>("GetCountNews" + count.ToString());
            if (lstInfo.IsEmpty())
            {
                lstInfo = mr.GetCountNews(count);
                Cache.Add("GetCountNews" + count.ToString(), lstInfo);
            }

            if (lstInfo != null && lstInfo.Count > 0)
            {
                foreach (NewsInfo info in lstInfo)
                {
                    lstNews.Add(TranslateNewsEntity(info, isApi));
                }
            }
            return lstNews;
        }

        public static NewsEntity TranslateNewsEntity(NewsInfo info,bool isApi)
        {
            NewsEntity entity = new NewsEntity();
            entity.ID = info.ID;
            entity.ChannelID = info.ChannelID;
            entity.Title = info.Title;
            entity.AttachmentIDs = info.AttachmentIDs;
            entity.zhaiyao = info.zhaiyao;
            if (isApi)//如果API接口调用 需要将图片地址替换成完成路径
            {
                entity.Content = info.Content.Replace("/Scripts/", FileUrl + "/Scripts/");
            }
            else
            {
                entity.Content = info.Content;
            }
            entity.Sort = info.Sort;
            entity.Status = info.Status;
            entity.Operator = info.Operator;
            entity.CreateDate = info.CreateDate;
            entity.ModifyDate = info.ModifyDate;
            List<AttachmentEntity>  lstAttach= BaseDataService.GetAttachmentInfoByKyes(info.AttachmentIDs);
            if (isApi == false)
            {
                entity.Attachments = lstAttach;
            }
            if (lstAttach != null && lstAttach.Count > 0)
            {
                entity.ImageUrl = lstAttach[0].FilePath.Replace("~", FileUrl);
            }
            return entity;
        }


        public static NewsEntity GetNewsByID(int ID)
        {
            NewsEntity entity = new NewsEntity();
            NewsRepository mr = new NewsRepository();
            NewsInfo info = mr.GetNewsByID(ID);
            if (info != null)
            {
                entity=TranslateNewsEntity(info,false);
            }
            return entity;
        }

        public static bool RemoveNews(int ID)
        {
            NewsRepository mr = new NewsRepository();
            return mr.DeleteNew(ID) > 1;
        }


        public static NewsInfo TranslateNewsInfo(NewsEntity entity)
        {
            NewsInfo info = new NewsInfo();
            if (entity != null)
            {
                info.ID = entity.ID;
                info.ChannelID = entity.ChannelID;
                info.Title = entity.Title;
                info.zhaiyao = entity.zhaiyao;
                info.Content = entity.Content;
                info.AttachmentIDs = entity.AttachmentIDs;
                info.Sort = entity.Sort;
                info.Status = entity.Status;
                info.Operator = entity.Operator;
            }
            return info;
        }
    }
}
