using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalCreationController : MonoBehaviour
{
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private Vector2 buttonSpacing = new Vector2(4, 3);

    // Start is called before the first frame update
    void Start()
    {
        float buttonWidth = buttonTemplate.GetComponent<RectTransform>().rect.width;
        float buttonHeight = buttonTemplate.GetComponent<RectTransform>().rect.height;
        for(int c  = 0; c < 6; c ++)
        {
            for(int r  = 0; r < 5; r ++)
            {
                Vector3 spawnPosition = new Vector3((c-3) * buttonWidth, (r-2.5f) * buttonHeight, 0f);
                GameObject buttonClone = Instantiate(buttonTemplate, transform);
                buttonClone.transform.localPosition = spawnPosition;
            }
        }
        buttonTemplate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
