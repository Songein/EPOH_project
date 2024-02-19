using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterIntroTransition : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = FindObjectOfType<PlayerController>().GetComponent<Animator>();
    }

    public void AfterIntro()
    {
        _animator.SetTrigger("IntroHit");
    }
}
