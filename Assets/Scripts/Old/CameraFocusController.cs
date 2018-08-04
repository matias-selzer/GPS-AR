using UnityEngine;
using System.Collections;
using Vuforia;

public class CameraFocusController : MonoBehaviour {


    void Start () 
    {
		InvokeRepeating ("SetAutofocus", 0, 1);
    }

 

    public void SetAutofocus()
    {
        if (CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO))
        {
            Debug.Log("Autofocus set");
        }
        else
        {
            // never actually seen a device that doesn't support this, but just in case
            Debug.Log("this device doesn't support auto focus");
        }
    }
}
