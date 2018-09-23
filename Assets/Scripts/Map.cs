using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
	public GameObject Rail;
	public GameObject Station;
	public static readonly int NUMSTATION = 0; //データ読取時の変換用 
	public static readonly int NUMRAIL = 1; //データ読取時の変換用 
	public static int LENGTH = 1; //線路長データ(この後の読み取りで数値確定)
	public GameObject[] Asset; 

	void Awake(){
		var LoadRawData = (Resources.Load ("Map", typeof(TextAsset)) as TextAsset).text;
		string[] LoadData = LoadRawData.Split (char.Parse ("\n"));
		Map.LENGTH = LoadData.Length;
		Asset = new GameObject[LoadData.Length];
		for (int i = 0; i < LoadData.Length; i++) {
			if ( int.Parse(LoadData [i]) == Map.NUMSTATION) {
				Asset [i] = Instantiate (Station);
				Asset [i].transform.parent = this.transform;
			}
			if ( int.Parse(LoadData [i]) == Map.NUMRAIL) {
				Asset [i] = Instantiate (Rail);
				Asset [i].transform.parent = this.transform;
			}
			var pos = new Vector3 (i * 2f, 0f, 0f);
			Asset [i].transform.localPosition = pos;
		}
	}

	void Start () {
	}
	
	void Update () {
	}
}
