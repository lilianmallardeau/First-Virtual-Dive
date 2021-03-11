using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{

    private Vector3[] positions = new Vector3[4];
    [SerializeField] float time;
    [SerializeField] float diametre;
    private float totalTime;
    private int target;


    


    // Start is called before the first frame update
    void Start()
    {
        positions[0] = new Vector3(transform.position.x - (diametre / 2), transform.position.y, transform.position.z + (diametre / 2));
        positions[1] = new Vector3(transform.position.x, transform.position.y, transform.position.z + diametre);
        positions[2] = new Vector3(transform.position.x + (diametre/2), transform.position.y, transform.position.z + (diametre/2));
        positions[3] = transform.position;
        Movement();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //totalTime += Time.deltaTime;
        //target = Mathf.CeilToInt(totalTime % time * positions.Length / time) - 1;
        //Debug.Log(target);
        //iTween.LookTo(this.gameObject, positions[target], 0.1f);
        

        transform.Rotate(0, Time.deltaTime * 360 / time, 0);


    }

    void Movement()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("path", positions, "time", time, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop));
    }
}
