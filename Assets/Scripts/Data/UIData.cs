using UnityEngine;


namespace LandsHeart
{
	[CreateAssetMenu(fileName = "UIData", menuName = "Data/UIData")]
	public sealed class UIData : ScriptableObject
	{
		#region Fields

		[SerializeField] private SceneLoadingCanvasModel _sceneLoadingCanvasModel;
		[SerializeField] private FeatureCanvasModel _featureCanvasPrefab;
		[SerializeField] private ItemCanvasModel _itemCanvasPrefab;

		#endregion


		#region Properties

		public SceneLoadingCanvasModel SceneLoadingCanvasModel => _sceneLoadingCanvasModel;
		public FeatureCanvasModel FeatureCanvasPrefab => _featureCanvasPrefab;
		public ItemCanvasModel ItemCanvasModel => _itemCanvasPrefab;

		#endregion
	}
}
