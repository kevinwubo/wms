using DataRepository.DataAccess.BaseData;
using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Charge
{
    public class ChargeRepository : DataAccessBase
    {
        /// <summary>
        /// 获取所有充电桩信息
        ///  1、 不传 返回所有2、 充电桩ID CPid  3、 供应点信息ID ChargeBaseID
        /// </summary>
        /// <param name="CPid">充电桩ID</param>
        /// <param name="ChargeBaseID">供应点信息ID</param>
        /// <returns></returns>
        public static List<ChargingPileInfo> GetChargingPileInfo(string ChargeBaseID)
        {
            List<ChargingPileInfo> result = new List<ChargingPileInfo>();
            string sqltext = ChargeStatement.GetAllChargingPile;
            sqltext += " where 1=1";
            if (!string.IsNullOrEmpty(ChargeBaseID))
            {
                sqltext += " and ChargingBaseID='" + ChargeBaseID + "'";
            }
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqltext, "Text"));
            result = command.ExecuteEntityList<ChargingPileInfo>();
            return result;
        }


        /// <summary>
        /// 获取所有供电站信息
        ///  1、 不传 返回所有2、 城市ID CPid  3、 供应点信息ID ChargeBaseID
        /// </summary>
        /// <param name="CPid">充电桩ID</param>
        /// <param name="ChargeBaseID">供应点信息ID</param>
        /// <returns></returns>
        public static List<ChargingBaseInfo> GetChargingBaseInfo(string CityID, string ChargeBaseID)
        {
            List<ChargingBaseInfo> result = new List<ChargingBaseInfo>();
            string sqltext = ChargeStatement.GetAllChargingBase;
            sqltext += " where 1=1";
            if (!string.IsNullOrEmpty(CityID))
            {
                sqltext += " and CityID='" + CityID + "'";
            }
            if (!string.IsNullOrEmpty(ChargeBaseID))
            {
                sqltext += " and ChargeBaseID='" + ChargeBaseID + "'";
            }
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqltext, "Text"));
            result = command.ExecuteEntityList<ChargingBaseInfo>();
            return result;
        }
		
        public List<ChargingBaseInfo> GetAllChargingBaseInfo()
        {
            List<ChargingBaseInfo> result = new List<ChargingBaseInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ChargeStatement.GetAllChargingBase, "Text"));
            result = command.ExecuteEntityList<ChargingBaseInfo>();
            return result;
        }

        public List<ChargingPileInfo> GetAllChargingPileInfo()
        {
            List<ChargingPileInfo> result = new List<ChargingPileInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ChargeStatement.GetAllChargingPile, "Text"));
            result = command.ExecuteEntityList<ChargingPileInfo>();
            return result;
        }

        public ChargingBaseInfo GetChargingBaseById(int id)
        {
            ChargingBaseInfo chbase = new ChargingBaseInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ChargeStatement.GetChargingBaseByKey, "Text"));
            command.AddInputParameter("@ChargeBaseID", DbType.Int32, id);
            chbase = command.ExecuteEntity<ChargingBaseInfo>();
            return chbase;
        }

        public ChargingPileInfo GetChargingPileById(long id)
        {
            ChargingPileInfo chpile = new ChargingPileInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ChargeStatement.GetChargingPileByKey, "Text"));
            command.AddInputParameter("@ID", DbType.Int64, id);
            chpile = command.ExecuteEntity<ChargingPileInfo>();
            return chpile;
        }

        public List<ChargingPileInfo> GetChargingPileListByBaseID(int bid)
        {
            List<ChargingPileInfo> result = new List<ChargingPileInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ChargeStatement.GetChargingPileByBaseID, "Text"));
            command.AddInputParameter("@ChargeBaseID", DbType.Int32, bid);
            result = command.ExecuteEntityList<ChargingPileInfo>();
            return result;
        }

        public int CreateNewChargingPile(ChargingPileInfo piple)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ChargeStatement.CreateChargingPile, "Text"));
            command.AddInputParameter("@Code", DbType.String, piple.Code);
            command.AddInputParameter("@Standard", DbType.String, piple.Standard);
            command.AddInputParameter("@SOC", DbType.String, piple.SOC);
            command.AddInputParameter("@Power", DbType.String, piple.Power);
            command.AddInputParameter("@Electric", DbType.String, piple.Electric);
            command.AddInputParameter("@CElectric", DbType.String, piple.CElectric);
            command.AddInputParameter("@Voltage", DbType.String, piple.Voltage);
            command.AddInputParameter("@CVoltage", DbType.String, piple.CVoltage);
            command.AddInputParameter("@ChargingBaseID", DbType.Int32, piple.ChargingBaseID);
            command.AddInputParameter("@IsUse", DbType.Int32, piple.IsUse);
            command.AddInputParameter("@CreateDate", DbType.DateTime, piple.CreateDate);

            return command.ExecuteNonQuery();
        }

        public int ModifyChargingPile(ChargingPileInfo piple)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ChargeStatement.ModifyChargingPile, "Text"));
            command.AddInputParameter("@ID", DbType.Int64, piple.ID);
            command.AddInputParameter("@Code", DbType.String, piple.Code);
            command.AddInputParameter("@Standard", DbType.String, piple.Standard);
            command.AddInputParameter("@SOC", DbType.String, piple.SOC);
            command.AddInputParameter("@Power", DbType.String, piple.Power);
            command.AddInputParameter("@Electric", DbType.String, piple.Electric);
            command.AddInputParameter("@CElectric", DbType.String, piple.CElectric);
            command.AddInputParameter("@Voltage", DbType.String, piple.Voltage);
            command.AddInputParameter("@CVoltage", DbType.String, piple.CVoltage);
            command.AddInputParameter("@ChargingBaseID", DbType.Int32, piple.ChargingBaseID);
            command.AddInputParameter("@IsUse", DbType.Int32, piple.IsUse);

            return command.ExecuteNonQuery();
        }

        public int CreateNewChargingBase(ChargingBaseInfo baseInfo)
        {
              DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ChargeStatement.CreateChargingBase, "Text"));
              command.AddInputParameter("@Name", DbType.String, baseInfo.Name);
              command.AddInputParameter("@Code", DbType.String, baseInfo.Code);
              command.AddInputParameter("@ChargeFee", DbType.Decimal, baseInfo.ChargeFee);
              command.AddInputParameter("@ServerFee", DbType.Decimal, baseInfo.ServerFee);
              command.AddInputParameter("@ParkFee", DbType.Decimal, baseInfo.ParkFee);
              command.AddInputParameter("@ChargeNum", DbType.Int32, baseInfo.ChargeNum);
              command.AddInputParameter("@PayType", DbType.String, baseInfo.PayType);
              command.AddInputParameter("@Address", DbType.String, baseInfo.Address);
              command.AddInputParameter("@Coordinate", DbType.String, baseInfo.Coordinate);
              command.AddInputParameter("@StartTime", DbType.String, baseInfo.StartTime);
              command.AddInputParameter("@EndTime", DbType.String, baseInfo.EndTime);
              command.AddInputParameter("@IsUse", DbType.Int32, baseInfo.IsUse);
              command.AddInputParameter("@CreateDate", DbType.DateTime, baseInfo.CreateDate);
              return command.ExecuteNonQuery();

        }


        public int ModifyChargingBase(ChargingBaseInfo baseInfo)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ChargeStatement.ModifyChargingBase, "Text"));
            command.AddInputParameter("@ChargeBaseID", DbType.Int32, baseInfo.ChargeBaseID);
            command.AddInputParameter("@Name", DbType.String, baseInfo.Name);
            command.AddInputParameter("@Code", DbType.String, baseInfo.Code);
            command.AddInputParameter("@ChargeFee", DbType.Decimal, baseInfo.ChargeFee);
            command.AddInputParameter("@ServerFee", DbType.Decimal, baseInfo.ServerFee);
            command.AddInputParameter("@ParkFee", DbType.Decimal, baseInfo.ParkFee);
            command.AddInputParameter("@ChargeNum", DbType.Int32, baseInfo.ChargeNum);
            command.AddInputParameter("@PayType", DbType.String, baseInfo.PayType);
            command.AddInputParameter("@Address", DbType.String, baseInfo.Address);
            command.AddInputParameter("@Coordinate", DbType.String, baseInfo.Coordinate);
            command.AddInputParameter("@StartTime", DbType.String, baseInfo.StartTime);
            command.AddInputParameter("@EndTime", DbType.String, baseInfo.EndTime);
            command.AddInputParameter("@CityID", DbType.String, baseInfo.CityID);
            command.AddInputParameter("@IsUse", DbType.Int32, baseInfo.IsUse);

            return command.ExecuteNonQuery();
        }

        public int RemoveChargeBase(int baseId)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ChargeStatement.RemoveChargingBase, "Text"));
            command.AddInputParameter("@ChargeBaseID", DbType.Int32, baseId);
            int result = command.ExecuteNonQuery();
            return result;
        }

        public int RemoveChargePile(long pid)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ChargeStatement.RemoveChargingPile, "Text"));
            command.AddInputParameter("@ID", DbType.Int64, pid);
            int result = command.ExecuteNonQuery();
            return result;
        }


        public List<ChargingBaseInfo> GetChargingBaseInfoRule(string name, int status)
        {
            List<ChargingBaseInfo> result = new List<ChargingBaseInfo>();
            string sqlText = ChargeStatement.GetAllChargingBaseByRule;
            if (!string.IsNullOrEmpty(name))
            {
                sqlText += " AND Name LIKE '%'+@key+'%'";
            }
            if (status > -1)
            {
                sqlText += " AND IsUse=@IsUse";
            }


            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(name))
            {
                command.AddInputParameter("@key", DbType.String, name);
            }
            if (status > -1)
            {
                command.AddInputParameter("@IsUse", DbType.Int32, status);
            }

            result = command.ExecuteEntityList<ChargingBaseInfo>();
            return result;
        }

        public List<ChargingPileInfo> GetChargingPileInfoRule(string name, int status,int cid)
        {
            List<ChargingPileInfo> result = new List<ChargingPileInfo>();
            string sqlText = ChargeStatement.GetAllChargingPileByRule;
            if (!string.IsNullOrEmpty(name))
            {
                sqlText += " AND Code LIKE '%'+@key+'%'";
            }
            if (status > -1)
            {
                sqlText += " AND IsUse=@IsUse";
            }
            if (cid > -1)
            {
                sqlText += " AND ChargingBaseID=@ChargingBaseID";
            }


            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(name))
            {
                command.AddInputParameter("@key", DbType.String, name);
            }
            if (status > -1)
            {
                command.AddInputParameter("@IsUse", DbType.Int32, status);
            }
            if (cid > -1)
            {
                command.AddInputParameter("@ChargingBaseID", DbType.Int32, cid);
            }

            result = command.ExecuteEntityList<ChargingPileInfo>();
            return result;
        }

        public int ModifyPileNum(int num, int bid)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ChargeStatement.ModifyPileNum, "Text"));
            command.AddInputParameter("@ChargeNum", DbType.Int32, num);
            command.AddInputParameter("@ChargeBaseID", DbType.Int32, bid);
            int result = command.ExecuteNonQuery();
            return result;
        }
    }
}
