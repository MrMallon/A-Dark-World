using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour
{

    public float minSwipeDistY;
    public float speed = 0.08F;
    public float minSwipeDistX;

    private Vector2 startPos;


    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Debug.Log("1");
           // cam.panCamera(1);
        }
        if (Input.GetKeyDown("2"))
        {
            Debug.Log("2");
           // cam.panCamera(2);
        }

        //#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;
                case TouchPhase.Ended:
                    float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

                    if (swipeDistVertical > minSwipeDistY)
                    {
                        float swipeValue = Mathf.Sign(touch.position.y - startPos.y);

                        if (swipeValue > 0)//up swipe
                            Debug.Log("Up");
                        //Jump ();
                        else if (swipeValue < 0)//down swipe
                            Debug.Log("Down");
                        //Shrink ();
                    }

                    float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

                    if (swipeDistHorizontal > minSwipeDistX)
                    {
                        float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

                        if (swipeValue > 0)//right swipe
                        {
                            Debug.Log("Right");
                            //cam.panCamera(1);
                            //MoveRight ();
                        }
                        if (swipeValue < 0)//left swipe
                        {
                            //cam.panCamera(2);
                        }
                        //MoveLeft ();
                    }
                    break;
            }
        }
    }
}