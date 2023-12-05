using UnityEngine;


namespace LandsHeart
{
#if UNITY_EDITOR
    public sealed class ArrayElementTitleAttribute : PropertyAttribute
    {
        public string Varname;
        public ArrayElementTitleAttribute(string ElementTitleVar)
        {
            Varname = ElementTitleVar;
        }
    }
#endif
}
