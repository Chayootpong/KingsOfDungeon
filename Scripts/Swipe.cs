using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {

    private bool tap, swleft, swright, swup, swdown, isDrag = false;
    private Vector2 startTouch, swDelta;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        tap = swdown = swleft = swright = swup = false;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDrag = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            Reset();
        }

        #endregion

        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDrag = true;
                tap = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDrag = false;
                Reset();
            }
        }
        #endregion

        swDelta = Vector2.zero;
        if (isDrag)
        {
            if (Input.touches.Length > 0)
            {
                swDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        if (swDelta.magnitude > 125)
        {
            float x = swDelta.x;
            float y = swDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    swleft = true;
                    //Debug.Log("LEFT");
                }
                else
                {
                    swright = true;
                    //Debug.Log("RIGHT");
                }
            }
            else
            {
                if (y > 0)
                {
                    swup = true;
                    //Debug.Log("UP");
                }
                else
                {
                    swdown = true;
                    //Debug.Log("DOWN");
                }
            }

            Reset();
        }

    }

    private void Reset()
    {
        startTouch = swDelta = Vector2.zero;
        isDrag = false;
    }

    public Vector2 SWDealt { get { return swDelta; } }
    public bool TAP { get { return tap; } }
    public bool SWLeft { get { return swleft; } }
    public bool SWRight { get { return swright; } }
    public bool SWUp { get { return swup; } }
    public bool SWDown { get { return swdown; } }
}
