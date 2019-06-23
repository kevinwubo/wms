using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Brand
{
    public class BrandRepository : DataAccessBase
    {
        public List<BrandInfo> GetAllBrand()
        {
            List<BrandInfo> result = new List<BrandInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BrandStatement.GetAllBrands, "Text"));
            result = command.ExecuteEntityList<BrandInfo>();
            return result;
        }


        public BrandInfo GetBrandByKey(long bid)
        {
            BrandInfo result = new BrandInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BrandStatement.GetBrandByKey, "Text"));
            command.AddInputParameter("@BrandID", DbType.Int64, bid);
            result = command.ExecuteEntity<BrandInfo>();
            return result;
        }

        public List<BrandInfo> GetBrandByRule(string name,int isuse)
        {
            List<BrandInfo> result = new List<BrandInfo>();
            string sql = BrandStatement.GetAllBrandsByRule;
            if (!string.IsNullOrEmpty(name))
            {
                sql += " AND BrandName LIKE '%" + name + "%'";
            }
            if (isuse>-1)
            {
                sql += " AND IsUse=" + isuse;
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sql, "Text"));

            result = command.ExecuteEntityList<BrandInfo>();
            return result;
        }

        public int CreateNew(BrandInfo brand)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BrandStatement.CreateBrand, "Text"));
            command.AddInputParameter("@BrandName", DbType.String, brand.BrandName);
            command.AddInputParameter("@BrandNameEN", DbType.String, brand.BrandNameEN);
            command.AddInputParameter("@ImageURL", DbType.String, brand.ImageURL);
            command.AddInputParameter("@Description", DbType.String, brand.Description);
            command.AddInputParameter("@IsUse", DbType.Int32, brand.IsUse);
            return command.ExecuteNonQuery();
        }

        public int ModifyBrand(BrandInfo brand)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BrandStatement.ModifyBrand, "Text"));
            command.AddInputParameter("@BrandID", DbType.Int64, brand.BrandID);
            command.AddInputParameter("@BrandName", DbType.String, brand.BrandName);
            command.AddInputParameter("@BrandNameEN", DbType.String, brand.BrandNameEN);
            command.AddInputParameter("@ImageURL", DbType.String, brand.ImageURL);
            command.AddInputParameter("@Description", DbType.String, brand.Description);
            command.AddInputParameter("@IsUse", DbType.Int32, brand.IsUse);

            return command.ExecuteNonQuery();
        }

        public int RemoveBrand(long bid)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BrandStatement.Remove, "Text"));
            command.AddInputParameter("@BrandID", DbType.Int64, bid);
            int result = command.ExecuteNonQuery();
            return result;
        }
    }
}
