using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject levelSelectorScreen;
    public GameObject controlsScreen;

    public void StartNew()
    {
        // "navigate" to level selector
        menuScreen.SetActive(false);
        controlsScreen.SetActive(false);
        levelSelectorScreen.SetActive(true);
    }

    public void showControls()
    {
        // "navigate" to controls menu
        menuScreen.SetActive(false);
        levelSelectorScreen.SetActive(false);
        controlsScreen.SetActive(true);
    }

    public void Return()
    {
        // "navigate" to main menu
        menuScreen.SetActive(true);
        levelSelectorScreen.SetActive(false);
        controlsScreen.SetActive(false);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

}