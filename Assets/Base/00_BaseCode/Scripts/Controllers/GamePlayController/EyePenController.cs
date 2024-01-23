using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePenController : MonoBehaviour
{
    public EyePen leftPen;
    public EyePen rightPen;
    Vector2 objPos;
    public void Update()
    {
        objPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (objPos.x > 0 && objPos.y > 0) //I
        {
            leftPen.MoveEye(TypePostEye.Type_I);
            rightPen.MoveEye(TypePostEye.Type_I);
        }
        if (objPos.x < 0 && objPos.y > 0)  //II
        {
            leftPen.MoveEye(TypePostEye.Type_II);
            rightPen.MoveEye(TypePostEye.Type_II);
        }
        if (objPos.x < 0 && objPos.y < 0) //III
        {
            leftPen.MoveEye(TypePostEye.Type_III);
            rightPen.MoveEye(TypePostEye.Type_III);
        }
        if (objPos.x > 0 && objPos.y < 0) //IV
        {
            leftPen.MoveEye(TypePostEye.Type_IV);
            rightPen.MoveEye(TypePostEye.Type_IV);
        }
    }

}
[System.Serializable]
public class EyePen
{
    public Transform eye;
    public Transform post_I;
    public Transform post_II;
    public Transform post_III;
    public Transform post_IV;

    public void MoveEye(TypePostEye typePostEye)
    {
       if(typePostEye == TypePostEye.Type_I)
        {
            eye.transform.position = post_I.transform.position;
        }
        if (typePostEye == TypePostEye.Type_II)
        {
            eye.transform.position = post_II.transform.position;
        }
        if (typePostEye == TypePostEye.Type_III)
        {
            eye.transform.position = post_III.transform.position;
        }
        if (typePostEye == TypePostEye.Type_IV)
        {
            eye.transform.position = post_IV.transform.position;
        }
    }

}
public enum TypePostEye
{
    Type_I,
    Type_II,
    Type_III,
    Type_IV
}