using System.Text;


namespace LandsHeart
{
	public sealed class LocalizationFieldVertical : LocalizationField
	{
        #region Fields

        private StringBuilder _builder;

        #endregion


        #region Methods

        protected override string GetLocalizedText()
        {
            var localizedString = base.GetLocalizedText();
            var array = localizedString.ToCharArray();
            _builder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                _builder.Append(array[i].ToString() + "\n");
            }

            return _builder.ToString();
        }

        #endregion
    }
}