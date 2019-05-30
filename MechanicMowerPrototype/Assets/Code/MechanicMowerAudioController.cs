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
                return;
            }

            SetAudioSource(startEngineClip);
        }

        void PlayEngineLoopedSource(Vector3 movementVector) {
            TryToEnableLoopedEngineAudio();
            TryToUpdateLoopedEngineAudio(movementVector);
        }

        void TryToUpdateLoopedEngineAudio(Vector3 movementVector) {
            if (isMowerWorking) {
                UpdateLoopedEngineSource(movementVector);
            }
        }

        void TryToEnableLoopedEngineAudio() {
            if (isMowerWorking == false) {
                return;
            }

            if (engineAudioSource.isPlaying == false) {
                SetAudioSource(loopedEngineClip, true);
            }
        }

        void PlayEngineTurnOff() {
            if (isMowerWorking == false) {
                StopAllCoroutines();
                return;
            }

            SetAudioSource(endEngineClip);
        }

        void SetAudioSource(AudioClip clip, bool isLooped = false) {
            engineAudioSource.clip = clip;
            engineAudioSource.loop = isLooped;
            engineAudioSource.Play();
        }

        void UpdateLoopedEngineSource(Vector3 movementVector) {
            var dt = Time.deltaTime;
            soundMultiplier = Mathf.Max(movementVector.x, movementVector.y);
            var scaledSoundMultiplier = soundMultiplier * dt * 10;
            Debug.Log(soundMultiplier + " " + scaledSoundMultiplier + " " + movementVector);
        }
    }
}