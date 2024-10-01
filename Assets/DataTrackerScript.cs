using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static DataTrackerScript;

public class DataTrackerScript : MonoBehaviour
{
    [SerializeField] Shot shotScript;
    private readonly string baseUrl = "https://simuladores-e51e9-default-rtdb.firebaseio.com/";
    private readonly string Environment = "game";
    public string gameName = "Simulator";
    public string uniqueShotName;
    [Serializable]
    public class Game
    {
        public int timeStarted;
        public int timeEnded;
        public Shoot shots;
    }
    [Serializable]
    public class Shoot
    {
        public float angleX;
        public float angleY;
        public float posX;
        public float force;
        public int time;
    }

    private string BuildURL()
    {
        return baseUrl+ "/" + Environment + "/" + gameName+ ".json";
    }
    private string URLShoot()
    {
        return baseUrl+ "/" + Environment + "/" + gameName+ "/"+ uniqueShotName+".json";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Get(URLShoot());           
        }
    }

    private string URLShootButton(String uniqueKeyy)
    {
        return baseUrl + "/" + Environment + "/" + gameName + "/" + uniqueKeyy + ".json";
    }
    public void CallGet(String uniqueKey1)
    {
        Get(URLShootButton(uniqueKey1));
    }
    public void CallDelete(String uniqueKey1)
    {
        Get(URLShootButton(uniqueKey1));
    }
   
    public void Get(string uniqueKey)
    {
        RestClient.Get(uniqueKey, GetHelper);
    }
    public void Delete(string uniqueKeyDelete)
    {
        RestClient.Delete(uniqueKeyDelete);
    }
    public void GetHelper(RequestException exception, ResponseHelper response)
    {
        Game game = JsonUtility.FromJson<Game>(response.Text);

        Debug.Log(game.shots.force + game.shots.angleX + game.shots.angleY + game.shots.posX);
        shotScript.ShotUniqueKey(game.shots.force,game.shots.angleX,game.shots.angleY, game.shots.posX);


        Debug.Log(response.StatusCode + " data:" + response.Text);

    }
    public void Put(float force, float angleX, float angleZ, float positionX)
    {
        Debug.Log("Put");
        Game game = createGame();

        Shoot shot = createShot(force, angleX, angleZ, positionX);

        game.shots = shot;
        RestClient.Post(BuildURL(), game, PutHelper);

    }

    public Shoot createShot(float force, float angleX, float angleZ, float positionX)
    {
        Shoot shot1 = new Shoot();
        shot1.angleX = angleX;
        shot1.angleY = angleZ;
        shot1.force = force;
        shot1.posX = positionX;
        shot1.time = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return shot1;
    }
    public Game createGame()
    {
        Game game = new Game();
        game.timeEnded = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        game.timeStarted = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return game;
    }
    public void PutHelper(RequestException exception, ResponseHelper response)
    {
        string result = response.Text;
        FirebaseKey key = JsonUtility.FromJson<FirebaseKey>(result);
        uniqueShotName =key.name;
        shotScript.CreateButton(key.name);
        
        Debug.Log("Shot added successfully. Unique key: " + key.name);

    }
    [Serializable]
    public class FirebaseKey
    {
        public string name;  // This will hold the unique key from Firebase
    }
  
}


