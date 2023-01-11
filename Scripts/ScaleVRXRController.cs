using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;

[DisallowMultipleComponent]
public class ScaleVRXRController : MonoBehaviour
{
    private InputDevice _leftController;
    private InputDevice _rightController;

    // trigger, grip, primary, secondary button
    private int numbuttons = 4;
    private bool[] is_button_pressing = new bool[8];
    private Vector2[] primary2DAxis = new Vector2[2];


    // Start is called before the first frame update
    void Start()
    {
        _leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        _rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        
        _leftController.TryGetFeatureValue(CommonUsages.triggerButton, out is_button_pressing[0]);
        _leftController.TryGetFeatureValue(CommonUsages.gripButton, out is_button_pressing[1]);
        _leftController.TryGetFeatureValue(CommonUsages.primaryButton, out is_button_pressing[2]);
        _leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out is_button_pressing[3]);
        _leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out primary2DAxis[0]);

        _rightController.TryGetFeatureValue(CommonUsages.triggerButton, out is_button_pressing[4]);
        _rightController.TryGetFeatureValue(CommonUsages.gripButton, out is_button_pressing[5]);
        _rightController.TryGetFeatureValue(CommonUsages.primaryButton, out is_button_pressing[6]);
        _rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out is_button_pressing[7]);
        _rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out primary2DAxis[1]);
    }

    public bool isPress(int hand, int btn)
    {
        int idx = hand * numbuttons + btn;
        return is_button_pressing[idx];
    }

    public Vector2 getPrimary2DAxis(int hand)
    {
        return primary2DAxis[hand];
    }

}