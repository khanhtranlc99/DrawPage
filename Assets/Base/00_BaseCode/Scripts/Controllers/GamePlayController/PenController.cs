using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class PenController : MonoBehaviour
{
   

    public Pen currentPen;
    public GameObject erase;
    public Transform postOut;
    public Transform postIn;
    public bool LockMovePen;
    public bool isPenIn;
    public void Init()
    {
        SimplePool2.Preload(erase,100);
     
        LockMovePen = true;
        currentPen.Init();
    }

    public void MoveInOutPen(bool PenIn, float delay = 0, Action callBack = null)
    {
        LockMovePen = true;
        currentPen.transform.DOKill();
        if (PenIn)
        {
            currentPen.transform.DOMove(postIn.transform.position, 1).SetDelay(delay).OnComplete(delegate { 
              
                if(callBack != null)
                {
                    callBack?.Invoke();
                
                }
                LockMovePen = false;
                isPenIn = true;
            }) ;
        } 
        else
        {
            currentPen.HandleMoveInFrontHeadPen();
            currentPen.transform.DOMove(postOut.transform.position, 1).SetDelay(delay).OnComplete(delegate {
                
                if (callBack != null)
                {
                    callBack?.Invoke();
                }
                isPenIn = false;
            });
        }
      
    }
    public void MoveInPen(Vector3 post, float delay = 0, Action callBack = null)
    {
        LockMovePen = true;
        currentPen.transform.DOKill();
       
            currentPen.transform.DOMove(post, 1).SetDelay(delay).OnComplete(delegate {

                if (callBack != null)
                {
                    callBack?.Invoke();

                }
                LockMovePen = false;
                isPenIn = true;
            });
      

    }




}
