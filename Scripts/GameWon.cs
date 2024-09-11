using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameWon : MonoBehaviour
{
    public TMP_Text text;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    public AudioClip winSound;
    // Start is called before the first frame update

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        text.enabled = true;
        AudioSource.PlayClipAtPoint(winSound, gameObject.transform.position);
        Destroy(gameObject);
    }
    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
