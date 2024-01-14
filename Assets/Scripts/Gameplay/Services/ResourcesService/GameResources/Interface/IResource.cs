using System;

namespace LandsHeart
{
	public interface IResource
	{
		public event Action<int, IResource> AmountChanged;
		public ResourcesTypes ResourceType { get; }
		public int MaxAmount { get; }
		public int Amount { get; }
		public abstract void Increase(int amount);
		public abstract void Decrease(int amount);
	}
}
