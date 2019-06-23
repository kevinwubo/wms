/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：AdviseService
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/9/2018 8:28:25 AM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using Common;
using DataRepository.DataAccess.Advise;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AdviseService : BaseService
    {
        private static AdviseInfo TranslateAdviseInfo(AdviseEntity adviseEntity)
        {
            AdviseInfo adviseInfo = new AdviseInfo();
            if (adviseEntity != null)
            {
                adviseInfo.AdviseID = adviseEntity.AdviseID;
                adviseInfo.AdviseTitle = adviseEntity.AdviseTitle;
                adviseInfo.Summary = adviseEntity.Summary;
                adviseInfo.CustomerID = adviseEntity.CustomerID;
                adviseInfo.CustomerName = adviseEntity.CustomerName;
                adviseInfo.CustomerMobile = adviseEntity.CustomerMobile;
                adviseInfo.DealStatus = adviseEntity.DealStatus;
                adviseInfo.DealSummary = adviseEntity.DealSummary;
                adviseInfo.Operator = adviseEntity.Operator;
                adviseInfo.ModifyDate = adviseEntity.ModifyDate;
            }


            return adviseInfo;
        }

        private static AdviseEntity TranslateAdviseEntity(AdviseInfo adviseInfo)
        {
            AdviseEntity adviseEntity = new AdviseEntity();
            if (adviseInfo != null)
            {
                adviseEntity.AdviseID = adviseInfo.AdviseID;
                adviseEntity.AdviseTitle = adviseInfo.AdviseTitle;
                adviseEntity.Summary = adviseInfo.Summary;
                adviseEntity.CustomerID = adviseInfo.CustomerID;
                adviseEntity.CustomerName = adviseInfo.CustomerName;
                adviseEntity.CustomerMobile = adviseInfo.CustomerMobile;
                adviseEntity.DealStatus = adviseInfo.DealStatus;
                adviseEntity.DealSummary = adviseInfo.DealSummary;
                adviseEntity.Operator = adviseInfo.Operator;
                adviseEntity.ModifyDate = adviseInfo.ModifyDate;
            }


            return adviseEntity;
        }

        public static List<AdviseEntity> GetAllAdvise()
        {
            List<AdviseEntity> all = new List<AdviseEntity>();
            AdviseRepository mr = new AdviseRepository();
            List<AdviseInfo> miList = mr.GetAllAdviseInfo();
            foreach (AdviseInfo mInfo in miList)
            {
                AdviseEntity adviseEntity = TranslateAdviseEntity(mInfo);
                all.Add(adviseEntity);
            }

            return all;
        }

        public static List<AdviseEntity> GetAdviseInfoByCustomerID(long customerID)
        {
            List<AdviseEntity> all = new List<AdviseEntity>();
            AdviseRepository mr = new AdviseRepository();
            List<AdviseInfo> miList = mr.GetAdviseInfoByCustomerID(customerID);
            foreach (AdviseInfo mInfo in miList)
            {
                AdviseEntity adviseEntity = TranslateAdviseEntity(mInfo);
                all.Add(adviseEntity);
            }
            return all;
        }

        public static List<AdviseEntity> GetAdviseInfoByUserID(long uid)
        {
            List<AdviseEntity> all = new List<AdviseEntity>();
            AdviseRepository mr = new AdviseRepository();
            List<AdviseInfo> miList = mr.GetAdviseInfoByUserID(uid);
            foreach (AdviseInfo mInfo in miList)
            {
                AdviseEntity adviseEntity = TranslateAdviseEntity(mInfo);
                all.Add(adviseEntity);
            }
            return all;
        }

        public static List<AdviseEntity> GetAdviseInfoByRule(string customerName, string title, int dealStatus,PagerInfo pager)
        {
            List<AdviseEntity> all = new List<AdviseEntity>();
            AdviseRepository mr = new AdviseRepository();
            List<AdviseInfo> miList = mr.GetAllAdviseInfoByRule(customerName, title, dealStatus, pager);
            foreach (AdviseInfo mInfo in miList)
            {
                AdviseEntity adviseEntity = TranslateAdviseEntity(mInfo);
                all.Add(adviseEntity);
            }
            return all;
        }

        public static int GetAdviseCount(string customerName, string title, int dealStatus)
        {
            return new AdviseRepository().GetAdviseCount(customerName, title, dealStatus);
        }

        public static List<AdviseEntity> GetAdviseInfoPager(PagerInfo pager)
        {
            List<AdviseEntity> all = new List<AdviseEntity>();
            AdviseRepository mr = new AdviseRepository();
            List<AdviseInfo> miList = mr.GetAllAdviseInfoPager(pager);
            foreach (AdviseInfo mInfo in miList)
            {
                AdviseEntity adviseEntity = TranslateAdviseEntity(mInfo);
                all.Add(adviseEntity);
            }
            return all;
        }

        public static AdviseEntity GetAdviseInfoByID(long id)
        {
            AdviseInfo resultInfo = new AdviseRepository().GetAdviseInfoByID(id);

            AdviseEntity result = TranslateAdviseEntity(resultInfo);
            return result; 
        }

        public static void CreateNewAdvise(AdviseEntity adviseEntity)
        {
            if (adviseEntity != null)
            {
                AdviseInfo resultInfo = TranslateAdviseInfo(adviseEntity);
                new AdviseRepository().CreateNewAdvise(resultInfo);
            }
        }

        public static void ModifyAdvise(AdviseEntity adviseEntity)
        {
            if (adviseEntity != null)
            {
                AdviseInfo resultInfo = TranslateAdviseInfo(adviseEntity);
                resultInfo.ModifyDate = DateTime.Now;
                new AdviseRepository().ModifyAdvise(resultInfo);
            }
        }

        public static int ModifyDealInfo(AdviseEntity adviseEntity)
        {
            int result = 0;
            if (adviseEntity != null)
            {
                AdviseInfo resultInfo = TranslateAdviseInfo(adviseEntity);
                resultInfo.ModifyDate = DateTime.Now;
                result=new AdviseRepository().ModifyAdviseDealInfo(resultInfo);
            }

            return result;
        }

    }
}
