using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensitySlider : MonoBehaviour
{

    [SerializeField] private Light light1;
    [SerializeField] private Light light2;
    [SerializeField] private Light light3;
    [SerializeField] private Light light4;

    private bool sliderIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LightSlider());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void RunSlider()
    {
        StartCoroutine(LightSlider());
    }

    IEnumerator LightSlider()
    {
        sliderIsRunning = true;
        float timer = 2f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            light1.intensity = 3 * (2 - timer);
            light2.intensity = 3 * (2 - timer);
            light3.intensity = 3 * (2 - timer);
            light4.intensity = 3 * (2 - timer);
            yield return null;
        }
        light1.intensity = 0;
        light2.intensity = 0;
        light3.intensity = 0;
        light4.intensity = 0;
        sliderIsRunning = false;
    }
}
