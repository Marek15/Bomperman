using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField] private Text text;

    private void Start() {
        text.text = PlayerPrefs.GetFloat("timeFromStart").ToString() + " s";
        PlayerPrefs.DeleteKey("timeFromStart");
    }
}
