using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0,60,0);
    public float timeToFade = 1f;
        private float timeElapsed=0f;
    private Color startColor;
    TextMeshProUGUI textMeshPro;
    RectTransform textTransform;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>(); 
        startColor = textMeshPro.color;
    }
    // Update is called once per frame
    void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;
        timeElapsed+= Time.deltaTime;
        if(timeElapsed<timeToFade)
        {
            textMeshPro.color = new Color  (startColor.r,startColor.g, startColor.b,startColor.a *(1-(timeElapsed/timeToFade)));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
