using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cmVirtCam;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        cmVirtCam = GetComponent<CinemachineVirtualCamera>();
    }
}
