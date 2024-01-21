using UnityEngine;


namespace LandsHeart
{
	[CreateAssetMenu(fileName = "StartResourcesData", menuName = "Data/Gameplay/StartResourcesData")]
	public sealed class StartResourcesData : ScriptableObject
	{
        #region Fields

        [SerializeField] private int _baseMaxGold;
        [SerializeField] private int _baseMaxFood;
        [SerializeField] private int _baseMaxWood;
        [SerializeField] private int _baseMaxOre;
        [SerializeField] private int _baseRock;
        [SerializeField] private int _baseMetal;
        [SerializeField] private int _baseFabric;
        [SerializeField] private int _basePaper;
        [SerializeField] private int _baseMaxPeople;

        [SerializeField] private int _startGold;
        [SerializeField] private int _startFood;
        [SerializeField] private int _startWood;
        [SerializeField] private int _startOre;
        [SerializeField] private int _startRock;
        [SerializeField] private int _startMetal;
        [SerializeField] private int _startFabric;
        [SerializeField] private int _startPaper;
        [SerializeField] private int _startPeople;

        #endregion


        #region Properties

        public int BaseMaxGold => _baseMaxFood;
        public int BaseMaxFood => _baseMaxFood;
        public int BaseMaxWood => _baseMaxWood;
        public int BaseMaxOre => _baseMaxOre;
        public int BaseRock => _baseRock;
        public int BaseMetal => _baseMetal; 
        public int BaseFabric => _baseFabric;
        public int BasePaper => _basePaper;
        public int BaseMaxPeople => _baseMaxPeople;

        public int StartGold => _startGold;
        public int StartFood => _startFood;
        public int StartWood => _startWood;
        public int StartOre => _startOre;
        public int StartRock => _startRock;
        public int StartMetal => _startMetal;
        public int StartFabric => _startFabric;
        public int StartPaper => _startPaper;
        public int StartPeople => _startPeople;

        #endregion
    }
}