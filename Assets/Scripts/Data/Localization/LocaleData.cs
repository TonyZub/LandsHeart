using UnityEngine;
using System;


namespace LandsHeart
{
    [Serializable]
    public struct LocaleData
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

        public override string ToString()
        {
            return $"{Key} {Part} {Info} {Rus} {Eng} {Chi}";
        }
    }
}

