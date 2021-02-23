using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image bar;
    [SerializeField] Text value;
    public float healthValue = 100f;
    public Gradient gradient;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) healthValue -= 10f;
        HealthChange(healthValue);   
    }

    void HealthChange(float healthValue)
    {
        float amount = healthValue / 100f;
        value.text = healthValue.ToString() + "%";
        bar.fillAmount = amount;
        bar.color = gradient.Evaluate(amount);
    }
}
