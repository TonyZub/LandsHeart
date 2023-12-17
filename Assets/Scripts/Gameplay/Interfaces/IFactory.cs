namespace LandsHeart
{
	public interface IFactory
	{
		public abstract T Create<T>();
	}
}
