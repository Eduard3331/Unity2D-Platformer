using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerDeath : MonoBehaviour
{
    Animator animator;
    public TMP_Text text;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!animator.GetBool(AnimationStrings.isAlive))
        {
            text.enabled = true;
        }
        else
        {
            text.enabled = false;
        }
    }
    // Update is called once per frame
    public void Respawn(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SceneManager.LoadScene("SampleScene");

        }
    }

}
