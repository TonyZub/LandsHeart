using UnityEngine;
using System;
using System.Text;


namespace LandsHeart
{
    [Serializable]
    public struct LocalesData
    {
        [SerializeField] public string Meta;
        [SerializeField] public LocaleData[] Data;

        public LocalesData(string meta, LocaleData[] data)
        {
            Meta = meta;
            Data = data;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(Meta);
            foreach (var item in Data)
            {
                str.AppendLine(item.ToString());
            }
            return str.ToString();
        }
    }
}

