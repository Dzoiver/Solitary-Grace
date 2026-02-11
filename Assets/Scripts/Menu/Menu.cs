using UnityEngine;
using GM;
using DG.Tweening;
using SolitaryAudio;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject systemPanel;
    [SerializeField] GameObject confirmPanel;
    [SerializeField] GameObject confirmPanelItem;
    [SerializeField] TextMeshProUGUI healthText;
    public Inventory inventory;
    ContextMenuItem context;
    WeaponManager weapon;
    MonsterManager monsterManager;
    AudioSource audio;
    [SerializeField] AudioClip exitSound;
    [SerializeField] TextMeshProUGUI itemnameLabel;
    public string healthString;
    [SerializeField] Chest chest;
    [SerializeField] GameObject description;
    [SerializeField] Image ownBlackScreen;

    private void Awake()
    {
        monsterManager = FindObjectOfType<MonsterManager>();
        weapon = FindAnyObjectByType<WeaponManager>();
        context = FindObjectOfType<ContextMenuItem>();
        audio = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        menuPanel.SetActive(false);
        confirmPanelItem.SetActive(false);
        systemPanel.SetActive(false);
    }

    public void OpenMenu()
    {
        if (NPCDialogue.DialoguePlaying)
            return;
        itemnameLabel.text = "";
        weapon.enabled = false;
        weapon.canAttack = false;
        inventory.DisplayItems();
        GameFuncs.fading = true;
        GameFuncs.PlayerScript.SetControl(false);
        AudioController.Play("openMenu");
        Cursor.lockState = CursorLockMode.None;
        description.SetActive(false);
        GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 1), 0.5f).onComplete = () => // Fadeout
        {
            monsterManager.FreezeMonsters();
            menuPanel.SetActive(true);
            // Fadein
            ownBlackScreen.DOColor(new Color(0, 0, 0, 0), 0.5f).onComplete = () =>
            {
                GameFuncs.fading = false;
            };
        };
    }

    public void CloseMenu()
    {
        context.gameObject.SetActive(false);
        confirmPanelItem.SetActive(false);
        GameFuncs.fading = true;
        AudioController.Play("closeMenu");
        Cursor.lockState = CursorLockMode.Locked;
        audio.clip = exitSound;
        audio.Play();
        ownBlackScreen.DOColor(new Color(0, 0, 0, 1), 0.5f).onComplete = () => // Fadeout
        {
            monsterManager.UnfreezeMonsters();
            weapon.enabled = true;
            weapon.canAttack = true;
            confirmPanel.SetActive(false);
            systemPanel.SetActive(false);
            menuPanel.SetActive(false);
            chest.gameObject.SetActive(false);

            // Fadein
            GameFuncs.PlayerScript.SetControl(true);
            GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 0), 0.5f).onComplete = () =>
            {
                GameFuncs.fading = false;
            };
        };
    }

    public void OpenChest()
    {
        OpenMenu();
    }


    public void CloseChest()
    {
        CloseMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameFuncs.fading)
            return;

        if (!menuPanel.activeSelf && GameFuncs.PlayerScript.IsControl() && Input.GetKeyDown(KeyCode.Tab))
        {
            OpenMenu();
        }
        else if (menuPanel.activeSelf && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)))
        {
            CloseMenu();
        }
    }

    public void ChangeHealth(float value)
    {
        healthText.text = healthString + ": " + value.ToString();
    }
}
