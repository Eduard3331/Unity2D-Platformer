using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioClip hoverAudio;
    public AudioClip clickAudio;
    public void ButtonHighlighted()
    {
        AudioSource.PlayClipAtPoint(hoverAudio, new Vector3(0,0,0));
    }
    public void OnClick()
    {
        AudioSource.PlayClipAtPoint(clickAudio, new Vector3(0, 0, 0));
    }
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
