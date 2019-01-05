using ExitGames;
using UnityEngine;
using System.Collections;
using Photon;

public class MicInput : Photon.MonoBehaviour{


	int lastSample;
	public AudioClip c;
	int FREQUENCY = 8000; //Default 44100
	int length = 256;
	public PhotonView pv;
	public AudioSource audioSource;

	void Start()
	{
		if (pv.isMine)
		{
			c = Microphone.Start(null, true, length, FREQUENCY);
			while (Microphone.GetPosition(null) < 0) { }
		}
	}

	void FixedUpdate()
	{
		if (pv.isMine)
		{

			int pos = Microphone.GetPosition(null);
			int diff = pos - lastSample;
			if (diff > 0)
			{
				float[] samples = new float[diff * c.channels];
				c.GetData(samples, lastSample);
				byte[] ba = ToByteArray(samples);
				if (Input.GetButton ("Jump"))
				{
					ReciveData(ba, c.channels);
					//Rpc_Send(ba, c.channels);
					photonView.RPC("Rpc_Send",PhotonTargets.All,ba,c.channels);
				}
			}
			lastSample = pos;
		}
	}

	[PunRPC]
	public void Rpc_Send(byte[] ba, int chan)
	{
		ReciveData(ba, chan);
	}

	void ReciveData(byte[] ba, int chan)
	{
		//improve efficiency in this section by reducing GC calls
		float[] f = ToFloatArray(ba);
		audioSource.clip = AudioClip.Create("test", f.Length, chan, FREQUENCY, true, false);
		audioSource.clip.SetData(f, 0);
		if (!audioSource.isPlaying) audioSource.Play();
	}

	public byte[] ToByteArray(float[] floatArray)
	{
		int len = floatArray.Length * 4;
		byte[] byteArray = new byte[len];
		int pos = 0;
		foreach (float f in floatArray)
		{
			byte[] data = System.BitConverter.GetBytes(f);
			System.Array.Copy(data, 0, byteArray, pos, 4);
			pos += 4;
		}
		return byteArray;
	}

	public float[] ToFloatArray(byte[] byteArray)
	{
		int len = byteArray.Length / 4;
		float[] floatArray = new float[len];
		for (int i = 0; i < byteArray.Length; i += 4)
		{
			floatArray[i / 4] = System.BitConverter.ToSingle(byteArray, i);
		}
		return floatArray;
	}
}


