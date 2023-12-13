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
    [SerializeField] Animator gunAnimatorRH;
    [SerializeField] bool leftHand = false;

    AudioSource _gunshotSound;

    static public int score = 0;

    bool _ableToShoot = true;
    [SerializeField] float timeBeforeShoot = 0.5f;
    float _tempTimeBeforeShoot = 0.0f;

    private void Start()
    {
        _gunshotSound = GetComponent<AudioSource>();
        _tempTimeBeforeShoot = timeBeforeShoot;
    }

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
                if (!leftHand) Shoot();
        }

        if (leftInput != null)
        {
            if (leftInput.GetButtonDown(VRButton.Back))
                Debug.Log("Back button pressed");

            if (leftInput.GetButtonDown(VRButton.One))
                if (leftHand) Shoot();
        }

        // Any input
        // VRDevice.Device.GetButtonDown(VRButton.One);

        if (!_ableToShoot)
        {
            if (_tempTimeBeforeShoot > 0)
            {
                _tempTimeBeforeShoot -= Time.deltaTime;
            }
            else
            {
                _ableToShoot = true;
                _tempTimeBeforeShoot = timeBeforeShoot;
            }
        }
    }

    private IVRInputDevice GetInput(VRInputDeviceHand hand)
    {
        var device = VRDevice.Device;
        return hand == VRInputDeviceHand.Left ? device.SecondaryInputDevice : device.PrimaryInputDevice;
    }

    public void Shoot()
    {
        if (!_ableToShoot) return;

        RaycastHit hit;

        Physics.Raycast(_barrel.transform.position, _barrel.forward, out hit, Mathf.Infinity, _attackableLayers);

        _gunshotSound.Play();
        gunAnimatorRH.Play("GunShoot");

        if(hit.transform != null)
        {
            hit.transform.GetComponent<Target>().Hit();
            score += hit.transform.GetComponent<Target>().scoreReward;
        }

        _ableToShoot = false;
    }
}
