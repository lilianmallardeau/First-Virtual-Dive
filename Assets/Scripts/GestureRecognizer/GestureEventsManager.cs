using System;
using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using UnityEngine;
using UnityEngine.Events;

public class GestureEventsManager : MonoBehaviour
{
    public enum Gesture
    {
        None,
        GoForward,
        Up,
        Down,
        Ok,
        NotOk,
        Cold,
        HalfPressure,
        Reserve,
        NoMoreOxygen,
        Menu
    }
    public enum HandGesture
    {
        None,
        Thumb,
        Ok,
        Flat,
        Fist,
        Menu
    }

    public enum Hand
    {
        LeftHand,
        RightHand,
        Default
    }
    
    // The current and previous gestures
    public Gesture CurrentGesture { get; private set; }
    private Gesture _previousGesture;
    
    // Timestamp of last gesture initialized 
    private float _timer;
    
    // The last gesture validated after delay
    public Gesture CurrentValidatedGesture { get; private set; }

    // Current gestures from hands
    private HandGesture _leftHandGesture;
    private HandGesture _rightHandGesture;

    // Which hand trigger the menu
    public static string hand;
    
    // Which hand is doing the gesture
    public static Hand mainHand;
    
    // Getter for the gameObject of the hand doing the gesture
    private GameObject MainHandGameObject => mainHand == Hand.LeftHand ? _leftHand : _rightHand;
    

    // Menu for each hand
    [Header("Menus")]
    [SerializeField] private Canvas leftMenu;
    [SerializeField] private Canvas rightMenu;

    // The two hands to get orientation, position and stuff
    [Header("Hands")]
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private GameObject _rightHand;
    
    // The two side cameras of the headset, to compute distance between hands and sides of the head
    [SerializeField] private GameObject _leftHeadCamera;
    [SerializeField] private GameObject _rightHeadCamera;
    [SerializeField] private GameObject _centerHeadCamera;
    
    
    // Events for each gesture initialized
    [Header("Gestures events")]
    [SerializeField] private UnityEvent None;
    [SerializeField] private UnityEvent GoForward;
    [SerializeField] private UnityEvent Up;
    [SerializeField] private UnityEvent Down;
    [SerializeField] private UnityEvent Ok;
    [SerializeField] private UnityEvent NotOk;
    [SerializeField] private UnityEvent Cold;
    [SerializeField] private UnityEvent HalfPressure;
    [SerializeField] private UnityEvent Reserve;
    [SerializeField] private UnityEvent NoMoreOxygen;
    [SerializeField] private UnityEvent Menu;

    
    [Header("Gestures events triggered (after delay)")]
    
    // Delay to wait before triggering gesture
    [Tooltip("Delay between gesture initialization and event trigger (in seconds)")] public float delay = 1.5f;
    [Tooltip("Delay between first hand position recognition and animated gesture initialization (in seconds)")] public float delayAnimatedFirst = .5f;
    [Tooltip("Delay between animated gesture initialization and event trigger (in seconds).\ndelayAnimatedFirst plus delayAnimatedSecond should add up to delay")] public float delayAnimatedSecond = 1f;
    
    // Events for each gesture initialized
    [SerializeField] private UnityEvent NoneTriggered;
    [SerializeField] private UnityEvent GoForwardTriggered;
    [SerializeField] private UnityEvent UpTriggered;
    [SerializeField] private UnityEvent DownTriggered;
    [SerializeField] private UnityEvent OkTriggered;
    [SerializeField] private UnityEvent NotOkTriggered;
    [SerializeField] private UnityEvent ColdTriggered;
    [SerializeField] private UnityEvent HalfPressureTriggered;
    [SerializeField] private UnityEvent ReserveTriggered;
    [SerializeField] private UnityEvent NoMoreOxygenTriggered;
    [SerializeField] private UnityEvent MenuTriggered;
    
    
    private void SetHandGesture(OVRHand.Hand hand, string gesture)
    {
        HandGesture handGesture = HandGesture.None;
        switch (gesture) {
            case "thumb":
            case "Thumb":
                handGesture = HandGesture.Thumb;
                break;
            case "ok":
            case "Ok":
                handGesture = HandGesture.Ok;
                break;
            case "flat":
            case "Flat":
                handGesture = HandGesture.Flat;
                break;
            case "fist":
            case "Fist":
                handGesture = HandGesture.Fist;
                break;
            case "menu":
            case "Menu":
                handGesture = HandGesture.Menu;
                break;
        }

        if (hand == OVRHand.Hand.HandLeft)
            _leftHandGesture = handGesture;
        else if (hand == OVRHand.Hand.HandRight)
            _rightHandGesture = handGesture;
        //ComputeFinalGesture();
    }
    
    public void SetLeftHandGesture(string gesture)
    {
        SetHandGesture(OVRHand.Hand.HandLeft, gesture);
    }
    
    public void SetRightHandGesture(string gesture)
    {
        SetHandGesture(OVRHand.Hand.HandRight, gesture);
    }

    private void ComputeFinalGesture()
    {
        //return;
        // Ok
        if (_leftHandGesture == HandGesture.Ok || _rightHandGesture == HandGesture.Ok)
            CurrentGesture = Gesture.Ok;
        
        // Up and Down
        else if (_rightHandGesture == HandGesture.Thumb)
            CurrentGesture = Vector3.Dot(Vector3.up, _rightHand.transform.forward) < 0 ? Gesture.Up : Gesture.Down;
        else if (_leftHandGesture == HandGesture.Thumb)
            CurrentGesture = Vector3.Dot(Vector3.up, _leftHand.transform.forward) > 0 ? Gesture.Up : Gesture.Down;
        
        // Go forward
        else if (_leftHandGesture == HandGesture.Fist && _rightHandGesture == HandGesture.Fist &&
                 Vector3.Distance(_rightHand.transform.position, _leftHand.transform.position) < .4)
            CurrentGesture = Gesture.GoForward;
        
        // Half pressure ("T" with the two hands)
        else if (_leftHandGesture == HandGesture.Flat && _rightHandGesture == HandGesture.Flat &&
                 Vector3.Distance(_rightHand.transform.position, _leftHand.transform.position) < .4 &&
                 Math.Abs(Vector3.Dot(_rightHand.transform.right, _leftHand.transform.right)) < .2)
            CurrentGesture = Gesture.HalfPressure;
        
        // Menu
        else if ((_rightHandGesture == HandGesture.Menu || _rightHandGesture == HandGesture.Flat) && Vector3.Dot(Vector3.up, _rightHand.transform.up) < 0 && Math.Abs(Vector3.Dot(Vector3.up, _rightHand.transform.up)) > .8)
        {
            CurrentGesture = Gesture.Menu;
            hand = "right";
        }
        else if ((_leftHandGesture == HandGesture.Menu || _leftHandGesture == HandGesture.Flat) && Vector3.Dot(Vector3.up, _leftHand.transform.up) > 0 && Math.Abs(Vector3.Dot(Vector3.up, _leftHand.transform.up)) > .8)
        {
            CurrentGesture = Gesture.Menu;
            hand = "left";
        }
        
        // Reserve
        else if ((_leftHandGesture == HandGesture.Fist || _rightHandGesture == HandGesture.Fist) && (Vector3.Distance(_leftHand.transform.position, _leftHeadCamera.transform.position) < .3f || Vector3.Distance(_rightHand.transform.position, _rightHeadCamera.transform.position) < .3f))
        {
            CurrentGesture = Gesture.Reserve;
        }
        
        // Cold
        else if (((_leftHandGesture == HandGesture.Menu && _rightHandGesture == HandGesture.Fist) || (_leftHandGesture == HandGesture.Fist && _rightHandGesture == HandGesture.Menu)) && Mathf.Abs(Vector3.Dot(_rightHand.transform.right, -_leftHand.transform.right)) < .2f && Vector3.Distance(_leftHand.transform.position, _rightHand.transform.position) < .3f)
        {
            if (_previousGesture != Gesture.Cold)
            {
                StartCoroutine(ComputeAnimatedGestureCoroutine(Gesture.Cold));
                CurrentGesture = Gesture.Cold;
            }
        }
        
        // No more oxygen
        else if (((_leftHandGesture == HandGesture.Flat && Vector3.Dot(_leftHand.transform.up, Vector3.up) < -.9f) || (_rightHandGesture == HandGesture.Flat && Vector3.Dot(_rightHand.transform.up, Vector3.up) > .9f)) && Mathf.Min(Vector3.Distance(_leftHand.transform.position, _centerHeadCamera.transform.position - _centerHeadCamera.transform.up * .3f), Vector3.Distance(_rightHand.transform.position, _centerHeadCamera.transform.position - _centerHeadCamera.transform.up * .3f)) < .5f)
        {
            if (_previousGesture != Gesture.NoMoreOxygen)
            {
                StartCoroutine(ComputeAnimatedGestureCoroutine(Gesture.NoMoreOxygen));
                CurrentGesture = Gesture.NoMoreOxygen;
            }
        }
        
        // Not ok
        else if (_leftHandGesture == HandGesture.Flat || _leftHandGesture == HandGesture.Menu)
        {
            if (_previousGesture != Gesture.NotOk)
            {
                mainHand = Hand.LeftHand;
                StartCoroutine(ComputeAnimatedGestureCoroutine(Gesture.NotOk));
                CurrentGesture = Gesture.NotOk;
            }
        }
        else if (_rightHandGesture == HandGesture.Flat || _rightHandGesture == HandGesture.Menu)
        {
            if (_previousGesture != Gesture.NotOk)
            {
                mainHand = Hand.RightHand;
                StartCoroutine(ComputeAnimatedGestureCoroutine(Gesture.NotOk));
                CurrentGesture = Gesture.NotOk;
            }
        }

        else
            CurrentGesture = CurrentValidatedGesture = Gesture.None;
        
        if (CurrentGesture != _previousGesture) {
            _previousGesture = CurrentGesture;
            _timer = Time.time;
            if (!(CurrentGesture == Gesture.Cold || CurrentGesture == Gesture.NotOk || CurrentGesture == Gesture.NoMoreOxygen))
            {
                InvokeGestureEvent(CurrentGesture);
            }
        }
    }

    private IEnumerator ComputeAnimatedGestureCoroutine(Gesture gesture)
    {
        float timer = 0f;
        float distance = 0f;
        float prevDistance = 0f;
        bool gestureStarted = false;
        float sampler = 0f;

        switch (gesture)
        {
            case Gesture.NotOk:
                float prevRotation = 0;
                while (_leftHandGesture == HandGesture.Flat || _leftHandGesture == HandGesture.Menu || _rightHandGesture == HandGesture.Flat || _rightHandGesture == HandGesture.Menu)
                {
                    if (!gestureStarted && timer >= delayAnimatedFirst)
                    {
                        InvokeGestureEvent(Gesture.NotOk);
                        timer = 0;
                        gestureStarted = true;
                    }
                    else if (timer >= delayAnimatedSecond)
                    {
                        InvokeTriggeredGestureEvent(Gesture.NotOk);
                        break;
                    }

                    if (MainHandGameObject.transform.rotation.x - prevRotation < 10)
                    {
                        break;
                    }

                    timer += Time.deltaTime;
                    yield return null;
                }
                break;
            
            case Gesture.NoMoreOxygen:
                do
                {
                    distance = Mathf.Min(Vector3.Distance(_leftHand.transform.position, _centerHeadCamera.transform.position - _centerHeadCamera.transform.up * .3f), Vector3.Distance(_rightHand.transform.position, _centerHeadCamera.transform.position - _centerHeadCamera.transform.up * .3f));
                    
                    if (!gestureStarted && timer >= delayAnimatedFirst)
                    {
                        InvokeGestureEvent(Gesture.NoMoreOxygen);
                        timer = .0f;
                        gestureStarted = true;
                    }
                    else if (timer >= delayAnimatedSecond)
                    {
                        InvokeTriggeredGestureEvent(CurrentValidatedGesture = Gesture.NoMoreOxygen);
                        break;
                    }
                    if (sampler > .2f)
                    {
                        if (Mathf.Abs(distance - prevDistance) < .08f)
                        {
                            timer -= Time.deltaTime;
                            if (timer < 0)
                            {
                                break;
                            }
                        }
                        prevDistance = distance;
                        sampler = 0f;
                    }

                    timer += Time.deltaTime;
                    sampler += Time.deltaTime;
                    yield return null;
                } while (((_leftHandGesture == HandGesture.Flat && Vector3.Dot(_leftHand.transform.up, Vector3.up) < -.9f) || (_rightHandGesture == HandGesture.Flat && Vector3.Dot(_rightHand.transform.up, Vector3.up) > .9f)) && distance < .5f);
                break;
            
            case Gesture.Cold:
                do
                {
                    distance = Vector3.Distance(_leftHand.transform.position, _rightHand.transform.position);
                    
                    if (!gestureStarted && timer >= delayAnimatedFirst)
                    {
                        InvokeGestureEvent(Gesture.Cold);
                        timer = .0f;
                        gestureStarted = true;
                    }
                    else if (timer >= delayAnimatedSecond)
                    {
                        InvokeTriggeredGestureEvent(CurrentValidatedGesture = Gesture.Cold);
                        break;
                    }

                    if (sampler > .2f)
                    {
                        if (Mathf.Abs(distance - prevDistance) < .08f)
                        {
                            timer -= Time.deltaTime;
                            if (timer < 0)
                            {
                                break;
                            }
                        }
                        prevDistance = distance;
                        sampler = 0f;
                    }

                    timer += Time.deltaTime;
                    sampler += Time.deltaTime;
                    yield return null;
                } while (((_leftHandGesture == HandGesture.Menu && _rightHandGesture == HandGesture.Fist) || (_leftHandGesture == HandGesture.Fist && _rightHandGesture == HandGesture.Menu)) && distance < .3f);
                break;
            
            default:
                break;
        }
    }

    private void InvokeGestureEvent(Gesture gesture)
    {
        if (gesture == Gesture.None)
            None.Invoke();
        else if (gesture == Gesture.GoForward)
            GoForward.Invoke();
        else if (gesture == Gesture.Up)
            Up.Invoke();
        else if (gesture == Gesture.Down)
            Down.Invoke();
        else if (gesture == Gesture.Ok)
            Ok.Invoke();
        else if (gesture == Gesture.NotOk)
            NotOk.Invoke();
        else if (gesture == Gesture.Cold)
            Cold.Invoke();
        else if (gesture == Gesture.HalfPressure)
            HalfPressure.Invoke();
        else if (gesture == Gesture.Reserve)
            Reserve.Invoke();
        else if (gesture == Gesture.NoMoreOxygen)
            NoMoreOxygen.Invoke();
        else if (gesture == Gesture.Menu)
            Menu.Invoke();
    }
    
    private void InvokeTriggeredGestureEvent(Gesture gesture)
    {
        if (gesture == Gesture.None)
            NoneTriggered.Invoke();
        else if (gesture == Gesture.GoForward)
            GoForwardTriggered.Invoke();
        else if (gesture == Gesture.Up)
            UpTriggered.Invoke();
        else if (gesture == Gesture.Down)
            DownTriggered.Invoke();
        else if (gesture == Gesture.Ok)
            OkTriggered.Invoke();
        else if (gesture == Gesture.NotOk)
            NotOkTriggered.Invoke();
        else if (gesture == Gesture.Cold)
            ColdTriggered.Invoke();
        else if (gesture == Gesture.HalfPressure)
            HalfPressureTriggered.Invoke();
        else if (gesture == Gesture.Reserve)
            ReserveTriggered.Invoke();
        else if (gesture == Gesture.NoMoreOxygen)
            NoMoreOxygenTriggered.Invoke();
        else if (gesture == Gesture.Menu)
            MenuTriggered.Invoke();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ComputeFinalGesture();
        //GameObject.Find("text").GetComponent<TextMesh>().text = Vector3.Dot(_rightHand.transform.right, _leftHand.transform.right).ToString();

        if (Time.time - _timer >= delay && CurrentValidatedGesture != CurrentGesture)
        {
            if (!(CurrentGesture == Gesture.Cold || CurrentGesture == Gesture.NotOk || CurrentGesture == Gesture.NoMoreOxygen))
            {
                InvokeTriggeredGestureEvent(CurrentValidatedGesture = CurrentGesture);
            }
        }
    }
}
