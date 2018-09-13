using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static Text text;

    public enum GameState {
        PREGAME,
        INGAME,
        POSTGAME
    }

    public static GameState currentState;

    private static int points;

    public static int Points {
        get {
            return points;
        }
        set {
            if(text == null) {
                text = GameObject.Find("txtPoints").GetComponent<Text>();
            }

            points = value;
            text.text = "Points: " + points.ToString();
        }
    }

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {

	}
}
