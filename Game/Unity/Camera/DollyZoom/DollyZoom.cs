using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyZoom : MonoBehaviour {
    public Transform target;
    public Camera camera;

    private float initHeightAtDist;
    private bool dzEnabled;

    // 计算离相机指定距离的锥体高度。
    float FrustumHeightAtDistance(float distance) {
        return 2.0f * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad) * distance ;//三角函数
    }

    // 根据指定的距离和指定的锥体高度,计算所需的FOV
    float FOVForHeightAndDistance(float height, float distance)
    {
        return 2.0f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
    }

    // Start the dolly zoom effect.
    void StartDZ()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        initHeightAtDist = FrustumHeightAtDistance(distance);// 首先获取锥体高度。
        dzEnabled = true;
    }
    // Turn dolly zoom off.
    void StopDZ()
    {
        dzEnabled = false;
    }
    // Use this for initialization
    void Start () {
        StartDZ();
	}
	
	// Update is called once per frame
	void Update () {
        if (dzEnabled) {
            // Measure the new distance and readjust the FOV accordingly.
            float distance = Vector3.Distance(transform.position, target.position);
            camera.fieldOfView  = FOVForHeightAndDistance(initHeightAtDist, distance);
           // 不断刷新相机的FOV
        }

        // Simple control to allow the camera to be moved in and out using the up/down arrows.
        transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * Time.deltaTime * 5f);
    }
}
