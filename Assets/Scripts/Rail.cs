using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour {
	private int _NumTrack = 1;
	public int NumTrack { get { return _NumTrack; } set { _NumTrack = Mathf.Max(value, 0); } }
	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnMouseDown() {
		var ObjCamera = GameObject.Find ("Main Camera");
		ObjCamera.GetComponent<ChangeCamera> ().SetCamera (this.gameObject);
		PlusNumTrack ();
	}
	void PlusNumTrack(){
		NumTrack++;
	}
}
