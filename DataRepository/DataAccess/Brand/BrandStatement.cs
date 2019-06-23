using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Brand
{
    public class BrandStatement
    {
        public static string GetAllBrands = @"SELECT * FROM [CarBrand](NOLOCK)";

        public static string GetBrandByKey = @"SELECT * FROM [CarBrand](NOLOCK) WHERE [BrandID]=@BrandID ";

        public static string GetAllBrandsByRule = @"SELECT * FROM [CarBrand](NOLOCK) WHERE 1=1 ";

        public static string Remove = @"UPDATE CarBrand SET IsUse=0 WHERE [BrandID]=@BrandID";

        public static string CreateBrand = @"  INSERT INTO [CarBrand] ([BrandName],[BrandNameEN],[ImageURL],[Description],[IsUse])
                                                VALUES (@BrandName,@BrandNameEN,@ImageURL,@Description,@IsUse)";

        public static string ModifyBrand = @"   UPDATE [CarBrand] SET [BrandName]=@BrandName,[BrandNameEN]=@BrandNameEN,[ImageURL]=@ImageURL,[Description]=@Description,[IsUse]=@IsUse WHERE [BrandID]=@BrandID";
    }
}
