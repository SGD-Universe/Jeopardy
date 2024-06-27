using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Category : MonoBehaviour
{

    public TMP_InputField[] categoryInputFields; // Assign in the Inspector
    public TMP_Text[] categoryTexts; // Assign in the Inspector

    void Start()
    {
        LoadCategories();
    }

    public void SaveCategories()
    {
        for (int i = 0; i < categoryInputFields.Length; i++)
        {
            string categoryName = categoryInputFields[i].text;
            PlayerPrefs.SetString("Category" + i, categoryName);
        }
        PlayerPrefs.Save();
        LoadCategories();
    }

    void LoadCategories()
    {
        for (int i = 0; i < categoryTexts.Length; i++)
        {
            string categoryName = PlayerPrefs.GetString("Category" + i, "Category " + (i + 1));
            categoryTexts[i].text = categoryName;
            categoryInputFields[i].text = categoryName; // Set the input field to the loaded name
        }
    }
}

