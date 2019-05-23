using UnityEngine;

namespace GardenFlipperMower {
    public class MowerInput : MonoBehaviour{
        
        [SerializeField] float horizontalInput;
        [SerializeField] float verticalInput;

        void OnEnable() {
            MechanicMowerEvents.OnMechanicMowerUpdate += GetInput;
        }

        void OnDisable() {
            MechanicMowerEvents.OnMechanicMowerUpdate -= GetInput;
        }
        
        void GetInput() {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            var inputVector = new Vector3(horizontalInput, verticalInput, 0f);
            
            MechanicMowerEvents.BroadcastOnPositionUpdate(inputVector);
        }
    }
}