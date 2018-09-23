using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {
	Vector3 screenPoint;
	Vector3 offset;
	GameObject rootTrain;
	private int _timeLoc; //DiagramのX軸
	public int timeLoc { get { return _timeLoc; } set { _timeLoc = Mathf.Min ( Mathf.Max(value, 0), Diagram.TIMELENGTH ); } }

	void Start () {
	}
	
	void Update () {
		gameObject.GetComponent<CircleCollider2D> ().enabled = (UI.GameMode == UI.Diagram);
		if (rootTrain != null) {
			transform.position = new Vector3 (transform.localPosition.x, rootTrain.GetComponent<Train> ().GetLoc(_timeLoc) * Diagram.GRAPHY, transform.localPosition.z);
		}
	}

	void OnMouseDown() {
		//this.GetComponent<SpriteRenderer> ().enabled = true; ～うまく機能しなかったのでいったんあきらめ
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(screenPoint.x, Input.mousePosition.y, screenPoint.z));
	}
	void OnMouseDrag() {		
		var currentScreenPoint = new Vector3(screenPoint.x, Input.mousePosition.y, screenPoint.z);
		var currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
		if ( currentPosition.y - transform.position.y > Diagram.GRAPHY / 2 ) {　
			rootTrain.GetComponent<Train>().ChangeLoc(_timeLoc, 1);
		}
		if ( currentPosition.y - transform.position.y < -Diagram.GRAPHY / 2 ) {　
			rootTrain.GetComponent<Train>().ChangeLoc(_timeLoc, -1);
		}
	}
	void OnMouseUp(){
		//this.GetComponent<SpriteRenderer> ().enabled = false;～うまく機能しなかったのでいったんあきらめ
	}
	public void SetRootTrain( GameObject train ){
		if (train.tag == "Train") {
			rootTrain = train;
			//gameObject.GetComponent<SpriteRenderer> ().color = rootTrain.GetComponent<Train>().myColor;
		}
	}
}
