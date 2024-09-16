using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class DataTrackerScript : MonoBehaviour
{
    private readonly string baseUrl = "https://uu89-aa8bf-default-rtdb.firebaseio.com/";
    private readonly string Environment = "String";
    public string gameName = "Name";

    [Serializable]
    public class Game
    {
        public int timeStarted;
        public int timeEnded;
        public Shot[] shots;
    }
    [Serializable]
    public class Shot
    {
        public float angleX;
        public float angleY;
        public Vector3 vector;
        public float force;
        public int hits;
        public int time;
    }

    private string BuildURL()
    {
        return baseUrl+ "/" + Environment + "/" + gameName+ ".json";
    }
    public void Get()
    {
        RestClient.Get(BuildURL(), GetHelper);
    }

    public void GetHelper(RequestException exception, ResponseHelper response)
    {
        Game game =  JsonUtility.FromJson<Game>(response.Text);
        Debug.Log(game);

        foreach(Shot shot in game.shots)
        {
            Debug.Log(shot.force);
        }
       Debug.Log(response.StatusCode + " data:" + response.Text);
    }
    public void Put()
    {
        
        Game game = createGame();

        Shot shot = createShot();

        game.shots = new[]
        {
            shot, shot, shot, shot,
        };
        

        RestClient.Put(BuildURL(), game, PutHelper);

    }
    public Shot createShot()
    {
        Shot shot = new Shot();
        shot.angleX = 34;
        shot.angleY = 34;
        shot.hits = 3;
        shot.force = 325;
        shot.time = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        return shot;
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
        Debug.Log(response.StatusCode + " data:" + response.Text);
    }
}


