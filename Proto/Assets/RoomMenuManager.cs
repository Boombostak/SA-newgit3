using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomMenuManager : MonoBehaviour {

	public NetworkManager nwm;
	public Canvas lobbyCanvas;

	// Use this for initialization
	void Start () {
		nwm = GameObject.Find ("NetworkManager").GetComponent<NetworkManager> ();
		lobbyCanvas = GameObject.Find ("LobbyCanvas").GetComponent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.GetComponent<Button> ().onClick.AddListener (TaskOnClick);
	}

	public void TaskOnClick(){
		nwm.JoinRoom (this.transform.GetChild (1).GetComponent<Text> ().text);
		//((SendMessage((nwm.JoinRoom(this.transform.GetChild(1).GetComponent<Text>().text.ToString())))));
		lobbyCanvas.gameObject.SetActive(false);
	}
}
