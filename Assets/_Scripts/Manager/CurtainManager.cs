using System;
using UnityEngine;

public class CurtainManager : MonoBehaviour
{
    private Animator animator;
    private Player assignedPlayer;

    public static event Action<bool> OnFinishedCurtainAnimation;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        SceneLoader.OnGameStarting += CrossFade;
        Player.OnInitializePlayerUI += AssignPlayer;
    }

    private void OnDisable()
    {
        SceneLoader.OnGameStarting -= CrossFade;
        Player.OnInitializePlayerUI -= AssignPlayer;
    }

    private void AssignPlayer(Player player)
    {
        assignedPlayer = player;
        assignedPlayer.OnDies += (Vector3) => Timer.Create(StartFadeOut, 3, "FadeOut Timer");//FadeOutTimer; 
    }

    //private void FadeOutTimer(Vector3 playerPosition) => Timer.Create(StartFadeOut, 3, "FadeOut Timer");
    
    private void StartFadeOut() => CrossFade(false);
    
    private void CrossFade(bool fadeIn)
    {
        if (fadeIn)
            animator.Play("Curtain_FadeIn");
        else
            animator.Play("Curtain_FadeOut");
    }

    public void CurtainAnimationDone(int fadeIn)
    {
        if (fadeIn == 1)
            OnFinishedCurtainAnimation?.Invoke(true);
        else if (fadeIn == 0)
            OnFinishedCurtainAnimation?.Invoke(false);
    }
}
