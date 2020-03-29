﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

    public EditorPathScript _Path;
    public int    _WaypointID = 0;
    public float  _Speed;
    public float  _RotSpeed   = 5.0f;
    public string _PathName;

    private float _ReachDistance = 1f;
    //private int   _lastDirection = 0;
    private Quaternion _Reverse = new Quaternion(0, 0, 0, 1);

    Vector3 _LastPos;
    Vector3 _CurrPos;

    // Start is called before the first frame update
    void Start() {
        // _Path = GameObject.Find(_PathName).GetComponent<EditorPathScript>();
        _LastPos = transform.position;
    }

    // Update is called once per frame
    public void MoveAlongPath(float joystickMagnitude) {
        int direction = joystickMagnitude > 0 ? 1 : -1;
        Debug.Log(direction);

        // store last direction -> if new direction is different, force change target waypoint.
        // ensure that out of switching when at ends of path is accounted for
        //if (direction != _lastDirection && _lastDirection != 0) {
        //    // if new direction is -1, decrement waypoint, 
        //    // if new direction is +1, increment waypoint
        //    _WaypointID += direction;
        //    ValidateWaypointEnds();
        //}

        //Debug.Log("Waypoint: " + _WaypointID + " - Direction: " + (direction == 1 ? "forward" : "backward"));
        //Debug.Log("Direction: " + direction);

        float dist         = Vector3.Distance(_Path._pathObjs[_WaypointID].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _Path._pathObjs[_WaypointID].position, _Speed * joystickMagnitude * Time.deltaTime);

        // rotate toards position of next waypoint
        var newRotation = Quaternion.LookRotation(_Path._pathObjs[_WaypointID].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _RotSpeed * Time.deltaTime);

        if (dist <= _ReachDistance) {
            // change target waypoint
            if (direction == 1) { 
                _WaypointID++;
            }
            else {
                _WaypointID--;
            }
            // turn on / off light on rails
        }

        ValidateWaypointEnds();

        //_lastDirection = direction;
    }

    private void ValidateWaypointEnds() {
        if (_WaypointID >= _Path._pathObjs.Count) {
            _WaypointID = _Path._pathObjs.Count;
            // activate light at end and allow dropping
        }
        else if(_WaypointID < 0) {
            _WaypointID = 0;
        }
    }
}


