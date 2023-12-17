using UnityEngine;


namespace LandsHeart
{
	public sealed class TableModel : MonoBehaviour
	{
		#region Fields

		[SerializeField] private GameObject[] _candleFlares;
		[SerializeField] private Light[] _candleLights;
		[SerializeField] private MeshRenderer _mainMapRenderer;
		[SerializeField] private MeshRenderer _wallMapRenderer;

		#endregion


		#region Properties

		public GameObject[] CandleFlares => _candleFlares;
		public Light[] CandleLights => _candleLights;
		public MeshRenderer MainMapRenderer => _mainMapRenderer;
		public MeshRenderer WallMapRenderer => _wallMapRenderer;

		#endregion
	}
}