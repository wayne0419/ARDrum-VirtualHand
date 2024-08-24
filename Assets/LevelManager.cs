using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public PlayBackPhaseRunner playBackPhaseRunner;
    public TransformPlayBacker transformPlayBacker;
    public GameObject virtualHands;
    public GameObject drumAudio;
    public TMPro.TextMeshProUGUI bpmText;
    // public DrumSheet LRSingleDrumFull;
    // public DrumSheet LRMultiDrumFull;
    // public DrumSheet LRSingleDrumMain;
    // public DrumSheet LRMultiDrumMain;
    // public DrumSheet LRSingleDrumOneThree;
    // public DrumSheet LRMultiDrumOneThree;
    // public DrumSheet LRSingleDrumTwoFour;
    // public DrumSheet LRMultiDrumTwoFour;
    // public float test;
    // public enum Level {
    //     LRTest60BPM,
    //     LRTest120BPM,
    // }
    // public Level level;
    // void OnValidate() {
    //     Debug.Log("OnValidate");
    //     switch (level) 
    //     {
    //         case Level.LRTest60BPM:
    //             LRTest60BPM();
    //             break;
    //     }
    // }
    // void Start(){
    //     // transformPlayBacker.playBackBPM;
    //     // virtualHands.SetActive(true);
    //     // drumAudio.SetActive(true);
    //     // transformPlayBacker.drumSheet
    //     // transformPlayBacker.drumHitIndicator.gameObject.SetActive(false);
    // }

    
    // public void LRTest60BPM() {
    //     transformPlayBacker.StopPlayBack();


    //     // ------
    //     transformPlayBacker.playBackBPM = 60f;
    //     virtualHands.SetActive(true);
    //     drumAudio.SetActive(true);
    //     transformPlayBacker.drumHitIndicator.gameObject.SetActive(true);
    //     transformPlayBacker.drumSheet = LRMultiDrumFull;
    //     // ------


    //     playBackPhaseRunner.StopPhaseRunner();
    //     playBackPhaseRunner.StartPhaseRunner();


    //     // ------
    //     for (int i=0; i<transformPlayBacker.drumSheet.drumNotes.Count; i++) {
    //         DrumNote drumNote = transformPlayBacker.drumSheet.drumNotes[i];
    //         if (drumNote.associatedSegment.skip != drumNote.skip) {
    //             drumNote.ToggleSkip();
    //         }
    //     }
    //     // ------
    // }

    void Update() {
        if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            transformPlayBacker.playBackBPM += 5f;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
            transformPlayBacker.playBackBPM -= 5f;
        }
        if (Input.GetKeyDown(KeyCode.Keypad9)) {
            virtualHands.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8)) {
            virtualHands.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6)) {
            transformPlayBacker.drumHitIndicator.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5)) {
            transformPlayBacker.drumHitIndicator.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3)) {
            drumAudio.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            drumAudio.SetActive(false);
        }

        bpmText.text = transformPlayBacker.playBackBPM.ToString();

    }
    

}
