using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform[] targets = new Transform[5];
    public GameObject checkpoint;

    public string bossName;
    GameObject boss = null;
    bool isRound = false, isBoss = false;

    float[] distacne;
    bool[] list = { false, false, false, false };
    public Vector3 offset;

    private Camera cam;
    public float minCam = 80f, maxCam = 40f, limitCam = 15f, smoothTime = 0.5f;

    private Vector3 velocity;

    public Vector3 centerPoint;

	// Use this for initialization
	void Start () {

        if (bossName != "") boss = GameObject.Find(bossName); //Temp
        cam = GetComponent<Camera>();
        offset = new Vector3(-4f, 10f, -4f);		
	}
	
	// Update is called once per frame
	void Update () {

        if (checkpoint.GetComponent<Checkpoint>().player[0] == null) return;

        if (!isRound)
        {
            for (int i = 0; i < 4; i++)
            {
                if (targets[i] == null && checkpoint.GetComponent<Checkpoint>().player[i] != null)
                {
                    targets[i] = checkpoint.GetComponent<Checkpoint>().player[i].transform;
                }

                if (i == 3) isRound = true;
            }

            transform.rotation = Quaternion.Euler(60, 45, 0);
        }

        Move();
        Zoom();
        //if (boss != null) isBoss = PosBoss();

    }

    void Move()
    {
        centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxCam, minCam, GetGreatestDistance() / limitCam);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime * 10);
    }

    /*bool PosBoss()
    {
        for (int i = 0; i < checkpoint.GetComponent<Checkpoint>().maxCount; i++)
        {
            if (Vector3.Distance(targets[i].transform.position, boss.transform.position) <= 15f)
            {
                targets[4] = boss.transform;
                return true;
            }
        }

        return false;
    }*/

    Vector3 GetCenterPoint()
    {
        if (targets.Length == 1) return targets[0].position;

        var bounds = new Bounds(targets[0].position, Vector3.zero);

        //if (isBoss && boss.GetComponent<Healthpoint>().hp > 0) bounds.Encapsulate(targets[4].position);
        //else bounds = new Bounds(Vector3.zero, Vector3.zero);

        for (int i = 0; i < checkpoint.GetComponent<Checkpoint>().maxCount; i++)
        {
            if (checkpoint.GetComponent<Checkpoint>().player[i].GetComponent<Healthpoint>().hp > 0 && !list[i])
            {
                bounds.Encapsulate(targets[i].position);
            }
        }

        return bounds.center;
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);

        //if (isBoss && boss.GetComponent<Healthpoint>().hp > 0) bounds.Encapsulate(targets[4].position);
        //else bounds = new Bounds(Vector3.zero, Vector3.zero);

        for (int i = 0; i < checkpoint.GetComponent<Checkpoint>().maxCount; i++)
        {
            if (checkpoint.GetComponent<Checkpoint>().player[i].GetComponent<Healthpoint>().hp > 0 && !list[i])
            {
                bounds.Encapsulate(targets[i].position);
            }
        }

        return bounds.size.x;
    }

    public void Recamera()
    {
        centerPoint = new Bounds(Vector3.zero, Vector3.zero).center;
    }
}
