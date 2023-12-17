using System;
using System.Collections.Generic;


namespace LandsHeart
{
	[Serializable]
	public sealed class Human
	{
        #region Fields

        private readonly Profession _profession;
        private readonly string _name;
		private readonly string _surname;
        private readonly string _prehistory;
		private readonly int _imageIndex;

		private List<Feature> _features;
		private List<Item> _items;

		private HumanStatus _status;

		#endregion


		#region Properties

		public HumanStatus Status => _status;
		public Feature[] Features => _features.ToArray();
		public Item[] Items => _items.ToArray();
		public Profession Profession => _profession;
		public string Name => _name;
		public string Surname => _surname;
		public string Prehistory => _prehistory;
		public int ImageIndex => _imageIndex;

        #endregion


        #region Constructor

		public Human(string name, string surname, string prehistory, Profession profession, List<Feature> features, 
			List<Item> items, HumanStatus status, int imageIndex)
		{
			_name = name;
			_surname = surname;
			_prehistory = prehistory;
			_profession = profession;
			_features = features;
			_items = items;
			_status = status;
			_imageIndex = imageIndex;
        }

        #endregion


        #region Methods



        #endregion
    }
}