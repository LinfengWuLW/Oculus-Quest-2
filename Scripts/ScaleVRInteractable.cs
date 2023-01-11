using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

//[RequireComponent(typeof(XRBaseInteractable))]
public class ScaleVRInteractable : XRBaseInteractable
{
    //XRBaseInteractable m_Interactable;
    public ScaleVRXRController ScaleVR_XRcontroller=null;
    public TextMeshProUGUI info;
    public int attachButtonId = 0;
    private bool isHovered = false;
    private bool isSelected = false;
    private Transform thisAttachTransform;
    private int handidx = -1; // 0:left, 1:right, -1:none
    private ActionBasedController actionBasedController;
    // Start is called before the first frame update
    void Start()
    {
        //m_Interactable = GetComponent<XRBaseInteractable>();
        this.firstHoverEntered.AddListener(OnFirstHoverEntered);
        this.lastHoverExited.AddListener(OnLastHoverExited);
        this.firstSelectEntered.AddListener(OnFirstSelectEntered);
        this.lastSelectExited.AddListener(OnLastSelectExited);
    }

    // Update is called once per frame
    void Update()
    {
        if (handidx == -1)
            return;
        if (isHovered && ScaleVR_XRcontroller.isPress(handidx, attachButtonId))
        {
            thisAttachTransform.SetParent(actionBasedController.transform, true);
        }
        else
            thisAttachTransform.SetParent(null, true);

        setInfo(ScaleVR_XRcontroller.getPrimary2DAxis(handidx).ToString());
    }

    protected virtual void OnFirstHoverEntered(HoverEnterEventArgs args) => test(args);
    protected virtual void OnLastHoverExited(HoverExitEventArgs args) => test2();
    protected virtual void OnFirstSelectEntered(SelectEnterEventArgs args) => test3();
    protected virtual void OnLastSelectExited(SelectExitEventArgs args) => test4();
    void setInfo(string s)
    {
        if (info != null)
            info.text = s;
    }
    protected void test(HoverEnterEventArgs args)
    {
        thisAttachTransform = this.GetAttachTransform(args.interactorObject);
        actionBasedController = args.interactorObject.transform.GetComponent<ActionBasedController>();
        if (actionBasedController.name == "LeftHand Controller")
            handidx = 0;
        else
            handidx = 1;
        setInfo(actionBasedController.name);
        isHovered = true;
    }

    protected void test2()
    {
        handidx = -1;
        setInfo("OnLastHoverExited");
        isHovered = false;
    }

    protected void test3()
    {
        setInfo("OnFirstSelectEntered");
        isSelected = true;
    }
    protected void test4()
    {
        setInfo("OnLastSelectExited");
        isSelected = false;
    }
}
