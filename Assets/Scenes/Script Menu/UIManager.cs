using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [Header("Win/Lose screens")]
    [SerializeField] private Canvas _winCanvas = default;
    [SerializeField] private Canvas _loseCanvas = default;


   

    // Update is called once per frame
  

    public void ShowWinScreen()
    {
        _winCanvas.gameObject.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        _loseCanvas.gameObject.SetActive(true);
    }
}
