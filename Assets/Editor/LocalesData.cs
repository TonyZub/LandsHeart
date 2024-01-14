using UnityEngine;
using System;
using System.Text;


namespace EditorExtensions
{
    [Serializable]
    public struct LocalesData
    {
        [SerializeField] public string Meta;
        [SerializeField] public LocalesPartData[] Data;

        public LocalesData(string meta, LocalesPartData[] data)
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

    [Serializable]
    public struct LocalesPartData
    {
        [SerializeField] public string Part;
        [SerializeField] public LocaleData[] Locales;

        public LocalesPartData(string part, LocaleData[] locales)
        {
            Part = part;
            Locales = locales;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(Part);
            foreach (var item in Locales)
            {
                str.AppendLine(item.ToString());
            }
            return str.ToString();
        }
    }
}