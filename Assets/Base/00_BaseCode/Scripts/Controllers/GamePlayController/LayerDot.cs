using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class LayerDot : MonoBehaviour
{
    public List<Vector2> lsPostDot;

   [Button]
   public void AddPostDot()
   {
        lsPostDot = new List<Vector2>();
        var temp = this.GetComponent<PolygonCollider2D>();
        foreach(var item in temp.points)
        {
            lsPostDot.Add(item);
        }
   }
}
