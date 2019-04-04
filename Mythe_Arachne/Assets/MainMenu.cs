using System;
using UnityEngine;

public class MainMenu : MonoBehaviour {


    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private GameObject swingObject;

    [SerializeField]
    private GameObject swingHolder;

    public Action newGameAction;

    public Action continueGameAction;

    // Opens Settings Panel
    public void OpenSettings()
    {
        swingObject.transform.SetParent(swingHolder.transform);

        settingsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    // Calls newGameAction delegate
    public void NewGame()
    {
        if (newGameAction == null) return;
        newGameAction();
    }

    // Calls continueGameAction delegate
    public void ContinueGame()
    {
        if (continueGameAction == null) return;

        continueGameAction();
    }

}
