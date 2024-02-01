using UnityEngine;
using Extensions;


namespace LandsHeart
{
	[CreateAssetMenu(fileName = "HumanStatusesColors", menuName = "Data/Gameplay/HumanStatusesColors")]
	public sealed class HumanStatusesColors : ScriptableObject
	{
        #region Public Data

        public class StatusColor
        {
            [SerializeField] private HumanStatusesNames _statusName;
            [SerializeField] private Color _color;

            public HumanStatusesNames StatusName => _statusName;
            public Color Color => _color;
        }

        #endregion


        #region Fields

        [SerializeField] private StatusColor[] _statusColors;

        #endregion


        #region Properties

        public StatusColor[] StatusColors => _statusColors;

        #endregion


        #region Methods

        public Color GetColorForStatus(HumanStatusesNames statusName)
        {
            return StatusColors.FirstWhich(x => x.StatusName == statusName).Color;
        }

        #endregion
    }
}