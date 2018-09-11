using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;
using UnityEngine.UI;

[RequireComponent(typeof(GazeAware))]
public class SphereGaze : MonoBehaviour {

    public float timeBeforeSwitch;

    public bool gainPoints;

    public Color colorBeforeSwitch;
    public Color colorAfterSwitch;

    public Text timeBeforeSwitchText;

    private GazeAware _gazeAware;

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = colorBeforeSwitch;
        _gazeAware = GetComponent<GazeAware>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_gazeAware.HasGazeFocus)
        {
            if (timeBeforeSwitch > 0f)
                timeBeforeSwitch -= Time.deltaTime;
        }

        if(timeBeforeSwitch <= 0)
        {
            timeBeforeSwitch = 0f;
            GetComponent<Renderer>().material.color = colorAfterSwitch;
        }

        timeBeforeSwitchText.text = timeBeforeSwitch.ToString();
	}
}
