using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using ExitGames;
using UnityEngine.EventSystems;

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

	public string roomNameText;
	public InputField roomNameInputField;
	public bool roomNameInputIsFocused;

	public GameObject content;
	public GameObject buttonPrefab;
	public GameObject myButton;
	public GameObject selectedRoomButton;

	public bool playingOffline;
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
        PhotonNetwork.ConnectUsingSettings("may21");
        connectionText = canvas.GetComponentInChildren<Text>();
		playingOffline = false;
    }

	void OnJoinedLobby() //add lobby system here.
    {
		RefreshRooms ();
		RoomOptions ro = new RoomOptions() { isVisible = true, maxPlayers = 10 };
        Debug.Log("joined room");
    }



	public void CreateRoom(){
			roomNameText = roomNameInputField.text;
			Debug.Log ("attempted to create room");
			PhotonNetwork.CreateRoom (roomNameText);
	}

	public void JoinRoom(string name){
		selectedRoomButton = EventSystem.current.currentSelectedGameObject;
		Debug.Log ("attempted to join room");
		PhotonNetwork.JoinRoom (selectedRoomButton.transform.GetChild (1).GetComponent<Text> ().text);
	}

	public void RefreshRooms(){
		if (PhotonNetwork.insideLobby==true) {
			Debug.Log ("you are in the lobby");
		}
		//cull old room buttons
		foreach (Transform child in content.transform) {
			GameObject.Destroy (child.gameObject);
		}

		//populate room list
		Debug.Log ("refreshing room list");
		rooms = new List<string>();
		roomsGOs = new List<GameObject> ();
		foreach (RoomInfo room in PhotonNetwork.GetRoomList()) {
			rooms.Add (room.ToStringFull());
			myButton = Instantiate (buttonPrefab);
			myButton.transform.GetChild (1).GetComponent<Text> ().text = room.name;
			myButton.transform.GetChild (2).GetComponent<Text> ().text = "Players:" +room.playerCount.ToString();
			myButton.transform.parent = content.transform;
		}

		foreach (Transform child in content.transform) {
			//child.GetComponent<Button>().onClick.AddListener()
				//);
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
    }

	public void PlayOffline()
	{
		playingOffline = true;
		PhotonNetwork.LeaveRoom ();
		PhotonNetwork.Disconnect ();
		PhotonNetwork.offlineMode=true;
		PhotonNetwork.CreateRoom("offlineRoom");
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
		if (connectionCountDown>=0 && !playingOffline) {
			connectionCountDown -= Time.deltaTime;	
		}

		connectionText.text = PhotonNetwork.connectionStateDetailed.ToString();

		if (PhotonNetwork.connected == false && connectionCountDown<=0 && !playingOffline) 
		{
			PhotonNetwork.offlineMode = true;
			PlayOffline();
			Debug.Log ("Failed to connect, running offline.");
		}
    }

	public void CheckForDropout()
	{
		if (true) {
			
		}
	}
}