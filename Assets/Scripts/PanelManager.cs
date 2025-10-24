using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManagement : MonoBehaviour
{
    [Header(" Game Panels")]
    public GameObject MainPanel;
    public GameObject InventoryPanel;
    public GameObject SettingsPanel;

    [Header(" Game Buttons")]
    public Button inventoryButton;

    [Header(" Health Elements")]
    public Image healthBar;
    public Text healthLeft;

    [Header(" Stamina Elements")]
    public Image staminaBar;
    public Text staminaLeft;

    [Header(" Hunger Elements")]
    public Image hungerBar;
    public Text hungerLeft;

    [Header(" Hydration Elements")]
    public Image hydrationBar;
    public Text hydrationLeft;

    [Header(" Direction Elements")]
    public Transform player;
    public Text Direction;

    //Placeholders for Testing
    private float health = 100;
    private float stamina = 75;
    private float hunger = 50;
    private float hydration = 80;
    private string direction = "N";

    [Header("Time Elements")]
    public Text TimeDisplay;
    public Text DayDisplay;
    public TimeManager timeManager;

    // Start is called before the first frame update
    void Start()
    {
        if (MainPanel != null) MainPanel.SetActive(true);
        if (InventoryPanel != null) InventoryPanel.SetActive(false);
        if (SettingsPanel != null) SettingsPanel.SetActive(false);

        if(inventoryButton != null) inventoryButton.gameObject.SetActive(true);

    }

    void Update()
    {
        /*if (MainPanel != null)
        {
            healthLeft.text = getHealth().ToString();
            healthBar.fillAmount = getHealth() / 100;

            staminaLeft.text = getStamina().ToString();
            staminaBar.fillAmount = getStamina() / 100;

            hungerLeft.text = getHunger().ToString();
            hungerBar.fillAmount = getHunger() / 100;

            hydrationLeft.text = getHydration().ToString();
            hydrationBar.fillAmount = getHydration() / 100;

            Direction.text = getDirection();
        */
        if (Time.timeScale == 0f) return;

        UpdateUI();


        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryPanel.activeSelf)
                CloseInventory();
            else
                OpenInventory();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SettingsPanel.activeSelf)
                CloseSettings();
            else
                OpenSettings();
        }
    }

    void UpdateUI()
    {
        if (healthBar != null)
        {
            healthLeft.text = health.ToString();
            healthBar.fillAmount = health / 100;
        }
        if (staminaBar != null)
        {
            staminaLeft.text = stamina.ToString();
            staminaBar.fillAmount = stamina / 100;
        }

        if (hungerBar != null)
        {
            hungerLeft.text = hunger.ToString();
            hungerBar.fillAmount = hunger / 100;
        }

        if (hydrationBar != null)
        {
            hydrationLeft.text = hydration.ToString();
            hydrationBar.fillAmount = hydration / 100;
        }

        if (Direction != null && player != null)
        {
            Direction.text = GetCompassDirection(player.forward);
        }

        if (timeManager != null)
        {
            if (TimeDisplay != null)
            {
                int hours = timeManager.GetHour();
                int minutes = timeManager.GetMinutes();
                string ampm = timeManager.GetAMPM(hours);

                // Convert to 12-hour format
                int displayHour = hours % 12;
                if (displayHour == 0) displayHour = 12;

                TimeDisplay.text = $"{displayHour:00}  {minutes:00} {ampm}";
            }
        }

        if (DayDisplay != null)
        {

            DayDisplay.text = timeManager.GetDays().ToString();
        }
    }

    string GetCompassDirection(Vector3 forward)
    {
        forward.y = 0;
        forward.Normalize();
        float angle = Vector3.SignedAngle(Vector3.forward, forward, Vector3.up);
        if (angle < 0) angle += 360f;

        if (angle >= 337.5f || angle < 22.5f) return "N";
        else if (angle < 67.5f) return "NE";
        else if (angle < 112.5f) return "E";
        else if (angle < 157.5f) return "SE";
        else if (angle < 202.5f) return "S";
        else if (angle < 247.5f) return "SW";
        else if (angle < 292.5f) return "W";
        else return "NW";
    }

    // Opening Panels 
    public void OpenInventory()
    {
        if (SettingsPanel != null && SettingsPanel.activeSelf)
            CloseSettings();

        if (InventoryPanel != null) InventoryPanel.SetActive(true);
        inventoryButton.gameObject.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseInventory()
    {
        if (InventoryPanel != null) InventoryPanel.SetActive(false);
        inventoryButton.gameObject.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void OpenSettings()
    {
        if (InventoryPanel != null && InventoryPanel.activeSelf)
            CloseInventory();

        if (SettingsPanel != null) SettingsPanel.SetActive(true);
        if (MainPanel != null) MainPanel.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0f;
    }

    public void CloseSettings()
    {
        if (SettingsPanel != null) SettingsPanel.SetActive(false);
        if (MainPanel != null) MainPanel.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;
    }

}

