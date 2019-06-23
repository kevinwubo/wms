using Infrastructure.EntityFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataModel
{
    [Serializable]
    public class BrandInfo
    {
        [DataMapping("BrandID", DbType.Int64)]
        public long BrandID { get; set; }
        [DataMapping("BrandName", DbType.AnsiString)]
        public string BrandName { get; set; }
        [DataMapping("BrandNameEN", DbType.AnsiString)]
        public string BrandNameEN { get; set; }
        [DataMapping("ImageURL", DbType.AnsiString)]
        public string ImageURL { get; set; }
        [DataMapping("Description", DbType.AnsiString)]
        public string Description { get; set; }
        [DataMapping("IsUse", DbType.Int32)]
        public int IsUse { get; set; }
    }
}
