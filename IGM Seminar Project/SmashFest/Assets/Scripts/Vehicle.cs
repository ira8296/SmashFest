using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vehicle : MonoBehaviour
{

    public Vector3 position;
    public Vector3 direction;
    public Vector3 velocity;
    public Vector3 acceleration;
    public int health;
    public int score;
    enum Direction { Left, Right, Forward, Backward }

    public float speed = 0.1f;
    public float maxSpeed = 0.3f;

    public float thrust = 0.0f;

    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    Rigidbody2D carRigidbody2D;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    public int Score
    {
        get { return score; }
    }
    public Vector3 Vector
    {
        get { return direction; }
    }
    public Vector3 Local
    {
        get { return position; }
        set { position = value; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        thrust = 0.0f;
        //position = new Vector3(-15, 5, -1);
        direction = new Vector3(0, 1);
        velocity = new Vector3(0, 0);
        acceleration = new Vector3(0, 0);
        health = 100;
        score = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(this.gameObject != null)
        {
            //Logic for accelerating and turning
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Turn(Direction.Right);
            }
            /*if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                thrust = 0.0f;
                velocity = Vector3.zero;
            }*/
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Turn(Direction.Left);
            }
            /*if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                thrust = 0.0f;
                velocity = Vector3.zero;
            }*/
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Advance(Direction.Forward);
                speed = 0.1f;
                thrust = 0.1f;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                thrust = 0.0f;
                velocity = Vector3.zero;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Advance(Direction.Backward);
                speed = -0.1f;
                thrust = -0.1f;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                thrust = 0.0f;
                velocity = Vector3.zero;
            }


            //If health is at or below 0, then the player is eliminated
            if (health <= 0)
            {
                Vehicle self = this.GetComponent<Vehicle>();
                self = null;
            }
        }

        acceleration = thrust * direction;
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        Debug.DrawLine(position, position + velocity);

        position += velocity;

        WrapAround();

        SendInputToServer();

        SetTransform();
    }

    private void SendInputToServer()
    {
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

    //Changes the vehicle's direction
    void Turn(Direction d)
    {
        if (d == Direction.Left)
        {
            direction = Quaternion.Euler(0, 0, -1f) * direction;
        }
        else if (d == Direction.Right)
        {
            direction = Quaternion.Euler(0, 0, 1f) * direction;
        }
    }

    //Adjusts vehicle's speed as it drives in its current direction
    void Advance(Direction d)
    {
        if (d == Direction.Forward)
        {
            thrust += 0.1f;
            speed = speed + 0.1f;
        }
        else if (d == Direction.Backward)
        {
            thrust += 0.1f;
            speed = speed + 0.1f;
        }
    }

    //Ensures that none of the vehicles go off the screen
    void WrapAround()
    {
        if (position.x > 20.03f)
        {
            position.x = -19.0f;
        }
        if (position.x < -19.93f)
        {
            position.x = 20.0f;
        }
        if (position.y > 10.15f)
        {
            position.y = -9.0f;
        }
        if (position.y < -9.83f)
        {
            position.y = 10.0f;
        }
    }

    void SetTransform()
    {
        float angle = Mathf.Atan2(direction.x, direction.y);
        angle *= Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = position;
    }
}
