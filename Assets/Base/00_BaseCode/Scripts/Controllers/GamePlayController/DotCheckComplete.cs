using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotCheckComplete : MonoBehaviour
{
   public  bool isCheck = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Pen")
        { 
            if(!isCheck)
            {
                GamePlayController.Instance.playerContain.levelData.currentLayer.CheckComplete();
                isCheck = true;
            }    
        
        }    
    }
}
