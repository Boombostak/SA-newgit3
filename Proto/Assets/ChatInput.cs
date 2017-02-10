using UnityEngine;
using System.Collections;
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
	public string temp;

	public List<string> chatHistory = new List<string>();
	public string currentMessage;
	public PhotonView photonView;
	public ChatClient chatClient;
	public GameObject thisPlayer;
	public string playerName;
	public GameObject p;

	// Use this for initialization
	void Start () {
		photonView = this.GetComponent<PhotonView>();
		chatCanvas = this.GetComponent<Canvas> ();
		chatClient = new ChatClient (this,ExitGames.Client.Photon.ConnectionProtocol.Udp);
		chatClient.ChatRegion = "US";
		chatClient.Connect ("11d9264f-b5f0-41e7-b6b3-19b3476bcd0c", "0.1", null);
		chatClient.Subscribe (new string[] {"main"});
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindObjectOfType<PlayerInput>()!=null) {
			p = GameObject.FindObjectOfType<PlayerInput> ().gameObject;
			if (p.GetComponent<UserID>().username!=null) {
				thisPlayer = FindObjectOfType<PlayerInput> ().gameObject;
				playerName = thisPlayer.GetComponent<UserID> ().username;
			}

		}

		if (Input.GetButtonUp ("Chat")) {
			chatCanvas.enabled = !chatCanvas.enabled;
			chatMode = !chatMode;
			inputField.Select ();
			inputField.ActivateInputField ();
		}
		if (chatMode == true) {
			inputField.Select();
		}
		if ((Input.GetKeyUp ("return")) && chatCanvas.enabled) {
			currentMessage = playerName +": " +inputField.text;
			chatClient.PublishMessage ("main", currentMessage.ToString ());
			inputField.text = string.Empty;
			chatHistory.Add (currentMessage);
			chatCanvas.enabled = false;
			chatMode = false;
			inputField.Select ();
		}
		chatClient.Service ();
	}
	public void OnDisconnected(){}
	public void OnConnected(){}
	public void OnChatStateChange(ChatState state){}

	public void OnPrivateMessage(string sender, object message, string channelName){}
	public void OnSubscribed(string[] channels, bool[] results){}
	public void OnUnsubscribed(string[] channels){}
	public void OnStatusUpdate(string user, int status, bool gotMessage, object message){}
	public void DebugReturn(DebugLevel level, string message){}

	public void CompileMessages(){
			temp = temp.ToString () + currentMessage.ToString () + "\n";
		historyText.text = temp;
	}

	public void OnGetMessages(string channelName, string[] senders, object[] messages){
		string msgs = "";
		for (int i = 0; i < senders.Length; i++) {
			msgs += senders [i] + "=" + messages [i] + ", ";
			temp = msgs;
			CompileMessages ();
		}
	}
}