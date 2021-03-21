using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    
    [SerializeField] private Transform player;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float wallSizeFactor = 8f;

    private Collider col;
    private Vector3 closestPoint;
    private float distFromPlayer;
    private Vector2 cutoutPos;
    private Material[] materials;

    private void Awake() {
        col = gameObject.GetComponent<Collider>();
        materials = transform.GetComponent<Renderer>().materials;
    }

    private void Update() {
        closestPoint = col.ClosestPoint(player.position);
        cutoutPos = mainCamera.WorldToViewportPoint(closestPoint);
        distFromPlayer = (player.position - closestPoint).magnitude;
        float c = Mathf.Clamp(1 - distFromPlayer / wallSizeFactor, 0f, 1f);
        //cutoutPos.y /= (Screen.width / Screen.height);

        for (int m = 0; m < materials.Length; m++)
        {
            materials[m].SetVector("_CutoutPosition", cutoutPos);
            materials[m].SetFloat("_CutoutSize", c*c*c);
            materials[m].SetFloat("_FalloffSize", 0.05f);
        }
    }
}
