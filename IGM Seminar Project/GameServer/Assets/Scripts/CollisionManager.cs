using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    //int playerCount = 4;
    List<GameObject> playerList;
    int playerCount;
    GameObject original;
    System.Random coord = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < players.Length; i++)
        {
            playerList.Add(players[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerCount = playerList.Count;

        // Determines the visual state of the players on-screen
        for (int i = 0; i < playerCount; i++)
        {
            SpriteRenderer p_Color = playerList[i].GetComponent<SpriteRenderer>();
            p_Color.color = Color.white;
        }

        // Collision Detection
        for (int n = 0; n < playerCount; n++)
        {
            for (int m = 0; m < playerCount; m++)
            {
                if(playerList[n] != null && playerList[m] != null)
                {
                    SpriteRenderer n_Color = playerList[n].GetComponent<SpriteRenderer>();
                    SpriteRenderer m_Color = playerList[m].GetComponent<SpriteRenderer>();
                    bool collision = AABBCollision(playerList[n], playerList[m]);

                    if (collision)
                    {
                        n_Color.color = Color.red;
                        m_Color.color = Color.red;
                    }
                }
            }
        }
    }

    public bool AABBCollision(GameObject playerA, GameObject playerB)
    {
        GameObject A = playerA;
        GameObject B = playerB;
        bool collision = false;
        float AminX = MinX(A);
        float AmaxX = MaxX(A);
        float AminY = MinY(A);
        float AmaxY = MaxY(A);
        float BminX = MinX(B);
        float BmaxX = MaxX(B);
        float BminY = MinY(B);
        float BmaxY = MaxY(B);
        if (BminX < AmaxX && BmaxX > AminX && BmaxY > AminY && BminY < AmaxY)
        {
            collision = true;
        }
        return collision;
    }
    public bool CircleCollision(GameObject playerA, GameObject playerB)
    {
        bool collision = false;
        float distance = Distance(playerA, playerB);
        if (distance < (Radius(playerA) + Radius(playerB)))
        {
            collision = true;
        }
        else
        {
            collision = false;
        }
        return collision;

    }
    public float MinX(GameObject instance)
    {
        Transform point = instance.GetComponent<Transform>();
        float minX = point.position.x - (point.localScale.x / 2);
        return minX;
    }
    public float MaxX(GameObject instance)
    {
        Transform point = instance.GetComponent<Transform>();
        float maxX = point.position.x + (point.localScale.x / 2);
        return maxX;
    }
    public float MinY(GameObject instance)
    {
        Transform point = instance.GetComponent<Transform>();
        float minY = point.position.y - (point.localScale.y / 2);
        return minY;
    }
    public float MaxY(GameObject instance)
    {
        Transform point = instance.GetComponent<Transform>();
        float maxY = point.position.y + (point.localScale.x / 2);
        return maxY;
    }
    float Radius(GameObject circle)
    {
        Transform center = circle.GetComponent<Transform>();
        float radius = Mathf.Sqrt(((center.position.x - MinX(circle)) * (center.position.x - MinX(circle)) + ((center.position.y - center.position.y) * (center.position.y - center.position.y))));
        return radius;
    }
    float Distance(GameObject player, GameObject obstacle)
    {
        Transform circle1 = player.GetComponent<Transform>();
        Transform circle2 = obstacle.GetComponent<Transform>();
        float distance = Mathf.Sqrt(((circle2.position.x - circle1.position.x) * (circle2.position.x - circle1.position.x)) + ((circle2.position.y - circle1.position.y) * ((circle2.position.y - circle1.position.y))));
        return distance;
    }
}
