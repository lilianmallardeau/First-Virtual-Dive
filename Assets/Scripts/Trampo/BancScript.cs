using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BancScript : MonoBehaviour
{

    [SerializeField] private FishMovement fishPrefab;
    [SerializeField] private int size;
    [SerializeField] private Vector3 spawnBounds;

    private FishMovement[] allFish;

    // Start is called before the first frame update
    void Start()
    {
        GenerateFishs();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateFishs()
    {
        allFish = new FishMovement[size];
        for (int i = 0; i < size; i++)
        {
            var randomVector = UnityEngine.Random.insideUnitSphere;
            randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z);
            var spawnPosition = transform.position + randomVector;
            var rotation = Quaternion.Euler(0, -90, 0);
            allFish[i] = Instantiate(fishPrefab, spawnPosition, rotation);
        }


    }
}
