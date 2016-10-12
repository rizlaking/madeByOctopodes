﻿using UnityEngine;
using System.Collections;

public class Ship_Movement : MonoBehaviour {

    public float shipForwardSpeed = 1.0f;

    public static float gameSpeed;

    [SerializeField]
    private float shipMovementSpeed = 20.0f;

    [SerializeField]
    private LevelManager levelManager = null;

    private Transform shipTransform;

    private Vector3 shipPosition = new Vector3(0, 0, 0);

    LaneManager.LaneInfo currentLane = new LaneManager.LaneInfo();
    LaneManager.LaneInfo targetLane = new LaneManager.LaneInfo();

    // Use this for initialization
    void Start () {
        currentLane = LaneManager.laneData[4];
        targetLane = currentLane;
        GameInput.OnTap += HandleOnTap;
        GameInput.OnSwipe += HandleOnSwipe;
        shipTransform = gameObject.GetComponent<Transform>();
        shipPosition = shipTransform.position;
        gameSpeed = shipForwardSpeed;
	}

    // Update is called once per frame
    void Update ()
    {
        MoveToLane();
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Obstacle") | (other.tag == "Laser"))
        {
            ResetShip();
            levelManager.ResetLevel();
        }
    }

    void ResetShip()
    {
        shipPosition = new Vector3 (0, 0, 0);
        transform.position = shipPosition;
        targetLane = LaneManager.laneData[4];
        currentLane = targetLane;
    }

    // Get the position of any new tap
    private void HandleOnTap(Vector3 position)
    {
       // Shoot bullet down lane
    }

    // Get the direction of any new swipe
    private void HandleOnSwipe(GameInput.Direction direction)
    {
        switch (direction)
        {
            
            case GameInput.Direction.W:
                //Move player left
                if (((int)currentLane.laneID != 0) & (((int)currentLane.laneID != 3) & ((int)currentLane.laneID != 6)))
                {
                    targetLane = LaneManager.laneData[((int)currentLane.laneID - 1)];
                }
                break;
            case GameInput.Direction.E:
                //Move player Right
                if (((int)currentLane.laneID != 2) & (((int)currentLane.laneID != 5) & ((int)currentLane.laneID != 8)))
                {
                    targetLane = LaneManager.laneData[((int)currentLane.laneID + 1)];
                }
                break;
            case GameInput.Direction.N:
                //Move player Up
                if ((int)currentLane.laneID > 2)
                {
                    targetLane = LaneManager.laneData[((int)currentLane.laneID - 3)];
                }
                break;
            case GameInput.Direction.S:
                //Move player Down
                if ((int)currentLane.laneID < 6)
                {
                    targetLane = LaneManager.laneData[((int)currentLane.laneID + 3)];
                }
                break;
            case GameInput.Direction.NE:
                //Move player up and right
                if (((int)currentLane.laneID < 8) & (((int)currentLane.laneID > 2) & ((int)currentLane.laneID != 5)))
                {
                    targetLane = LaneManager.laneData[((int)currentLane.laneID - 2)];
                }
                break;
            case GameInput.Direction.SE:
                //Move player down and right
                if (((int)currentLane.laneID < 5) & ((int)currentLane.laneID != 2))
                {
                    targetLane = LaneManager.laneData[((int)currentLane.laneID + 4)];
                }
                break;
            case GameInput.Direction.SW:
                //Move player down and left
                if (((int)currentLane.laneID < 6) & (((int)currentLane.laneID > 0) & ((int)currentLane.laneID != 3)))
                {
                    targetLane = LaneManager.laneData[((int)currentLane.laneID + 2)];
                }
                break;
            case GameInput.Direction.NW:
                //Move player up and left
                if (((int)currentLane.laneID > 3) & ((int)currentLane.laneID != 6))
                {
                    targetLane = LaneManager.laneData[((int)currentLane.laneID - 4)];
                }
                break;

        }
        currentLane = LaneManager.laneData[(int)targetLane.laneID];
    }

    private void MoveToLane()
    {
        shipPosition = shipTransform.position;
        shipPosition.z += (shipForwardSpeed * Time.deltaTime);
        if ((shipPosition.x != targetLane.laneX) | (shipPosition.y != targetLane.laneY))
        {
            // Move toward the target position
            shipPosition.x = Mathf.SmoothStep(shipPosition.x, targetLane.laneX, Time.deltaTime * shipMovementSpeed);
            shipPosition.y = Mathf.SmoothStep(shipPosition.y, targetLane.laneY, Time.deltaTime * shipMovementSpeed);
        }
        // Set the updated position
        transform.position = shipPosition;
    } 

}
