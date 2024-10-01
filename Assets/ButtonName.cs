using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonName : MonoBehaviour
{
    [SerializeField] DataTrackerScript trackerScript;
    private void Start()
    {
        trackerScript = GameObject.Find("DataTracker").GetComponent<DataTrackerScript>();
    }
    public void ButtonPress()
    {
        trackerScript.CallGet(gameObject.name);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            trackerScript.CallGet(gameObject.name);
        }
    }

}
