using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInterruptBehaviour : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    AudioSource audioSource;

    public float volume = 1f;
    public float pitch = 1f;
    public bool playOnEnter = true, playOnExit = false, playAfterDelay = false;
    public float playDelay = 0.25f;
    private float timeElapsed1 = 0f;
    private bool hasPlayedDelayedSound = false;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        audioSource = animator.GetComponent<AudioSource>();
        audioSource.clip = soundToPlay;
        audioSource.volume = volume;
        audioSource.pitch = pitch;

        audioSource.gameObject.transform.position = animator.gameObject.transform.position;
        if (playOnEnter)
        {
            audioSource.Play();
        }
        timeElapsed1 = 0f;
        hasPlayedDelayedSound = false;
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playAfterDelay && !hasPlayedDelayedSound)
        {
            
            timeElapsed1 += Time.deltaTime;
            if (timeElapsed1 > playDelay)
            {
                audioSource.Play();
                hasPlayedDelayedSound = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
        {
            audioSource.Play();
        }

    }
}
