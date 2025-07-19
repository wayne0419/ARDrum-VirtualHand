using UnityEngine;
using System.Collections;
using TMPro; // Required for TextMeshProUGUI.

public class Metronome : MonoBehaviour
{
    public enum MetronomeMode { FourBeats, SixteenBeats } // Defines the metronome's operational modes.
    public MetronomeMode mode = MetronomeMode.FourBeats; // The current metronome mode, defaults to 4 beats.

    public float bpm = 120f; // Beats Per Minute for the metronome.
    public AudioClip metronomeSfxHigh; // Audio clip for the first beat (downbeat).
    public AudioClip metronomeSfxLow;  // Audio clip for subsequent beats.
    public MetronomeNote[] metronomeNotes; // Array of MetronomeNote objects, typically 16 for visual representation.
    public Color warmUpColor = Color.gray; // Color for metronome notes during warm-up or idle state.
    public Color playColor = Color.green;  // Color for metronome notes during active playback.
    public TransformPlayBacker transformPlayBacker; // Reference to the TransformPlayBacker for BPM synchronization.
    public TextMeshProUGUI bpmText; // TextMeshProUGUI component to display the current BPM.

    private AudioSource audioSource; // AudioSource component for playing metronome sounds.
    private Coroutine metronomeCoroutine; // Stores the reference to the running metronome coroutine.
    private bool isPaused = false; // Flag to control metronome pause state (though not actively used in current routine).

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if not present.
        }

        // Initialize metronome notes to the warm-up color.
        SetWarmUpColor();

        // Subscribe to TransformPlayBacker events to change metronome note colors based on playback state.
        if (transformPlayBacker != null)
        {
            transformPlayBacker.OnPlayTransformDataStart += SetPlayColor;
            transformPlayBacker.OnPlayTransformDataEnd += SetWarmUpColor;
        }
    }

    void Update()
    {
        // Update the displayed BPM text based on the TransformPlayBacker's current BPM.
        if (bpmText != null && transformPlayBacker != null)
        {
            bpmText.text = transformPlayBacker.playBackBPM.ToString();
        }

        // Handle input for adjusting BPM.
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (transformPlayBacker != null)
            {
                // Stop playback, adjust BPM, then restart playback to apply changes.
                transformPlayBacker.StopPlayBack();
                transformPlayBacker.playBackBPM += 5;
                transformPlayBacker.StartPlayBack();
            }
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (transformPlayBacker != null)
            {
                // Stop playback, adjust BPM, then restart playback to apply changes.
                transformPlayBacker.StopPlayBack();
                transformPlayBacker.playBackBPM -= 5;
                transformPlayBacker.StartPlayBack();
            }
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks when the object is destroyed.
        if (transformPlayBacker != null)
        {
            transformPlayBacker.OnPlayTransformDataStart -= SetPlayColor;
            transformPlayBacker.OnPlayTransformDataEnd -= SetWarmUpColor;
        }
    }

    /// <summary>
    /// Starts the metronome coroutine if it's not already running.
    /// </summary>
    public void StartMetronome()
    {
        if (metronomeCoroutine == null)
        {
            metronomeCoroutine = StartCoroutine(MetronomeRoutine());
        }
    }

    /// <summary>
    /// Stops the metronome coroutine if it's running.
    /// </summary>
    public void StopMetronome()
    {
        if (metronomeCoroutine != null)
        {
            StopCoroutine(metronomeCoroutine);
            metronomeCoroutine = null;
        }
    }

    /// <summary>
    /// The main coroutine for metronome operation.
    /// It plays sounds and highlights visual notes based on the selected metronome mode (4 beats or 16 beats).
    /// </summary>
    private IEnumerator MetronomeRoutine()
    {
        int beatCount = 0; // Tracks the current beat number.
        while (true)
        {
            float beatDuration = 60f / bpm; // Calculate the duration of a single beat in seconds.

            if (mode == MetronomeMode.FourBeats)
            {
                // 4-beat mode: Plays a sound for each quarter note.
                if (!isPaused) // isPaused is currently not actively used to pause the routine.
                {
                    if (beatCount % 4 == 0)
                    {
                        PlaySound(metronomeSfxHigh); // Play high SFX for the first beat of the measure.
                    }
                    else
                    {
                        PlaySound(metronomeSfxLow); // Play low SFX for other beats.
                    }

                    // Highlight the corresponding MetronomeNote (e.g., 0, 4, 8, 12 for quarter notes in a 16-note array).
                    HighlightMetronomeNoteAtIndex((beatCount % 4) * 4);

                    beatCount++;
                }
                yield return new WaitForSeconds(beatDuration); // Wait for the duration of one beat.
            }
            else if (mode == MetronomeMode.SixteenBeats)
            {
                // 16-beat mode: Divides each beat into four sub-beats.
                for (int subBeat = 0; subBeat < 4; subBeat++)
                {
                    if (!isPaused) // isPaused is currently not actively used to pause the routine.
                    {
                        if (subBeat == 0)
                        {
                            PlaySound(metronomeSfxHigh); // Play high SFX for the main beat (first sub-beat).
                        }
                        else
                        {
                            PlaySound(metronomeSfxLow); // Play low SFX for the sub-beats.
                        }

                        // Highlight the corresponding MetronomeNote based on the current beat and sub-beat.
                        HighlightMetronomeNoteAtIndex((beatCount % 4) * 4 + subBeat);
                    }
                    yield return new WaitForSeconds(beatDuration / 4f); // Wait for the duration of one sub-beat.
                }
                beatCount++;
            }
        }
    }

    /// <summary>
    /// Plays a given audio clip once through the AudioSource.
    /// </summary>
    /// <param name="clip">The AudioClip to play.</param>
    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// Highlights the MetronomeNote at the specified index and sets all other notes to their 'on' state.
    /// This creates a visual "pulse" effect.
    /// </summary>
    /// <param name="index">The index of the MetronomeNote to highlight.</param>
    private void HighlightMetronomeNoteAtIndex(int index)
    {
        for (int i = 0; i < metronomeNotes.Length; i++)
        {
            if (i == index)
            {
                metronomeNotes[i].SetHighlight(); // Set the specific note to its highlighted state.
            }
            else
            {
                metronomeNotes[i].SetOn(); // Set other notes to their 'on' (active but not highlighted) state.
            }
        }
    }

    /// <summary>
    /// Sets the 'onColor' of all MetronomeNotes to the 'playColor'.
    /// This is typically called when playback starts.
    /// </summary>
    private void SetPlayColor()
    {
        SetMetronomeNotesColor(playColor);
    }

    /// <summary>
    /// Sets the 'onColor' of all MetronomeNotes to the 'warmUpColor'.
    /// This is typically called when playback stops or is in an idle state.
    /// </summary>
    private void SetWarmUpColor()
    {
        SetMetronomeNotesColor(warmUpColor);
    }

    /// <summary>
    /// Helper method to apply a specific color to the 'onColor' property of all MetronomeNotes.
    /// </summary>
    /// <param name="color">The color to set.</param>
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