using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private List<string> scenesToLoad;
    [SerializeField] private List<string> scenesLoaded;

    private bool gameStarted = false;

    public static event Action<bool> OnGameStarting;

    void Start()
    {
        LoadScenes();
    }

    private void OnEnable()
    {
        CurtainManager.OnFinishedCurtainAnimation += CurtainAnimationFollowup;
    }

    private void OnDisable()
    {
        CurtainManager.OnFinishedCurtainAnimation -= CurtainAnimationFollowup;
    }

    private void CurtainAnimationFollowup(bool fadeIn)
    {
        //if fadeIn = when curtain fully open
    }


    private void LoadScenes()
    {
        for(int i = 0; i<scenesToLoad.Count; i++)
        {
            SceneManager.LoadSceneAsync(scenesToLoad[i], LoadSceneMode.Additive);
            SceneManager.sceneLoaded += SceneLoadedCheck;
        }
    }

    private void SceneLoadedCheck(Scene scene, LoadSceneMode mode)
    {
        if(!scenesLoaded.Contains(scene.name))
        {
            scenesLoaded.Add(scene.name);
            Debug.Log("Scene added: " + scene.name);
        }

        if(scenesLoaded.Count == scenesToLoad.Count && !gameStarted)
        {
            gameStarted = true;
            StartGame();
        }
    }

    private void StartGame()
    {
        OnGameStarting?.Invoke(true);
    }

    private void OnDestroy()
    {
        scenesLoaded.Clear();
    }
}
