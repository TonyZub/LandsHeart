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
        private readonly RockResource _rock;
        private readonly MetalResource _metal;
        private readonly FabricResource _fabric;
        private readonly PaperResource _paper;

        private List<Human> _people;

        #endregion


        #region Properties

        public GoldResource Gold => _gold;
        public FoodResource Food => _food;
        public WoodResource Wood => _wood;
        public OreResource Ore => _ore;
        public RockResource Rock => _rock;
        public MetalResource Metal => _metal;
        public FabricResource Fabric => _fabric;
        public PaperResource Paper => _paper;
        public Human[] People => _people.ToArray();

        #endregion


        #region Constructor

        public ResourcesService()
        {
            _gold = new GoldResource(Data.StartResourcesData.StartGold);
            _food = new FoodResource(Data.StartResourcesData.StartFood);
            _wood = new WoodResource(Data.StartResourcesData.StartWood);
            _ore = new OreResource(Data.StartResourcesData.StartOre);
            _rock = new RockResource(Data.StartResourcesData.StartRock);
            _metal = new MetalResource(Data.StartResourcesData.StartMetal);
            _fabric = new FabricResource(Data.StartResourcesData.StartFabric);
            _paper = new PaperResource(Data.StartResourcesData.StartPaper);
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
            _rock.AmountChanged += OnResourceAmountChanged;
            _metal.AmountChanged += OnResourceAmountChanged;
            _fabric.AmountChanged += OnResourceAmountChanged;
            _paper.AmountChanged += OnResourceAmountChanged;
        }

        private void UnsubscribeEvents()
        {
            _gold.AmountChanged -= OnResourceAmountChanged;
            _food.AmountChanged -= OnResourceAmountChanged;
            _wood.AmountChanged -= OnResourceAmountChanged;
            _ore.AmountChanged -= OnResourceAmountChanged;
            _rock.AmountChanged -= OnResourceAmountChanged;
            _metal.AmountChanged -= OnResourceAmountChanged;
            _fabric.AmountChanged -= OnResourceAmountChanged;
            _paper.AmountChanged -= OnResourceAmountChanged;
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
        public void AddRock(int amount) => Rock.Increase(amount);
        public void RemoveRock(int amount) => Rock.Decrease(amount);
        public void AddMetal(int amount) => Metal.Increase(amount);
        public void RemoveMetal(int amount) => Metal.Decrease(amount);
        public void AddFabric(int amount) => Fabric.Increase(amount);   
        public void RemoveFabric(int amount) => Fabric.Decrease(amount);    
        public void AddPaper(int amount) => Paper.Increase(amount);
        public void RemovePaper(int amount) => Paper.Decrease(amount);
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

        public IResource GetResource(ResourcesTypes type) => type switch
        {
            ResourcesTypes.Gold => Gold,
            ResourcesTypes.Food => Food,
            ResourcesTypes.Wood => Wood,
            ResourcesTypes.Ore => Ore,
            ResourcesTypes.Rock => Rock,
            ResourcesTypes.Metal => Metal,
            ResourcesTypes.Fabric => Fabric,
            ResourcesTypes.Paper => Paper,
            _ => throw new ArgumentException(nameof(type), $"Not expected resource name: {type}"),
        };

        #endregion
    }
}