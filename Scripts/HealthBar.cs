using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
public class HealthBar : MonoBehaviour
{
    Damageable damageable;
    public TMP_Text healthBarText;
    public Slider healthSlider;
    private void Awake()
    {
        damageable = GameObject.FindGameObjectWithTag("Player").GetComponent<Damageable>();
       // healthBarText = GetComponentInParent<TMP_Text>();
        //healthSlider = GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {

        healthSlider.value = damageable.Health / damageable.MaxHealth;
        healthBarText.text = "HP: " + damageable.Health + "/" + damageable.MaxHealth;
    }
    private void OnEnable()
    {

        CharacterEvents.characterDamaged += PlayerHurt;
        CharacterEvents.characterHealed += PlayerHeal;
    }
    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= (PlayerHurt);
        CharacterEvents.characterHealed -= (PlayerHeal);
    }
    private void PlayerHurt(GameObject player, int damage)
    {
        if (player.tag == "Player")
        {
            if (damageable != null)
            {
                healthSlider.value = (float)damageable.Health / damageable.MaxHealth;
                healthBarText.text = "HP: " + damageable.Health + "/" + damageable.MaxHealth;
            }
            else Debug.Log("Damageable component is null!");
        }
    }
    private void PlayerHeal(GameObject player, int heal)
    {
        if (player.tag == "Player")
        {
            if (damageable != null)
            {
                healthSlider.value = (float)damageable.Health / damageable.MaxHealth;
                healthBarText.text = "HP: " + damageable.Health + "/" + damageable.MaxHealth;
            }
            else Debug.Log("Damageable component is null!");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
