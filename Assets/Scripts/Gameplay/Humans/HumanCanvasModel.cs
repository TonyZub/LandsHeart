using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace LandsHeart
{
	public sealed class HumanCanvasModel : MonoBehaviour
	{
		#region Fields


		[SerializeField] private Image _image;

		[SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _status;
        [SerializeField] private TMP_Text _age;
        [SerializeField] private TMP_Text _gender;
        [SerializeField] private TMP_Text _prehistory;
		[SerializeField] private TMP_Text _profession;
        [SerializeField] private TMP_Text _descriptionField;

        [SerializeField] private Transform _featuresParent;
		[SerializeField] private Transform _itemsParent;

        [SerializeField] private Button _extendPrehistoryBtn;
        [SerializeField] private Button _extendFeaturesBtn;
        [SerializeField] private Button _extendItemsBtn;

        private HumanModel _humanModel;

        #endregion


        #region Properties

        public HumanModel HumanModel => _humanModel;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _humanModel = GetComponentInParent<HumanModel>(true);
        }

        #endregion


        #region Methods

        public void SetData(Human human)
        {
            _image.sprite = human.Gender switch
            { 
                HumanGenders.Male => Data.HumanImages.MenSprites[human.ImageIndex],
                HumanGenders.Female => Data.HumanImages.WomenSprites[human.ImageIndex],
                _ => null
            };
            _name.text = $"{human.Name} {human.Surname}";
            _status.text = $"{human.Status.StatusName}";
            _status.color = Data.HumanStatusesColors.GetColorForStatus(human.Status.StatusName);
            _age.text = human.Age.ToString();
            _prehistory.text = human.Prehistory;
            _profession.text = human.Profession.ProfessionName.ToString();
            CreateFeatures();
            CreateItems();
        }

        private void CreateFeatures()
        {
            for (int i = _featuresParent.childCount - 1; i >= 0; i--)
            {
                Destroy(_featuresParent.GetChild(i));
            }
            //TODO
        }

        private void CreateItems()
        {
            for (int i = _itemsParent.childCount - 1; i >= 0; i--)
            {
                Destroy(_featuresParent.GetChild(i));
            }
            //TODO
        }

        #endregion
    }
}