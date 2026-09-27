using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName ="Data/TextData")]
    public class TextData : ScriptableObject
    {
        [Header("文本内容")]
        [TextArea(0,60)] public string text;
    }
}