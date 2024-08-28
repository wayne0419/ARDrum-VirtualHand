using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class LevelManagementAnnouncement : MonoBehaviour
{
    public LevelManager levelManager;
    public TextMeshProUGUI levelPassedText;

    void OnEnable() {
        // 關閉 levelPassedText
        levelPassedText.gameObject.SetActive(false);

        // 訂閱 OnLevelPassed
        levelManager.OnLevelPassed += AnnounceLevelPassed;
    }
    void DisEnable() {
        // 訂閱 OnLevelPassed
        levelManager.OnLevelPassed += AnnounceLevelPassed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AnnounceLevelPassed() {
        StartCoroutine(announceLevelPassedCoroutine());
    }
    IEnumerator announceLevelPassedCoroutine() {
        levelPassedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        levelPassedText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        levelPassedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        levelPassedText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        levelPassedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        levelPassedText.gameObject.SetActive(false);
    }
}
