using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    [RequireComponent(typeof(Camera))]

    public class UIViewDemo : MonoBehaviour
    {
       
    private Camera cam;
    private Canvas[] canvases;

    /// <summary>
    /// Awake this instance,get the camera and canvas component
    /// </summary>
    public virtual void Awake()
    {
        cam = this.GetComponent<Camera>();
        canvases = this.GetComponentsInChildren<Canvas>();
        Application.targetFrameRate = 60;
    }


    /// <summary>
    /// Show this instance enables the camera and canvases
    /// </summary>
    public virtual void ShowView()
    {
        ViewWillAppear();
        GameObject mainCanvas = this.transform.Find("UICanvas").gameObject;
        GameObject viewContent = mainCanvas.transform.Find("ViewContent").gameObject;
        viewContent.transform.localPosition = Vector3.zero;
        cam.enabled = true;
        foreach (Canvas canvas in canvases) canvas.enabled = true;

        Canvas.ForceUpdateCanvases();
    }

    public virtual void HideView()
    {
        cam.enabled = false;
        foreach (Canvas canvas in canvases) canvas.enabled = false;
        Canvas.ForceUpdateCanvases();
    }

    public virtual void ViewWillAppear()
    {

    }

    public virtual void ViewWillDisappear()
    {

    }

}
