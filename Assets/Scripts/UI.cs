using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {
	//ゲーム内の統一利用変数の定義
	public static byte GameMode = 0;
	public const byte Run = 0; //運転モード
	public const byte Diagram = 1;	//ダイアグラムモード
	public static int timeG;
	//private float timeleft;

	void Start () {
		
	}
	
	void Update () {
		//timeleft -= Time.deltaTime;	//0.5秒ごとに処理を行う
		if (Input.GetKeyDown(KeyCode.Escape)) {
			GameModeChange ();
		}
	}
	void FixedUpdate(){
		timeG++;
	}
	public static void GameModeChange(){
		if (GameMode == Diagram) {
			timeG = 0;
			GameMode = Run;
		} else {
			GameMode = Diagram;
		}			
	}
}
