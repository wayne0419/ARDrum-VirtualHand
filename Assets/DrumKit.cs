using UnityEngine;
using UnityEngine.InputSystem;

public class DrumKit : MonoBehaviour
{
    public InputAction hitBassDrum;
    public InputAction hitSnareDrum;
    public InputAction hitTom1;
    public InputAction hitTom2;
    public InputAction hitFloorTom;
    public InputAction hitClosedHiHat;
    public InputAction hitCrash;
    public InputAction hitRide;

    public AudioSource bassDrumSource;
    public AudioSource snareDrumSource;
    public AudioSource tom1Source;
    public AudioSource tom2Source;
    public AudioSource floorTomSource;
    public AudioSource closedHiHatSource;
    public AudioSource crashSource;
    public AudioSource rideSource;

    void OnEnable()
    {
        hitBassDrum.Enable();
        hitSnareDrum.Enable();
        hitTom1.Enable();
        hitTom2.Enable();
        hitFloorTom.Enable();
        hitClosedHiHat.Enable();
        hitCrash.Enable();
        hitRide.Enable();
    }

    void OnDisable()
    {
        hitBassDrum.Disable();
        hitSnareDrum.Disable();
        hitTom1.Disable();
        hitTom2.Disable();
        hitFloorTom.Disable();
        hitClosedHiHat.Disable();
        hitCrash.Disable();
        hitRide.Disable();
    }

    void Update()
    {
        if (hitBassDrum.triggered)
        {
            PlayAudio(bassDrumSource, hitBassDrum.ReadValue<float>());
        }

        if (hitSnareDrum.triggered)
        {
            PlayAudio(snareDrumSource, hitSnareDrum.ReadValue<float>());
        }

        if (hitTom1.triggered)
        {
            PlayAudio(tom1Source, hitTom1.ReadValue<float>());
        }

        if (hitTom2.triggered)
        {
            PlayAudio(tom2Source, hitTom2.ReadValue<float>());
        }

        if (hitFloorTom.triggered)
        {
            PlayAudio(floorTomSource, hitFloorTom.ReadValue<float>());
        }

        if (hitClosedHiHat.triggered)
        {
            PlayAudio(closedHiHatSource, hitClosedHiHat.ReadValue<float>());
        }

        if (hitCrash.triggered)
        {
            PlayAudio(crashSource, hitCrash.ReadValue<float>());
        }

        if (hitRide.triggered)
        {
            PlayAudio(rideSource, hitRide.ReadValue<float>());
        }
    }

    void PlayAudio(AudioSource audioSource, float value)
    {
        // You can adjust the volume or other parameters using the value if needed
        audioSource.volume = value;
        audioSource.PlayOneShot(audioSource.clip);
    }
}
