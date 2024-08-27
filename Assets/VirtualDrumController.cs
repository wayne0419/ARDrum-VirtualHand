using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VirtualDrumController : MonoBehaviour
{
    public InputAction ToggleOnOffInput;
    public List<GameObject> drumList;
    // Start is called before the first frame update
    void Start()
    {
        ToggleOnOffInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (ToggleOnOffInput.triggered) {
            foreach (var drum in drumList) {
                drum.SetActive(!drum.activeSelf);
            }
        }
    }
}
