using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public GameObject[] canvas, pointer; //Intro Menu Start Play Setting Quit
    public GameObject block, stbqp;

    public Scrollbar[] setVar;
    public Text[] getVar;

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    void Update() {

        for (int i = 0; i < setVar.Length; i++)
        {
            if (i < 2) getVar[i].text = ((int)(setVar[i].value * 100)).ToString();
            else
            {
                if (setVar[i].value > 0.88f) { getVar[i].text = "Ultra"; SettingConsole.quality = 5; }
                else if (setVar[i].value > 0.63f) { getVar[i].text = "High"; SettingConsole.quality = 4; }
                else if (setVar[i].value > 0.40f) { getVar[i].text = "Medium"; SettingConsole.quality = 3; }
                else if (setVar[i].value > 0.15f) { getVar[i].text = "Low"; SettingConsole.quality = 2; }
                else { getVar[i].text = "Fastest"; SettingConsole.quality = 1; }
            }
        }		
	}

    public void TapToStart()
    {
        StartCoroutine(FadeOut(0));
        StartCoroutine(FadeIn(1));
        StartCoroutine(FadeIn(2));
    }

    public void SelectCanvas(int index)
    {
        for (int i = 2; i < canvas.Length; i++)
        {
            if (i != index)
            {
                StartCoroutine(FadeOut(i));
            }
            else StartCoroutine(FadeIn(i));
        }

        for (int i = 0; i < pointer.Length; i++)
        {
            if (i != index - 3)
            {
                pointer[i].SetActive(false);
            }
            else pointer[i].SetActive(true);
        }
    }

    IEnumerator FadeOut(int i)
    {

        block.SetActive(true);

        while (canvas[i].GetComponent<CanvasGroup>().alpha > 0)
        {
            yield return new WaitForSeconds(0.005f);
            canvas[i].GetComponent<CanvasGroup>().alpha -= 0.075f;
        }

        canvas[i].GetComponent<CanvasGroup>().interactable = false;
        canvas[i].SetActive(false);

        yield return null;
    }

    IEnumerator FadeIn(int i)
    {
        yield return new WaitForSeconds(1.5f);

        canvas[i].SetActive(true);

        while (canvas[i].GetComponent<CanvasGroup>().alpha < 1)
        {
            yield return new WaitForSeconds(0.005f);
            canvas[i].GetComponent<CanvasGroup>().alpha += 0.075f;
        }

        canvas[i].GetComponent<CanvasGroup>().interactable = true;
        block.SetActive(false);

        yield return null;
    }

    public void ConfirmYes()
    {
        Application.Quit();
    }

    public void QuickPlay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
