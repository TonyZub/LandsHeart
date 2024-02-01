using UnityEngine;


namespace LandsHeart
{
	public sealed class HumanModel : InteractiveExtendableObjectModel
	{
		#region Fields

		[SerializeField] private HumanCanvasModel _canvasModel;

        private Human _human;

        #endregion


        #region Properties

        public HumanCanvasModel CanvasModel => _canvasModel;
        public Human Human => _human;

        #endregion


        #region Methods

        public void SetData(Human human)
        {
            _human = human;
        }

        #endregion
    }
}