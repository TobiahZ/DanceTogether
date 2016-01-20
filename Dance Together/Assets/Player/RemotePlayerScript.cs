﻿using UnityEngine;
using System.Collections;

public class RemotePlayerScript : MonoBehaviour {
    [SerializeField]
    int distance = 3;
    [SerializeField]
    int movementSpeed = 5;

    int position;
    int numberOfPlayers;

    //GameObject localPlayer;
    Vector3 localPlayerStartingPosition;

    void Start()
    {
        GameObject localPlayer = GameObject.Find("LOCAL Player");
        if (localPlayer)
            SetLocalPlayerStart(localPlayer.GetComponent<LocalPlayerScript>().startingLocation);
    }

    void Update()
    {
        float step = movementSpeed * Time.deltaTime;
        Vector3 goal = localPlayerStartingPosition;
        float degreeMath;
        if (numberOfPlayers > 2) degreeMath = position * (2 * Mathf.PI) / (numberOfPlayers - 1);
        else degreeMath = Mathf.PI; //Cheat if 2 players. If theory this won't happen if only one player, but just in case also prevents divide by 0.
        goal.x += distance * Mathf.Cos(degreeMath);
        goal.y += distance * Mathf.Sin(degreeMath);
        transform.position = Vector3.MoveTowards(transform.position, goal, step);
    }

    public void SetLocalPlayerStart(Vector3 p)
    {
        localPlayerStartingPosition = p;

        transform.position = localPlayerStartingPosition;
    }

    public void SetPosition(int p, int numPlayers)
    {
        position = p;
        numberOfPlayers = numPlayers;
    }
}
