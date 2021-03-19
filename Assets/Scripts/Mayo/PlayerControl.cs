using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using Object = System.Object;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private XRNode _xrNode = XRNode.RightHand;
    [SerializeField] private GameObject bodyToMove;
    [SerializeField] private Image _fadingScreen;
    [SerializeField] private Ender ender;
    public float mvtSpeed = 10f;
    [SerializeField] private float layerChangeTime = 4f;
    [SerializeField] private GestureEventsManager _gestureManager;
    [SerializeField] private float layerTransitionSpeed = .5f;
    [SerializeField] private float layerHeight = 5f;
    [SerializeField] private Entertainor entertainor;
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private float _headTorsoDistance = .1f;
    [SerializeField] private float _speedUpMultiplier = 2;

    private List<InputDevice> _devices = new List<InputDevice>();
    private InputDevice _device;
    public static bool _canMove = true;
    private bool _changingLayer = false;
    private int layer = 0;
    private float startingHeight;

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
        startingHeight = bodyToMove.transform.position.y;
    }

    void Update()
    {
        if (!_device.isValid)
        {
            //GetDevices();
        }
        // Décommenter quand on build
        if (CheckPanneau.allCheck != 8) _canMove = false;
        else _canMove = true;

        if (expire == true)
        {
            StartCoroutine(RespirationCouroutine());
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            mvtSpeed = 11f;
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
        if (layer == 0 && up) StartCoroutine(ender.endCoroutine());
        else
        {
            _canMove = false;
            _changingLayer = true;
            layer += up ? -1 : 1;
            //float distance = Mathf.Abs(bodyToMove.transform.position.y - (up ? -.4f - layer : .4f - layer) * layerHeight + startingHeight);

            float timer = layerChangeTime;
            while (timer > 0)
            {
                bodyToMove.transform.Translate((up ? Vector3.up : Vector3.down) * (layerTransitionSpeed * Time.deltaTime));
                timer -= Time.deltaTime;
                _fadingScreen.color = new Color(0, 0, 0, 1 - timer / layerChangeTime);
                yield return null;
            }
            bodyToMove.transform.position = new Vector3(50f, -layer*layerHeight + startingHeight, -70f);
            timer = 4f;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                _fadingScreen.color = new Color(0, 0, 0, timer / 4f);
                yield return null;
            }
            _fadingScreen.color = new Color(0, 0, 0, 0);
            _canMove = true;
            _changingLayer = false;
        }
    }

    private IEnumerator MoveForwardCoroutine()
    {
        while (_gestureManager.CurrentGesture == GestureEventsManager.Gesture.GoForward)
        {
            if (_canMove)
            {
                Vector3 direction = (_leftHand.transform.position + _rightHand.transform.position) / 2 - (this.transform.position - Vector3.up*_headTorsoDistance);
                float intensity = direction.magnitude * _speedUpMultiplier;
                bodyToMove.transform.Translate(direction.normalized * (intensity * mvtSpeed * Time.fixedDeltaTime));
                float finalHeight = Mathf.Clamp(bodyToMove.transform.position.y, (-.5f - layer) * layerHeight + startingHeight, (.5f - layer) * layerHeight + startingHeight);
                bodyToMove.transform.position = new Vector3(bodyToMove.transform.position.x, finalHeight, bodyToMove.transform.position.z);
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
