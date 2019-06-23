/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：CarRepository
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/7/2018 3:27:38 PM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using Common;
using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Car
{
    public class CarRepository : DataAccessBase
    {
        public List<CarInfo> GetAllCarInfo()
        {
            List<CarInfo> result = new List<CarInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarStatement.GetAllCarInfo, "Text"));
            result = command.ExecuteEntityList<CarInfo>();
            return result;
        }

        public List<CarInfo> GetAllCarInfo(string carid, string supplierid)
        {

            string sqltext = CarStatement.GetAllCarInfoByRule;
            if (!string.IsNullOrEmpty(carid))
            {
                sqltext += " and CarID='" + carid + "'";
            }
            if (!string.IsNullOrEmpty(supplierid))
            {
                sqltext += " and SupplierID='" + supplierid + "'";

            }
            List<CarInfo> result = new List<CarInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqltext, "Text"));
            result = command.ExecuteEntityList<CarInfo>();
            return result;
        }

        /// <summary>
        /// 查询指定品牌下的车
        /// </summary>
        /// <param name="supilerType"></param>
        /// <returns></returns>
        public List<CarInfo> GetCarInfoByBrandID(long brandID)
        {
            List<CarInfo> result = new List<CarInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarStatement.GetCarInfoByBrandID, "Text"));
            command.AddInputParameter("@BrandID", DbType.Int64, brandID);
            result = command.ExecuteEntityList<CarInfo>();
            return result;
        }


        /// <summary>
        /// 查询随机热门汽车车型
        /// </summary>
        /// <param name="supilerType"></param>
        /// <returns></returns>
        public List<CarInfo> GetHotCarInfo(string SupplierType)
        {            
            List<CarInfo> result = new List<CarInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarStatement.GetHotCarInfo, "Text"));
            //command.AddInputParameter("@SupplierID", DbType.Int64, supplierID);
            result = command.ExecuteEntityList<CarInfo>();
            return result;
        }

        public List<CarInfo> GetCarInfoBySupplierID(long supplierID)
        {
            List<CarInfo> result = new List<CarInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarStatement.GetCarInfoBySupplierID, "Text"));
            command.AddInputParameter("@SupplierID", DbType.Int64, supplierID);
            result = command.ExecuteEntityList<CarInfo>();
            return result;
        }

        public List<CarInfo> GetCarInfoByRule(string name, string modelCode,int status,PagerInfo pager)
        {
            List<CarInfo> result = new List<CarInfo>();
           
            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND CarName LIKE '%'+@key+'%'");
            }
            if (!string.IsNullOrEmpty(modelCode))
            {
                builder.Append(" AND ModelCode=@ModelCode");
            }
            if (status > -1)
            {
                builder.Append(" AND Status=@Status");
            }
            string sqlText = CarStatement.GetAllCarInfoByRulePagerHeader + builder.ToString() + CarStatement.GetAllCarInfoByRulePagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(name))
            {
                command.AddInputParameter("@key", DbType.String, name);
            }
            if (!string.IsNullOrEmpty(modelCode))
            {
                command.AddInputParameter("@ModelCode", DbType.String, modelCode);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<CarInfo>();
            return result;
        }

        public int GetCarCount(string name, string modelCode, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(CarStatement.GetCarCount);
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND CarName LIKE '%'+@key+'%'");
            }
            if (!string.IsNullOrEmpty(modelCode))
            {
                builder.Append(" AND ModelCode=@ModelCode");
            }
            if (status > -1)
            {
                builder.Append(" AND Status=@Status");
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(builder.ToString(), "Text"));

            if (!string.IsNullOrEmpty(name))
            {
                command.AddInputParameter("@key", DbType.String, name);
            }
            if (!string.IsNullOrEmpty(modelCode))
            {
                command.AddInputParameter("@ModelCode", DbType.String, modelCode);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }


            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }



        public CarInfo GetCarInfoByKey(long cid)
        {
            CarInfo result = new CarInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarStatement.GetAllCarInfoByID, "Text"));
            command.AddInputParameter("@CarID", DbType.Int64, cid);
            result = command.ExecuteEntity<CarInfo>();
            return result;
        }

        public int CreateNewCarInfo(CarInfo car)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarStatement.CreateNewCarInfo, "Text"));
            command.AddInputParameter("@CarName", DbType.String, car.CarName);
            command.AddInputParameter("@ModelCode", DbType.String, car.ModelCode);
            command.AddInputParameter("@CarModel", DbType.String, car.CarModel);
            command.AddInputParameter("@AttachmentIDs", DbType.String, car.AttachmentIDs);
            command.AddInputParameter("@ContractCode", DbType.String, car.ContractCode);
            command.AddInputParameter("@AppearanceSize", DbType.String, car.AppearanceSize);
            command.AddInputParameter("@PlateSize", DbType.String, car.PlateSize);
            command.AddInputParameter("@Capacity", DbType.String, car.Capacity);
            command.AddInputParameter("@Slope", DbType.String, car.Slope);
            command.AddInputParameter("@MaxWeight", DbType.String, car.MaxWeight);
            command.AddInputParameter("@Wheelbase", DbType.String, car.Wheelbase);
            command.AddInputParameter("@BatteryCapacity", DbType.String, car.BatteryCapacity);
            command.AddInputParameter("@Quality", DbType.String, car.Quality);
            command.AddInputParameter("@Braking", DbType.String, car.Braking);
            command.AddInputParameter("@MaxSpeed", DbType.String, car.MaxSpeed);
            command.AddInputParameter("@SiteNum", DbType.String, car.SiteNum);
            command.AddInputParameter("@BatteryType", DbType.String, car.BatteryType);
            command.AddInputParameter("@SafeConfigure", DbType.String, car.SafeConfigure);
            command.AddInputParameter("@OuterConfigure", DbType.String, car.OuterConfigure);
            command.AddInputParameter("@Renewal", DbType.String, car.Renewal);
            command.AddInputParameter("@SupplierID", DbType.Int64, car.SupplierID);
            command.AddInputParameter("@Status", DbType.Int32, car.Status);
            command.AddInputParameter("@CarLicNumber", DbType.String, car.CarLicNumber);
            command.AddInputParameter("@CreateDate", DbType.DateTime, car.CreateDate);
            command.AddInputParameter("@ModifyDate", DbType.DateTime, car.ModifyDate);
            command.AddInputParameter("@Operator", DbType.Int64, car.Operator);
            command.AddInputParameter("@SalePrice", DbType.Decimal, car.SalePrice);
            command.AddInputParameter("@LeasePrice", DbType.Decimal, car.LeasePrice);
            command.AddInputParameter("@BrandID", DbType.Int64, car.BrandID);

            return command.ExecuteNonQuery();
        }

        public int ModifyCarInfo(CarInfo car)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarStatement.ModifyCarInfo, "Text"));
            command.AddInputParameter("@CarID", DbType.Int64, car.CarID);
            command.AddInputParameter("@CarName", DbType.String, car.CarName);
            command.AddInputParameter("@ModelCode", DbType.String, car.ModelCode);
            command.AddInputParameter("@CarModel", DbType.String, car.CarModel);
            command.AddInputParameter("@AttachmentIDs", DbType.String, car.AttachmentIDs);
            command.AddInputParameter("@ContractCode", DbType.String, car.ContractCode);
            command.AddInputParameter("@AppearanceSize", DbType.String, car.AppearanceSize);
            command.AddInputParameter("@PlateSize", DbType.String, car.PlateSize);
            command.AddInputParameter("@Capacity", DbType.String, car.Capacity);
            command.AddInputParameter("@Slope", DbType.String, car.Slope);
            command.AddInputParameter("@MaxWeight", DbType.String, car.MaxWeight);
            command.AddInputParameter("@Wheelbase", DbType.String, car.Wheelbase);
            command.AddInputParameter("@BatteryCapacity", DbType.String, car.BatteryCapacity);
            command.AddInputParameter("@Quality", DbType.String, car.Quality);
            command.AddInputParameter("@Braking", DbType.String, car.Braking);
            command.AddInputParameter("@MaxSpeed", DbType.String, car.MaxSpeed);
            command.AddInputParameter("@SiteNum", DbType.String, car.SiteNum);
            command.AddInputParameter("@BatteryType", DbType.String, car.BatteryType);
            command.AddInputParameter("@SafeConfigure", DbType.String, car.SafeConfigure);
            command.AddInputParameter("@OuterConfigure", DbType.String, car.OuterConfigure);
            command.AddInputParameter("@Renewal", DbType.String, car.Renewal);
            command.AddInputParameter("@SupplierID", DbType.Int64, car.SupplierID);
            command.AddInputParameter("@Status", DbType.Int32, car.Status);
            command.AddInputParameter("@CarLicNumber", DbType.String, car.CarLicNumber);
            command.AddInputParameter("@ModifyDate", DbType.DateTime, car.ModifyDate);
            command.AddInputParameter("@Operator", DbType.Int64, car.Operator);
            command.AddInputParameter("@SalePrice", DbType.Decimal, car.SalePrice);
            command.AddInputParameter("@LeasePrice", DbType.Decimal, car.LeasePrice);
            command.AddInputParameter("@BrandID", DbType.Int64, car.BrandID);

            return command.ExecuteNonQuery();
        }

        public int RemoveCarInfo(long cid)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarStatement.RemoveCarInfo, "Text"));
            command.AddInputParameter("@CarID", DbType.Int64, cid);
            int result = command.ExecuteNonQuery();
            return result;
        }

        public List<CarInfo> GetAllCarInfoPager(PagerInfo pager)
        {
            List<CarInfo> result = new List<CarInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarStatement.GetAllCarPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<CarInfo>();
            return result;
        }

        /// <summary>
        /// 获取我的租赁/试驾信息
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="RType">ZL：汽车租赁 SJ:汽车试驾</param>
        /// <returns></returns>
        public DataSet MyReservation(string userid, string RType)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarStatement.MyReservation, "Text"));
            command.AddInputParameter("@CustomerID", DbType.String, userid);
            command.AddInputParameter("@RType", DbType.String, RType);
            return command.ExecuteDataSet();
        }

        public DataSet MyCarInfoByCityID(string cityid)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarStatement.MyCarInfoByCityID, "Text"));
            command.AddInputParameter("@CityID", DbType.String, cityid);
            return command.ExecuteDataSet();
        }

        
    }
}
