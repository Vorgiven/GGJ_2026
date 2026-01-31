using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    CanvasGroup currentActiveGrp;
    [SerializeField] List<CanvasGroup> canvasGrpMenus;

    private void Start()
    {
        Time.timeScale = 1.0f;
        foreach (CanvasGroup grp in canvasGrpMenus)
        {
            grp.gameObject.SetActive(false);    
        }
        OnLoadMenu(canvasGrpMenus[0]);
    }
    public void ForceOpenMenu(CanvasGroup menuToLoad)
    {
        foreach (CanvasGroup grp in canvasGrpMenus)
        {
            if (grp == menuToLoad)
            {
                grp.gameObject.SetActive(true);
                break;
            }
        }
    }
    public void ForceCloseMenu(CanvasGroup menuToLoad)
    {
        foreach (CanvasGroup grp in canvasGrpMenus)
        {
            if (grp == menuToLoad)
            {
                grp.gameObject.SetActive(false);
                break;
            }
        }
    }

    public void OnLoadMenu(CanvasGroup menuToLoad)
    {
        if(currentActiveGrp)
        {
            currentActiveGrp.gameObject.SetActive(false);
        }
        foreach(CanvasGroup grp in canvasGrpMenus)
        {
            if(grp == menuToLoad)
            {
                currentActiveGrp = grp;
                grp.gameObject.SetActive(true);
                break;
            }
        }
    }
    public void OnLoadGame()
    {
        SceneManager.LoadScene("Game over scene");
    }
}
