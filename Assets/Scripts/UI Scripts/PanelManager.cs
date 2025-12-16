using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManagement : MonoBehaviour
{
    [Header("Game Canvas")]
    public GameObject MainPanel;
    public GameObject InventoryPanel;
    public GameObject SettingsPanel;
    public GameObject SavePanel;
    public GameObject AudioPanel;
    public GameObject CraftingPanel;

    [Header("Game Buttons")]
    public Button inventoryButton;

    [Header("Health Elements")]
    public Image healthBar;
    public Text healthLeft;

    [Header("Stamina Elements")]
    public Image staminaBar;
    public Text staminaLeft;

    [Header("Hunger Elements")]
    public Image hungerBar;
    public Text hungerLeft;

    [Header("Hunger Elements")]
    public Image thirstBar;
    public Text thirstLeft;

    [Header("Direction Elements")]
    public Transform player;
    public Text Direction;

    public PlayerMotor playerMoter;

    // Placeholders for Testing
    private float health;
    private float stamina;
    private float hunger;
    private float thirst;
    private string direction;

    [Header("Time Elements")]
    public Text TimeDisplay;
    public Text DayDisplay;
    public TimeManager timeManager;

    void Start()
    {
        if (MainPanel != null) MainPanel.SetActive(true);
        if (InventoryPanel != null) InventoryPanel.SetActive(false);
        if (SettingsPanel != null) SettingsPanel.SetActive(false);
        if (AudioPanel != null) AudioPanel.SetActive(false);
        if (SavePanel != null) SavePanel.SetActive(false);
        if (CraftingPanel != null) CraftingPanel.SetActive(false);
        if (inventoryButton != null) inventoryButton.gameObject.SetActive(true);

        if (playerMoter == null)
            playerMoter = FindObjectOfType<PlayerMotor>();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        UpdateUI();

        if (Input.GetKeyDown(KeyCode.Q))
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CraftingPanel.activeSelf)
                CloseCrafting();
            else
                OpenCrafting();
        }
    }

    void UpdateUI()
    {
        if (healthBar != null)
        {
            health = Mathf.RoundToInt(playerMoter.getMaxHealth());
            healthLeft.text = health.ToString();
            healthBar.fillAmount = health / 100;
        }
        if (staminaBar != null)
        {
            stamina = Mathf.RoundToInt(playerMoter.getStamina());
            staminaLeft.text = stamina.ToString();
            staminaBar.fillAmount = stamina / playerMoter.getMaxStamina();
        }

        if (hungerBar != null)
        {
            hunger = Mathf.RoundToInt(playerMoter.getHunger());
            hungerLeft.text = hunger.ToString();
            hungerBar.fillAmount = hunger / playerMoter.getMaxHunger(); ;
        }

        if (thirstBar != null)
        {
            thirst = Mathf.RoundToInt(playerMoter.getThirst());
            thirstLeft.text = thirst.ToString();
            thirstBar.fillAmount = thirst / playerMoter.getMaxThirst();
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

                int displayHour = hours % 12;
                if (displayHour == 0) displayHour = 12;

                TimeDisplay.text = $"{displayHour:00}:{minutes:00} {ampm}";
            }

            if (DayDisplay != null)
            {
                DayDisplay.text = timeManager.GetDays().ToString();
            }
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
        if (inventoryButton != null) inventoryButton.gameObject.SetActive(false);

        if (CraftingPanel != null) CraftingPanel.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseInventory()
    {
        if (InventoryPanel != null) InventoryPanel.SetActive(false);
        if (inventoryButton != null) inventoryButton.gameObject.SetActive(true);
        

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
        Debug.Log("Close Settings");
        if (SettingsPanel != null) SettingsPanel.SetActive(false);
        if (MainPanel != null) MainPanel.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;
    }

    public void YesSave()
    {
        StartCoroutine(YesSaveCoroutine());
    }

    private IEnumerator YesSaveCoroutine()
    {
        Debug.Log("Game saved!");
        yield return new WaitForSeconds(5f);
        QuitGame();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Audio()
    {
        if (AudioPanel != null)
        {
            MainPanel.SetActive(false);
            SettingsPanel.SetActive(false);
            AudioPanel.SetActive(true);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseAudio()
    {
        if (AudioPanel != null)
        {
            AudioPanel.SetActive(false);
            SettingsPanel.SetActive(true);
        }
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void OpenSaving()
    {
        if (SavePanel != null)
        {
            SettingsPanel.SetActive(false);
            SavePanel.SetActive(true);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OpenCrafting()
    {
        if (InventoryPanel != null) InventoryPanel.SetActive(false);
        if (SettingsPanel != null) SettingsPanel.SetActive(false);
        if (CraftingPanel != null) CraftingPanel.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseCrafting()
    {
        if (CraftingPanel != null) CraftingPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
