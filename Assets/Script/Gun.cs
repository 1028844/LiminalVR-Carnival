using UnityEngine;
using Liminal.Core.Fader;
using Liminal.Platform.Experimental.App.Experiences;
using Liminal.SDK.Core;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Input;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform _barrel;
    [SerializeField] LayerMask _attackableLayers;

    public int score = 0;

    private void Update()
    {
        var avatar = VRAvatar.Active;
        if (avatar == null)
            return;

        var rightInput = GetInput(VRInputDeviceHand.Right);
        var leftInput = GetInput(VRInputDeviceHand.Left);

        // Input Examples
        if (rightInput != null)
        {
            if (rightInput.GetButtonDown(VRButton.Back))
                Debug.Log("Back button pressed");

            if (rightInput.GetButtonDown(VRButton.One))
            FindObjectOfType<AudioManager>().Play("Shoot");
                Shoot();
        }

        if (leftInput != null)
        {
            if (leftInput.GetButtonDown(VRButton.Back))
                Debug.Log("Back button pressed");

            if (leftInput.GetButtonDown(VRButton.One))
                Debug.Log("Trigger button pressed");
        }

        // Any input
        // VRDevice.Device.GetButtonDown(VRButton.One);
    }

    private IVRInputDevice GetInput(VRInputDeviceHand hand)
    {
        var device = VRDevice.Device;
        return hand == VRInputDeviceHand.Left ? device.SecondaryInputDevice : device.PrimaryInputDevice;
    }

    public void Shoot()
    {
        RaycastHit hit;

        Physics.Raycast(_barrel.transform.position, _barrel.forward, out hit, Mathf.Infinity, _attackableLayers);

        if(hit.transform != null)
        {
            hit.transform.GetComponent<Target>().Hit();
            score += hit.transform.GetComponent<Target>().scoreReward;
        }
    }
}
