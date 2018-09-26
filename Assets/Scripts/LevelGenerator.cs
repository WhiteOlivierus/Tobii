using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public int fieldWidth;
    public int fieldHeight;

    public GameObject [] prefabObstacle;
    public GameObject prefabBridge;
    public GameObject genericCube;

    public GameObject player;
    public Vector3 [] targets;

    public int playerPosition;

    public int [] field;

    public double timePassed;
    public double secondsBeforeMove;

    public Vector3 newPosition;

    public List<GameObject> allGameObjects;

    // Use this for initialization
    void Start () {
        SetGame ();
    }

    // Update is called once per frame
    void Update () {
        timePassed += Time.deltaTime;
        if (timePassed >= secondsBeforeMove) {
            CheckMove ();
            if (Vector3.Distance (player.transform.position, newPosition) <= 0.05f) {
                playerPosition++;
                timePassed = 0;
            }
        }
    }

    private void CheckMove () {
        if (field [playerPosition + 1] == 0 || field [playerPosition + 1] == 2 || field [playerPosition + 1] == 3) {
            newPosition = targets [playerPosition + 1];
            Debug.Log (newPosition.ToString ());
            player.transform.position = Vector3.MoveTowards (player.transform.position, newPosition, 0.01f);
        } else {
            GameOver ();
        }
    }

    public void SetToEmpty (Vector3 v) {
        //Set integer to 3 -> burnt obstacle
        int indexNr = System.Array.IndexOf (targets, v);
        if (field [indexNr] == 1) {
            field [indexNr] = 3;
        }
        //Set integer to 4 -> burnt bridge
        else if (field [indexNr] == 2) {
            field [indexNr] = 4;
        }
    }

    private void SetGame () {
        if (targets [0] != null) {
            foreach (GameObject g in allGameObjects) {
                Destroy (g);
                allGameObjects.Remove (g);
            }
        }

        field = new int [fieldWidth * fieldHeight];
        targets = new Vector3 [fieldWidth * fieldHeight];
        System.Random rnd = new System.Random ();
        field [0] = 0;
        GameObject go;
        targets [0] = new Vector3 (0, 0, 0);
        go = Instantiate (genericCube, new Vector3 (0, -1f, 0), Quaternion.identity);

        bool direction = false;

        go.transform.position = new Vector3 (0, -1, 0);
        // Assign a random number (0, 1 or 2) to each int in the field. 0 = empty, 1 = blockade, 2 = walkable
        for (int i = 1; i < fieldWidth * fieldHeight; i++) {
            if (i % fieldWidth != 0 && i % fieldWidth != fieldWidth - 1) {
                field [i] = rnd.Next (0, 3);
            } else {
                field [i] = 0;
            }
            int widthNr = 0;
            if (!direction) { widthNr = i % fieldWidth; } else { widthNr = (fieldWidth - 1) - (i % fieldWidth); }
            int heightNr = i / fieldWidth;

            if ((!direction && widthNr == fieldWidth - 1) || (direction && widthNr == 0)) {
                direction = !direction;
            }
            if (field [i] == 0) {
                allGameObjects.Add (Instantiate (genericCube, new Vector3 (widthNr, -1f, heightNr), Quaternion.identity));
            } else if (field [i] == 1) {
                allGameObjects.Add (Instantiate (genericCube, new Vector3 (widthNr, -1f, heightNr), Quaternion.identity));
                allGameObjects.Add (Instantiate (prefabObstacle [Random.Range (0, prefabObstacle.Length)], new Vector3 (widthNr, -0.49f, heightNr), Quaternion.identity));
            } else if (field [i] == 2) {
                allGameObjects.Add (Instantiate (prefabBridge, new Vector3 (widthNr, -0.49f, heightNr), prefabBridge.transform.rotation));
            }

            targets [i] = new Vector3 (widthNr, 0, heightNr);
            Debug.Log (targets [i].ToString ());
        }

        playerPosition = 0;
        timePassed = 0;
        player.transform.position = targets [0];
    }

    private void GameOver () {
        SetGame ();
    }
}
