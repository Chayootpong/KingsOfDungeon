using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityStandardAssets.CrossPlatformInput;

public class NetworkClientUI : MonoBehaviour {

    static NetworkClient client;
    //NetworkIdentity id;
    public Text address;
    public GameObject UI, vtpy, recon, join, standby, end;
    public static int queue;
    bool isQueue;
    string check = "False";

    public static GameObject vtpystt, ctl;

    public GameObject[] respondUI;
    public static GameObject[] respondUIstt;

    public static bool inGame;

    //SKILL
    public static GameObject warchiefSkill3;

    //SIDEQUEST
    public GameObject[] sideq;
    public GameObject[] resSide;

    //RESURRECTION
    public GameObject heartbeat, deathcanvas;
    int helpless = -1;

    void OnGUI()
    {
        /*string ipaddress = Network.player.ipAddress;
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
        GUI.Label(new Rect(20, Screen.height - 35, 100, 20), queue.ToString());*/

        //if(!client.isConnected) if (GUI.Button(new Rect(10, 10, 60, 50), "Connect")) Connect();
    }

    // Use this for initialization
    void Start() {

        client = new NetworkClient();
        client.RegisterHandler(432, ReceiveServerIDClient); //IDC - ID Client
        client.RegisterHandler(338, ReceiveServerDeath); //DET - Death
        client.RegisterHandler(787, ReceiveServerStatus); //STS - Status
        client.RegisterHandler(784, ReceiveServerStartGame); //STG - Start Game
        client.RegisterHandler(384, ReceiveServerEndGame); //ETG - Start Game
        client.RegisterHandler(777, ReceiveResurrect); //RRS - Receive Resurrect
        client.RegisterHandler(733, ReceiveDisableDeath); //RDD - Receive Resurrect

        queue = -1;
        inGame = isQueue = false;

        vtpystt = vtpy;
        respondUIstt = respondUI;

        ctl = GameObject.Find("Main Canvas");
        warchiefSkill3 = GameObject.Find("Build");

    }

    void Connect()
    {
        //Debug.Log(UIStandby.ip);
        client.Connect(UIStandby.ip, 25000);
        join.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

        if (client.isConnected)
        {
            if (!isQueue)
            {
                isQueue = true;
                StringMessage msg = new StringMessage();
                msg.value = vtpy.GetComponent<VirturePlayer>().playerName;
                client.Send(796, msg);
            }
            else if (recon.activeSelf) recon.SetActive(false);
        }

        else if (!client.isConnected && isQueue)
        {
            if (!isQueue) Connect();
            else
            {
                if (!recon.activeSelf) recon.SetActive(true);
            }
        }
    }

    static public void SendJoystickInfo(float hDelta, float vDelta)
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = hDelta + "|" + vDelta + "|" + queue;
            client.Send(578, msg);
        }
    }

    static public void SendButtonInfo(string name)
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = name + "|" + queue;
            client.Send(286, msg);

            if (name == "Skill 3" && vtpystt.name == "Archer")
            {
                ctl.GetComponent<CanvasGroup>().alpha = 0;
                ctl.GetComponent<CanvasGroup>().blocksRaycasts = false;
                SceneManager.LoadScene("Rifleshot", LoadSceneMode.Additive);
            }

            if (name == "Skill 3" && vtpystt.name == "Berserker")
            {
                ctl.GetComponent<CanvasGroup>().alpha = 0;
                ctl.GetComponent<CanvasGroup>().blocksRaycasts = false;
                SceneManager.LoadScene("Splitting Fall", LoadSceneMode.Additive);
            }
        }
    }

    public static void SendRespond(float adddmg)
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = adddmg + "|" + queue;
            client.Send(737, msg);
        }
    }

    public static void SendUpdateHealth(float hptake)
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = hptake + "|" + queue;
            client.Send(834, msg);
        }
    }

    public static void SendResurrect(int queue, int target)
    {
        if (client.isConnected)
        {
            GameObject hb = GameObject.Find("Heartbeat");
            hb.SetActive(false);
            StringMessage msg = new StringMessage();
            msg.value = queue + "|" + target;
            client.Send(773, msg);
        }
    }

    public void ReceiveServerIDClient(NetworkMessage massage)
    {
        StringMessage msg = new StringMessage();
        msg.value = massage.ReadMessage<StringMessage>().value;
        if (queue == -1) queue = int.Parse(msg.value);
    }

    public void ReceiveServerStatus(NetworkMessage massage)
    {
        StringMessage msg = new StringMessage();
        msg.value = massage.ReadMessage<StringMessage>().value;

        string[] deltas = msg.value.Split('|');

        if (queue == int.Parse(deltas[0]))
        {
            UI.GetComponent<UI>().status = deltas[1];
            UI.GetComponent<UI>().etc = deltas[2];
        }
    }

    public void ReceiveServerDeath(NetworkMessage massage)
    {
        StringMessage msg = new StringMessage();
        msg.value = massage.ReadMessage<StringMessage>().value;

        if (queue == int.Parse(msg.value)) UI.GetComponent<UI>().death = true;
    }

    public void ReceiveServerStartGame(NetworkMessage massage)
    {
        StringMessage msg = new StringMessage();
        msg.value = massage.ReadMessage<StringMessage>().value;

        string[] deltas = msg.value.Split('|');

        if (int.Parse(deltas[4]) == queue) sideq[int.Parse(deltas[5]) + 7].SetActive(true);
        sideq[int.Parse(deltas[queue])].SetActive(true);

        standby.SetActive(false);
        inGame = true;
    }

    public void ReceiveResurrect(NetworkMessage massage)
    {
        StringMessage msg = new StringMessage();
        msg.value = massage.ReadMessage<StringMessage>().value;

        string[] deltas = msg.value.Split('|');

        if (int.Parse(deltas[1]) == queue)
        {
            if (deltas[0] == "Yes")
            {
                heartbeat.SetActive(true);
                helpless = int.Parse(deltas[2]);
            }
            else
            {
                heartbeat.SetActive(false);
                helpless = -1;
            }
        }
    }

    public void ReceiveDisableDeath(NetworkMessage massage)
    {
        StringMessage msg = new StringMessage();
        msg.value = massage.ReadMessage<StringMessage>().value;
        if (queue == int.Parse(msg.value)) UI.GetComponent<UI>().death = false;
    }

    public void ReceiveServerEndGame(NetworkMessage massage) //NOT SHOWING RESULT YET!!!
    {
        StringMessage msg = new StringMessage();
        msg.value = massage.ReadMessage<StringMessage>().value;

        string[] deltas = msg.value.Split('|');

        //MAIN QUEST && SIDE QUEST
        if (deltas[queue] == "Win") PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") + int.Parse(deltas[queue + 4]) + 1000);
        else PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") + int.Parse(deltas[queue + 4]) + 100);

        CalculateLevel();

        if (deltas[queue] == "Win") resSide[0].SetActive(true);
        else resSide[1].SetActive(true);
        end.SetActive(true);
    }

    public void ClickToConnect()
    {
        Connect();
    }

    public static void SceneAdditive(string cmd)
    {
        ctl.GetComponent<CanvasGroup>().alpha = 0;
        ctl.GetComponent<CanvasGroup>().blocksRaycasts = false;

        bool isDuplicate = false;
        if (cmd == "Butch")
        {
            for (int i = 0; i < SceneManager.sceneCount; i++) if (SceneManager.GetSceneAt(i).name == "Butcher") isDuplicate = true;
            if (!isDuplicate) SceneManager.LoadScene("Butcher", LoadSceneMode.Additive);
        }
    }

    public static void SceneSubitive(string cmd)
    {
        ctl.GetComponent<CanvasGroup>().alpha = 1;
        ctl.GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (cmd == "Butch") for (int i = 0; i < SceneManager.sceneCount; i++) if (SceneManager.GetSceneAt(i).name == "Butcher") SceneManager.UnloadSceneAsync("Butcher");
    }

    //CALCULATE LEVEL
    public void CalculateLevel()
    {
        int i = 1, sum = 0;

        while (true)
        {
            if ((i * 1000) + sum >= PlayerPrefs.GetInt("EXP"))
            {               
                PlayerPrefs.SetInt("Level", i);
                break;
            }
            else
            {
                sum += i * 1000;
                i++;
            }
        }
    }

    //HIDE SIDEQUEST
    public void Hiding(GameObject parent)
    {
        parent.SetActive(false);
    }

    //RESURRECTION CLICK
    public void Resurrect()
    {
        if (helpless > -1) SendResurrect(queue, helpless);
    }
}
