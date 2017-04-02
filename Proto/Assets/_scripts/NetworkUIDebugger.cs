using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon.Chat;
using Photon;
using UnityEngine.UI;

public class NetworkUIDebugger : Photon.MonoBehaviour {

	public Text chatText;
	public Text punText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		punText.text = "PUN: " + PhotonNetwork.connectionStateDetailed.ToString ();
		chatText.text = "Chat: " + ExitGames.Client.Photon.Chat.ChatState.ConnectedToNameServer.ToString();
	}
}
