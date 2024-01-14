using UnityEngine;
using System;


namespace EditorExtensions
{
    [Serializable]
    public struct LocaleData
    {
        [SerializeField] public string Key;
        [SerializeField] public string Info;
        [SerializeField] public string Rus;
        [SerializeField] public string Eng;
        [SerializeField] public string Chi;

        public string[] Array => new string[5]
        {
            Key, Info, Rus, Eng, Chi
        };

        public LocaleData(string key, string info, string rus, string eng, string chi)
        {
            Key = key;
            Info = info;
            Rus = rus;
            Eng = eng;
            Chi = chi;
        }

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
                case 2:
                    locale = Chi;
                    break;
                default:
                    break;
            }
            return locale;
        }

        public override string ToString()
        {
            return $"{Key} {Info} {Rus} {Eng} {Chi}";
        }
    }
}