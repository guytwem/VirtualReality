using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace BreadAndButter.VR
{
    [RequireComponent(typeof(SteamVR_Behaviour_Pose))]
    [RequireComponent(typeof(VRControllerInput))]
    public class VRController : MonoBehaviour
    {
        public VRControllerInput Input => input;

        /// <summary>
        /// how fast the controller is moving in worldspace
        /// </summary>
        public Vector3 Velocity => pose.GetVelocity();

        /// <summary>
        /// how fast the controller is rotating in which direction
        /// </summary>
        public Vector3 AngularVelocity => pose.GetAngularVelocity();

        public SteamVR_Input_Sources InputSource => pose.inputSource;


        private SteamVR_Behaviour_Pose pose;
        private VRControllerInput input;

        public void Initialise()
        {
            pose = gameObject.GetComponent<SteamVR_Behaviour_Pose>();
            input = gameObject.GetComponent<VRControllerInput>();

            input.Initialise(this);
        }

      
    }
}
