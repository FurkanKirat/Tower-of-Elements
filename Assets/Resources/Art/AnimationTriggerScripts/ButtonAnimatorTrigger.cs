using UnityEngine;

public class ButtonAnimatorTrigger : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerClickAnim()
    {
        animator.SetTrigger("Click");
    }
}
