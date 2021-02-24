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
    [SerializeField] private GestureEventsManager _gestureManager;
    [SerializeField] private float layerTransitionSpeed = .5f;

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
    }

    public void MoveForward()
    {
        StartCoroutine(MoveForwardCoroutine());
    }

    public void ChangeLayerProcedure(bool up)
    {
        if (!_changingLayer)
        {
            if (true) // TODO Check here that looking at instructor / instructor is looking
            {
                StartCoroutine(ChangeLayerProcedureCoroutine(up));
            }
        }
    }

    private IEnumerator ChangeLayerProcedureCoroutine(bool up)
    {
        _canMove = false;
        _changingLayer = true;

        // TODO make instructor start the procedure
        // Instructor asks if everything is ok
        yield return new WaitForSeconds(.01f);
        do
        {
            yield return null;
        } while (_gestureManager.currentGesture != GestureEventsManager.Gesture.Ok); // TODO Get current gesture from gesture manager

        float timer = layerChangeTime;
        while (timer > 0)
        {
            bodyToMove.transform.Translate((up ? Vector3.up : Vector3.down) * (layerTransitionSpeed * Time.deltaTime));
            timer -= Time.deltaTime;
            // TODO fade screen to black
            _fadingScreen.color = new Color(0, 0, 0, 1 - timer / layerChangeTime);
            yield return null;
        }

        _fadingScreen.color = new Color(0, 0, 0, 0);
        _canMove = true;
        _changingLayer = false;
    }

    private IEnumerator MoveForwardCoroutine()
    {
        while (_gestureManager.currentGesture == GestureEventsManager.Gesture.GoForward)
        {
            if (_canMove)
            {
                bodyToMove.transform.Translate(transform.forward * (mvtSpeed * Time.fixedDeltaTime));
            }

            yield return null;
        }
    }
}
