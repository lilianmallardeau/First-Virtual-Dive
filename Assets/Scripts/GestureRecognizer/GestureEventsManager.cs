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
        CheckMano,
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
    
    // The current and previous gestures
    public Gesture currentGesture { get; private set; }
    private Gesture _previousGesture;
    
    // Current gestures from hands
    private HandGesture _leftHandGesture;
    private HandGesture _rightHandGesture;

    // Which hand trigger the menu
    public static string hand;

    // Menu for each hand
    [Header("Menus")]
    [SerializeField] private Canvas leftMenu;
    [SerializeField] private Canvas rightMenu;

    // The two hands to get orientation, position and stuff
    [Header("Hands")]
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private GameObject _rightHand;

    // Events for each gesture
    [Header("Gestures events")]
    [SerializeField] private UnityEvent None;
    [SerializeField] private UnityEvent GoForward;
    [SerializeField] private UnityEvent Up;
    [SerializeField] private UnityEvent Down;
    [SerializeField] private UnityEvent Ok;
    [SerializeField] private UnityEvent NotOk;
    [SerializeField] private UnityEvent CheckMano;
    [SerializeField] private UnityEvent Cold;
    [SerializeField] private UnityEvent HalfPressure;
    [SerializeField] private UnityEvent Reserve;
    [SerializeField] private UnityEvent NoMoreOxygen;
    [SerializeField] private UnityEvent Menu;

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
            currentGesture = Gesture.Ok;
        
        // Up and Down
        else if (_rightHandGesture == HandGesture.Thumb)
            currentGesture = Vector3.Dot(Vector3.up, _rightHand.transform.forward) < 0 ? Gesture.Up : Gesture.Down;
        else if (_leftHandGesture == HandGesture.Thumb)
            currentGesture = Vector3.Dot(Vector3.up, _leftHand.transform.forward) > 0 ? Gesture.Up : Gesture.Down;
        
        // Go forward
        else if (_leftHandGesture == HandGesture.Fist && _rightHandGesture == HandGesture.Fist &&
                 Vector3.Distance(_rightHand.transform.position, _leftHand.transform.position) < .4)
            currentGesture = Gesture.GoForward;
        
        // Half pressure ("T" with the two hands)
        else if (_leftHandGesture == HandGesture.Flat && _rightHandGesture == HandGesture.Flat &&
                 Vector3.Distance(_rightHand.transform.position, _leftHand.transform.position) < .3 &&
                 Math.Abs(Vector3.Dot(_rightHand.transform.right, _leftHand.transform.right)) < .1)
            currentGesture = Gesture.HalfPressure;
        
        // Menu
        else if ((_rightHandGesture == HandGesture.Menu || _rightHandGesture == HandGesture.Flat) && Vector3.Dot(Vector3.up, _rightHand.transform.up) < 0 && Math.Abs(Vector3.Dot(Vector3.up, _rightHand.transform.up)) > .8)
        {
            currentGesture = Gesture.Menu;
            hand = "right";
        }
        else if ((_leftHandGesture == HandGesture.Menu || _leftHandGesture == HandGesture.Flat) && Vector3.Dot(Vector3.up, _leftHand.transform.up) > 0 && Math.Abs(Vector3.Dot(Vector3.up, _leftHand.transform.up)) > .8)
        {
            currentGesture = Gesture.Menu;
            hand = "left";
        }
        
        // TODO: tests for gestures NotOk, Cold, Reserve, NoMoreOxygen

        else
            currentGesture = Gesture.None;
        if (currentGesture != _previousGesture) {
            _previousGesture = currentGesture;
            InvokeGestureEvent(currentGesture);
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
        else if (gesture == Gesture.CheckMano)
            CheckMano.Invoke();
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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ComputeFinalGesture();
        //GameObject.Find("text").GetComponent<TextMesh>().text = Vector3.Dot(_rightHand.transform.right, _leftHand.transform.right).ToString();
    }
}
