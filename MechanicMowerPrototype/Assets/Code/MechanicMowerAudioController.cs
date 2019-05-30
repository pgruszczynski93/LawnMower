using System;
using System.Collections;
using UnityEngine;

namespace GardenFlipperMower {
    public class MechanicMowerAudioController : MonoBehaviour {
        const float MIN_ENGINE_PITCH = 0.3f;
        const float MAX_ENGINE_PITCH = 1.0f;
        const float START_SOUND_DELAY = 2.0f;
        const float END_SOUND_DELAY = 1.8f;

        bool isMowerWorking;

        [Range(0.2f, 1f)] [SerializeField] float engineSoundTreshold;
        [SerializeField] float soundMultiplier;

        [SerializeField] AudioSource engineAudioSource;

        [SerializeField] AudioClip startEngineClip;
        [SerializeField] AudioClip loopedEngineClip;
        [SerializeField] AudioClip endEngineClip;

        void Start() {
            Initialise();
        }

        void OnEnable() {
            AssignEvents();
        }

        void OnDisable() {
            RemoveEvents();
        }

        void OnDestroy() {
            StopAllCoroutines();
        }

        void Initialise() {
            if (engineAudioSource == null) {
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
                StopAllCoroutines();
                return;
            }

            engineAudioSource.clip = startEngineClip;
            engineAudioSource.Play();
        }

        void PlayEngineLoopedSource(Vector3 movementVector) {
            if (isMowerWorking == false) {
                return;
            }

            if (engineAudioSource.isPlaying == false) {
                engineAudioSource.clip = loopedEngineClip;
                engineAudioSource.loop = true;
                engineAudioSource.Play();
            }

            if (isMowerWorking) {
                UpdateLoopedEngineSource(movementVector);
            }
        }

        void PlayEngineTurnOff() {
            if (isMowerWorking == false) {
                StopAllCoroutines();
                return;
            }

            engineAudioSource.clip = endEngineClip;
            engineAudioSource.loop = false;
            engineAudioSource.Play();
//            mowerLoopedSource.Stop();
//            mowerEndSource.Play();
////
//        StartCoroutine(PlayDelayedSource(END_SOUND_DELAY, () => {
//            mowerLoopedSource.Stop();
//            mowerEndSource.Play();
//        }));
        }

        void UpdateLoopedEngineSource(Vector3 movementVector) {
            var dt = Time.deltaTime;
            soundMultiplier = Mathf.Max(movementVector.x, movementVector.y);
            var scaledSoundMultiplier = soundMultiplier * dt * 10;
            Debug.Log(soundMultiplier + " " + scaledSoundMultiplier + " " + movementVector);

//        if (soundMultiplier >= engineSoundTreshold && mowerLoopedSource.pitch < MAX_ENGINE_PITCH) {
//            mowerLoopedSource.volume += scaledSoundMultiplier;
//            mowerLoopedSource.pitch += scaledSoundMultiplier;
//        }
//        else if (soundMultiplier < engineSoundTreshold && mowerLoopedSource.pitch > MIN_ENGINE_PITCH) {
//            mowerLoopedSource.volume += scaledSoundMultiplier;
//            mowerLoopedSource.pitch += scaledSoundMultiplier;
//        }
//
//        mowerLoopedSource.volume = Mathf.Clamp(mowerLoopedSource.volume, MIN_ENGINE_PITCH, MAX_ENGINE_PITCH);
//        mowerLoopedSource.pitch = Mathf.Clamp(mowerLoopedSource.pitch, MIN_ENGINE_PITCH, MAX_ENGINE_PITCH);
        }

        IEnumerator PlayDelayedSource(float delay, Action onSourceChanged = null) {
            yield return new WaitForSeconds(delay);
            onSourceChanged?.Invoke();
        }
        
    }
}