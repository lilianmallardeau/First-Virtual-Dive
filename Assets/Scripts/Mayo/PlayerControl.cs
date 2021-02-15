using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEngine;
using UnityEngine.XR;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private XRNode _xrNode = XRNode.RightHand;
    [SerializeField] private GameObject bodyToMove;
    public float mvtSpeed = 10f;

    private List<InputDevice> devices = new List<InputDevice>();
    private InputDevice device;

    private void GetDevices()
    {
        InputDevices.GetDevicesAtXRNode(_xrNode, devices);
        device = devices.FirstOrDefault();
        Debug.LogError($"Controlled device is {device}");
    }
    
    void Start()
    {
        GetDevices();
    }

    void Update()
    {
        if (!device.isValid)
        {
            GetDevices();
        }
        
        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out var triggerButtonAction) && triggerButtonAction)
        {
            Move();
        }
    }

    private void Move()
    {
        bodyToMove.transform.Translate(transform.forward * (mvtSpeed * Time.deltaTime));
    }
}
