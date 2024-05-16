using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings Instance { get; private set; }

    // Sensitivity - Controls the sensitivity of the camera
    public float _horizontalSensitivity { get; set; } = 0.1f;
    public float _verticalSensitivity { get; set; } = 0.1f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
    }
}
