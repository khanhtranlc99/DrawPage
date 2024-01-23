using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenFollow : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    public GameObject erase;
    public List<GameObject> eraseList;
    Transform m_tranform;
    bool wasTouch = false;
    Pen currentPen;
    public void Init(Pen pen)
    {
        m_tranform = GetComponent<Transform>();
        wasSpawnDot = true;
        currentPen = pen;
    }



    private struct PointInSpace
    {
        public Vector3 Position;
        public float Time;
    }

    [SerializeField]
    [Tooltip("The transform to follow")]
    public Transform target;

    [SerializeField]
    [Tooltip("The offset between the target and the camera")]
    private Vector3 offset;

    [Tooltip("The delay before the camera starts to follow the target")]
    [SerializeField]
    private float delay = 0.5f;

    [SerializeField]
    [Tooltip("The speed used in the lerp function when the camera follows the target")]
    private float speed = 5;
    public bool wasSpawnDot;
    public Vector3 firstPost;
    public Vector3 secondPost;
    ///<summary>
    /// Contains the positions of the target for the last X seconds
    ///</summary>
    private Queue<PointInSpace> pointsInSpace = new Queue<PointInSpace>();

    private void LateUpdate()
    {
      
        // Add the current target position to the list of positions
        pointsInSpace.Enqueue(new PointInSpace() { Position = target.position, Time = Time.time });

        // Move the camera to the position of the target X seconds ago 
        while (pointsInSpace.Count > 0 && pointsInSpace.Peek().Time <= Time.time - delay + Mathf.Epsilon)
        {
            transform.position = Vector3.Lerp(transform.position, pointsInSpace.Dequeue().Position + offset, Time.deltaTime * speed);

        }
        if(Input.GetMouseButtonDown(0))
        {
            firstPost = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            secondPost = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(secondPost != firstPost)
            {
                wasSpawnDot = true;
                firstPost = secondPost;
            }
            else
            {
                wasSpawnDot = false;
            }

        }
        if (wasSpawnDot)
        {
            var Dot = SimplePool2.Spawn(erase.gameObject, this.transform.position, Quaternion.identity);
            //Dot.transform.parent = this.transform;
            eraseList.Add(Dot);
        }    
  

    }
    public void DisableDot()
    {
        wasSpawnDot = false;
        foreach (var item in eraseList)
        {
            //SimplePool2.Despawn(item.gameObject);
            item.transform.parent = this.transform;
        }
        this.gameObject.SetActive(false);
        foreach (var item in eraseList)
        {
            item.transform.parent = null;
            SimplePool2.Despawn(item.gameObject);
        }
       // this.gameObject.SetActive(true);
    }    
     
}
