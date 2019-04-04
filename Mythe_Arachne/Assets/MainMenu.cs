using System;
using System.Collections;
using System.Collections.Generic;
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

    public void OpenSettings()
    {
        swingObject.transform.SetParent(swingHolder.transform);

        settingsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void NewGame()
    {
        newGameAction();
    }

    public void ContinueGame()
    {
        continueGameAction();
    }

}
