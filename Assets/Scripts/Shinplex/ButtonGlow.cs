using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGlow : MonoBehaviour
{
    public Sprite normalImage;
    public Sprite glowImage;

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.CompareTag("YubiYubi")) {
            Debug.LogWarning("AHAH");
            gameObject.GetComponent<Image>().sprite = glowImage;
        }
    }
    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.CompareTag("YubiYubi")) {
            gameObject.GetComponent<Image>().sprite = normalImage;
        }
    }
}
