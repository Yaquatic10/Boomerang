using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Mainmenu : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject _mainMenuFirst;
    
    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }
    public void Changeschene(string name)
    {

        SceneManager.LoadScene(name);
    }

    public void Closegame()
    {

        Application.Quit();

    }
}


