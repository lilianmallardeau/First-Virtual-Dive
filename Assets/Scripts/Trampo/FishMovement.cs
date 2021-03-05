using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{

    [SerializeField] Vector3[] positions = new Vector3[4];
    [SerializeField] float time;
    private float totalTime;
    private int target;


    // Start is called before the first frame update
    void Start()
    {
        Movement();
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        target = Mathf.CeilToInt(totalTime % time * positions.Length / time) - 1;
        Debug.Log(target);
        iTween.LookTo(this.gameObject, positions[target], 0.1f);
        

        //transform.Rotate(0, Time.deltaTime * 360 / time, 0);

    }

    void Movement()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("path", positions, "time", time, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop));
    }
}
