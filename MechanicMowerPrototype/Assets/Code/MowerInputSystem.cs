using UnityEngine;

namespace GardenFlipperMower {
    public class MowerInputSystem : MonoBehaviour{
        
        [SerializeField] float horizontalInput;
        [SerializeField] float verticalInput;

        void OnEnable() {
            MechanicMowerEvents.OnCustomUpdate += StartMoweInput;
            MechanicMowerEvents.OnCustomUpdate += FinishMoweInput;
            
            MechanicMowerEvents.OnCustomFixedUpdate += UpdateMowerMovementInput;

        }

        void OnDisable() {
            MechanicMowerEvents.OnCustomUpdate -= StartMoweInput;
            MechanicMowerEvents.OnCustomUpdate -= FinishMoweInput;
            
            MechanicMowerEvents.OnCustomFixedUpdate -= UpdateMowerMovementInput;
        }
        
        void UpdateMowerMovementInput() {
//            horizontalInput = Input.GetAxis("Horizontal");
//            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            var normalizedInputVector = new Vector3(horizontalInput, verticalInput, 0f).normalized;
            
            MechanicMowerEvents.BroadcastOnMovementUpdate(normalizedInputVector);
        }

        void StartMoweInput() {
            if (Input.GetKeyDown(KeyCode.E)) {
                MechanicMowerEvents.BroadcastOnMechanicMowerStarted();
            }
        }
        
        void FinishMoweInput() {
            if (Input.GetKeyDown(KeyCode.F)) {
                MechanicMowerEvents.BroadcastOnMechanicMowerFinished();
            }
        }
    }
}