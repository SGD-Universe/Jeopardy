using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CategoryManager : MonoBehaviour
{
    public TMP_InputField categoryInputField;
    public TMP_Text categoryText;

    public int categoryNumber;
    private string categoryName;

    void Start()
    {
        LoadCategories();
    }

    public void SaveCategories()
    {
        categoryName = categoryInputField.text;
        PlayerPrefs.SetString($"Category_{categoryNumber}", categoryName);
    }

    public void LoadCategories()
    {
        categoryName = PlayerPrefs.GetString($"Category_{categoryNumber}");
        categoryText.text = categoryName;
    }
}

