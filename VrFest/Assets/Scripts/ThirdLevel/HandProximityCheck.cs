using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class HandProximityCheck : MonoBehaviour
{
    public float maxDistanceForTogether = 0.15f;
    public float successDuration = 2.0f;

    private InputDevice leftController;
    private InputDevice rightController;
    private float togetherTimer = 0f;
    public bool wasTogether = false;

    public static HandProximityCheck instance;
    public Slider Slider;
    public bool AreHandsTogether => togetherTimer >= successDuration;
    public float CurrentHandDistance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GetControllers();
    }

    void Update()
    {
        if (!leftController.isValid || !rightController.isValid)
            GetControllers();

        CheckHandsTogether();
    }

    void GetControllers()
    {
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void CheckHandsTogether()
    {
        if (GetControllerPosition(leftController, out Vector3 leftPos) &&
            GetControllerPosition(rightController, out Vector3 rightPos))
        {
            CurrentHandDistance = Vector3.Distance(leftPos, rightPos);

            if (CurrentHandDistance < maxDistanceForTogether)
            {
                togetherTimer += Time.deltaTime;
                Slider.gameObject.SetActive(true);
                Slider.value = togetherTimer / 2;

                if (togetherTimer >= successDuration && !wasTogether)
                {
                    HandsTogetherSuccess();
                    wasTogether = true;
                    ThirdLevelController.instance.HandsSet();
                    DisActiveSlider();
                }
            }
            else
            {
                togetherTimer = 0f;
                wasTogether = false;
                DisActiveSlider();
            }
        }
    }

    bool GetControllerPosition(InputDevice device, out Vector3 position)
    {
        if (device.isValid && device.TryGetFeatureValue(CommonUsages.devicePosition, out position))
        {
            return true;
        }
        position = Vector3.zero;
        return false;
    }

    void HandsTogetherSuccess()
    {
        Debug.Log("Pico hands together - success!");
    }

    void DisActiveSlider()
    {
        Slider.gameObject.SetActive(false);
        Slider.value = 0;
    }
}