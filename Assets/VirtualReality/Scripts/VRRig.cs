using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadAndButter.VR
{
    public class VRRig : MonoBehaviour
    {
        public static VRRig instance = null;

        public Transform LeftController => leftController;
        public Transform RightController => rightController;
        public Transform Headset => headset;
        public Transform PlayArea => playArea;



        [SerializeField]
        private Transform leftController;
        [SerializeField]
        private Transform rightController;
        [SerializeField]
        private Transform headset;
        [SerializeField]
        private Transform playArea;

        private VRController left;
        private VRController right;

        private void OnValidate()
        {
            //check if the set object isnt a VRController if it isnt unset it and warn the user.
            if(leftController != null && leftController.GetComponent<VRController>() == null)
            {
                
                //the object set to this variable is not of type VrController
                leftController = null;
                Debug.LogWarning("The object you are trying to set to leftController does not have VrController component on it!");
                
            }
            //check if the set object isnt a VRController if it isnt unset it and warn the user.
            if (rightController != null && rightController.GetComponent<VRController>() == null)
            {

                //the object set to this variable is not of type VrController
                rightController = null;
                Debug.LogWarning("The object you are trying to set to rightController does not have VrController component on it!");

            }
        }

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            //Validate all the transform components
            ValidateComponent(leftController);
            ValidateComponent(rightController);
            ValidateComponent(headset);
            ValidateComponent(playArea);

            //get the VRcontrollerComponents from the relevant controller
            left = leftController.GetComponent<VRController>();
            right = rightController.GetComponent<VRController>();

            left.Initialise();
            right.Initialise();

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void ValidateComponent<T>(T _component) where T : Component
        {
            //if the component is null then log out the name of the component in an error
            if(_component == null)
            {
                Debug.LogError($"Component {nameof(_component)} is null! This has to be set");
#if UNITY_EDITOR
                //the component was null and we are in the editor so stop the editor from playing
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }

    }
}
