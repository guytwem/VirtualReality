using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Serializable = System.SerializableAttribute;


namespace BreadAndButter.VR
{
    [Serializable]
    public class VRInputEvent : UnityEvent<InputEventArgs> { }


    [Serializable]
    public class InputEventArgs
    {
        /// <summary>
        /// the controller firing the event
        /// </summary>
        public VRController controller;

        /// <summary>
        /// the input source the event is coming from
        /// </summary>
        public SteamVR_Input_Sources source;

        /// <summary>
        /// the position the player is touching the touchpad on
        /// </summary>
        public Vector2 touchpadAxis;

        public InputEventArgs(VRController _controller, SteamVR_Input_Sources _source, Vector2 _touchpadAxis)
        {
            controller = _controller;
            source = _source;
            touchpadAxis = _touchpadAxis;
        }
    }
}