using Unity.Cinemachine;
using UnityEngine;

public class CameraLogic
{
    readonly CinemachineThirdPersonFollow follow;
    readonly PlayerConfig config;

    float currentDistance;

    public CameraLogic(CinemachineCamera camera, PlayerConfig config)
    {
        this.config = config;

        follow =
            camera.GetComponent<CinemachineThirdPersonFollow>()
            ?? camera.GetComponentInChildren<CinemachineThirdPersonFollow>();

        if (follow != null)
            currentDistance = follow.CameraDistance;
    }

    public void Tick(bool isSprinting)
    {
        if (follow == null)
            return;

        float targetDistance = isSprinting
            ? config.sprintCameraDistance
            : config.normalCameraDistance;

        currentDistance = Mathf.Lerp(
            currentDistance,
            targetDistance,
            10f * Time.deltaTime);

        follow.CameraDistance = currentDistance;
    }
}