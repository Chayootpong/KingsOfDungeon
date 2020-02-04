using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    public GameObject target, joystick;
    public GameObject[] elements;
    public int count = 4;
    public bool isReal;

    private void OnDisable()
    {
        if (target.activeSelf) gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        
    }

    public void CountDown()
    {
        if (isReal) target.GetComponent<Destroy>().count--;
        gameObject.SetActive(false);
        if (target.GetComponent<Destroy>().count == 0) DestroyButton();
    }

    public void DestroyButton()
    {
        target.GetComponent<Destroy>().count = 4;
        target.SetActive(false);
        joystick.SetActive(true);
    }

    public void DestroyButton2()
    {
        target.SetActive(false);
        joystick.SetActive(true);
    }
}
