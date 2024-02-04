using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gotchi.Core.Helpers;

public static class GameSettings
{
    public static readonly string GameSettingsPath = "GameSettings.json";
    public static Settings Values() 
    {
        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
        };


        var values = JsonSerializer.Deserialize<Settings>
            (
                File.ReadAllText(GameSettingsPath), options
            );

        return values ?? throw new Exception($"Error when deserializing Game settings. Path : {GameSettingsPath}");
    }

    public class Settings 
    {
        public int DeductionBaseAmount { get; set; }
        public int DeductionPercentage { get; set; }
        public int StartingBalance { get; set; }
        public int StartingMaxHunger { get; set; }
        public int HungerAmountPerHour { get; set; }
        public int FoodHungerValue { get; set; }
        public int FoodBaseCost { get; set; }
        public int FoodAddedCost { get; set;}
        public int UpdateGotchiInMinutes { get; set; }
        public int UpdateCoinValuesDbInMinutes{ get; set;}
    }
}

