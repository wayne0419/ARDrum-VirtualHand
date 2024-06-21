using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFollowController : MonoBehaviour
{
    public Transform leftHand;
    public Transform leftController;
    public Transform rightHand;
    public Transform rightController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftHand.position = leftController.position;
        leftHand.rotation = leftController.rotation;
        rightHand.position = rightController.position;
        rightHand.rotation = rightController.rotation;
    }
}
