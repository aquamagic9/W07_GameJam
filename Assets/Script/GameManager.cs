using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public string CurrentStageAndPage;
    [SerializeField] MapData Map;
    [SerializeField] PageManager _pageManager;
    void Start()
    {
        LoadCurrentStageAndPage();
    }

    void Update()
    {
        
    }

    public void LoadCurrentStageAndPage()
    {
        int stageIndex = 0;
        int pageIndex = 2;

        _pageManager.ResetPages();
        _pageManager.UpdatePagesIndex(pageIndex);
        for (int i = 0; i <= pageIndex; i++)
        {
            var PageSections = Map.Stages[stageIndex].Pages[i].Sections;
            for (int j = 0; j < PageSections.Count; j++)
            {
                if (j == 0)
                    _pageManager.leftPages.Add(PageSections[j]);
                else if (j == 1)
                    _pageManager.middlePages.Add(PageSections[j]);
                else if (j == 2)
                    _pageManager.rightPages.Add(PageSections[j]);
                if (i != pageIndex)
                {
                    PageSections[j].SetActive(false);
                }
                else
                {
                    PageSections[j].SetActive(true);
                }
            }
        }
    }
}
