using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// accept WASD key input to move the main camera (or whatever else this might get attached to)
public class cameraMove : MonoBehaviour {

    const float speed = 2.0f;

	void Update () {
        if (Input.GetKey("w")) {
            transform.Translate(0, Time.deltaTime * speed, 0);
        }
        if (Input.GetKey("a")) {
            transform.Translate(Time.deltaTime * (-speed), 0, 0);
        }
        if (Input.GetKey("s")) {
            transform.Translate(0, Time.deltaTime * (-speed), 0);
        }
        if (Input.GetKey("d")) {
            transform.Translate(Time.deltaTime * speed, 0, 0);
        }
    }
}
