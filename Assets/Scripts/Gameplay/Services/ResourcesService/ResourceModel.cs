using TMPro;
using UnityEngine;


namespace LandsHeart
{
	public sealed class ResourceModel : InteractiveObjectModel
	{
		#region Fields

		[SerializeField] private ResourcesTypes _resourceType;
        [SerializeField] private GameObject _canvas;
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private TMP_Text _amountTextOnHover;

        private GameplayServices _services;

		#endregion


		#region Properties

		public ResourcesTypes ResourceType => _resourceType;
        public GameObject Canvas => _canvas;
        public TMP_Text AmountText => _amountText;
        public TMP_Text AmountTextOnHover => _amountTextOnHover;

        public bool IsCanvasShown { get; private set; }

        #endregion


        #region UnityMethods

        private void Start()
        {
            _services = GlobalContext.Instance.GetDependency<GameplayServices>();
            _services.ResourcesService.ResourceAmountChanged += OnResourceAmountChanged;
            PresetResourceAmounts();
        }

        private void Update()
        {
            if (!IsCanvasShown) return;
            Canvas.transform.LookAt(_services.CameraService.MainCamera.transform);
        }

        private void OnDestroy()
        {
            _services.ResourcesService.ResourceAmountChanged -= OnResourceAmountChanged;
        }

        #endregion


        #region Methods

        protected override void OnMouseEntered()
        {
            base.OnMouseEntered();
            Canvas.gameObject.SetActive(true);
            IsCanvasShown = true;
        }

        protected override void OnMouseExited()
        {
            base.OnMouseExited();
            Canvas.gameObject.SetActive(false);
            IsCanvasShown = false;
        }

        private void PresetResourceAmounts()
        {
            var resource = _services.ResourcesService.GetResource(ResourceType);
            _amountText.text = resource.Amount.ToString();
            _amountTextOnHover.text = $"{resource.Amount} / {resource.MaxAmount}";
        }

        private void OnResourceAmountChanged(int amount, IResource resource)
        {
            if (resource.ResourceType != ResourceType) return;
            _amountText.text = amount.ToString();
            _amountTextOnHover.text = $"{amount} / {_services.ResourcesService.GetResource(ResourceType).MaxAmount}";
        }

        #endregion
    }
}