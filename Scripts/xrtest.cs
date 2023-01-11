using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;

public class xrtest : MonoBehaviour
{
    //public InputActionReference inputActionReference;
    public GameObject test;
    public TextMeshProUGUI test2;
    private InputDevice _leftController;
    private InputDevice _rightController;

    // Start is called before the first frame update
    void Start()
    {
        _leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        _rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        bool left_trigger = false;
        bool right_trigger = false;
        bool x_button = false;
        bool y_button = false;
        bool menu_button = false;
        bool press = false;

        Vector3 pos;
        Quaternion rot;
        _leftController.TryGetFeatureValue(CommonUsages.triggerButton, out left_trigger);
        _rightController.TryGetFeatureValue(CommonUsages.triggerButton, out right_trigger);

        _rightController.TryGetFeatureValue(CommonUsages.primaryButton, out x_button);
        _rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out y_button);
        _rightController.TryGetFeatureValue(CommonUsages.menuButton, out menu_button);

        _rightController.TryGetFeatureValue(CommonUsages.devicePosition, out pos);
        _rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out rot);

        test2.text = pos + " " + rot;
        press = left_trigger | right_trigger | x_button | y_button | menu_button;
        if (press)
            test.SetActive(false);
        else
        {
            test.SetActive(true);
            test.transform.SetPositionAndRotation(pos, rot);
        }

    }
}
