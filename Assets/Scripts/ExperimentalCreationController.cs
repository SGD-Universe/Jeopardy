using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalCreationController : MonoBehaviour
{
    [SerializeField] private GameObject panelTemplate;
    [SerializeField] private Vector2 panelSpacing = new Vector2(4, 3);
    private Vector2 panelCount = new Vector2(1, 1);

    // Start is called before the first frame update
    void Start()
    {
        // RectTransform panelRectTransform = panelTemplate.GetComponent<RectTransform>();
        // float panelWidth = panelRectTransform.localScale.x * panelRectTransform.rect.size.x;
        // float panelHeight = panelRectTransform.localScale.y * panelRectTransform.rect.size.y;
        // Debug.Log(panelWidth + " " + panelHeight);
        for(int c  = 0; c < panelCount.x; c ++)
        {
            for(int r  = 0; r < panelCount.y; r ++)
            {
                // float spawnX = (c - panelCount.x/2) * panelWidth + (c - panelCount.x/2) * panelSpacing.x;
                // float spawnY = (r - panelCount.y/2) * panelHeight + (r - panelCount.y/2) * panelSpacing.y;
                float spawnX = (c + 0.5f - panelCount.x / 2f) * panelTemplate.transform.localScale.x * panelSpacing.x;
                float spawnY = (r + 0.5f - panelCount.y / 2f) * panelTemplate.transform.localScale.y * panelSpacing.y;
                Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
                GameObject panelClone = Instantiate(panelTemplate, transform);
                panelClone.transform.localPosition = spawnPosition;
            }
        }
        panelTemplate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
