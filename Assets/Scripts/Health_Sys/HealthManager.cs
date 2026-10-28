using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    
    // ESPACIOS RESERVADOS PARA GAMEOBJECTS
    public GameObject[] _player1;
    public GameObject[] _player2;

    //ACCESO A SLIDERS.
    public Slider _p1HealthBar;
    public Slider _p2HealthBar;

    // DECLARACIÓN DE VARIABLES DEL SISTEMA DE VIDA.
    float _totalHealth = 100;
    float _p1Health;
    float _p2Health;
    public int _totalLives = 3; //MODIFICA PARA AJUSTAR LA DURACIÓN ESTIMADA DE CADA RONDA.
    int _p1Lives;
    int _p2Lives;


    //VARIABLES RELACIONADOS AL SISTEMA DE DAÑO.
    string[] _dmgType = { "Boomerang", "Collision" };

    [Header("Factores de Daño")]
    public float BoomerangDMG = 20f;
    public float CollisionDMG = 10f;

    void Start()
    {
        _p1HealthBar.value = 0;
        _p2HealthBar.value = 0;

    }
    
    public void Player1Initialize()
    {
        _player1 = GameObject.FindGameObjectsWithTag("Player1");
        _p1Health = _totalHealth;
        _p1HealthBar.value = _p1Health;
        //Debug.Log("Player 1 Health Initialized on " + _p1Health);
    }

    public void Player2Initialize()
    {
        _player2 = GameObject.FindGameObjectsWithTag("Player2");
        _p2Health = _totalHealth;
        _p2HealthBar.value = _p2Health;
        //Debug.Log("Player 2 Health Initialized on " + _p2Health);
    }

    public void ApplyDamage(int IsPlayer, int DmgType)
    {
        if (IsPlayer == 1)
        {
            switch (_dmgType[DmgType])
            {
                case "Boomerang":
                    _p1Health -= BoomerangDMG;
                    _p1Health = Mathf.Clamp(_p1Health, 0, _totalHealth);
                    _p1HealthBar.value = _p1Health;
                    if (_p1Health <= 0) P1LivesReduction();
                    break;
                case "Collision":
                    _p1Health -= CollisionDMG;
                    _p1Health = Mathf.Clamp(_p1Health, 0, _totalHealth);
                    _p1HealthBar.value = _p1Health;
                    
                    break;
            }
        }

        if (IsPlayer == 2)
        {
            switch (_dmgType[DmgType])
            {
                case "Boomerang":
                    _p2Health -= BoomerangDMG;
                    _p2Health = Mathf.Clamp(_p2Health, 0, _totalHealth);
                    _p2HealthBar.value = _p2Health;
                    if (_p2Health <= 0) Destroy(_player2[0]);
                    break;
                case "Collision":
                    _p2Health -= CollisionDMG;
                    _p2Health = Mathf.Clamp(_p2Health, 0, _totalHealth);
                    _p2HealthBar.value = _p2Health;
                    if (_p2Health <= 0) Destroy(_player2[0]);
                    break;
            }
        }
    }

    public void P1LivesReduction()
    {
        if (_p1Lives >= 1)
        {
            Destroy(_player1[0]);
            _p1Lives--;
            //Añade aquí el llamado a la función de respawn
        }
        else
        {
            Destroy(_player1[0]);
            //Añade aquí el paso a la pantalla de victoria.
        }
        
    }
}
