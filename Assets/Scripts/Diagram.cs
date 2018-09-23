using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diagram : MonoBehaviour {
	public static readonly int TIMELENGTH = 24; //１日のターン数
	public static readonly float GRAPHX = 0.5f; //X軸の幅 
	public static readonly float GRAPHY = 0.5f; //Y軸の幅 
	public GameObject Cell;
	void Start () {
		for (int i = 0; i < TIMELENGTH - 1; i++) { //ダイアグラムフォーマットの描画
			for (int j = 0; j < Map.LENGTH - 1; j++) { 
				var pos = new Vector3 ( i * GRAPHX + 0.5f * GRAPHX, j * GRAPHY+ 0.5f * GRAPHY, -1f); //格子状にするため中心位置をそれぞれ半分ずらす
				var cell = Instantiate (Cell);
				cell.transform.parent = transform;
				cell.transform.localPosition = pos;
			}
		}
	}
	
	void Update () {
		var pos = transform.position;
		if (UI.GameMode == UI.Diagram) {
			pos.y = 0; //位置変更
		} else {
			if (pos.y == 0) { //Runに戻りたての場合のみ
				CheckDiagram();
			}
			pos.y = 20;
		}
		transform.position = pos;

	}
	void CheckDiagram(){	//交差判定
		Map map = GameObject.Find ("Map").GetComponent<Map> ();
		int[] numCross = new int[Map.LENGTH - 1];
		int tempCount = 0;
		GameObject[] objTrains = GameObject.FindGameObjectsWithTag ("Train");
		for (int i = 0; i < Map.LENGTH - 1; i++) {
			for (int j = 0; j < TIMELENGTH - 1; j++) {
				foreach (GameObject objTrain in objTrains) {
					int tempLoc;
					tempLoc = objTrain.GetComponent<Train> ().GetLoc (j);
					if (tempLoc == i) {
						tempCount++;
					} else {
						if (tempLoc < i && objTrain.GetComponent<Train> ().GetLoc (j + 1) > i) {
							tempCount++;
						}
						if (tempLoc > i && objTrain.GetComponent<Train> ().GetLoc (j + 1) < i) {
							tempCount++;
						}
					}
				}
				numCross [i] = Mathf.Max (numCross [i], tempCount);
				tempCount = 0;
			}
			int NumTrack = 0;
			if (map.Asset [i].tag == "Rail") {
				NumTrack = map.Asset [i].GetComponent<Rail> ().NumTrack;
			}
			if (map.Asset [i].tag == "Station") {
				NumTrack = map.Asset [i].GetComponent<Station> ().NumTrack;
			}
			if (NumTrack < numCross [i]) {
				UI.GameModeChange ();
				return;
			}
		}
	}
}
