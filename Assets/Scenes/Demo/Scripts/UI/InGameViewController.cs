using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameViewController : UIViewDemo
{
    public static InGameViewController instance;
    public override void Awake()
    {
        instance = this;
        base.Awake();
    }

    public override void HideView()
    {
        base.HideView();
    }

    public override void ShowView()
    {
        base.ShowView();

    }

    public void OnButtonClicked()
    {
        HideView();
        ViewControllerDemo.instance.viewGameOver.ShowView();
    }
}
