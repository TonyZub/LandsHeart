using Extensions;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;


namespace LandsHeart
{
	public sealed class ResourcesService : IDisposable
	{
        #region Events

        public event Action<int, IResource> ResourceAmountChanged;
        public event Action<Human> HumanAdded;
        public event Action<Human> HumanRemoved;

        #endregion


        #region Fields

        private readonly GoldResource _gold;
        private readonly FoodResource _food;
        private readonly WoodResource _wood;
        private readonly OreResource _ore;

        private List<Human> _people;

        #endregion


        #region Properties

        public GoldResource Gold => _gold;
        public FoodResource Food => _food;
        public WoodResource Wood => _wood;
        public OreResource Ore => _ore;
        public Human[] People => _people.ToArray();

        #endregion


        #region Constructor

        public ResourcesService()
        {
            _gold = new GoldResource(Data.StartResourcesData.StartGold);
            _food = new FoodResource(Data.StartResourcesData.StartFood);
            _wood = new WoodResource(Data.StartResourcesData.StartWood);
            _ore = new OreResource(Data.StartResourcesData.StartOre);
            CreatePeople();
            SubscribeEvents();
        }

        #endregion


        #region IDisposable

        public void Dispose()
        {
            UnsubscribeEvents();
        }

        #endregion


        #region Methods

        private void CreatePeople()
        {
            _people = new List<Human>();
            for (int i = 0; i < Data.StartResourcesData.StartPeople; i++)
            {
                _people.Add(HumanFactory.MakeHuman());
            }
        }

        private void SubscribeEvents()
        {
            _gold.AmountChanged += OnResourceAmountChanged;
            _food.AmountChanged += OnResourceAmountChanged;
            _wood.AmountChanged += OnResourceAmountChanged;
            _ore.AmountChanged += OnResourceAmountChanged;
        }

        private void UnsubscribeEvents()
        {
            _gold.AmountChanged -= OnResourceAmountChanged;
            _food.AmountChanged -= OnResourceAmountChanged;
            _wood.AmountChanged -= OnResourceAmountChanged;
            _ore.AmountChanged -= OnResourceAmountChanged;
        }

        private void OnResourceAmountChanged(int difference, IResource resource)
        {
            ResourceAmountChanged?.Invoke(difference, resource);
        }

        #endregion


        #region Public Methods

        public void AddGold(int amount) => Gold.Increase(amount);
        public void RemoveGold(int amount) => Gold.Decrease(amount);
        public void AddFood(int amount) => Food.Increase(amount);
        public void RemoveFood(int amount) => Food.Decrease(amount);
        public void AddWood(int amount) => Wood.Increase(amount);
        public void RemoveWood(int amount) => Wood.Decrease(amount);
        public void AddOre(int amount) => Ore.Increase(amount);
        public void RemoveOre(int amount) => Ore.Increase(amount);
        public void AddPeople(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                _people.Add(HumanFactory.MakeHuman());
                HumanAdded?.Invoke(_people.LastObject());
            }
        }
        public void RemovePeople(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var humanIndex = Random.Range(0, _people.Count);
                HumanRemoved?.Invoke(_people[humanIndex]);
                _people.RemoveAt(humanIndex);
            }
        }

        #endregion
    }
}