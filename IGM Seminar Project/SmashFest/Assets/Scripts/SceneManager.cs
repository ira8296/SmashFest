using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    enum Scenes { Title, Instructions, Menu, Game, GameOver, Win }
    GameObject p1;
    GameObject p2;
    GameObject p3;
    GameObject p4;
    List<GameObject> players;
    List<int> scores;
    Vehicle script;
    Scene active;
    static Scenes current = Scenes.Title;

    public static SceneManager manager;

    // Use this for initialization
    private void Awake()
    {
        GameObject scene = GameObject.Find("SceneManager");
        if (scene != null && scene != this.gameObject)
        {
            Destroy(scene);
        }
        //DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        active = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        //reference = gameObject.GetComponent<GUIManager>();

        if (active.name == "Title")
        {
            current = Scenes.Title;
        }
        else if(active.name == "Instructions")
        {
            current = Scenes.Instructions;
        }
        else if(active.name == "Menu")
        {
            current = Scenes.Menu;
        }
        else if (active.name == "Game")
        {
            current = Scenes.Game;
        }
        else if (active.name == "Game Over")
        {
            current = Scenes.GameOver;
        }
        else if (active.name == "Win")
        {
            current = Scenes.Win;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //reference = gameObject.GetComponent<GUIManager>();
        if (current == Scenes.Title)
        {
            p1 = null;
            p2 = null;
            p3 = null;
            p4 = null;
            //reference.enabled = false;
        }
        else if (current == Scenes.Game)
        {
            //reference.enabled = false;
            p1 = GameObject.Find("Vehicle_Blue");
            p2 = GameObject.Find("Vehicle_Green");
            p3 = GameObject.Find("Vehicle_Red");
            p4 = GameObject.Find("Vehicle_Dark");

            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);

            /*for(int n = 0; n < players.Count; n++)
            {
                if(players[n] != null)
                {
                    Vehicle player = players[n].GetComponent<Vehicle>();
                    int score = player.score;
                    scores.Add(score);

                    if(player.Health <= 0)
                    {
                        players.Remove(players[n]);
                        Destroy(players[n]);
                    }
                }
            }*/

            if(players.Count == 1)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
                current = Scenes.Win;
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        current = Scenes.Game;

    }

    public void Instructions()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Instructions");
        current = Scenes.Instructions;
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        current = Scenes.Menu;
    }

    public void BackToStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        current = Scenes.Title;
    }
}
