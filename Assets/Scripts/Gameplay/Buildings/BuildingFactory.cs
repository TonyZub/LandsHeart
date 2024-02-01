using System;

namespace LandsHeart
{
    public sealed class BuildingFactory
    {
        #region Methods

        public Building GetBuilding(BuildingsNames buildingName) => buildingName switch
        {
            BuildingsNames.CommonHouse => new CommonHouse(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.Cemetery => new Cemetery(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.Warehouse => new Warehouse(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.FoodStorage => new FoodStorage(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.WatchTower => new WatchTower(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.TiltYard => new TiltYard(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.Market => new Market(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.GoldMine => new GoldMine(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.Mine => new Mine(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.Pub => new Pub(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.HuntingYard => new HuntingYard(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.FarmingField => new FarmingField(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.GunsmithsShop => new GunsmithsShop(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.Library => new Library(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.TailorsShop => new TailorsShop(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.BarnYard => new BarnYard(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.LoggingArea => new LoggingArea(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.Smithy => new Smithy(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.PaperManufactory => new PaperManufactory(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.Jeweler => new Jeweler(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.Hospital => new Hospital(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.SmallHospital => new SmallHospital(Data.Buildings.GetBuildingByName(buildingName)),
            BuildingsNames.Ambulatorium => new Ambulatorium(Data.Buildings.GetBuildingByName(buildingName)),
            _ => throw new ArgumentException($"There is no building for name {buildingName}")
        };

        #endregion
    }
}