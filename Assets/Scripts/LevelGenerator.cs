using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public int fieldWidth;
    public int fieldHeight;

    public GameObject prefabObstacle;
    public GameObject prefabBridge;

    public GameObject player;

    public int playerPosition;

    public int[] field;

    public double timePassed;
    public double secondsBeforeMove;

    public Vector3 newPosition;

    // Use this for initialization
    void Start () {
        field = new int[fieldWidth * fieldHeight];
        System.Random rnd = new System.Random();
        field[0] = 0;
        GameObject go;
        go = GameObject.CreatePrimitive(PrimitiveType.Cube);

        go.transform.position = new Vector3(0, -1, 0);
        // Assign a random number (0, 1 or 2) to each int in the field. 0 = empty, 1 = blockade, 2 = walkable
        for (int i = 1; i < fieldWidth * fieldHeight; i++) {
            field[i] = rnd.Next(0, 3);
            int widthNr = (i % fieldWidth);
            int heightNr = i / fieldWidth;

            if(field[i] == 1) {
                Instantiate(prefabObstacle, new Vector3(widthNr, 0, heightNr), Quaternion.identity);
            }
            else if(field[i] == 2) {
                Instantiate(prefabBridge, new Vector3(widthNr, -0.25f, heightNr), Quaternion.identity);
            }

            go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = new Vector3(widthNr, -1, heightNr);

            playerPosition = 0;
            timePassed = 0;
        }
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
        if(timePassed >= secondsBeforeMove) {
            CheckMove();
            if(Vector3.Distance(player.transform.position, newPosition) <= 0.05f)
            {
                playerPosition++;
                timePassed = 0;
            }
        }
	}

    private void CheckMove() {
        if(field[playerPosition] == 1) {
            GameOver();
        }
        else {
            newPosition = new Vector3((playerPosition + 1) % fieldWidth, 0, (playerPosition + 1) / fieldWidth);
            Debug.Log(newPosition.ToString());
            player.transform.position = Vector3.MoveTowards(player.transform.position, newPosition, 0.01f);
        }
    }

    private void GameOver() {

    }
}
