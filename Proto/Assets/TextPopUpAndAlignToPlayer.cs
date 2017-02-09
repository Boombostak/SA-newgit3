using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextPopUpAndAlignToPlayer : MonoBehaviour {

	public Transform playerTrans;
	public GameObject clientPlayer;
	public float distanceThreshold = 200f;
	public Text text;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < FindObjectsOfType<PlayerInput>().Length; i++) {
			if (GameObject.FindObjectsOfType<PlayerInput> ()[i].GetComponent<PhotonView> ().isMine) {
				clientPlayer = GameObject.FindObjectsOfType<PlayerInput> () [i].gameObject;
			}
		}
		playerTrans = clientPlayer.transform;
			Debug.Log ("playertrans is" + playerTrans.name);
		this.transform.rotation = playerTrans.rotation;
	text = this.GetComponent<Text> ();
	}
	void FixedUpdate(){
		//Debug.Log(Vector3.Distance(playerTrans.position, this.transform.position));
		if (Vector3.Distance(playerTrans.position, this.transform.position)<distanceThreshold) {
			text.enabled = true;
		} else {
			text.enabled = false;
		}
	}
}
