using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderManager : MonoBehaviour
{
    public Slider HealthSlider;
    public PlayerHealth PlayerHealth;
   

    // Start is called before the first frame update
    void Start()
    {
        HealthSlider.value = PlayerHealth.Health;
        HealthSlider.maxValue = PlayerHealth.MaxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HealthSlider.value = PlayerHealth.Health;
        HealthSlider.maxValue = PlayerHealth.MaxHealth;
    }
}
