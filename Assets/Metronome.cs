using UnityEngine;
using System.Collections;
using TMPro;

public class Metronome : MonoBehaviour
{
    public enum MetronomeMode { FourBeats, SixteenBeats } // 枚举类型，表示模式
    public MetronomeMode mode = MetronomeMode.FourBeats; // 默认模式为4拍

    public float bpm = 120f; // beats per minute
    public AudioClip metronomeSfxHigh; // 第一個 beat 的聲效
    public AudioClip metronomeSfxLow; // 其他 beat 的聲效
    public MetronomeNote[] metronomeNotes; // 存储 MetronomeNote，共16个
    public Color warmUpColor = Color.gray; // 准备期间的颜色
    public Color playColor = Color.green; // 播放期间的颜色
    public TransformPlayBacker transformPlayBacker; // TransformPlayBacker 的引用
    public TextMeshProUGUI bpmText;

    private AudioSource audioSource;
    private Coroutine metronomeCoroutine;
    private bool isPaused = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 初始化为 warmUpColor
        SetWarmUpColor();

        // 订阅 TransformPlayBacker 的事件
        if (transformPlayBacker != null)
        {
            transformPlayBacker.OnPlayTransformDataStart += SetPlayColor;
            transformPlayBacker.OnPlayTransformDataEnd += SetWarmUpColor;
        }
    }

    void Update() {
        // 更新 bpmText
        if (bpmText != null && transformPlayBacker != null) {
            bpmText.text = transformPlayBacker.playBackBPM.ToString();
        }
        // Input 調整 bpm
        if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            if (transformPlayBacker != null) {
                // 先暫停播放，再調整 bpm
                transformPlayBacker.StopPlayBack();
                transformPlayBacker.playBackBPM += 5;
                transformPlayBacker.StartPlayBack();
            }
        } 
        else if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
            if (transformPlayBacker != null) {
                // 先暫停播放，再調整 bpm
                transformPlayBacker.StopPlayBack();
                transformPlayBacker.playBackBPM -= 5;
                transformPlayBacker.StartPlayBack();
            }
        }
    }

    private void OnDestroy()
    {
        if (transformPlayBacker != null)
        {
            transformPlayBacker.OnPlayTransformDataStart -= SetPlayColor;
            transformPlayBacker.OnPlayTransformDataEnd -= SetWarmUpColor;
        }
    }

    public void StartMetronome()
    {
        if (metronomeCoroutine == null)
        {
            metronomeCoroutine = StartCoroutine(MetronomeRoutine());
        }
    }

    public void StopMetronome()
    {
        if (metronomeCoroutine != null)
        {
            StopCoroutine(metronomeCoroutine);
            metronomeCoroutine = null;
        }
    }

    private IEnumerator MetronomeRoutine()
    {
        int beatCount = 0;
        while (true)
        {
            float beatDuration = 60f / bpm; // 计算每个 beat 的持续时间

            if (mode == MetronomeMode.FourBeats)
            {
                // 4拍模式
                if (!isPaused)
                {
                    if (beatCount % 4 == 0)
                    {
                        PlaySound(metronomeSfxHigh);
                    }
                    else
                    {
                        PlaySound(metronomeSfxLow);
                    }

                    // Highlight the corresponding MetronomeNote
                    HighlightMetronomeNoteAtIndex((beatCount % 4) * 4);

                    beatCount++;
                }
                yield return new WaitForSeconds(beatDuration);
            }
            else if (mode == MetronomeMode.SixteenBeats)
            {
                // 16拍模式
                for (int subBeat = 0; subBeat < 4; subBeat++)
                {
                    if (!isPaused)
                    {
                        if (subBeat == 0)
                        {
                            // 播放主要节拍
                            PlaySound(metronomeSfxHigh);
                        }
                        else
                        {
                            // 播放子节拍
                            PlaySound(metronomeSfxLow);
                        }

                        // Highlight the corresponding MetronomeNote based on the subbeat
                        HighlightMetronomeNoteAtIndex((beatCount % 4) * 4 + subBeat);
                    }
                    yield return new WaitForSeconds(beatDuration / 4f); // 每个 subbeat 持续时间是原来的 1/4
                }
                beatCount++;
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void HighlightMetronomeNoteAtIndex(int index)
    {
        for (int i = 0; i < metronomeNotes.Length; i++)
        {
            if (i == index)
            {
                metronomeNotes[i].SetHighlight();
            }
            else
            {
                metronomeNotes[i].SetOn();
            }
        }
    }

    private void SetPlayColor()
    {
        // 设置 metronomeNotes 的 onColor 为 playColor
        SetMetronomeNotesColor(playColor);
    }

    private void SetWarmUpColor()
    {
        // 设置 metronomeNotes 的 onColor 为 warmUpColor
        SetMetronomeNotesColor(warmUpColor);
    }

    private void SetMetronomeNotesColor(Color color)
    {
        foreach (var note in metronomeNotes)
        {
            if (note != null)
            {
                note.onColor = color;
            }
        }
    }
}
