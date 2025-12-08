using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExperimentalCategoryIntroduction : MonoBehaviour
{
    [SerializeField] private Transform categoriesContainer;

    private readonly List<GameObject> categories = new List<GameObject>();
    private GameObject currentCategory;
    private Animator currentCategoryAnimator;
    private int currentCategoryIndex = 0;
    private bool isPresenting = false;
    private bool hasQueuedNext = false;

    public UnityEvent OnFinalCategoryDisplayed;

    private void Start()
    {
        if (categoriesContainer == null)
        {
            Debug.LogError("[ExperimentalCategoryIntroduction] Categories Container is not assigned.");
            enabled = false;
            return;
        }

        // We look through each child under the categories container and add them to a list so we can cycle through them one by one when presenting

        categories.Clear();
        foreach (Transform category in categoriesContainer)
        {
            if (category == null) continue;

            if (category.gameObject.activeSelf)
                category.gameObject.SetActive(false);

            categories.Add(category.gameObject);
        }

        if (categories.Count == 0)
        {
            Debug.LogWarning("[ExperimentalCategoryIntroduction] No category children found under Categories Container.");
            return;
        }

        currentCategoryIndex = 0;
        currentCategory = categories[currentCategoryIndex];
    }

    private void Update()
    {
        if (!isPresenting || currentCategory == null)
            return;

        if (currentCategoryAnimator == null)
            return;

        // Check if the current category's fade-out animation has finished
        AnimatorStateInfo stateInfo = currentCategoryAnimator.GetCurrentAnimatorStateInfo(0);
        bool isInFadeOutState = stateInfo.IsName("CategoryFadeOut");
        bool fadeOutFinished = isInFadeOutState && stateInfo.normalizedTime >= 1f && !currentCategoryAnimator.IsInTransition(0);

        if (fadeOutFinished && !hasQueuedNext)
        {
            hasQueuedNext = true; // ensure we only process this once

            // Hide the current category (optional, depending on your animation)
            currentCategory.SetActive(false);

            // Move to next category if any
            if (currentCategoryIndex < categories.Count - 1)
            {
                currentCategoryIndex++;
                currentCategory = categories[currentCategoryIndex];
                currentCategory.SetActive(true);

                currentCategoryAnimator = currentCategory.GetComponent<Animator>();
                if (currentCategoryAnimator == null)
                {
                    Debug.LogWarning($"[ExperimentalCategoryIntroduction] Category {currentCategory.name} is missing an Animator.");
                }

                // Ready to detect fade-out for the next one
                hasQueuedNext = false;
            }
            else
            {
                // We just finished the last category
                isPresenting = false;
                OnFinalCategoryDisplayed?.Invoke();
            }
        }
    }

    public void BeginIntroduction()
    {
        if (categories.Count == 0)
        {
            Debug.LogWarning("[ExperimentalCategoryIntroduction] Cannot begin introduction; no categories found.");
            return;
        }

        // Reset state in case this is called multiple times
        currentCategoryIndex = 0;
        currentCategory = categories[currentCategoryIndex];
        hasQueuedNext = false;
        isPresenting = true;

        currentCategory.SetActive(true);
        currentCategoryAnimator = currentCategory.GetComponent<Animator>();

        if (currentCategoryAnimator == null)
        {
            Debug.LogWarning($"[ExperimentalCategoryIntroduction] First category {currentCategory.name} is missing an Animator.");
        }
    }
}
