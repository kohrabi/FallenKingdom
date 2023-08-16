using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{


    public void Show(bool show)
    {
        var button = GetComponent<CanvasGroup>();
        if (show)
            button.alpha = 1;
        else
            button.alpha = 0;
    }
}
