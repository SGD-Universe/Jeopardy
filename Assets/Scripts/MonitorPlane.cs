using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonitorPlane : MonoBehaviour
{
    [SerializeField] private float animationLerpFactor;
    private float animationLerpFactorAdjusted;
    private int animationDirection = 1;

    private Animator animator;
    
    private Vector3 baseScale;
    private Vector3 originalScale;
    private Vector3 hoverScale;
    private Vector3 fullscreenScale;

    private Vector3 originalPosition;
    private Vector3 fullscreenPosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = true;

        originalScale = fullscreenScale = transform.localScale;
        originalPosition = fullscreenPosition = transform.localPosition;

        fullscreenScale = originalScale * 6.2f;
        fullscreenPosition = Vector3.zero;



    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(animationDirection == 1) animationLerpFactorAdjusted = animationLerpFactor;
        else animationLerpFactorAdjusted = 1 - animationLerpFactor;

        transform.localPosition = Vector3.Lerp(originalPosition, fullscreenPosition, animationLerpFactorAdjusted);
        transform.localScale = Vector3.Lerp(originalScale, fullscreenScale, animationLerpFactorAdjusted);

        */
        if(Input.GetKeyDown(KeyCode.Escape)) OnEscapeDown();
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        
    }

    private void OnMouseDown()
    {
        animator.Play("MonitorPlaneFullscreen");
        /*
        animationDirection = 1;
        animationLerpFactor = 0f;
        animator.enabled = true;
        animator.Play(0);

        // targetScale = originalScale * 6.2f;
        // targetPosition = Vector3.zero;
        */
    }

    private void OnEscapeDown()
    {
        animator.Play("MonitorPlaneMinimize");

        /*
        animationDirection = -1;
        animationLerpFactor = 0f;
        animator.Play(0);

        // targetScale = originalScale;
        // targetPosition = originalPosition;
        */
    }
}
