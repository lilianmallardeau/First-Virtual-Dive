using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{

    private Vector3[] positionsCercle = new Vector3[4];
    private Vector3[] positions8 = new Vector3[8];
    private Vector3[] positionsRand = new Vector3[10];
    [SerializeField] float time;
    [SerializeField] float diametre;
    [SerializeField] int navChoice;
    private float totalTime;
    private int target;
    private bool look = true;
    private Vector3 spawnBounds3 = new Vector3(5, 5, 5);




    // Start is called before the first frame update
    void Start()
    {
        positionsCercle[0] = new Vector3(transform.position.x - (diametre / 2), transform.position.y, transform.position.z + (diametre / 2));
        positionsCercle[1] = new Vector3(transform.position.x, transform.position.y, transform.position.z + diametre);
        positionsCercle[2] = new Vector3(transform.position.x + (diametre/2), transform.position.y, transform.position.z + (diametre/2));
        positionsCercle[3] = transform.position;

        positions8[0] = new Vector3(transform.position.x - (diametre / 2), transform.position.y, transform.position.z + (diametre / 2));
        positions8[1] = new Vector3(transform.position.x, transform.position.y, transform.position.z + diametre);
        positions8[2] = new Vector3(transform.position.x + (diametre/2), transform.position.y, transform.position.z + 3 * diametre / 2);
        positions8[3] = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2*diametre);
        positions8[4] = new Vector3(transform.position.x - (diametre / 2), transform.position.y, transform.position.z + 3*(diametre / 2));
        positions8[5] = new Vector3(transform.position.x, transform.position.y, transform.position.z + diametre);
        positions8[6] = new Vector3(transform.position.x + (diametre / 2), transform.position.y, transform.position.z + (diametre / 2));
        positions8[7] = transform.position;

        RandPosition();
        positionsRand[9] = transform.position;

        Movement(navChoice);
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (look)
        {
            StartCoroutine(Look());
        }



    }

    void Movement(int rand)
    {
        if (rand == 1)
            iTween.MoveTo(this.gameObject, iTween.Hash("path", positionsCercle, "time", time, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop));
        if (rand == 2)
            iTween.MoveTo(this.gameObject, iTween.Hash("path", positions8, "time", time, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop));
        if (rand == 3)
            iTween.MoveTo(this.gameObject, iTween.Hash("path", positionsRand, "time", time, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop));
    }

    IEnumerator Look()
    {
        look = false;
        Vector3 pos1 = transform.position;
        yield return null;
        Vector3 pos2 = transform.position;
        transform.forward = (pos2 - pos1).normalized;
        look = true;

    }
    private void RandPosition()
    {
        for (int i = 0; i < positionsRand.Length - 1; i++)
        {
            Vector3 randomVector = UnityEngine.Random.insideUnitSphere;
            randomVector = new Vector3(randomVector.x * spawnBounds3.x, randomVector.y * spawnBounds3.y, randomVector.z * spawnBounds3.z);
            positionsRand[i] = transform.position + randomVector;
        }
    }
}
