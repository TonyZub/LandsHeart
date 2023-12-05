using UnityEngine;
using System;


namespace EditorExtensions
{
    [Serializable]
    public struct LocaleData
    {
        [SerializeField] public string Key;
        [SerializeField] public string Part;
        [SerializeField] public string Info;
        [SerializeField] public string Rus;
        [SerializeField] public string Eng;

        public string[] Array => new string[5]
        {
                Key, Part, Info, Rus, Eng
        };

        public string GetLocaleByLanguageIndex(int index)
        {
            string locale = "";
            switch (index)
            {
                case 0:
                    locale = Rus;
                    break;
                case 1:
                    locale = Eng;
                    break;
                default:
                    break;
            }
            return locale;
        }

        public override string ToString()
        {
            return $"{Key} {Part} {Info} {Rus} {Eng}";
        }
    }
}