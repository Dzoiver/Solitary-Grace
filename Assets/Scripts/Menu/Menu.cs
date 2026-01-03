using UnityEngine;
using GM;
using DG.Tweening;
using SolitaryAudio;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject systemPanel;
    [SerializeField] GameObject confirmPanel;
    [SerializeField] GameObject confirmPanelItem;
    [SerializeField] TextMeshProUGUI healthText;
    Inventory inventory;
    ContextMenuItem context;
    WeaponManager weapon;
    MonsterManager monsterManager;
    private ScriptableItem bufferItem;
    private GameObject bufferPickupObject;
    AudioSource audio;
    [SerializeField] AudioClip exitSound;
    [SerializeField] TextMeshProUGUI itemnameLabel;
    string healthString;

    private void Awake()
    {
        monsterManager = FindObjectOfType<MonsterManager>();
        weapon = FindAnyObjectByType<WeaponManager>();
        context = FindObjectOfType<ContextMenuItem>();
        audio = GetComponent<AudioSource>();
        healthString = healthText.text;
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
        GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 1), 0.5f).onComplete = () => // Fadeout
        {
            monsterManager.FreezeMonsters();
            menuPanel.SetActive(true);
            // Fadein
            GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 0), 0.5f).onComplete = () =>
            {
                GameFuncs.fading = false;
            };
        };
    }

    public void ConfirmBox(ScriptableItem item, GameObject itemToDisable = null)
    {
        if (itemToDisable != null)
            bufferPickupObject = itemToDisable;
        bufferItem = item; // Buffers item which player decides to take or not
        OpenMenu();
        confirmPanelItem.SetActive(true);
        confirmPanelItem.GetComponent<ConfirmationBox>().ChangeItemName(item.name);
    }

    public void YesConfirm()
    {
        if (bufferPickupObject != null)
        {
            bufferPickupObject.SetActive(false);
            bufferPickupObject = null;
        }
        //inventory.AddItem(bufferItem);
        CloseMenu();
    }

    public void NoConfirm()
    {

        CloseMenu();
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
        GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 1), 0.5f).onComplete = () => // Fadeout
        {
            monsterManager.UnfreezeMonsters();
            weapon.enabled = true;
            weapon.canAttack = true;
            confirmPanel.SetActive(false);
            systemPanel.SetActive(false);
            menuPanel.SetActive(false);

            // Fadein
            GameFuncs.PlayerScript.SetControl(true);
            GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 0), 0.5f).onComplete = () =>
            {
                GameFuncs.fading = false;
            };
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (GameFuncs.fading)
            return;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!menuPanel.activeSelf && GameFuncs.PlayerScript.IsControl())
            {
                OpenMenu();
            }
            else if (menuPanel.activeSelf)
            {
                CloseMenu();
            }
        }
    }

    public void ChangeHealth(float value)
    {
        healthText.text = healthString + value.ToString();
    }
}
