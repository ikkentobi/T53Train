using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}
	public void SizeSet(Vector3 vector){
		transform.localScale = vector;
		transform.localPosition = new Vector3 (vector.x / 2, vector.y / 2, 0);
	}
}
