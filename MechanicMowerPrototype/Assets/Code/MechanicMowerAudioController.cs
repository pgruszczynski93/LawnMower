using System;
using System.Collections;
using UnityEngine;

namespace GardenFlipperMower {
    public class MechanicMowerAudioController : MonoBehaviour {
        const float MIN_AUDIO_VALUE = 0.75f;
        const float MAX_AUDIO_VALUE = 1.0f;

        bool isMowerWorking;
        bool isMowerMowing;

        [Range(0.5f, 2f)][SerializeField] float soundEffectMultiplier;
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

            StartCoroutine(FadeAudio(0.25f, 1.75f, () => { SetAudioSource(startEngineClip); }));
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
            var scaledSoundMultiplier = dt * soundEffectMultiplier;
            isMowerMowing = (movementVector.magnitude > 0);
            var pitch = engineAudioSource.pitch;
            var canIncreaseAudioEffects = isMowerMowing && pitch < MAX_AUDIO_VALUE;
            var canDecreaseAudioEffects = isMowerMowing == false && pitch > MIN_AUDIO_VALUE;
            
            if (canIncreaseAudioEffects) {
                UpdateVolumeAndPitch(scaledSoundMultiplier);
            }
            else if (canDecreaseAudioEffects) {
                UpdateVolumeAndPitch(-scaledSoundMultiplier);
            }
        }

        void UpdateVolumeAndPitch(float scaledSoundMultiplier) {
            engineAudioSource.pitch += scaledSoundMultiplier;
            engineAudioSource.volume += scaledSoundMultiplier;
            engineAudioSource.pitch = Mathf.Clamp(engineAudioSource.pitch, MIN_AUDIO_VALUE, MAX_AUDIO_VALUE);
            engineAudioSource.volume = Mathf.Clamp(engineAudioSource.volume, MIN_AUDIO_VALUE, MAX_AUDIO_VALUE);
        }

        IEnumerator FadeAudio(float duration, float fadeDelay = 0.0f, Action onFadeStart = null,
            Action onFadeFinished = null) {
            onFadeStart?.Invoke();
            
            yield return new WaitForSeconds(fadeDelay);
            var time = 0.0f;
            var pitch = engineAudioSource.pitch;
            var volume = engineAudioSource.volume;
            while (time < duration) {
                time += Time.deltaTime;
                var lerpStep = time / duration;
                engineAudioSource.pitch = Mathf.Lerp(pitch, MIN_AUDIO_VALUE, lerpStep);
                engineAudioSource.volume = Mathf.Lerp(volume, MIN_AUDIO_VALUE, lerpStep);
                yield return null;
            }

            onFadeFinished?.Invoke();
        }
    }
}