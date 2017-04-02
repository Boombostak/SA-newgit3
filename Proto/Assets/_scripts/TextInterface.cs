using UnityEngine;
using System.Collections;
using Photon;

public class TextInterface : UnityEngine.MonoBehaviour {

	public GameObject player;
	public GameObject title;
	public GameObject content;
	public float range = 1;
	public float countdown = 1;
	public float waittime;

	// Use this for initialization
	void Start () {
		/*for (int i = 0; i < GameObject.FindObjectsOfType<PlayerInput>().Length; i++) {
			if (GameObject.FindObjectsOfType<PlayerInput>()[i].GetComponent<PhotonView>().isMine) {
				player = GameObject.FindObjectsOfType<PlayerInput> () [i].gameObject;
			}*/
		}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < FindObjectsOfType<PlayerInput>().Length; i++) {
			if (GameObject.FindObjectsOfType<PlayerInput> ()[i].GetComponent<PhotonView> ().isMine) {
				player = GameObject.FindObjectsOfType<PlayerInput> () [i].gameObject;
			}
		}

		if ((Vector3.Distance(this.gameObject.transform.position,player.transform.position)<range))
			{countdown-=Time.deltaTime;}
		
			else {countdown = waittime;}

			if (countdown<=0) {
			title.SetActive(false);
			content.SetActive(true);
			}
			else {
			title.SetActive(true);
			content.SetActive(false);
			}

	}
}
