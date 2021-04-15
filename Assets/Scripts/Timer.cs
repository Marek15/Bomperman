using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField] private Text text;

    private void Start() {
        Debug.Log(PlayerPrefs.GetString("timeFromStart"));
        text.text = PlayerPrefs.GetString("timeFromStart");
        PlayerPrefs.DeleteKey("timeFromStart");
    }
}
