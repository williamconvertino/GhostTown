using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameState
{
    public static int currentLevel = 0;
    public static State currentState = State.MainMenu;
    public static int highestLevel = 1;
    public static bool audioStarted = false;

    public enum State
    {
        MainMenu,
        EscapeMenu,
        Level,
        LevelSelect
    }

    public static void StartAudio(AudioClip audioClip)
    {
        if (audioStarted)
        {
            return;
        }
        audioStarted = true;
        AudioSource audioSource = new AudioSource();
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public static void LoadNextLevel()
    {
        CompletedLevel(currentLevel);
        if (currentLevel == 5)
        {
            currentLevel = 0;
            SceneManager.LoadScene("MainMenu");
        }
        LoadLevel(currentLevel + 1);
    }
    
    public static void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level" + level);
        currentLevel = level;
        currentState = State.Level;
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        currentLevel = 0;
        currentState = State.MainMenu;
    }

    public static void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
        currentLevel = 0;
        currentState = State.LevelSelect;
    }

    public static void LoadCurrentLevel()
    {
        SceneManager.LoadScene("Level" + currentLevel);
    }

    public static void LoadRestart()
    {
        SceneManager.LoadScene("Restart");
    }

    public static void CompletedLevel(int level)
    {
        highestLevel = Math.Max(level + 1, highestLevel);
    }

}
