using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyControl : MonoBehaviour
{
    public enum StudyMode {
        baseline,
        MRDrum
    }
    public StudyMode studyMode;
    public List<GameObject> MRDrumObjects;
    public List<GameObject> baselineObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (studyMode == StudyMode.baseline) {
                studyMode = StudyMode.MRDrum;
                foreach (var obj in MRDrumObjects) {
                    obj.SetActive(true);
                }
                foreach (var obj in baselineObjects) {
                    obj.SetActive(false);
                }
            }
            else {
                studyMode = StudyMode.baseline;
                foreach (var obj in MRDrumObjects) {
                    obj.SetActive(false);
                }
                foreach (var obj in baselineObjects) {
                    obj.SetActive(true);
                }
            }
        }
    }
}
