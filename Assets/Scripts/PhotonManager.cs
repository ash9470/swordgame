using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;



public class PhotonManager : MonoBehaviourPunCallbacks
{

    public const string Redplayer = "Red";
    public const string Blueplayer = "Blue";


    public TMP_InputField PlayerName;

    public int maxplayer = 2;
    public Button LoginBtn;
    public Button Blue;
    public Button Red;
    public Button PlayBTN;
    public GameObject PlayerNamepanel;
    public GameObject connectingpanel;

    public GameObject Playpanel;
    public GameObject createroompanel;

    public TypedLobby metaLobby = new TypedLobby("Meta", LobbyType.Default);



    // Start is called before the first frame update
    void Start()
    {
        Activatepanel(PlayerNamepanel.name);
        LoginBtn.onClick.AddListener(Onloginclick);
        Blue.onClick.AddListener(Onclickcreateroom);
        Red.onClick.AddListener(Onclickcreateroomred);
        PlayBTN.onClick.AddListener(playbtnclick);
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Onloginclick()
    {
        string name = PlayerName.text;
        if (!string.IsNullOrEmpty(name))
        {
            PhotonNetwork.LocalPlayer.NickName = name;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.JoinLobby();
            Activatepanel(connectingpanel.name);
        }
        else
        {

        }
    }

    public void Onclickcreateroom()
    {

        Debug.Log(PhotonNetwork.IsConnected);
        Debug.Log(PhotonNetwork.InLobby);
        Debug.Log(PhotonNetwork.CountOfRooms);
        Debug.Log(PhotonNetwork.CountOfPlayers);
        Debug.Log(PhotonNetwork.CurrentRoom);

        Debug.Log("22323");

        if (PhotonNetwork.CountOfRooms == 0)
        {
            createroomb();
            Debug.Log("22323");
        }
        else
        {
            PhotonNetwork.JoinRoom("red");
            Debug.Log("22323");
        }




    }


    void createroomb()
    {
        string roomname = "blue";
        if (string.IsNullOrEmpty(roomname))
        {
            roomname = roomname + Random.Range(1000, 9999);
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)maxplayer;

        PhotonNetwork.CreateRoom(roomname, roomOptions);
    }
    void createroomr()
    {
        string roomname = "red";
        if (string.IsNullOrEmpty(roomname))
        {
            roomname = roomname + Random.Range(1000, 9999);
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)maxplayer;
        PhotonNetwork.CreateRoom(roomname, roomOptions);
    }



    public void Onclickcreateroomred()
    {
        Debug.Log(PhotonNetwork.IsConnected);
        Debug.Log(PhotonNetwork.InLobby);
        Debug.Log(PhotonNetwork.CountOfRooms);
        Debug.Log(PhotonNetwork.CountOfPlayers);
        Debug.Log(PhotonNetwork.CurrentRoom);

        Debug.Log("22323");
        if (PhotonNetwork.CountOfRooms == 0)
        {
            createroomr();
           
        }
        else
        {
           
            PhotonNetwork.JoinRoom("blue");
            
        }


    }

    public void OnEventcustom(PlayerHealth data)
    {
        if (data == null)
            return;
        object[] dataReciver = new object[] { };
        switch(data)
        {

        }

    }


    public void Activatepanel(string panelname)
    {

        PlayerNamepanel.SetActive(panelname.Equals(PlayerNamepanel.name));
        connectingpanel.SetActive(panelname.Equals(connectingpanel.name));
        Playpanel.SetActive(panelname.Equals(Playpanel.name));
        createroompanel.SetActive(panelname.Equals(createroompanel.name));

    }


    public override void OnConnected()
    {
        Debug.Log("connected to ineternet");

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "connected to master");
        Activatepanel(createroompanel.name);
        PhotonNetwork.JoinLobby(metaLobby);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + "Room craeted");
      
            Activatepanel(Playpanel.name);
        

    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "joined room");
        Debug.Log(PhotonNetwork.InRoom);

        Activatepanel(Playpanel.name);

        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount + "data");
       


    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PlayBTN.gameObject.SetActive(true);

        }
        else
        {
            PlayBTN.gameObject.SetActive(false);
        }

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }




    public void playbtnclick()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Preview");
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
        Activatepanel(createroompanel.name);
    }
    



}
