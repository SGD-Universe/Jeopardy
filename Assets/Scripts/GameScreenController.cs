using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenController : MonoBehaviour
{
    [Header("Panel Setup")]
    [SerializeField] private GameObject panelTemplate;
    [SerializeField] private Vector2 panelSpacing = new Vector2(3f, 1.85f);

    [Tooltip("Desired number of columns (x) and rows (y).")]
    [SerializeField] private int columns = 6;
    [SerializeField] private int rows = 6;

    private readonly List<List<MonitorPlane>> panelsBoard = new List<List<MonitorPlane>>();

    private SaveManager.BoardData loadedBoardData;

    private void Start()
    {
        if (panelTemplate == null)
        {
            Debug.LogError("[GameScreenController] Panel template is not assigned.");
            return;
        }

        // Make sure the template itself is hidden in-game
        panelTemplate.SetActive(false);

        loadedBoardData = SaveManager.LoadRandomBoardData();

        if (loadedBoardData == null || loadedBoardData.columns == null || loadedBoardData.columns.Count == 0)
        {
            Debug.LogWarning("[GameScreenController] No board data found. Nothing will be spawned.");
            return;
        }

        // Clamp to available data
        int actualColumns = Mathf.Min(columns, loadedBoardData.columns.Count);

        for (int c = 0; c < actualColumns; c++)
        {
            List<MonitorPlane> panelsColumn = new List<MonitorPlane>();

            var columnData = loadedBoardData.columns[c];
            if (columnData == null || columnData.panels == null)
            {
                Debug.LogWarning($"[GameScreenController] Column {c} has no panel data.");
                continue;
            }

            int actualRows = Mathf.Min(rows, columnData.panels.Count);

            for (int r = 0; r < actualRows; r++)
            {
                // Compute spawn position
                float spawnX = ((c + 0.5f) - (actualColumns / 2f))
                               * panelTemplate.transform.localScale.x * panelSpacing.x;

                float spawnY = ((rows / 2f) - (r + 0.5f))
                               * panelTemplate.transform.localScale.y * panelSpacing.y;

                Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

                // Instantiate panel
                GameObject panelClone = Instantiate(panelTemplate, transform);
                panelClone.transform.localPosition = spawnPosition;
                panelClone.SetActive(true);

                MonitorPlane monitorPlane = panelClone.GetComponent<MonitorPlane>();
                if (monitorPlane == null)
                {
                    Debug.LogError("[GameScreenController] Panel clone is missing a MonitorPlane component.");
                    continue;
                }

                SaveManager.PanelData loadedPanelData = columnData.panels[r];

                if (loadedPanelData != null && loadedPanelData.isCategory)
                    monitorPlane.SetPanelType(MonitorPlane.Type.Category);
                else
                    monitorPlane.SetPanelType(MonitorPlane.Type.Question);

                if (loadedPanelData != null)
                {
                    monitorPlane.SetPrimaryInputString(loadedPanelData.primaryText);
                    monitorPlane.SetSecondaryInputString(loadedPanelData.secondaryText);
                }
                else
                {
                    // If for some reason data is missing, spawn blank question
                    monitorPlane.SetPrimaryInputString(string.Empty);
                    monitorPlane.SetSecondaryInputString(string.Empty);
                }

                panelsColumn.Add(monitorPlane);
            }

            panelsBoard.Add(panelsColumn);
        }
    }

    private void Update()
    {
        // Currently unused; keep for future features (e.g., refreshing board, debug keys)
    }
}
