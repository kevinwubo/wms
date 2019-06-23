using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.BaseData
{
    /// <summary>
    /// 客户管理
    /// </summary>
    public class CustomerStatement
    {
        public static string GetCount = @"SELECT COUNT(1) AS C FROM wms_CustomerInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllCustomerInfoPager = @" DECLARE @UP INT
        
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
		                                                  WITH customer AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_CustomerInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM customer 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllCustomerInfoByRulePagerHeader = @"DECLARE @UP INT
        
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
		                                                  WITH customer AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_CustomerInfo WHERE 1=1 ";
        public static string GetAllCustomerInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM customer 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string GetAllCustomer = @"SELECT * FROM wms_CustomerInfo(NOLOCK)";

        public static string GetAllCustomerByRule = @"SELECT * FROM wms_CustomerInfo(NOLOCK) WHERE 1=1 ";

        public static string GetCustomerByKey = @"SELECT * FROM wms_CustomerInfo(NOLOCK) WHERE CustomerID=@CustomerID";

        public static string Remove = @"UPDATE wms_CustomerInfo SET Status=0 WHERE CustomerID=@CustomerID";

        public static string GetCustomerByKeys = @"SELECT * FROM wms_CustomerInfo(NOLOCK) WHERE CustomerID IN (#ids#)";

        public static string CreateNewCustomer = @"INSERT INTO [wms_CustomerInfo]([CustomerName],[CustomerNo],[OperatorID],[Status],[CreateDate],[ChangeDate])
                                                VALUES(@CustomerName,@CustomerNo,@OperatorID,@Status,@CreateDate,@ChangeDate) select @@IDENTITY";

        public static string ModifyCustomer = @"UPDATE [wms_CustomerInfo] SET [CustomerName] = @CustomerName,[CustomerNo] = @CustomerNo,[OperatorID] = @OperatorID
                                                ,[Status] = @Status,[ChangeDate] = @ChangeDate
                                                 WHERE CustomerID=@CustomerID";
    }
}
