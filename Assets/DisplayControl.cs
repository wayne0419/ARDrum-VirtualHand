using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayControl : MonoBehaviour
{
    bool renderers1on = true;
    public List<GameObject> renderers1;
    bool renderers2on = true;
    public List<GameObject> renderers2;
    bool renderers3on = true;
    public List<GameObject> renderers3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7)) {
            for(int i=0; i<renderers1.Count; i++) {
                renderers1[i].SetActive(!renderers1on);
            }
            renderers1on = !renderers1on;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4)) {
            for(int i=0; i<renderers2.Count; i++) {
                renderers2[i].SetActive(!renderers2on);
            }
            renderers2on = !renderers2on;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            for(int i=0; i<renderers3.Count; i++) {
                renderers3[i].SetActive(!renderers3on);
            }
            renderers3on = !renderers3on;
        }
    }
}
