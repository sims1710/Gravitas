using UnityEngine;

public class FlowerAnimatorController : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    public void PlayAnimation(string animationName)
    {
        anim.Play(animationName); 
    }
}
