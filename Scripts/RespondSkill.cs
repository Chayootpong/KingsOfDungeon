using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RespondSkill : MonoBehaviour {

    public float duration, stayDuration;
    float currenttw, tw, elapsedtw, remaintw, ctw;
    bool isStay;

    //ARCHER
    public Image charge;
    public GameObject cam;

	// Use this for initialization
	void Start () {

        isStay = false;
	}

    private void OnEnable()
    {
        currenttw = 0;
        elapsedtw = Time.time;
    }

    // Update is called once per frame
    void Update () {

        tw = Time.time;
        if (currenttw != tw) currenttw = tw - elapsedtw;
        remaintw = duration - currenttw;
        if (remaintw <= 1) Respond(1);

        //CHARGE
        if (isStay)
        {
            ctw += Time.deltaTime;
            charge.fillAmount = Mathf.Lerp(charge.fillAmount, stayDuration - ctw, Time.deltaTime * 10);
            cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cam.GetComponent<Camera>().fieldOfView, 40, Time.deltaTime * 3);
        }
        else
        {
            ctw = 0;
            charge.fillAmount = Mathf.Lerp(charge.fillAmount, stayDuration, Time.deltaTime * 25);
            cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cam.GetComponent<Camera>().fieldOfView, 60, Time.deltaTime * 15);
        }
    }

    public void Respond(float time)
    {
        NetworkClientUI.SendRespond(time);
        NetworkClientUI.ctl.GetComponent<CanvasGroup>().alpha = 1;
        NetworkClientUI.ctl.GetComponent<CanvasGroup>().blocksRaycasts = true;
        SceneManager.UnloadSceneAsync("Rifleshot");
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            isStay = true;
            StartCoroutine("WaitToRespond", 0.75f);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy") isStay = false;
    }

    IEnumerator WaitToRespond(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (isStay) Respond(remaintw);
    }
}
