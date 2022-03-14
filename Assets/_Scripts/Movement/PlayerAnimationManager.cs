using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private Transform parent;
    private Vector3 tempPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        tempPosition = parent.position;
        tempPosition.x += animator.deltaPosition.x;

        parent.position = tempPosition;
    }

}