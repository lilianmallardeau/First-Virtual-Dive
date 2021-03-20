using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.2f;
    [SerializeField] private float frequency = 3f;


    void Start()
    {
        StartCoroutine(idling());
    }

    void Update()
    {

    }

    public IEnumerator idling()
    {
        float t = 0f;
        float time = 0f;
        while (true){
            time = 0f;
            for (t = 0f ; t<= 0.99f ; time+=Time.deltaTime) {
                t = time / frequency;
                
                t = t * t * (3f - 2f * t);
                transform.position = Vector3.Lerp(transform.parent.position, transform.parent.position + Vector3.up * amplitude, t);
                yield return null;
                
            }
            time = 0f;
            for (t = 0f ; t<= 0.99f ; time+=Time.deltaTime) {
                t = time / frequency;
                t = t * t * (3f - 2f * t);
                transform.position = Vector3.Lerp(transform.parent.position + Vector3.up * amplitude, transform.parent.position, t);
                yield return null;
            }
            yield return null;
        }
        
    }
}
