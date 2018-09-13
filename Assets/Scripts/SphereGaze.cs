using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;
using UnityEngine.UI;

[RequireComponent(typeof(GazeAware))]
public class SphereGaze : MonoBehaviour {

    private float timeBeforeSwitch;

    public float TimeBeforeSwitch {
        get {
            return timeBeforeSwitch;
        }
        set {
            if(timeBeforeSwitch > 0 && value <=  0) {
                GetComponent<Renderer>().material.color = colorAfterSwitch;
                if (gainPoints) {
                    GameManager.Points += 10;
                }
                else {
                    GameManager.Points -= 10;
                }
            }
            timeBeforeSwitch = value;
        }
    }

    public int initTimeBeforeSwitch;

    public bool gainPoints;

    public Color colorBeforeSwitch;
    public Color colorAfterSwitch;

    public Text timeBeforeSwitchText;

    private GazeAware _gazeAware;

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = colorBeforeSwitch;
        _gazeAware = GetComponent<GazeAware>();
        TimeBeforeSwitch = initTimeBeforeSwitch;
	}
	
	// Update is called once per frame
	void Update () {
		if(_gazeAware.HasGazeFocus)
        {
            if (TimeBeforeSwitch > 0f)
                TimeBeforeSwitch -= Time.deltaTime;
        }

        if(TimeBeforeSwitch <= 0)
        {
            TimeBeforeSwitch = 0f;
        }

        timeBeforeSwitchText.text = timeBeforeSwitch.ToString();
	}
}
