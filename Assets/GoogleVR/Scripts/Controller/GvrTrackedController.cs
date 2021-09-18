

using System.Collections;
using Gvr.Internal;
using UnityEngine;


[HelpURL("https://developers.google.com/vr/reference/unity/class/GvrTrackedController")]
public class GvrTrackedController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Arm model used to control the pose (position and rotation) of the object, " +
    "and to propagate to children that implement IGvrArmModelReceiver.")]
    private GvrBaseArmModel armModel;
    private GvrControllerInputDevice controllerInputDevice;

    [SerializeField]
    [Tooltip("Is the object's active status determined by the controller connection status.")]
    private bool isDeactivatedWhenDisconnected = true;

    [SerializeField]
    [Tooltip("Controller Hand")]
    private GvrControllerHand controllerHand = GvrControllerHand.Dominant;

    public GvrControllerInputDevice ControllerInputDevice
    {
        get
        {
            return controllerInputDevice;
        }
    }

    public GvrControllerHand ControllerHand
    {
        get
        {
            return controllerHand;
        }

        set
        {
            if (value != controllerHand)
            {
                controllerHand = value;
                SetupControllerInputDevice();
            }
        }
    }


    public GvrBaseArmModel ArmModel
    {
        get
        {
            return armModel;
        }

        set
        {
            if (armModel == value)
            {
                return;
            }

            armModel = value;
            PropagateControllerInputDeviceToArmModel();
            PropagateArmModel();
        }
    }

    public bool IsDeactivatedWhenDisconnected
    {
        get
        {
            return isDeactivatedWhenDisconnected;
        }

        set
        {
            if (isDeactivatedWhenDisconnected == value)
            {
                return;
            }

            isDeactivatedWhenDisconnected = value;

            if (isDeactivatedWhenDisconnected)
            {
                OnControllerStateChanged(controllerInputDevice.State, controllerInputDevice.State);
            }
        }
    }

    [SuppressMemoryAllocationError(
        IsWarning = false,
        Reason = "Only called when ArmModel is instantiated or changed.")]
    public void PropagateArmModel()
    {
        IGvrArmModelReceiver[] receivers =
            GetComponentsInChildren<IGvrArmModelReceiver>(true);

        for (int i = 0; i < receivers.Length; i++)
        {
            IGvrArmModelReceiver receiver = receivers[i];
            receiver.ArmModel = armModel;
        }
    }

    private void Awake()
    {
 
        GvrControllerInput.OnDevicesChanged += SetupControllerInputDevice;
    }

    private void OnEnable()
    {
        // Print an error to console if no GvrControllerInput is found.
        if (controllerInputDevice.State == GvrConnectionState.Error)
        {
            Debug.LogWarning(controllerInputDevice.ErrorDetails);
        }


        GvrControllerInput.OnPostControllerInputUpdated += OnPostControllerInputUpdated;


        UpdatePose();

        OnControllerStateChanged(controllerInputDevice.State, controllerInputDevice.State);
    }

    private void OnDisable()
    {
        GvrControllerInput.OnPostControllerInputUpdated -= OnPostControllerInputUpdated;
    }

    private void Start()
    {
        PropagateArmModel();
        if (controllerInputDevice != null)
        {
            PropagateControllerInputDevice();
            OnControllerStateChanged(controllerInputDevice.State, controllerInputDevice.State);
        }
    }

    private void OnDestroy()
    {
        GvrControllerInput.OnDevicesChanged -= SetupControllerInputDevice;
        if (controllerInputDevice != null)
        {
            controllerInputDevice.OnStateChanged -= OnControllerStateChanged;
            controllerInputDevice = null;
            PropagateControllerInputDevice();
        }
    }

    private void PropagateControllerInputDevice()
    {
        IGvrControllerInputDeviceReceiver[] receivers =
            GetComponentsInChildren<IGvrControllerInputDeviceReceiver>(true);

        foreach (var receiver in receivers)
        {
            receiver.ControllerInputDevice = controllerInputDevice;
        }

        PropagateControllerInputDeviceToArmModel();
    }

    private void PropagateControllerInputDeviceToArmModel()
    {
       
        if (armModel != null)
        {
            IGvrControllerInputDeviceReceiver[] receivers =
                armModel.GetComponentsInChildren<IGvrControllerInputDeviceReceiver>(true);

            foreach (var receiver in receivers)
            {
                receiver.ControllerInputDevice = controllerInputDevice;
            }
        }
    }

    private void SetupControllerInputDevice()
    {
        GvrControllerInputDevice newDevice = GvrControllerInput.GetDevice(controllerHand);
        if (controllerInputDevice == newDevice)
        {
            return;
        }

        if (controllerInputDevice != null)
        {
            controllerInputDevice.OnStateChanged -= OnControllerStateChanged;
            controllerInputDevice = null;
        }

        controllerInputDevice = newDevice;
        if (controllerInputDevice != null)
        {
            controllerInputDevice.OnStateChanged += OnControllerStateChanged;
            OnControllerStateChanged(controllerInputDevice.State, controllerInputDevice.State);
        }
        else
        {
            OnControllerStateChanged(GvrConnectionState.Disconnected,
                                     GvrConnectionState.Disconnected);
        }

        PropagateControllerInputDevice();
    }

    private void OnPostControllerInputUpdated()
    {
        UpdatePose();
    }

    private void OnControllerStateChanged(GvrConnectionState state, GvrConnectionState oldState)
    {
        if (isDeactivatedWhenDisconnected && enabled)
        {
            gameObject.SetActive(state == GvrConnectionState.Connected);
        }
    }

    private void UpdatePose()
    {
        if (controllerInputDevice == null)
        {
            return;
        }

        if (controllerInputDevice.SupportsPositionalTracking)
        {
            transform.localPosition = controllerInputDevice.Position;
            transform.localRotation = controllerInputDevice.Orientation;
        }
        else
        {
            if (armModel == null || !controllerInputDevice.IsDominantHand)
            {
                return;
            }

            transform.localPosition = ArmModel.ControllerPositionFromHead;
            transform.localRotation = ArmModel.ControllerRotationFromHead;
        }
    }
#if UNITY_EDITOR
    /// <summary>This MonoBehavior's `OnValidate` override.</summary>
    /// <remarks>
    /// If the `armModel` serialized field is changed while the application is playing by using the
    /// inspector in the editor, then we need to call `PropagateArmModel` to ensure all children
    /// `IGvrArmModelReceiver` are updated.
    /// <para>
    /// Outside of the editor, this can't happen because the arm model can only change when a Setter
    /// is called that automatically calls `PropagateArmModel`.
    /// </para></remarks>
    private void OnValidate()
    {
        if (Application.isPlaying && isActiveAndEnabled)
        {
            PropagateArmModel();
            if (controllerInputDevice != null)
            {
                OnControllerStateChanged(controllerInputDevice.State, controllerInputDevice.State);
            }
        }
    }
#endif 
}
