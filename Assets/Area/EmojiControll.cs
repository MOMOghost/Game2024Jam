using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Emoji", menuName = "Block/Emoji")]
public class EmojiControll:ScriptableObject
{
    public List<EmojiCombine> emojiList;
}
[Serializable]
public struct EmojiCombine
{
    public BlockEmoji emoji;
    public Sprite sprite;
}
