

using UnityEngine;
using System.Collections;

public class MultiPlayerMover : Photon.MonoBehaviour
{

    Vector3 position;
    Quaternion rotation;
    float smoothing = 10f;
    float health = 100f;


    void Start()
    {
        if (photonView.isMine && gameObject.tag =="Player")
        {
            GetComponent<PlayerInput>().enabled = true;
            GetComponent<SmoothMouseLook>().enabled = true;
            GetComponent<CharacterController>().enabled = true;
            foreach (Camera cam in GetComponentsInChildren<Camera>())
                cam.enabled = true;
        }
        else
        {
            StartCoroutine("UpdateData");
        }
    }

    IEnumerator UpdateData()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * smoothing);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smoothing); //Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smoothing);
            yield return null;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(health);
        }
        else
        {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
            health = (float)stream.ReceiveNext();
        }
    }
}