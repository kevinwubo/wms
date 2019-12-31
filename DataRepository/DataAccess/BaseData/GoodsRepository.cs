using Common;
using DataRepository.DataAccess.Group;
using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Goods
{
    public class GoodsRepository : DataAccessBase
    {
        public List<GoodsInfo> GetAllGoods()
        {
            List<GoodsInfo> result = new List<GoodsInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(GoodsStatement.GetAllGoods, "Text"));
            result = command.ExecuteEntityList<GoodsInfo>();
            return result;
        }

        public List<GoodsInfo> GetGoodsByKeys(string keys)
        {
            List<GoodsInfo> result = new List<GoodsInfo>();
            if (!string.IsNullOrEmpty(keys))
            {
                string sqlText = GoodsStatement.GetGoodsByKey;
                sqlText = sqlText.Replace("#ids#", keys);
                DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
                result = command.ExecuteEntityList<GoodsInfo>();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <param name="status">状态</param>
        /// <param name="BarCode">条形码</param>
        /// <returns></returns>
        public List<GoodsInfo> GetGoodsByRule(string goodsNo, int status, string goodsName, string goodsModel, string BarCode, int customerID)
        {
            List<GoodsInfo> result = new List<GoodsInfo>();
            string sqlText = GoodsStatement.GetAllGoodsByRule;
            if (!string.IsNullOrEmpty(goodsNo))
            {
                sqlText += " AND GoodsNo = @goodsNo";
            }
            if (!string.IsNullOrEmpty(goodsName))
            {
                sqlText += " AND goodsName = @goodsName";
            }
            if (!string.IsNullOrEmpty(goodsModel))
            {
                sqlText += " AND goodsModel = @goodsModel";
            }
            if (status > -1)
            {
                sqlText += " AND Status=@Status";
            }
            if (!string.IsNullOrEmpty(BarCode))
            {
                sqlText += " AND BarCode=@BarCode";
            }
            if (customerID > 0)
            {
                sqlText += " AND customerID = @customerID";
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(goodsNo))
            {
                command.AddInputParameter("@goodsNo", DbType.String, goodsNo);
            }
            if (!string.IsNullOrEmpty(goodsName))
            {
                command.AddInputParameter("@goodsName", DbType.String, goodsName);
            }
            if (!string.IsNullOrEmpty(goodsModel))
            {
                command.AddInputParameter("@goodsModel", DbType.String, goodsModel);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }
            if (!string.IsNullOrEmpty(BarCode))
            {
                command.AddInputParameter("@BarCode", DbType.String, BarCode);
            }
            if (customerID > 0)
            {
                command.AddInputParameter("@customerID", DbType.String, customerID);
            }
            result = command.ExecuteEntityList<GoodsInfo>();
            return result;
        }

        public GoodsInfo GetGoodsByKey(long gid)
        {
            GoodsInfo result = new GoodsInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(GoodsStatement.GetGoodsByKey, "Text"));
            command.AddInputParameter("@GoodsID", DbType.String, gid);
            result = command.ExecuteEntity<GoodsInfo>();
            return result;
        }

        public int CreateNew(GoodsInfo group)
        {
            List<GoodsInfo> goodsList = GetGoodsByRule("", -1, group.GoodsName, "", "", group.CustomerID);
            if (goodsList == null || goodsList.Count == 0)
            {
                DataCommand command = new DataCommand(ConnectionString, GetDbCommand(GoodsStatement.CreateNewGoods, "Text"));
                command.AddInputParameter("@TypeCode", DbType.String, group.TypeCode);
                command.AddInputParameter("@CustomerID", DbType.Int64, group.CustomerID);
                command.AddInputParameter("@GoodsName", DbType.String, group.GoodsName);
                command.AddInputParameter("@GoodsModel", DbType.String, group.GoodsModel);
                command.AddInputParameter("@Weight", DbType.String, group.Weight);
                command.AddInputParameter("@Size", DbType.String, group.Size);
                command.AddInputParameter("@Units", DbType.String, group.Units);
                command.AddInputParameter("@Costing", DbType.Decimal, group.Costing);
                command.AddInputParameter("@SalePrice", DbType.Decimal, group.SalePrice);
                command.AddInputParameter("@Torr", DbType.String, group.Torr);
                command.AddInputParameter("@exDate", DbType.String, group.exDate);
                command.AddInputParameter("@exUnits", DbType.String, group.exUnits);
                command.AddInputParameter("@AnotherNO", DbType.String, group.AnotherNO);
                command.AddInputParameter("@AnotherName", DbType.String, group.AnotherName);
                command.AddInputParameter("@Remark", DbType.String, group.Remark);
                command.AddInputParameter("@BarCode", DbType.String, group.BarCode);
                command.AddInputParameter("@OperatorID", DbType.Int64, group.OperatorID);
                command.AddInputParameter("@GoodsNo", DbType.String, group.GoodsNo);
                command.AddInputParameter("@Status", DbType.Int32, group.Status);
                command.AddInputParameter("@CreateDate", DbType.DateTime, group.CreateDate);
                return command.ExecuteNonQuery();
            }
            return 0;
        }

        public int ModifyGroup(GoodsInfo group)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(GoodsStatement.ModifyGoods, "Text"));
            command.AddInputParameter("@TypeCode", DbType.String, group.TypeCode);
            command.AddInputParameter("@CustomerID", DbType.Int64, group.CustomerID);
            command.AddInputParameter("@GoodsName", DbType.String, group.GoodsName);
            command.AddInputParameter("@GoodsModel", DbType.String, group.GoodsModel);
            command.AddInputParameter("@Weight", DbType.String, group.Weight);
            command.AddInputParameter("@Size", DbType.String, group.Size);
            command.AddInputParameter("@Units", DbType.String, group.Units);
            command.AddInputParameter("@Costing", DbType.Decimal, group.Costing);
            command.AddInputParameter("@SalePrice", DbType.Decimal, group.SalePrice);
            command.AddInputParameter("@Torr", DbType.String, group.Torr);
            command.AddInputParameter("@exDate", DbType.String, group.exDate);
            command.AddInputParameter("@exUnits", DbType.String, group.exUnits);
            command.AddInputParameter("@AnotherNO", DbType.String, group.AnotherNO);
            command.AddInputParameter("@AnotherName", DbType.String, group.AnotherName);
            command.AddInputParameter("@Remark", DbType.String, group.Remark);
            command.AddInputParameter("@BarCode", DbType.String, group.BarCode);
            command.AddInputParameter("@OperatorID", DbType.Int64, group.OperatorID);            
            command.AddInputParameter("@ChangeDate", DbType.DateTime, group.ChangeDate);
            command.AddInputParameter("@Status", DbType.Int32, group.Status);
            command.AddInputParameter("@GoodsID", DbType.Int32, group.GoodsID);
            command.AddInputParameter("@GoodsNo", DbType.String, group.GoodsNo);

            return command.ExecuteNonQuery();
        }

        public int RemoveGoods(long GoodsID)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(GoodsStatement.Remove, "Text"));
            command.AddInputParameter("@GoodsID", DbType.Int64, GoodsID);
            int result = command.ExecuteNonQuery();
            return result;
        }


        #region 分页相关
        public List<GoodsInfo> GetAllGoodsInfoPager(PagerInfo pager)
        {
            List<GoodsInfo> result = new List<GoodsInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(GoodsStatement.GetAllGoodsInfoPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<GoodsInfo>();
            return result;
        }


        public List<GoodsInfo> GetGoodsInfoByRule(string name, string modelCode,int CustomerID, int status, PagerInfo pager)
        {
            List<GoodsInfo> result = new List<GoodsInfo>();

            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND (GoodsName LIKE '%'+@key+'%' OR GoodsNo LIKE '%'+@key+'%' )");
            }
            if (!string.IsNullOrEmpty(modelCode))
            {
                builder.Append(" AND TypeCode=@TypeCode");
            }
            if (CustomerID > 0)
            {
                builder.Append(" AND CustomerID=@CustomerID");
            }
            if (status > -1)
            {
                builder.Append(" AND Status=@Status");
            }
            string sqlText = GoodsStatement.GetAllGoodsInfoByRulePagerHeader + builder.ToString() + GoodsStatement.GetAllGoodsInfoByRulePagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(name))
            {
                command.AddInputParameter("@key", DbType.String, name);
            }
            if (!string.IsNullOrEmpty(modelCode))
            {
                command.AddInputParameter("@TypeCode", DbType.String, modelCode);
            }
            if (CustomerID > 0)
            {
                command.AddInputParameter("@CustomerID", DbType.Int32, CustomerID);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<GoodsInfo>();
            return result;
        }

        public int GetGoodsCount(string name, string modelCode,int CustomerID, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(GoodsStatement.GetCount);
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND (GoodsName LIKE '%'+@key+'%' OR GoodsNo LIKE '%'+@key+'%' )");
            }
            if (!string.IsNullOrEmpty(modelCode))
            {
                builder.Append(" AND TypeCode=@TypeCode");
            }
            if (CustomerID > 0)
            {
                builder.Append(" AND CustomerID=@CustomerID");
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
                command.AddInputParameter("@TypeCode", DbType.String, modelCode);
            }
            if (CustomerID > 0)
            {
                command.AddInputParameter("@CustomerID", DbType.Int32, CustomerID);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }


            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }

        #endregion
    }
}
