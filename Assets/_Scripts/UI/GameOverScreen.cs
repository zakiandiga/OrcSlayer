using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MenuNavigation
{
    [SerializeField] private string startMenuScene;

    public void BackToStart()
    {
        SceneManager.LoadScene(startMenuScene, LoadSceneMode.Single);
    }
}
