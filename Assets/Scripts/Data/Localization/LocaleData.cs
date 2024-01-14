using UnityEngine;
using System;


namespace LandsHeart
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

    [Serializable]
    public struct SerializedLocaleData
    {
        [SerializeField] public string Key;
        [SerializeField] public string Part;
        [SerializeField] public string Info;
        [SerializeField] public string Rus;
        [SerializeField] public string Eng;
        [SerializeField] public string Chi;

        public string[] Array => new string[6]
        {
            Key, Part, Info, Rus, Eng, Chi
        };

        public string[] LocalesArray => new string[3]
        {
            Rus, Eng, Chi,
        };

        public SerializedLocaleData(string key, string part, string info, string rus, string eng, string chi)
        {
            Key = key;
            Part = part;
            Info = info;
            Rus = rus;
            Eng = eng;
            Chi = chi;
        }

        public override string ToString()
        {
            return $"{Key} {Part} {Info} {Rus} {Eng} {Chi}";
        }
    }
}

