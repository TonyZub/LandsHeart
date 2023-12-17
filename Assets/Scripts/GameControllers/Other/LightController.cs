using System;
using UnityEngine;

namespace LandsHeart
{
	public sealed class LightController : MonoBehaviour
	{
		#region Private Data

		[Serializable]
		private struct LightData
		{
            [SerializeField] private Color _pointLightColor;
            [SerializeField] private Color _directionalLightColor;
            [SerializeField] private Vector3 _directionalLightRotation;
            [SerializeField] private float _pointLightIntencity;
            [SerializeField] private float _directionalLightIntencity;

			public Color PointLightColor => _pointLightColor;
			public Color DirectionalLightColor => _directionalLightColor;
            public Vector3 DirectionalLightRotation => _directionalLightRotation;
			public float PointLightIntencity => _pointLightIntencity;
			public float DirectionalLightIntencity => _directionalLightIntencity;
        }

        #endregion


        #region Fields

        [SerializeField] private Light _pointLight;
		[SerializeField] private Light _directionalLight;

		[SerializeField] private LightData _dayStartData;
        [SerializeField] private LightData _dayMiddleData;
        [SerializeField] private LightData _dayEndData;

		[Range(0f, 1f)]
		[SerializeField] private float _dayTime;

        #endregion


        #region Properties

        public Light PointLight => _pointLight;
		public Light DirectionalLight => _directionalLight;

		public float DayTime => _dayTime;

        #endregion


        #region UnityMethods

        private void OnValidate()
        {
            UpdateLight();
        }

        #endregion


        #region Methods

        private void UpdateLight()
		{
            float dayTime;
            LightData startData;
            LightData targetData;

            if (_dayTime < 0.5f)
            {
                dayTime = _dayTime / 0.5f;
                startData = _dayStartData;
                targetData = _dayMiddleData;
            }
            else
            {
                dayTime = (_dayTime - 0.5f) / 0.5f;
                startData = _dayMiddleData;
                targetData = _dayEndData;
            }

            PointLight.intensity = Mathf.Lerp(startData.PointLightIntencity,
                targetData.PointLightIntencity, dayTime);
            PointLight.color = Color.Lerp(startData.PointLightColor,
                targetData.PointLightColor, dayTime);
            DirectionalLight.intensity = Mathf.Lerp(startData.DirectionalLightIntencity,
                targetData.DirectionalLightIntencity, dayTime);
            DirectionalLight.color = Color.Lerp(startData.DirectionalLightColor,
                targetData.DirectionalLightColor, dayTime);
            DirectionalLight.transform.eulerAngles = Vector3.Lerp(startData.DirectionalLightRotation,
                targetData.DirectionalLightRotation, dayTime);
        }

        public void SetDayTime(float dayTime)
        {
            _dayTime = Mathf.Clamp(dayTime, 0f, 1f);
            UpdateLight();
        }

		#endregion
	}
}