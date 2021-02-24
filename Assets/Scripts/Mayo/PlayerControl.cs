using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using TreeEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Object = System.Object;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private XRNode _xrNode = XRNode.RightHand;
    [SerializeField] private GameObject bodyToMove;
    [SerializeField] private Image _fadingScreen;
    public float mvtSpeed = 10f;
    [SerializeField] private float layerChangeTime = 4f;

    private List<InputDevice> _devices = new List<InputDevice>();
    private InputDevice _device;
    private bool _canMove = true;
    private bool _changingLayer = false;

    private void GetDevices()
    {
        InputDevices.GetDevicesAtXRNode(_xrNode, _devices);
        _device = _devices.FirstOrDefault();
        Debug.LogError($"Controlled device is {_device}");
    }
    
    void Start()
    {
        GetDevices();
    }

    void Update()
    {
        if (!_device.isValid)
        {
            GetDevices();
        }
        
        if (_device.TryGetFeatureValue(CommonUsages.primaryButton, out var primaryButtonAction) && primaryButtonAction && !_changingLayer)
        {
            ChangeLayerProcedure(true);
        }
        
        if (_device.TryGetFeatureValue(CommonUsages.triggerButton, out var triggerButtonAction) && triggerButtonAction)
        {
            MoveForward();
        }
    }

    public void MoveForward()
    {
        if (_canMove)
        {
            bodyToMove.transform.Translate(transform.forward * (mvtSpeed * Time.deltaTime));
        }
    }

    public void ChangeLayerProcedure(bool up)
    {
        if (true) // TODO Check here that looking at instructor / instructor is looking
        {
            StartCoroutine(ChangeLayerProcedureCoroutine(up));
        }
    }

    private IEnumerator ChangeLayerProcedureCoroutine(bool up)
    {
        _canMove = false;
        _changingLayer = true;
        Vector3 direction = up ? Vector3.up : Vector3.down;
        
        // TODO make instructor start the procedure
        // Instructor asks if everything is ok
        yield return new WaitForSeconds(.01f);
        do
        {
            yield return null;
        } while (!(_device.TryGetFeatureValue(CommonUsages.secondaryButton, out var secundaryButtonAction) && secundaryButtonAction)); // TODO Get current gesture from gesture manager

        float timer = layerChangeTime;
        while (timer > 0)
        {
            bodyToMove.transform.Translate(direction * (mvtSpeed * Time.deltaTime));
            timer -= Time.deltaTime;
            // TODO fade screen to black
            _fadingScreen.color = new Color(0, 0, 0, 1 - timer / layerChangeTime);
            yield return null;
        }

        _canMove = true;
        _changingLayer = false;
    }
}
