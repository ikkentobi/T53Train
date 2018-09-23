using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SetCamera( GameObject Object ){
		var pos = Object.transform.position;
		pos.z = -10;
		this.transform.position = pos;
	}
}
