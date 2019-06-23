using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Charge
{
    public class ChargeStatement
    {
        public static string GetAllChargingPile = @"SELECT * FROM [ChargingPile](NOLOCK)";

        public static string GetAllChargingBase = @"SELECT * FROM [ChargingBase](NOLOCK)";

        public static string GetAllChargingBaseByRule = @"SELECT * FROM [ChargingBase](NOLOCK) WHERE 1=1 ";

        public static string GetAllChargingPileByRule = @"SELECT * FROM [ChargingPile](NOLOCK) WHERE 1=1 ";

        public static string GetChargingPileByKey = @"SELECT * FROM [ChargingPile](NOLOCK) WHERE ID=@ID";

        public static string GetChargingBaseByKey = @"SELECT * FROM [ChargingBase](NOLOCK) WHERE ChargeBaseID=@ChargeBaseID";

        public static string GetChargingPileByBaseID = @"SELECT * FROM [ChargingPile](NOLOCK) WHERE ChargingBaseID=@ChargingBaseID";

        public static string RemoveChargingBase = @"UPDATE [ChargingBase] SET IsUse=0 WHERE ChargeBaseID=@ChargeBaseID";

        public static string RemoveChargingPile = @"UPDATE [ChargingPile] SET IsUse=0 WHERE ID=@ID";

        public static string ModifyPileNum = @"UPDATE [ChargingBase] SET ChargeNum=@ChargeNum WHERE ChargeBaseID=@ChargeBaseID";


        public static string ModifyChargingBase = @"UPDATE [ChargingBase] SET Name=@Name,Code=@Code,ChargeFee=@ChargeFee,ServerFee=@ServerFee,ParkFee=@ParkFee,ChargeNum=@ChargeNum,PayType=@PayType,Address=@Address,Coordinate=@Coordinate,StartTime=@StartTime,EndTime=@EndTime,IsUse=@IsUse,CityID=@CityID
                                                    WHERE ChargeBaseID=@ChargeBaseID";
        public static string ModifyChargingPile = @"UPDATE [ChargingPile] SET Code=@Code,Standard=@Standard,SOC=@SOC,Power=@Power,Electric=@Electric,CElectric=@CElectric,Voltage=@Voltage,CVoltage=@CVoltage,ChargingBaseID=@ChargingBaseID,IsUse=@IsUse
                                                    WHERE ID=@ID";

        public static string CreateChargingBase = @"INSERT INTO [ChargingBase] ([Name],[Code],[ChargeFee],[ServerFee],[ParkFee],[ChargeNum],[PayType],[Address],[Coordinate],[StartTime],[EndTime],[IsUse],[CreateDate])
                                                    VALUES(@Name,@Code,@ChargeFee,@ServerFee,@ParkFee,@ChargeNum,@PayType,@Address,@Coordinate,@StartTime,@EndTime,@IsUse,@CreateDate)";
        public static string CreateChargingPile = @"INSERT INTO [ChargingPile]([Code],[Standard],[SOC],[Power],[Electric],[CElectric],[Voltage],[CVoltage],[ChargingBaseID],[IsUse],[CreateDate])
                                                    VALUES (@Code,@Standard,@SOC,@Power,@Electric,@CElectric,@Voltage,@CVoltage,@ChargingBaseID,@IsUse,@CreateDate)";

        
    }
}
