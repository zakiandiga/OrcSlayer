using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MenuNavigation
{
    [SerializeField] private string inGameScene;
    [SerializeField] private GameObject curtain;

    private Animator curtainAnimator;

    private void Start()
    {
        curtainAnimator = curtain.GetComponent<Animator>();
        curtainAnimator.Play("Curtain_FadeIn");
    }

    private void OnEnable()
    {
        CurtainManager.OnFinishedCurtainAnimation += CurtainAnimationDone;
    }

    private void OnDisable()
    {
        CurtainManager.OnFinishedCurtainAnimation -= CurtainAnimationDone;
    }

    public void StartGame() => curtainAnimator.Play("Curtain_FadeOut");


    public void CurtainAnimationDone(bool fadeIn)
    {
        if(!fadeIn)
            SceneManager.LoadScene(inGameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
