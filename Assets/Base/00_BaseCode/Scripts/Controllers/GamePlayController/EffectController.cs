using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public List<Sprite> lsSprite;
    public EmojiVFX emojiVFX;
    Vector2 objPos;
    public void SpawnEmoji()
    {
        objPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var temp = SimplePool2.Spawn(emojiVFX, objPos, Quaternion.identity);
        var randomEmoji = Random.Range(0, lsSprite.Count);
        temp.Init(lsSprite[randomEmoji]);
    }


}
