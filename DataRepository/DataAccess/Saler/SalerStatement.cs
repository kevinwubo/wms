using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Saler
{
    public  class SalerStatement
    {
        public static string GetSalers = @"SELECT * FROM [Salesman](NOLOCK)";

        public static string GetSalersByRule = @"SELECT * FROM [Salesman](NOLOCK) WHERE 1=1 ";

        public static string GetSalersByKey = @"SELECT * FROM [Salesman](NOLOCK) WHERE SID=@SID";

        public static string Remove = @"UPDATE [Salesman] SET Status=0 WHERE SID=@SID";

        public static string GetSalersAllCount = @"SELECT * FROM [Salesman](NOLOCK) WHERE 1=1 ";


        public static string CreateSaler = @" INSERT INTO [Salesman] ([SCode],[Name],[Sex],[Birthday],[CertificateType],[CertificateNo],[WXCode],[Mobile],[Status],[AttachmentIDs],[CreateDate]) VALUES (@SCode,@Name,@Sex,@Birthday,@CertificateType,@CertificateNo,@WXCode,@Mobile,@Status,@AttachmentIDs,@CreateDate) ";

        public static string ModifySaler = @"  UPDATE [Salesman] SET SCode=@SCode,Name=@Name,Sex=@Sex,Birthday=@Birthday,CertificateType=@CertificateType,CertificateNo=@CertificateNo,WXCode=@WXCode,Mobile=@Mobile,Status=@Status,AttachmentIDs=@AttachmentIDs WHERE SID=@SID";

        public static string GetSalerAllPagerHeader = @"DECLARE @UP INT
        
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
		                                                  WITH ce AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY [Status]) AS RowNumber FROM (SELECT * FROM Salesman WHERE 1=1 ";

        public static string GetSalerAllPagerFooter = @")as T ) 
		                                                  SELECT *  FROM ce 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string CreateRelation = @"INSERT INTO [SalerAndCustomer] ([CustomerID],[CustomerCode],[SalerID],[SalerCode],[Status],[SalerSource]) VALUES ( @CustomerID,@CustomerCode,@SalerID,@SalerCode,@Status,@SalerSource )";

        public static string GetSalerCustomerBySalerCode = @"SELECT ID,CustomerID,CustomerCode,SalerID,SalerCode,Status,CreateDate  FROM SalerAndCustomer WHERE SalerCode=@SalerCode";
        
        public static string GetSalerCustomerByTelephone = @"select a.CustomerID,a.CustomerCode,a.SalerID,a.SalerCode,a.SalerSource from dbo.SalerAndCustomer a,Customer b where a.CustomerID=b.CustomerID and b.Mobile=@Mobile";


    }
}
