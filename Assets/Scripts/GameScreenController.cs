using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenController : MonoBehaviour
{
    [SerializeField] private GameObject panelTemplate;
    [SerializeField] private Vector2 panelSpacing = new Vector2(3, 1.85f);

    private List<List<MonitorPlane>> panelsBoard = new List<List<MonitorPlane>>();
    private Vector2 panelCount = new Vector2(6, 6);

    public GameObject pauseMenu;

    SaveManager.BoardData loadedBoardData;
    // Start is called before the first frame update
    void Start()
    {
        loadedBoardData = SaveManager.LoadRandomBoardData();

        for(int c = 0; c < panelCount.x; c++)
        {
            List<MonitorPlane> panelsColumn = new List<MonitorPlane>();
            for(int r = 0; r < panelCount.y; r++)
            {

                // float spawnX = (c + 0.5f - panelCount.x / 2f) * panelTemplate.transform.localScale.x * panelSpacing.x;
                // float spawnY = (r + 0.5f - panelCount.y / 2f) * panelTemplate.transform.localScale.y * panelSpacing.y;
                // Spawns from top-to-bottom, left-to-right
                float spawnX = ((c + 0.5f) - (panelCount.x / 2f)) * panelTemplate.transform.localScale.x * panelSpacing.x;
                float spawnY = ((panelCount.y / 2f) - (r + 0.5f)) * panelTemplate.transform.localScale.y * panelSpacing.y;
                Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

                GameObject panelClone = Instantiate(panelTemplate, transform);
                panelClone.transform.localPosition = spawnPosition;

                MonitorPlane monitorPlane = panelClone.GetComponent<MonitorPlane>();
                SaveManager.PanelData loadedPanelData = loadedBoardData.columns[c].panels[r];

                if (loadedPanelData.isCategory == true) 
                    monitorPlane.SetPanelType(MonitorPlane.Type.Category);
                else 
                    monitorPlane.SetPanelType(MonitorPlane.Type.Question);
                monitorPlane.SetPrimaryInputString(loadedPanelData.primaryText);
                monitorPlane.SetSecondaryInputString(loadedPanelData.secondaryText);
                
                panelsColumn.Add(monitorPlane);
            }
            panelsBoard.Add(panelsColumn);
        }
        panelTemplate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == false)
        {
            pauseMenu.SetActive(true);
        }
    }

    public void Return()
    {
        pauseMenu.SetActive(false);
    }
}
