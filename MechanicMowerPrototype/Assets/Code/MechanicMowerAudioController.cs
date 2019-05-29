using System.Collections;
using GardenFlipperMower;
using UnityEngine;

public class MechanicMowerAudioController : MonoBehaviour {
    const float MIN_ENGINE_PITCH = 0.3f;
    const float MAX_ENGINE_PITCH = 1.0f;

    bool isMowerWorking;
    
    [Range(0.2f,1f)][SerializeField] float engineSoundTreshold;
    [SerializeField] float soundMultiplier; 
    
    [SerializeField] AudioSource mowerStartSource;
    [SerializeField] AudioSource mowerEndSource;
    [SerializeField] AudioSource mowerLoopedSource;

    [SerializeField] AudioClip loopedEngineClip;

    void Start() {
        Initialise();
    }

    void OnEnable() {
        AssignEvents();
    }

    void OnDisable() {
        RemoveEvents();
    }

    void Initialise() {
        if (mowerEndSource == null || mowerEndSource == null || mowerLoopedSource == null || loopedEngineClip == null) {
            Debug.LogError("Assign proper components first.");
            return;
        }

        isMowerWorking = false;
    }

    void AssignEvents() {
        MechanicMowerEvents.OnMechanicMowerStarted += PlayEngineTurnOn;
        MechanicMowerEvents.OnMechanicMowerStarted += TurnOnMower;
        MechanicMowerEvents.OnMechanicMowerFinished += PlayEngineTurnOff;
        MechanicMowerEvents.OnMechanicMowerFinished += TurnOffMower;
        MechanicMowerEvents.OnMovementUpdate += PlayEngineLoopedSource;

    }

    void RemoveEvents() {
        MechanicMowerEvents.OnMechanicMowerStarted -= PlayEngineTurnOn;
        MechanicMowerEvents.OnMechanicMowerStarted -= TurnOnMower;
        MechanicMowerEvents.OnMechanicMowerFinished -= PlayEngineTurnOff;
        MechanicMowerEvents.OnMechanicMowerFinished -= TurnOffMower;
        MechanicMowerEvents.OnMovementUpdate -= PlayEngineLoopedSource;
    }

    void TurnOnMower() {
        isMowerWorking = true;
    }

    void TurnOffMower() {
        isMowerWorking = false;
    }
    
    void PlayEngineTurnOn() {
        if (isMowerWorking) {
            return;
        }
        
        mowerStartSource.Play();
        
    }

    void PlayEngineLoopedSource(Vector3 movementVector) {
        if (isMowerWorking == false) {
            return;
        }
        
        if (mowerLoopedSource.isPlaying == false) {
            mowerLoopedSource.Play();
        }

        if (isMowerWorking) {
            UpdateLoopedEngineSource(movementVector);
        }
    }

    void PlayEngineTurnOff() {
        if (isMowerWorking == false) {
            return;
        }
        mowerLoopedSource.Stop();
        mowerEndSource.Play();
    }

    void UpdateLoopedEngineSource(Vector3 movementVector) {
        var dt = Time.deltaTime;
        soundMultiplier = Mathf.Max(movementVector.x, movementVector.y);
        var scaledSoundMultiplier = soundMultiplier * dt * 2;
        Debug.Log(soundMultiplier);

        if (soundMultiplier >= engineSoundTreshold && mowerLoopedSource.pitch < MAX_ENGINE_PITCH) {

            mowerLoopedSource.volume += scaledSoundMultiplier;
            mowerLoopedSource.pitch += scaledSoundMultiplier;
            mowerLoopedSource.volume = Mathf.Clamp(mowerLoopedSource.volume, MIN_ENGINE_PITCH, MAX_ENGINE_PITCH);
            mowerLoopedSource.pitch = Mathf.Clamp(mowerLoopedSource.pitch, MIN_ENGINE_PITCH, MAX_ENGINE_PITCH);
        }
        else if(soundMultiplier < engineSoundTreshold && mowerLoopedSource.pitch > MIN_ENGINE_PITCH){
            mowerLoopedSource.volume -= scaledSoundMultiplier;
            mowerLoopedSource.pitch -= scaledSoundMultiplier;
            mowerLoopedSource.volume = Mathf.Clamp(mowerLoopedSource.volume, MIN_ENGINE_PITCH, MAX_ENGINE_PITCH);
            mowerLoopedSource.pitch = Mathf.Clamp(mowerLoopedSource.pitch, MIN_ENGINE_PITCH, MAX_ENGINE_PITCH);
        }
    }
}