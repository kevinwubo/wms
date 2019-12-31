using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.BaseData
{
    /// <summary>
    /// 承运商管理
    /// </summary>
    public class CarrierStatement
    {
        public static string GetCount = @"SELECT COUNT(1) AS C FROM wms_CarrierInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllCarrierInfoPager = @" DECLARE @UP INT
        
	                                                  ---------分页区间计算-------------最大页数
                                                      IF(@recordCount<@PageSize*(@PageIndex-1)) 
                                                      BEGIN
                                                        SET @PageIndex= @recordCount/@PageSize+1
                                                      END
                                                      --最小页数
	                                                  IF(@PageIndex<1)
	                                                     SET @PageIndex=1
                                                      --当前页起始游标值
	                                                  IF(@recordCount>@PageSize*(@PageIndex-1))
	                                                  BEGIN
		                                                  SET @UP=@PageSize*(@PageIndex-1);
		                                                  ---------分页查询-----------
		                                                  WITH goods AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_CarrierInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM goods 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllCarrierInfoByRulePagerHeader = @"DECLARE @UP INT
        
	                                                  ---------分页区间计算-------------最大页数
                                                      IF(@recordCount<@PageSize*(@PageIndex-1)) 
                                                      BEGIN
                                                        SET @PageIndex= @recordCount/@PageSize+1
                                                      END
                                                      --最小页数
	                                                  IF(@PageIndex<1)
	                                                     SET @PageIndex=1
                                                      --当前页起始游标值
	                                                  IF(@recordCount>@PageSize*(@PageIndex-1))
	                                                  BEGIN
		                                                  SET @UP=@PageSize*(@PageIndex-1);
		                                                  ---------分页查询-----------
		                                                  WITH goods AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_CarrierInfo WHERE 1=1 ";
        public static string GetAllCarrierInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM goods 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string GetAllCarrier = @"SELECT * FROM wms_CarrierInfo(NOLOCK) WHERE Status=1";

        public static string GetAllCarrierByRule = @"SELECT * FROM wms_CarrierInfo(NOLOCK) WHERE 1=1 ";

        public static string GetCarrierByKey = @"SELECT * FROM wms_CarrierInfo(NOLOCK) WHERE CarrierID=@CarrierID";

        public static string Remove = @"UPDATE wms_CarrierInfo SET Status=0 WHERE CarrierID=@CarrierID";

        public static string GetCarrierByKeys = @"SELECT * FROM wms_CarrierInfo(NOLOCK) WHERE CarrierID IN (#ids#)";

        public static string CreateNewCarrier = @"INSERT INTO [wms_CarrierInfo]([CarrierName],[CarrierShortName],[CarrierNo],[Remark],Type,CarNo)VALUES(@CarrierName,@CarrierShortName,@CarrierNo,@Remark,@Type,@CarNo) select @@IDENTITY";

        public static string ModifyCarrier = @"UPDATE [wms_CarrierInfo]SET [CarrierName] = @CarrierName,[CarrierShortName] = @CarrierShortName,[CarrierNo] = @CarrierNo,[Remark] = @Remark,Status=@Status,Type=@Type,CarNo=@CarNo WHERE CarrierID=@CarrierID";
    }
}
