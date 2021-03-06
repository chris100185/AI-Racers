﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarFront : MonoBehaviour {
    CarControl car;
    GameObject curNode; //the current node. when cur=destination, set destination to next target
    public GameObject destination; //the current destination
    public GameObject nextTarget; //the next Target. calculate inbetween targets. calculate if destination=next
    GameObject goal; //the target location
    enum facing {up,down,left,right};
    facing face;

    //distance between nodes 1.2
	// Use this for initialization
	void Start ()
    {
        car = GetComponent<CarControl>();
        //get current node
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //print(transform.eulerAngles.z); //we can use the eueler angle z value to determine facing, use facing to determine how to navigate car
        print(destination.name);
        //update facing
        updateFacing();
        //navigate towards destination
        //first check if we are alligned with destination, if not steer car car

        //steer
        correction();
        //drive
        car.Forward();
        //car.transform.position += new Vector3(-0.5f, 0, 0) * Time.deltaTime;


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "node")
        {
            destination = nextTarget;
        }
    }


    //used to propel car towards destination when lined up
    void goForward()
    {
        car.Forward();
    }
    //used to turn to face direction
    void turn(int direction)
    {
        car.Turn(direction);
    }
    void updateFacing()
    {
        if(transform.eulerAngles.z>=45&&transform.eulerAngles.z<=135)//we are facing left
        {
            face = facing.left;
        }
        else if (transform.eulerAngles.z >= 135 && transform.eulerAngles.z <= 225)//we are facing down
        {
            face = facing.down;
        }
        else if (transform.eulerAngles.z >= 225 && transform.eulerAngles.z <=315)//we are facing down
        {
            face = facing.right;
        }
        else if (transform.eulerAngles.z >=315 && transform.eulerAngles.z <=45)//we are facing down
        {
            face = facing.up;
        }
    }
    void correction() //check if we are properly lined up with node, if not, correct until we are
    {
        //we are driving horizontal, check allignment on the y axis
        if (face == facing.left)
        {
            if (transform.position.y > (destination.transform.position.y + 0.2f))//we are off to the right by more than the threshold
            {
                car.Turn(1);
            }
            else if (transform.position.y < (destination.transform.position.y - 0.2f))// we are off to the left by more than the threshold
            {
                car.Turn(-1);
            }
        }
        else if(face == facing.right)
        {
            if (transform.position.y > (destination.transform.position.y + 0.2f))//we are off to the right by more than the threshold
            {
                car.Turn(-1);
            }
            else if (transform.position.y < (destination.transform.position.y - 0.2f))// we are off to the left by more than the threshold
            {
                car.Turn(1);
            }
        }
        //we are driving vertical, check allignment on the x axis
        if (face == facing.up)
        {
            if (transform.position.x > (destination.transform.position.x + 0.2f))//we are off to the right by more than the threshold
            {
                car.Turn(1);
            }
            else if (transform.position.x < (destination.transform.position.x - 0.2f))// we are off to the left by more than the threshold
            {
                car.Turn(-1);
            }
        }
        else if (face == facing.down)
        {
            if (transform.position.x > (destination.transform.position.x + 0.2f))//we are off to the right by more than the threshold
            {
                car.Turn(-1);
            }
            else if (transform.position.x < (destination.transform.position.x - 0.2f))// we are off to the left by more than the threshold
            {
                car.Turn(1);
            }
        }
    }
}
