using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnFinger : MonoBehaviour {

    Vector3 t;
    public GameObject joy;

    public void OnPointerDown()
    {
        joy.SetActive(true);
        joy.transform.position = t;
    }

    public void OnPointerUp()
    {
        joy.SetActive(false);
    }

    private void Update()
    {
        t = Input.mousePosition;
    }
}
