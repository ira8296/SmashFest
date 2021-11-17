using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager instance;

        public GameObject playerPrefab;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else if (instance != this)
            {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
            }
            Debug.Log("Network Manager: " + instance);
        }

        private void Start()
        {

            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 30;

            Server.Start(4, 26950);
        }

        private void Update()
        {
            int x = 10;
        }

        private void OnApplicationQuit()
        {
            Server.Stop();
        }

        public Player InstantiatePlayer()
        {
            return Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        }
    }