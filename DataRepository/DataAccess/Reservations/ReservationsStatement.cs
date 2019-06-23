using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Reservations
{
    public class ReservationsStatement
    {
        public static string SelectSql = @"SELECT * FROM Reservations";

        public static string InsertSql = @"INSERT INTO Reservations(CustomerID,CustomerName,RType,PayType,CarID,LeaseTime,Price,Remark,RDate,Status,CreateDate)VALUES(@CustomerID,@CustomerName,@RType,@PayType,@CarID,@LeaseTime,@Price,@Remark,@RDate,@Status,@CreateDate)";

        public static string GetReservationCount = @"SELECT * FROM Reservations(NOLOCK) WHERE 1=1 ";

        public static string GetReservationByID = @"SELECT * FROM Reservations(NOLOCK) WHERE ID=@ID ";

        public static string EditReservationStatus = @"  UPDATE [Reservations] SET [Status]=@Status WHERE [ID]=@ID";

        public static string GetReservationPagerHeader = @"DECLARE @UP INT
        
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
		                                                  WITH resever AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status) AS RowNumber FROM (SELECT * FROM Reservations WHERE 1=1";

        public static string GetReservationPagerFooter = @")as T ) 
		                                                  SELECT *  FROM resever 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
    }
}
