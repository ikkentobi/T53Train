using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagramLine : MonoBehaviour {
	GameObject rootTrain;

	void Start () {
	}
	
	void Update () {
		if (rootTrain != null) {
			LineRenderer renderer = gameObject.GetComponent<LineRenderer> (); //グラフの描画
			if (UI.GameMode == UI.Diagram) {
				renderer.enabled = true;
				// 頂点の数
				renderer.positionCount = Diagram.TIMELENGTH;
				// 頂点を設定
				for (int i = 0; i < Diagram.TIMELENGTH; i++) {
					var pos2 = new Vector3 (i * Diagram.GRAPHX, rootTrain.GetComponent<Train> ().GetLoc (i) * Diagram.GRAPHY, -2f);
					renderer.SetPosition (i, pos2);
				}
			} else {
				renderer.enabled = false;
			}
		}
	}
	public void SetRootTrain( GameObject train ){
		if (train.tag == "Train") {
			rootTrain = train;
			GetComponent<LineRenderer> ().material.SetColor ("_Color", rootTrain.GetComponent<Train> ().myColor);
			GetComponent<LineRenderer> ().material.EnableKeyword ("_EMISSION");
			GetComponent<LineRenderer> ().material.SetColor ("_EmissionColor", rootTrain.GetComponent<Train> ().myColor);
		}
	}
}
