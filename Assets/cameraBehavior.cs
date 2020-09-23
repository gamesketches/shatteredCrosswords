using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBehavior : MonoBehaviour
{
    public Vector3 pivot;
    public float rotationSpeed = 3;
    public Vector3 winSpot;
    public GameObject xwd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(pivot, Vector3.up, rotationSpeed * Time.deltaTime);
        } else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(pivot, Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = Vector3.Lerp(transform.position, pivot, 0.01f);
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = Vector3.LerpUnclamped(transform.position, pivot, -0.01f);
        }
        if(Vector3.Distance(transform.position, winSpot) < 2)
        {
            Debug.Break();
            xwd.SetActive(true);
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag(("letters")))
            {
                obj.SetActive(false);
            }

        }
    }
}
