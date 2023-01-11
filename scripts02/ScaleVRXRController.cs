using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;
using System;

// for 2D Axis
public enum Axis { NONE = -1, UP, DOWN, LEFT, RIGHT };

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
        // left controller
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
    // -1,0,1,2,3 : none, up, down, left, right
    public Axis getAxis2DState(int hand)
    {
        float up = primary2DAxis[hand].y > 0 ? primary2DAxis[hand].y : 0;
        float down = primary2DAxis[hand].y < 0 ? -1 * primary2DAxis[hand].y : 0;
        float left = primary2DAxis[hand].x < 0 ? -1 * primary2DAxis[hand].x : 0;
        float right = primary2DAxis[hand].x > 0 ? primary2DAxis[hand].x : 0;
        float maxValue = Math.Max(Math.Max(Math.Max(up, down), right), left);
        if (maxValue == 0)
            return Axis.NONE;
        if (up == maxValue)
            return Axis.UP;
        if (down == maxValue)
            return Axis.DOWN;
        if (left == maxValue)
            return Axis.LEFT;
        if (right == maxValue)
            return Axis.RIGHT;

        return Axis.NONE;
    }

}