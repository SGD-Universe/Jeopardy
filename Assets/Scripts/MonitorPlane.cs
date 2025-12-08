using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MeshRenderer))]
public class MonitorPlane : MonoBehaviour
{
    [Header("Behavior")]
    [SerializeField] private bool isEditable = false;
    [Tooltip("How fast the panel moves between normal and fullscreen (higher = faster).")]
    [SerializeField] private float positionLerpFactor = 5f;

    [Header("Visuals")]
    [SerializeField] private Color displayColor = Color.white;
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

    private Vector3 originalPosition;
    private Vector3 fullscreenPosition;

    // 0 = fully at originalPosition, 1 = fully at fullscreenPosition
    private float positionLerpT = 0f;

    private bool isFullscreen = false;
    private bool isHovered = false;
    private bool isAnswered = false;

    private void Start()
    {
        if (primaryInputField == null)
        {
            Debug.LogError("[MonitorPlane] Primary InputField is not assigned.");
        }
        else
        {
            placeholderText = primaryInputField.placeholder as TMP_Text;
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("[MonitorPlane] Animator component is missing.");
        }
        else
        {
            animator.enabled = true;
            animator.SetLayerWeight(1, 1f);
            animator.SetLayerWeight(2, 1f);
        }

        originalPosition = transform.localPosition;
        fullscreenPosition = new Vector3(0f, 0f, -1f);

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer != null ? meshRenderer.material : null;
        if (material == null)
        {
            Debug.LogError("[MonitorPlane] MeshRenderer or material is missing.");
        }

        // Ensure secondary input starts hidden for non-question or non-fullscreen states
        if (secondaryInputField != null)
        {
            secondaryInputField.gameObject.SetActive(false);
            secondaryInputField.interactable = false;
        }

        if (dividerGraphic != null)
        {
            dividerGraphic.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Smoothly animate between positions based on fullscreen state
        float targetT = isFullscreen ? 1f : 0f;
        positionLerpT = Mathf.MoveTowards(positionLerpT, targetT, positionLerpFactor * Time.deltaTime);

        Vector3 calculatedPosition = Vector3.Lerp(originalPosition, fullscreenPosition, positionLerpT);

        // Pop forward in Z when hovered or fullscreen
        calculatedPosition.z = (isHovered || isFullscreen) ? -1f : 0f;
        transform.localPosition = calculatedPosition;

        if (material != null)
        {
            material.color = displayColor;
        }

        // Position input fields depending on mode
        UpdateInputFieldPositions();

        // Cache current text for external access
        if (primaryInputField != null)
            primaryInputString = primaryInputField.text;

        if (secondaryInputField != null)
            secondaryInputString = secondaryInputField.text;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscapeDown();
        }

        HandleAnswerInput();
    }

    private void HandleAnswerInput()
    {
        if (!isFullscreen || isAnswered)
            return;

        var gm = GameManager.Instance;

        if (gm == null)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            gm.TriggerQuestionCorrect();
            if (animator != null)
                animator.Play("MonitorPlaneCorrect", 2, 0f);

            isAnswered = true;
        }
        else if (Input.GetKeyDown(KeyCode.Delete))
        {
            gm.TriggerQuestionIncorrect();
            if (animator != null)
                animator.Play("MonitorPlaneIncorrect", 2, 0f);

            isAnswered = true;
        }
    }

    private void UpdateInputFieldPositions()
    {
        if (primaryInputField == null)
            return;

        if (isFullscreen && type == Type.Question && isEditable)
        {
            primaryInputField.transform.localPosition = new Vector3(-0.1f, inputFieldsSpacing, -0.1f);

            if (secondaryInputField != null)
            {
                secondaryInputField.transform.localPosition = new Vector3(-0.1f, -inputFieldsSpacing, -0.1f);
            }
        }
        else
        {
            // Single centered primary field
            primaryInputField.transform.localPosition = new Vector3(0f, 0f, -0.1f);
        }
    }

    private void OnMouseOver()
    {
        if (!isHovered && !isFullscreen && animator != null)
        {
            animator.CrossFadeInFixedTime("MonitorPlaneHover", 0.1f);
            isHovered = true;
        }
    }

    private void OnMouseExit()
    {
        if (!isFullscreen && animator != null)
        {
            animator.CrossFadeInFixedTime("MonitorPlaneUnhover", 0.1f);
            isHovered = false;
        }
    }

    private void OnMouseDown()
    {
        if (isFullscreen || animator == null)
            return;

        animator.Play("MonitorPlaneFullscreen");
        animator.Play("MonitorPlaneCenter");

        isFullscreen = true;
        isHovered = false;
        isAnswered = false; // reset so you can answer again

        if (!isEditable)
            return;

        primaryInputField.interactable = true;

        if (type == Type.Question && secondaryInputField != null && dividerGraphic != null)
        {
            primaryInputField.pointSize = 12f;
            secondaryInputField.gameObject.SetActive(true);
            secondaryInputField.interactable = true;
            dividerGraphic.gameObject.SetActive(true);
        }
    }

    private void OnEscapeDown()
    {
        if (!isFullscreen || animator == null)
            return;

        animator.Play("MonitorPlaneMinimize");
        animator.Play("MonitorPlaneUncenter");

        isFullscreen = false;
        isHovered = false;
        isAnswered = false;

        if (!isEditable || primaryInputField == null)
            return;

        primaryInputField.interactable = false;

        if (type == Type.Question && secondaryInputField != null && dividerGraphic != null)
        {
            primaryInputField.pointSize = 18f;
            secondaryInputField.gameObject.SetActive(false);
            secondaryInputField.interactable = false;
            secondaryInputField.DeactivateInputField();
            dividerGraphic.gameObject.SetActive(false);
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
        if (primaryInputField == null)
        {
            Debug.LogError("[MonitorPlane] You forgot to assign the Primary InputField reference.");
            return;
        }

        if (placeholderText == null)
        {
            placeholderText = primaryInputField.placeholder as TMP_Text;
        }

        if (placeholderText == null)
            return;

        if (type == Type.Category)
        {
            primaryInputField.pointSize = 40f;

            RectTransform rect = primaryInputField.gameObject.GetComponent<RectTransform>();
            if (rect != null)
            {
                Vector2 size = rect.sizeDelta;
                size.y = 164f;
                rect.sizeDelta = size;
            }

            placeholderText.text = "<b>ENTER CATEGORY...</b>";
            placeholderText.font = categoryFont;
        }
        else if (type == Type.Question)
        {
            placeholderText.text = "Enter Question...";
        }
    }

    public string GetPrimaryInputString()
    {
        return primaryInputString;
    }

    public void SetPrimaryInputString(string newText)
    {
        if (primaryInputField != null)
            primaryInputField.text = newText;

        primaryInputString = newText;
    }

    public string GetSecondaryInputString()
    {
        return secondaryInputString;
    }

    public void SetSecondaryInputString(string newText)
    {
        if (secondaryInputField != null)
            secondaryInputField.text = newText;

        secondaryInputString = newText;
    }

    public void FlashError()
    {
        if (animator != null)
        {
            animator.Play("MonitorPlaneError", 2, 0f);
        }
    }
}
