using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensitySlider : MonoBehaviour
{

    [SerializeField] private GameObject rightHands;
    [SerializeField] private GameObject leftHands;
    [SerializeField] private GestureEventsManager _gestureManager;


    private Color startColor;


    private bool sliderIsRunning = false;
    private bool none = false;

    // Start is called before the first frame update
    void Start()
    {
        startColor = rightHands.GetComponent<Renderer>().material.color;
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
        GestureEventsManager.Gesture previousGesture = _gestureManager.CurrentGesture;
        float timer = 2f;
        while (timer > 0)
        {
            if (_gestureManager.CurrentGesture != previousGesture) break;
            previousGesture = _gestureManager.CurrentGesture;
            timer -= Time.deltaTime;
            rightHands.GetComponent<Renderer>().material.color = new Color(startColor.r, startColor.g + 0.2f * (2 - timer), startColor.b);
            leftHands.GetComponent<Renderer>().material.color = new Color(startColor.r, startColor.g + 0.2f * (2 - timer), startColor.b);
            Debug.Log("green");

            yield return null;
        }
        rightHands.GetComponent<Renderer>().material.color = startColor;
        leftHands.GetComponent<Renderer>().material.color = startColor;
        sliderIsRunning = false;
    }
}
