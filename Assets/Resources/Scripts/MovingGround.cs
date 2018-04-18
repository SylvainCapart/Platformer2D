
using UnityEngine;
using UnityStandardAssets._2D;

public class MovingGround : MonoBehaviour
{
    private float measurementTime = 0f;
    private Vector2 platMovement = Vector2.zero;
    private Vector3 lastMeasurementPosition = Vector3.zero;
    private bool movePlayer = false;

    void Start()
    {
        lastMeasurementPosition = transform.localPosition;

    }

    void Update()
    {    
            float diffX = (transform.localPosition.x - lastMeasurementPosition.x);
            float diffY = (transform.localPosition.y - lastMeasurementPosition.y);

            platMovement.x = (diffX);
            platMovement.y = (diffY);
            
            movePlayer = true;

            lastMeasurementPosition = transform.localPosition;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.transform.tag == "Player") && movePlayer)
        {
            collision.transform.localPosition += new Vector3( platMovement.x, platMovement.y, 0f);
            movePlayer = false;
        }
    }


}
