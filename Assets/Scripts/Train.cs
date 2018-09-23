using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {
	public static int NUMTRAIN = 0; //列車の通し番号
	public int passenger = 0;
	public GameObject DiagramLine;
	public GameObject Point;
	public GameObject TrainFrag;
	public GameObject TrainLine;

	public Color myColor;

	GameObject[] point;
	GameObject objDLine;
	GameObject ObjDiag;
	GameObject objTrainFrag;
	GameObject objTrainLine;

	bool DiagramPointOn;
	bool DiagramLineOn;
	bool DiagramTrainOn;
	int numTrain;
	int speed = 2; //
	int[] loc;	 //フレームごとの場所を示す
	void Awake () {
		loc = new int[Diagram.TIMELENGTH];
	}
	void Start(){
		numTrain = Train.NUMTRAIN;
		Train.NUMTRAIN++;
		ObjDiag = GameObject.Find("Diagram");
	}
	void Update () {
		if (UI.GameMode == UI.Diagram){
			SetDiagramTrain();
		}
		if (UI.GameMode != UI.Diagram){
			SetRailTrain ();
			DestroyDiagramLine ();
			DestroyDiagramPoint ();
		}
	}
	void FixedUpdate(){
		if (UI.GameMode == UI.Run) {	//列車の移動
			var turn = UI.timeG % Diagram.TIMELENGTH;
			var pos = Vector3.zero;
			pos.x = loc [turn] * 2f;
			transform.position = pos;
			var asset = GameObject.Find ("Map").GetComponent<Map> ().Asset [loc [turn]];
			if (asset.tag == "Station") { //降車・乗車の判定へ
				var lastTurn = Mathf.Max( turn - 1, 0 );
				if (loc [turn] != loc [lastTurn]) {
					asset.GetComponent<Station> ().getOff (this);
					passenger = 0;
				}
				var nextTurn = Mathf.Min( turn + 1, Diagram.TIMELENGTH - 1 );
				if (loc [turn] != loc [nextTurn]) {
					passenger = asset.GetComponent<Station> ().getOnPassenger();
				}
			}
		}
	}
	void OnMouseDown() {
		if (!DiagramPointOn) {
			if (!DiagramLineOn) {
				SetDiagramLine ();
			} else {
				SetDiagramPoint ();
			}
		}else{
			DestroyDiagramLine ();
			DestroyDiagramPoint ();
		}
	}
	public int GetLoc( int a ){
		return loc [a];
	}
	public void ChangeLoc( int aLoc,int aLength ){
		loc [aLoc] += aLength;
		loc [aLoc] = Mathf.Min (Mathf.Max (loc [aLoc], 0), Map.LENGTH -1 );  //異常値排除
		var i = aLoc; //Speed以上の幅にならないよう周囲を調整
		while ( i != 0 ){ 
			if (Mathf.Abs(loc [i] - loc [i - 1]) > speed) {
				loc [i - 1] += aLength;
			}
			i--;
		}
		i = aLoc;
		while ( i != Diagram.TIMELENGTH - 1){ 
			if (Mathf.Abs(loc [i] - loc [i + 1]) > speed) {
				loc [i + 1] += aLength;
			}
			i++;
		} 
	}
	public void SetDiagramLine(){
		if (DiagramLineOn) return;
		DiagramLineOn = true;
		objTrainLine = Instantiate (TrainLine);
		objTrainLine.transform.parent = transform;
		objTrainLine.transform.localPosition = Vector3.zero;
		objTrainLine.transform.localScale = new Vector3 (1, 1, 1);
		objTrainLine.GetComponent<SpriteRenderer> ().color = myColor;

		objDLine = Instantiate( DiagramLine ); //DiagramLineオブジェクト初期セット
		objDLine.GetComponent<DiagramLine> ().SetRootTrain (gameObject);
		objDLine.transform.parent = ObjDiag.transform;
		objDLine.transform.localPosition = new Vector3(0,0,-1f);
	}
	public void DestroyDiagramLine(){
		if (!DiagramLineOn) return;
		DiagramLineOn = false;
		Object.Destroy (objTrainLine);
		Object.Destroy (objDLine);
	}
	public void SetDiagramPoint(){
		if (DiagramPointOn) return;

		GameObject[] trains = GameObject.FindGameObjectsWithTag("Train"); //他のTrainのPointをすべて消す
		foreach (GameObject train in trains) {
			train.GetComponent<Train> ().DestroyDiagramPoint ();
		}

		DiagramPointOn = true;
		objTrainFrag = Instantiate (TrainFrag);
		objTrainFrag.transform.parent = transform;
		objTrainFrag.transform.localPosition = Vector3.zero;
		objTrainFrag.transform.localScale = new Vector3 (1, 1, 1);
		objTrainFrag.GetComponent<SpriteRenderer> ().color = myColor;

		point = new GameObject[Diagram.TIMELENGTH]; //pointオブジェクト初期セット
		for (int i = 0; i < Diagram.TIMELENGTH; i++) { 
			point [i] = Instantiate( Point );
			var pos = new Vector3(i * Diagram.GRAPHX , GetLoc(i) * Diagram.GRAPHY, -1f);
			point [i].GetComponent<Point> ().timeLoc = i;
			point [i].GetComponent<Point> ().SetRootTrain (gameObject);
			point [i].transform.parent = ObjDiag.transform;
			point [i].transform.localPosition = pos;
		}
	}
	public void DestroyDiagramPoint(){
		if (!DiagramPointOn) return;
		DiagramPointOn = false;
		Object.Destroy (objTrainFrag);
		for (int i = 0; i < Diagram.TIMELENGTH; i++) { 
			Object.Destroy(point[i]);
		}
	}
	public void SetDiagramTrain(){
		if (DiagramTrainOn) return;
		DiagramTrainOn = true;
		var pos = new Vector3 (Diagram.GRAPHX * Diagram.TIMELENGTH, Diagram.GRAPHY * Map.LENGTH - (0.7f + numTrain*0.5f), -1f); 
		transform.position = pos;
		transform.localScale = new Vector3(0.3f,0.3f,1f);
		GetComponent<BoxCollider2D> ().enabled = true;
	}
	public void SetRailTrain(){
		if (!DiagramTrainOn) return;
		DiagramTrainOn = false;
		transform.position = Vector3.zero;
		transform.localScale = new Vector3(1f,1f,1f);
		GetComponent<BoxCollider2D> ().enabled = false;
	}
}
