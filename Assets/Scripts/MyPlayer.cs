using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour {
	public GameObject Train;
	void Start () {
		//とりあえず２つをベタ打ちで作成
		var train = Instantiate (Train);
		train.GetComponent<SpriteRenderer> ().color = Color.red;
		train.GetComponent<Train> ().myColor = Color.red;
		var train2 = Instantiate(Train);
		train2.GetComponent<SpriteRenderer> ().color = Color.blue;
		train2.GetComponent<Train> ().myColor = Color.blue;
	}
	
	void Update () {
		
	}
}
