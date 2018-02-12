using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using ExitGames;

public class NetworkManager : MonoBehaviour {

    //This script handles player joining and instantiating over network.

    public Canvas canvas;
    public Text connectionText;
    [SerializeField] Camera lobbyCam;
    public GameObject[] spawnpoints;
    public int playerCount;
    public static int numberOfPlayers;
	public float connectionCountDown;
	public string roomInfotest;
	public List<string> rooms = new List<string>();
	public List<GameObject> roomsGOs;
	public GameObject go;

	/*public struct room
	{
		public string name;
		public int maxPlayers;
		public int currentPlayers;
		public float pingPongTime;
	}*/

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.ConnectUsingSettings("0.1");
        connectionText = canvas.GetComponentInChildren<Text>();
    }

	void OnJoinedLobby() //add lobby system here.
    {
		InstantiateLobbyUI ();
		RefreshRooms ();
		CreateRoom ();
		RoomOptions ro = new RoomOptions() { isVisible = true, maxPlayers = 10 };
        //PhotonNetwork.JoinOrCreateRoom("room1", ro, TypedLobby.Default);
        Debug.Log("joined room");
    }

	public void CreateRoom(){
		 
	}

	public void InstantiateLobbyUI(){
		
	}

	public void RefreshRooms(){
		if (PhotonNetwork.insideLobby==true) {
			Debug.Log ("you are in the lobby");
		}
		Debug.Log ("refreshing room list");
		rooms = new List<string>();
		roomsGOs = new List<GameObject> ();
		foreach (RoomInfo room in PhotonNetwork.GetRoomList()) {
			rooms.Add (room.ToStringFull());
		}
			for (int i = 0; i < rooms.Count; i++) {
			go = new GameObject();
			go.name = rooms [i];
			roomsGOs.Add (go);
		}
		foreach (RoomInfo room in PhotonNetwork.GetRoomList()){
			Debug.Log(room.name);
		}
		Debug.Log ("Number fo rooms:"+PhotonNetwork.GetRoomList ().Length);
	}

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join room!");
		PhotonNetwork.CreateRoom ("room1");//, true, true, 10);
    }

    void OnJoinedRoom()
    {
		PhotonNetwork.Instantiate("player", spawnpoints[playerCount].transform.position, spawnpoints[playerCount].transform.rotation, 0);
        lobbyCam.gameObject.SetActive(false);
        playerCount = CountPlayers();
        Debug.Log("playercount:" + playerCount);
    }

    public int CountPlayers()
    {
        numberOfPlayers = PhotonNetwork.FindGameObjectsWithComponent(typeof(PlayerInput)).Count;
        return numberOfPlayers;
    }

    // Update is called once per frame
    void Update()
    {
		if (connectionCountDown>=0) {
			connectionCountDown -= Time.deltaTime;	
		}
		connectionText.text = PhotonNetwork.connectionStateDetailed.ToString();
		if (PhotonNetwork.connected == false && connectionCountDown<=0) {
			PhotonNetwork.offlineMode = true;
			PhotonNetwork.CreateRoom("some name");
			Debug.Log ("Failed to connect, running offline.");
		}
    }
}