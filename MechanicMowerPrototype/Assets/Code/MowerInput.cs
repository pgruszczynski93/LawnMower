using UnityEngine;

namespace GardenFlipperMower {
    public class MowerInput : MonoBehaviour{
        
        [SerializeField] float horizontalInput;
        [SerializeField] float verticalInput;

        void OnEnable() {
            MechanicMowerEvents.OnCustomUpdate += GetInput;
        }

        void OnDisable() {
            MechanicMowerEvents.OnCustomUpdate -= GetInput;
        }
        
        void GetInput() {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            var inputVector = new Vector3(horizontalInput, verticalInput, 0f);
            
            MechanicMowerEvents.BroadcastOnPositionUpdate(inputVector);
        }
    }
}