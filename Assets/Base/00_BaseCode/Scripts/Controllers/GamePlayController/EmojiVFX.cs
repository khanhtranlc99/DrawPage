using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmojiVFX : MonoBehaviour
{
    public SpriteRenderer icon;
    public float currentPostY;
    public void Init(Sprite param)
    {
        icon.sprite = param;
        currentPostY = icon.transform.position.y;
        this.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.3f).OnComplete(delegate
        {
            icon.transform.DOMoveY(currentPostY + 0.5f, 0.7f).OnComplete(delegate {
                this.transform.localScale = new Vector3(0, 0, 0);
                icon.sprite = null;
                SimplePool2.Despawn(this.gameObject);
            });
        });
     
  
    
    }    
}
