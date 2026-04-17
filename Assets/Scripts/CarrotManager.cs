using UnityEngine;
using TMPro;
using Unity.Collections;

public class CarrotManager : MonoBehaviour
{
    public static CarrotManager instance;

    [Header(" Data ")]
    [SerializeField] private double totalCarrotsCount;
    [SerializeField] private int frenzyModeMultiplier;
    private double carrotIncrement;
    [SerializeField] private TextMeshProUGUI carrotCountText;

    public double GetCarrotIncrement() { return carrotIncrement; }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        carrotIncrement = 1;
        LoadData();
    }
    void OnEnable()
    {
        InputManager.onCarrotClicked += CarrotClickedCallback;
        Carrot.onFrenzyModeStarted += FrenzyModeStartCallback;
        Carrot.onFrenzyModeStopped += FrenzyModeStopCallback;
    }

    void OnDisable()
    {
        InputManager.onCarrotClicked -= CarrotClickedCallback;
        Carrot.onFrenzyModeStarted -= FrenzyModeStartCallback;
        Carrot.onFrenzyModeStopped -= FrenzyModeStopCallback;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCarrot(float value)
    {
        totalCarrotsCount += value;
        UpdateCarrotCountText();
        SaveData();
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
        carrotCountText.text = totalCarrotsCount.ToString("F1") + " Carrots";
    }

    private void FrenzyModeStartCallback()
    {
        carrotIncrement *= frenzyModeMultiplier;
    }

    private void FrenzyModeStopCallback()
    {
        carrotIncrement /= frenzyModeMultiplier;
    }

}
