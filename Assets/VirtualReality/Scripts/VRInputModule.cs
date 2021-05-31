using UnityEngine;
using UnityEngine.EventSystems;

namespace BreadAndButter.VR
{
    public class VRInputModule : BaseInputModule
    {
        public Vector3 ControllerPosition { get; set; }
        public bool ControllerButtonDown { get; set; }
        public bool ControllerButtonUp { get; set; }

        private GameObject currentObject = null;
        private PointerEventData data = null;
        private new Camera camera;

        protected override void Awake()
        {
            base.Awake();

            data = new PointerEventData(eventSystem);
        }

        protected override void Start()
        {
            base.Start();

            camera = VRRig.instance.Headset.GetComponent<Camera>();
        }

        //this is the same as the update loop for the input modules
        public override void Process()
        {
            data.Reset();
            data.position = camera.WorldToScreenPoint(ControllerPosition);

            //Raycast
            eventSystem.RaycastAll(data, m_RaycastResultCache);
            data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            currentObject = data.pointerCurrentRaycast.gameObject;

            //Clear the raycast data
            m_RaycastResultCache.Clear();

            //Handle hovering for selectable UI elements
            HandlePointerExitAndEnter(data, currentObject);

            //handle press and releasing of the controller buttons
            if (ControllerButtonDown)
            {
                ProcessPress();
            }
            if (ControllerButtonUp)
            {
                ProcessRelease();
            }

            //reset the button flags to prevent multiple calling of the events
            ControllerButtonDown = false;
            ControllerButtonUp = false;
        }

        private void ProcessPress()
        {
            //set the press raycast to the current raycast
            data.pointerPressRaycast = data.pointerCurrentRaycast;

            //check for the hit object, get the down handler and call it
            GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.pointerDownHandler);

            //if no down handler was found, try and get the click handler
            if(newPointerPress == null)
            {
                newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);


            }

            //copy the pointer event data into the data variable
            data.pressPosition = data.position;
            data.pointerPress = newPointerPress;
            data.rawPointerPress = currentObject;
        }

        private void ProcessRelease()
        {
            //Executing the pointer up function
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

            //check for a click handler
            GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

            //check if the clicked object matches the one that was set in the press fuinction
            if(data.pointerPress == pointerUpHandler)
            {
                ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);

                
            }

            //clear the selected gameObject and reset the pointer data
            eventSystem.SetSelectedGameObject(null);
            data.pressPosition = Vector2.zero;
            data.pointerPress = null;
            data.rawPointerPress = null;
        }
    }
}
