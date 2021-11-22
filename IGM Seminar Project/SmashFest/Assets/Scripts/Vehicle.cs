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
            Input.GetKey(KeyCode.UpArrow),
            Input.GetKey(KeyCode.DownArrow),
            Input.GetKey(KeyCode.Space)
        };

        ClientSend.PlayerMovement(_inputs);
    }
}
