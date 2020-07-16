using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WhaleTurnDemoScript : MonoBehaviour {

    public ParticleSystem Sprayparticals;

    public Slider SpeedSlider;
    public Slider HSlider;
    public Slider VSlider;
    public Toggle SprayTogle;

    private bool Spurt = false;

    Animator animator;
    
    ParticleSystem.EmissionModule emissionModule;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        emissionModule = Sprayparticals.emission;
        SpeedSlider.onValueChanged.AddListener(delegate {SpeedChange(); });
        HSlider.onValueChanged.AddListener(delegate { TurnH(); });
        VSlider.onValueChanged.AddListener(delegate { TurnV(); });
        SprayTogle.onValueChanged.AddListener(delegate { StartSpray(SprayTogle);});
    }
 

    void StartSpray(Toggle change)
    {
        if (SprayTogle.isOn == true) 
        emissionModule.rateOverTime = 80.0f;
        else if (SprayTogle.isOn == false)
        emissionModule.rateOverTime = 0.0f;
    }

    void TurnH()
    {
        animator.SetFloat("Turn_X", HSlider.value);
    }

    void TurnV()
    {
        animator.SetFloat("Turn_Y", VSlider.value);
    }
    public void SpeedChange()
    {
        animator.SetFloat("Speed", SpeedSlider.value);
    }
}
