using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace LandsHeart
{
	public sealed class HumanModel : InteractiveExtendableObjectModel
	{
		#region Fields

		[SerializeField] private HumanCanvasModel _canvasModel;

        #endregion


        #region Properties

        public HumanCanvasModel CanvasModel => _canvasModel;

        #endregion
    }
}