using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vehicle : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        Debug.Log("Updating player movement...");
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.LeftArrow),
            Input.GetKey(KeyCode.RightArrow),
            Input.GetKeyDown(KeyCode.UpArrow),
            Input.GetKeyDown(KeyCode.DownArrow),
            Input.GetKeyUp(KeyCode.UpArrow),
            Input.GetKeyUp(KeyCode.DownArrow),
        };

        ClientSend.PlayerMovement(_inputs);
    }
}
