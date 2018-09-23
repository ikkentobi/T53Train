using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour {
	public float trainUsingRate = 0.2f;
	public string title;
	public int population;
	private int _NumTrack = 1;
	public int NumTrack { get { return _NumTrack; } set { _NumTrack = Mathf.Max(value, 0); } }

	void Start () {
		
	}
	
	void Update () {
		
	}
	public int getOnPassenger(){
		return (int) (population * trainUsingRate);
	}
	public void getOff( Train scrTrain ){
		population += (int) (scrTrain.passenger * trainUsingRate);
	}
	void OnMouseDown() {
		var ObjCamera = GameObject.Find ("Main Camera");
		ObjCamera.GetComponent<ChangeCamera> ().SetCamera (this.gameObject);
	}
	void PlusNumTrack(){
		NumTrack++;
	}
}
