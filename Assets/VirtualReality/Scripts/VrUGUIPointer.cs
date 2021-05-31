using UnityEngine;
using Valve.VR;
namespace BreadAndButter.VR
{
    [RequireComponent(typeof(VRControllerInput))]
    public class VrUGUIPointer : MonoBehaviour
    {
        [SerializeField] private SteamVR_Action_Boolean clickAction;
        [SerializeField] private LayerMask uiMask = LayerMask.NameToLayer("UI");
        [SerializeField] private Pointer pointer; 
        private VRInputModule inputModule;

        // Start is called before the first frame update
        void Start()
        {
            
            inputModule = FindObjectOfType<VRInputModule>();
        }

        // Update is called once per frame
        void Update()
        {
            inputModule.ControllerButtonDown = clickAction.stateDown;
            inputModule.ControllerButtonUp = clickAction.stateUp;

            Vector3 position = Vector3.zero;
            bool hitUI = false;
            if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, uiMask))
            {
                position = hit.point;
                hitUI = true;
            }

            inputModule.ControllerPosition = position;
            if(pointer != null)
            {
                pointer.Active = hitUI;
            }
        }
    }
}
