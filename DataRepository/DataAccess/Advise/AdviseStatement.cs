using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Advise
{
    public class AdviseStatement
    {
        public static string GetAdviseCount = @"SELECT COUNT(1) AS C FROM AdviseInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllAdviseInfoPager = @"	  DECLARE @UP INT

                                                      IF(@recordCount<@PageSize*(@PageIndex-1)) 
                                                      BEGIN
                                                        SET @PageIndex= @recordCount/@PageSize+1
                                                      END
   
	                                                  IF(@PageIndex<1)
	                                                     SET @PageIndex=1
             
	                                                  IF(@recordCount>@PageSize*(@PageIndex-1))
	                                                  BEGIN
		                                                  SET @UP=@PageSize*(@PageIndex-1);

		                                                  WITH Advise AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY DealStatus) AS RowNumber FROM (SELECT * FROM AdviseInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM Advise 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string GetAllAdviseInfoPagerHeader=@"	  DECLARE @UP INT
        
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
		                                                  WITH Advise AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY DealStatus) AS RowNumber FROM (SELECT * FROM AdviseInfo WHERE 1=1 ";
        public static string GetAllAdviseInfoPagerFooter = @")as T ) 
		                                                  SELECT *  FROM Advise 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string GetAllAdviseInfo = @"SELECT * FROM [AdviseInfo](NOLOCK) ORDER BY DealStatus ";//查询的是未处理和处理中的

        public static string GetAdviseInfoByID = @"SELECT * FROM [AdviseInfo](NOLOCK) WHERE [AdviseID]=@AdviseID";

        public static string GetAdviseInfoByCustomerID = @"SELECT * FROM [AdviseInfo](NOLOCK) WHERE CustomerID=@CustomerID ORDER BY DealStatus";

        public static string GetAdviseInfoByUserID = @"SELECT * FROM [AdviseInfo](NOLOCK) WHERE Operator=@Operator ORDER BY DealStatus";

        public static string CreateNewtAdviseInfo = @"INSERT INTO ([AdviseInfo] [AdviseTitle],[Summary],[CustomerID],[CustomerName],[CustomerMobile],[DealStatus],[DealResult],[DealSummary],[Operator],[CreateDate],[ModifyDate])
                                                    VALUES (@AdviseTitle,@Summary,@CustomerID,@CustomerName,@CustomerMobile,@DealStatus,@DealResult,@DealSummary,@Operator,@CreateDate,@ModifyDate))";

        public static string ModifytAdviseInfo = @"UPDATE [AdviseInfo] SET AdviseTitle=@AdviseTitle,Summary=@Summary,CustomerID=@CustomerID,CustomerName=@CustomerName,CustomerMobile=@CustomerMobile,DealStatus=@DealStatus,DealSummary=@DealSummary,Operator=@Operator,ModifyDate=@ModifyDate
                                                   WHERE AdviseID=@AdviseID ";

        public static string DealAdviseInfo = @"UPDATE [AdviseInfo] SET DealStatus=@DealStatus,DealSummary=@DealSummary,Operator=@Operator,ModifyDate=@ModifyDate WHERE AdviseID=@AdviseID";

        public static string GetAllAdviseInfoByRule = @"SELECT * FROM [AdviseInfo](NOLOCK) WHERE 1=1 ";
    }
}
