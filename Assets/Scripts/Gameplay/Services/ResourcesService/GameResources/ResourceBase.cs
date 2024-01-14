using System;


namespace LandsHeart
{
	public abstract class ResourceBase : IResource
	{
        #region Events

        public event Action<int, IResource> AmountChanged;

        #endregion


        #region Fields

        private int _amount;

        #endregion


        #region Properties

        public virtual ResourcesTypes ResourceType => ResourcesTypes.None;
        public int MaxAmount => Data.StartResourcesData.BaseMaxWood; // TO REFACTOR - add more calculations
        public int Amount => _amount;

        #endregion


        #region Methods

        protected virtual void Preset(int amount)
        {
            _amount = amount;
        }

        public virtual void Increase(int amount)
        {
            if (_amount + amount > MaxAmount)
            {
                amount = MaxAmount - _amount;
            }
            _amount += amount;
            AmountChanged?.Invoke(amount, this);
        }

        public virtual void Decrease(int amount)
        {
            if (_amount - amount < 0)
            {
                amount = _amount;
            }
            _amount -= amount;
            AmountChanged?.Invoke(-amount, this);
        }

        #endregion
    }
}