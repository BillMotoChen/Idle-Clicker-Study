using UnityEngine;
using TMPro;

public class CarrotManager : MonoBehaviour
{
    [Header(" Data ")]
    [SerializeField] private double totalCarrotsCount;
    [SerializeField] private double carrotIncrement;
    [SerializeField] private TextMeshProUGUI carrotCountText;

    void Awake()
    {
        LoadData();
    }
    void OnEnable()
    {
        InputManager.onCarrotClicked += CarrotClickedCallback;
    }

    void OnDisable()
    {
        InputManager.onCarrotClicked -= CarrotClickedCallback;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CarrotClickedCallback()
    {
        totalCarrotsCount += carrotIncrement;
        UpdateCarrotCountText();
        SaveData();
    }

    private void SaveData()
    {
        PlayerPrefs.SetString("Carrots", totalCarrotsCount.ToString());
    }
    
    private void LoadData()
    {
        double.TryParse(PlayerPrefs.GetString("Carrots"), out totalCarrotsCount);
        UpdateCarrotCountText();

    }

    private void UpdateCarrotCountText()
    {
        carrotCountText.text = totalCarrotsCount + " Carrots";
    }

}
