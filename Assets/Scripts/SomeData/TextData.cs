using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName ="Data/TextData")]
    public class TextData : ScriptableObject
    {
        [Header("ÎÄ±¾ÄÚÈİ")]
        [TextArea(0,60)] public string text;
    }
}