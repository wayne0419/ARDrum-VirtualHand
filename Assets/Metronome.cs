using UnityEngine;
using System.Collections;

public class Metronome : MonoBehaviour
{
    public float bpm = 120f; // beats per minute
    public AudioClip metronomeSfxHigh; // 第一個 beat 的聲效
    public AudioClip metronomeSfxLow; // 其他 beat 的聲效
    public MetronomeNote[] metronomeNotes; // 存储4个 MetronomeNote
    public Color warmUpColor = Color.gray; // 准备期间的颜色
    public Color playColor = Color.green; // 播放期间的颜色
    public TransformPlayBacker transformPlayBacker; // TransformPlayBacker 的引用

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
            float beatDuration = 60f / bpm; // Calculate beatDuration here
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
                HighlightMetronomeNoteAtIndex(beatCount % 4);
                
                beatCount++;
            }
            yield return new WaitForSeconds(beatDuration);
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
        if (metronomeNotes == null || metronomeNotes.Length < 4)
            return;

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
        if (metronomeNotes != null)
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
}
