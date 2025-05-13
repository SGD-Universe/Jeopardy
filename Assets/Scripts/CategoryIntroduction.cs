using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Attached on GameObject: CatagoryIntroductionScreen

public class CategoryIntroduction : MonoBehaviour
{
    [SerializeField] private Transform categoriesContainer;

    private List<GameObject> categories = new List<GameObject>();
    private GameObject currentCategory;
    private Animator currentCategoryAnimator;
    private int currentCategoryIndex = 0;
    private bool isPresenting = false;
    private bool isFadingOut = false;

    public UnityEvent OnFinalCategoryDisplayed;


    void Start()
    {
        // Extract all children from categoriesContainer into our own list
        foreach (Transform category in categoriesContainer)
        {
            if(category.gameObject.activeSelf) category.gameObject.SetActive(false);
            categories.Add(category.gameObject);
        }

        currentCategory = categories[currentCategoryIndex];
    }

    void Update()
    {
            if(isPresenting) isFadingOut = currentCategoryAnimator.GetCurrentAnimatorStateInfo(0).IsName("CategoryFadeOut");
            if(isFadingOut)
            {
                if(currentCategoryIndex < categories.Count - 1)
                {
                    currentCategoryIndex++;
                    currentCategory = categories[currentCategoryIndex];
                    currentCategory.SetActive(true);
                    currentCategoryAnimator = currentCategory.GetComponent<Animator>();
                }
                else
                {
                    OnFinalCategoryDisplayed.Invoke();
                }
            }
    }

    public void BeginIntroduction()
    {
        isPresenting = true;
        currentCategory.SetActive(true);
        currentCategoryAnimator = currentCategory.GetComponent<Animator>();
    }
}
