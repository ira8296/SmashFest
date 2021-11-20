using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public int health;
    public int score;

    public Vector3 position;
    public Vector3 direction;
    public Vector3 velocity;
    public Vector3 acceleration;

    enum Direction { Left, Right, Forward, Backward }

    public float moveSpeed = 0.1f;
    public float maxSpeed = 0.3f;
    public float thrust = 0.0f;
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;

    private bool[] inputs;

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

    private void Start()
    {
        //moveSpeed *= Time.fixedDeltaTime;

        thrust = 0.0f;
        direction = new Vector3(0, 1);
        velocity = new Vector3(0, 0);
        acceleration = new Vector3(0, 0);
    }

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        health = 100;
        score = 0;

        inputs = new bool[6];
    }

    public void FixedUpdate()
    {
        //Vector2 _inputDirection = Vector2.zero;
        if (inputs[0])
        {
            Turn(Direction.Right);
        }
        if (inputs[1])
        {
            Turn(Direction.Left);
        }
        if (inputs[2])
        {
            Advance(Direction.Forward);
            moveSpeed = 0.1f;
            thrust = 0.1f;
        }
        if (inputs[3])
        {
            Advance(Direction.Backward);
            moveSpeed = -0.1f;
            thrust = -0.1f;
        }
        if (inputs[4])
        {
            thrust = 0.0f;
            velocity = Vector3.zero;
        }
        if (inputs[5])
        {
            thrust = 0.0f;
            velocity = Vector3.zero;
        }

        Move();
    }

    private void Move()
    {
        acceleration = thrust * direction;
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        Debug.DrawLine(position, position + velocity);

        position += velocity;

        WrapAround();

        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
    }

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

    void Advance(Direction d)
    {
        if (d == Direction.Forward)
        {
            thrust += 0.1f;
            moveSpeed = moveSpeed + 0.1f;
        }
        else if (d == Direction.Backward)
        {
            thrust += 0.1f;
            moveSpeed = moveSpeed + 0.1f;
        }

    }

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

    public void SetInput(bool[] _inputs)
    {
        inputs = _inputs;
        float angle = Mathf.Atan2(direction.x, direction.y);
        angle *= Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = position;
    }
}
