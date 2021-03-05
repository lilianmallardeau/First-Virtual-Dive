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
    [SerializeField] private Entertainor entertainor;
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private float _headTorsoDistance = .1f;
    [SerializeField] private float _speedUpMultiplier = 2;

    private List<InputDevice> _devices = new List<InputDevice>();
    private InputDevice _device;
    public static bool _canMove = true;
    private bool _changingLayer = false;

    [SerializeField] private ParticleSystem bubles;
    private bool expire = true;

    private void GetDevices()
    {
        InputDevices.GetDevicesAtXRNode(_xrNode, _devices);
        _device = _devices.FirstOrDefault();
        Debug.LogError($"Controlled device is {_device}");
    }
    
    void Start()
    {
        //GetDevices();
    }

    void Update()
    {
        if (!_device.isValid)
        {
            //GetDevices();
        }
        // Décommenter quand on build
        // if (CheckPanneau.allCheck != 5) _canMove = false;
        else _canMove = true;

        if (expire == true)
        {
            StartCoroutine(RespirationCouroutine());
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
            if (Entertainor.lookAtMe)
            {
                StartCoroutine(ChangeLayerProcedureCoroutine(up));
            }
        }
    }

    private IEnumerator ChangeLayerProcedureCoroutine(bool up)
    {
        _canMove = false;
        _changingLayer = true;

        // Instructor asks if everything is ok
        entertainor.AskOK();

        yield return new WaitForSeconds(.01f);
        do
        {
            yield return null;
        } while (_gestureManager.currentGesture != GestureEventsManager.Gesture.Ok || !Entertainor.lookAtMe);

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
                Vector3 direction = (_leftHand.transform.position + _rightHand.transform.position) / 2 - (bodyToMove.transform.position - Vector3.up*_headTorsoDistance);
                float intensity = direction.magnitude * _speedUpMultiplier;
                bodyToMove.transform.Translate(/*transform.forward*/ direction.normalized * (mvtSpeed * Time.fixedDeltaTime));
            }

            yield return null;
        }
    }

     private IEnumerator RespirationCouroutine()
    {
        expire = false;
        yield return new WaitForSeconds(4.39f);
        bubles.Play();
        expire = true;
    }
}
