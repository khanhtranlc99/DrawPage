using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public abstract class LayerBase : MonoBehaviour
{
    public LayerType layerType;
   /* [HideInInspector]*/ public bool isComplete;
    public bool isMove;
    [ShowIf("isMove", true)] public Transform postMove;
    public bool isZoom;
    [ShowIf("isZoom", true)] public float zoomValue;
    public virtual void Init()
    {

    }    
    public abstract void CheckComplete();
    public abstract void HandleRedo();
    public virtual void HandleTouchLayer()
    {

    }
    public virtual void HandleChooseColorForColorLayer(Color color32)
    {

    }
    public virtual void HandleOffLayerArena()
    {

    }    

}
 