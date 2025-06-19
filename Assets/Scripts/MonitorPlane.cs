using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MonitorPlane : MonoBehaviour
{
    // Used to animate the position of the panel to the center of the screen
    // (Grow/shrink animations are handled with an actual animator component)
    [Space(10)]
    [SerializeField] private bool isEditable = false;
    [Space(10)]
    [SerializeField] private float positionLerpFactor;
    [SerializeField] private float colorLerpFactor;
    [SerializeField] private Color errorColor;
    [SerializeField] private TMP_InputField primaryInputField;
    [SerializeField] private TMP_InputField secondaryInputField;
    [SerializeField] private TMP_Text dividerGraphic;
    [SerializeField] private TMP_FontAsset categoryFont;
    [SerializeField] private float inputFieldsSpacing = 0.5f;

    private TMP_Text placeholderText;
    private string primaryInputString = "";
    private string secondaryInputString = "";

    public enum Type
    {
        Category,
        Question
    }

    private Type type;

    private Animator animator;

    private Material material;
    private Color originalColor;
    private Vector3 originalPosition;
    private Vector3 fullscreenPosition;

    private bool isFullscreen = false;
    private bool isHovered = false;

    // Start is called before the first frame update
    void Start()
    {
        placeholderText = primaryInputField.placeholder as TMP_Text;

        animator = GetComponent<Animator>();
        animator.enabled = true;
        animator.SetLayerWeight(1, 1f);
        animator.SetLayerWeight(2, 1f);

        originalPosition = transform.localPosition;

        fullscreenPosition = new Vector3(0, 0, -1);

        material = GetComponent<MeshRenderer>().material;
        originalColor = material.color;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 calculatedPosition = Vector3.Lerp(originalPosition, fullscreenPosition, positionLerpFactor);
        if(isHovered || isFullscreen) calculatedPosition.z = -1f;
        else calculatedPosition.z = 0f;
        transform.localPosition = calculatedPosition;

        material.color = Color.Lerp(originalColor, errorColor, colorLerpFactor);

        if(isFullscreen && type == Type.Question && isEditable)
        {
            primaryInputField.transform.localPosition = new Vector3(-0.1f, inputFieldsSpacing, 0f);
            secondaryInputField.transform.localPosition = new Vector3(-0.1f, -inputFieldsSpacing, 0f);
        }
        else primaryInputField.transform.localPosition = Vector3.forward * -0.1f;

        primaryInputString = primaryInputField.text;
        secondaryInputString = secondaryInputField.text;

        if(Input.GetKeyDown(KeyCode.Escape)) OnEscapeDown();
        if(Input.GetKeyDown(KeyCode.Return) && isFullscreen) AudioManager.Instance.PlaySoundCorrect();
    }

    private void OnMouseOver()
    {
        if(!isHovered && !isFullscreen)
        {
            animator.CrossFadeInFixedTime("MonitorPlaneHover", 0.1f);
            isHovered = true;
        }

    }

    private void OnMouseExit()
    {
        if(!isFullscreen)
        {
            animator.CrossFadeInFixedTime("MonitorPlaneUnhover", 0.1f);
            isHovered = false;
        }

    }

    private void OnMouseDown()
    {
        if(!isFullscreen)
        {
            animator.Play("MonitorPlaneFullscreen");
            animator.Play("MonitorPlaneCenter");

            isFullscreen = true;
            isHovered = false;

            if(isEditable)
            {
                primaryInputField.interactable = true;
                if(type == Type.Question)
                {
                    primaryInputField.pointSize = 12f;
                    secondaryInputField.gameObject.SetActive(true);
                    secondaryInputField.interactable = true;
                    dividerGraphic.gameObject.SetActive(true);
                }
            }

        }
    }

    private void OnEscapeDown()
    {
        if(isFullscreen)
        {
            animator.Play("MonitorPlaneMinimize");
            animator.Play("MonitorPlaneUncenter");

            isFullscreen = false;

            if(isEditable)
            {
                primaryInputField.interactable = false;

                if(type == Type.Question)
                {
                    primaryInputField.pointSize = 18f;
                    secondaryInputField.gameObject.SetActive(false);
                    secondaryInputField.interactable = false;
                    secondaryInputField.DeactivateInputField();
                    dividerGraphic.gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetPanelType(Type type)
    {
        this.type = type;
        UpdatePlaceholderText();
    }

    public Type GetPanelType()
    {
        return this.type;
    }

    private void UpdatePlaceholderText()
    {
        if(primaryInputField == null) Debug.LogError("You forgot to assign a reference to the InputField in the MonitorPlane template, silly");
        else
        {
            if(placeholderText == null) placeholderText = primaryInputField.placeholder as TMP_Text;
            if(type == Type.Category)
            {
                primaryInputField.pointSize = 40f;
                RectTransform _rect = primaryInputField.gameObject.GetComponent<RectTransform>();
                Vector2 size = _rect.sizeDelta;
                size.y = 164f;
                _rect.sizeDelta = size;
                placeholderText.text = "<b>ENTER CATEGORY...</b>";
                // placeholderText.fontSize = 30f;
                placeholderText.font = categoryFont;
            }
            else if(type == Type.Question) placeholderText.text = "Enter Question...";
        }
    }

    public string GetPrimaryInputString()
    {
        return primaryInputString;
    }

    public void SetPrimaryInputString(string newText)
    {
        primaryInputField.text = newText;
        primaryInputString = newText;
    }
    
    public string GetSecondaryInputString()
    {
        return secondaryInputString;
    }

    public void SetSecondaryInputString(string newText)
    {
        secondaryInputField.text = newText;
        secondaryInputString = newText;
    }

    public void FlashError()
    {
        animator.Play("MonitorPlaneError", 2, 0f);
    }
}
