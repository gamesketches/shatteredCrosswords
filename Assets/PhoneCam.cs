using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCam : MonoBehaviour
{

    public Material renderMat;
    WebCamTexture texture;
    // Start is called before the first frame update
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        texture = new WebCamTexture();
        foreach (WebCamDevice device in devices)
        {
            Debug.Log(device.name);
            texture.deviceName = "FaceTime HD Camera (Built-in)";
            texture.Play();
        }
        renderMat.mainTexture = texture;
    }

    // Update is called once per frame
    void Update()
    {
           if(Input.GetKeyDown(KeyCode.Space))
        {
            texture.Play();

        }
    }
}
