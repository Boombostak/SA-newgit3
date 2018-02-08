using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections.Generic;
using Photon;
using ExitGames.Client.Photon.Chat;
using ExitGames.Client.Photon;

public class ChatInput : UnityEngine.MonoBehaviour, IChatClientListener {

	public Canvas chatCanvas;
	public bool chatMode = false;
	public InputField inputField;
	public Text historyText;
	public ScrollRect historyScroll;
	public string temp;

	public List<string> chatHistory = new List<string>();
	public string currentMessage;
	public PhotonView photonView;
	public ChatClient chatClient;
	public GameObject thisPlayer;
	public string playerName;
	public List<GameObject> players;
	public List<PlayerInput> playerInputs;
	public ExitGames.Client.Photon.Chat.AuthenticationValues authValues;

	// Use this for initialization
	void Start () {
		Application.runInBackground = true;
		authValues = new ExitGames.Client.Photon.Chat.AuthenticationValues ();
		photonView = this.GetComponent<PhotonView>();
		chatCanvas = this.GetComponent<Canvas> ();
		chatClient = new ChatClient (this,ExitGames.Client.Photon.ConnectionProtocol.Tcp);
		chatClient.ChatRegion = "US";
		authValues.UserId = System.DateTime.UtcNow.ToString ();
		chatClient.Connect ("11d9264f-b5f0-41e7-b6b3-19b3476bcd0c", "0.1", authValues);

		authValues = new ExitGames.Client.Photon.Chat.AuthenticationValues ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindObjectOfType<PlayerInput> () != null) {
			playerInputs = new List<PlayerInput>(GameObject.FindObjectsOfType<PlayerInput> ());
			players = new List<GameObject> ();
			foreach (var pi in playerInputs) {
				players.Add (pi.gameObject);
			}


			if (authValues.UserId == null) {
				authValues.UserId = playerName;
			}
			if (authValues.AuthType == null) {
				authValues.AuthType = ExitGames.Client.Photon.Chat.CustomAuthenticationType.None;
			}
			if (chatClient.CanChat == false) {
				//Debug.Log ("Can't chat!");
				chatClient.Connect ("11d9264f-b5f0-41e7-b6b3-19b3476bcd0c", "0.1", authValues);
				Debug.Log (chatClient.State);
			}
			if (chatClient.CanChat == true) {
				//Debug.Log ("Can chat");
			}
		}
	
			

		if (Input.GetButtonUp ("Chat")) {
			chatCanvas.enabled = !chatCanvas.enabled;
			chatMode = !chatMode;

		}
		if (chatMode == true) {
			inputField.Select ();
			inputField.ActivateInputField ();
		}
		if (chatMode == false) {
			inputField.text = string.Empty;
		}
		if ((Input.GetKeyUp ("return")) && chatCanvas.enabled && inputField.text!=string.Empty) {
			foreach (var p in players) {
				if (p.GetPhotonView().isMine) {
					thisPlayer = p;
					playerName = thisPlayer.GetComponent<UserID> ().username;
				}
			}
			currentMessage = playerName +": " +inputField.text;
			inputField.text = string.Empty;
			chatHistory.Add (currentMessage);
			chatCanvas.enabled = false;
			chatMode = false;
			chatClient.PublishMessage ("channelA", currentMessage.ToString ());
		}
		if (chatClient!=null) {
			chatClient.Service ();
			//Debug.Log ("ChatClient in service!");
		}
		}
	public void OnDisconnected(){}
	public void OnConnected(){
		Debug.Log ("connected to chat server");
		chatClient.Subscribe (new string[] {"channelA","channelB"});
	}
	public void OnChatStateChange(ChatState state){}

	public void OnPrivateMessage(string sender, object message, string channelName){}
	public void OnSubscribed(string[] channels, bool[] results){
		Debug.Log ("Subscribed to: " + channels);
	}
	public void OnUnsubscribed(string[] channels){}
	public void OnStatusUpdate(string user, int status, bool gotMessage, object message){}
	public void DebugReturn(DebugLevel level, string message){}

	public void CompileMessages(){
		Debug.Log ("compiling chat messages");
		historyText.text = temp;
	}

	public void OnGetMessages(string channelName, string[] senders, object[] messages){
		Debug.Log ("Got a message!");
		//string msgs = "";
		for (int i = 0; i < senders.Length; i++) {
			temp += "\n" + messages [i] /*+ ", "*/;
			CompileMessages ();
		}
	}

	void OnApplicationQuit(){
		this.chatClient.Disconnect ();}
}