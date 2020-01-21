using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script used to manage the character customization screen and save these changes, attached in the object with same name
public class CustomCharMenu : MonoBehaviour
{
    // Instance this to access variables
    public static CustomCharMenu instance;

    // Public Components
    [Header("Player character menu animators")]
    public Animator charMenuIdle;
    public Animator charPresentation;

    [Header("Player character menu animators")]
    [Tooltip("Character Game Object to use in customization char menu")]
    public GameObject charMenuGO;

    [Header("Sprites from all selectable parts, divided into each part of the character")]
    public List<Sprite> bodySprites;
    public List<Sprite> capeSprites;
    public List<Sprite> headSprites;
    public List<Sprite> weaponSprites;
    public List<Sprite> leftArmsSprites;
    public List<Sprite> rightArmsSprites;
    public List<Sprite> leftLegsSprites;
    public List<Sprite> rightLegsSprites;
    public List<Sprite> leftShouldersSprites;
    public List<Sprite> rightShouldersSprites;

    // Audio Clips for this menu
    [Header("Audios for use in this menu")]
    [SerializeField] private AudioClip pieceSelected;
    [SerializeField] private AudioClip sectionSelected;

    // Private Variables
    [Header("Button to end customization character")]
    [SerializeField] private Text finishBtn;

    [Header("Buttons of disponible pieces for player")]
    [SerializeField] private List<GameObject> pieceBtns;

    private string bodySectionName; // To determine the current body section of the character that is selected

    // Colors to match or not manipulated parts
    private static Color grayColor;
    private static Color normalColor;

    // Private Components
    private AudioSource audioCustomMenu;

    public void Awake()
    {
        instance = this;

        audioCustomMenu = GetComponent<AudioSource>();

        // When the menu is opened
        CharacterMenuOpened();
    }

    private void Start()
    {
        // Setting the default values for variables
        DefaultValues();
    }

    private void Update()
    {
        // Check the game situation and assign a different word for the final customization character button.r
        if (GameManager.instance.inGame)
        {
            finishBtn.text = "DONE";
        }

        else
        {
            finishBtn.text = "PLAY";
        }
    }

    // Method used on buttons, for player to select body section to customize
    public void BodySectionSelect(int selection)
    {
        audioCustomMenu.PlayOneShot(sectionSelected);

        // Active the respective buttons of part
        for (int i = 0; i < pieceBtns.Count; i++)
        {
            if (i == selection)
            {
                pieceBtns[i].gameObject.SetActive(true);
            }

            else
            {
                pieceBtns[i].gameObject.SetActive(false);
            }
        }

        // Remove highlight of all parts, before highlight the chosen
        BackToGray();

        // Activate the matching buttons on the available parts and highlight
        switch (selection)
        {
            // Body
            case 0:
                PlayerRenderer.instance.uniqueParts[selection].color = normalColor;
                bodySectionName = "Body";
                break;

            // Cape
            case 1:
                PlayerRenderer.instance.uniqueParts[selection].color = normalColor;
                bodySectionName = "Cape";
                break;

            // Head
            case 2:
                PlayerRenderer.instance.uniqueParts[selection].color = normalColor;
                bodySectionName = "Head";
                break;

            // Weapon
            case 3:
                PlayerRenderer.instance.uniqueParts[selection].color = normalColor;
                bodySectionName = "Weapon";
                break;

            // Arms
            case 4:
                foreach (var item in PlayerRenderer.instance.armsRenderer)
                {
                    item.color = normalColor;
                }
                bodySectionName = "Arms";
                break;

            // Legs
            case 5:
                foreach (var item in PlayerRenderer.instance.legsRenderer)
                {
                    item.color = normalColor;
                }
                bodySectionName = "Legs";
                break;

            // Shoulders
            case 6:
                foreach (var item in PlayerRenderer.instance.shouldersRenderer)
                {
                    item.color = normalColor;
                }
                bodySectionName = "Shoulders";
                break;
        }
    }

    // When the menu is opened
    public void CharacterMenuOpened()
    {
        charMenuGO.SetActive(true);
        charPresentation.enabled = false;
        charMenuIdle.enabled = true;
    }

    // Method to use in the buttons of the pieces available for the player to choose
    public void SelectedBodyPiece(int chosen)
    {
        audioCustomMenu.PlayOneShot(pieceSelected);

        // Get the pody section and change to the piece chosen
        switch (bodySectionName)
        {
            case "Body":
                PlayerRenderer.instance.uniqueParts[0].sprite = bodySprites[chosen];
                PlayerPrefs.SetInt("PieceBody", chosen);
                break;

            case "Cape":
                PlayerRenderer.instance.uniqueParts[1].sprite = capeSprites[chosen];
                PlayerPrefs.SetInt("PieceCape", chosen);
                break;

            case "Head":
                PlayerRenderer.instance.uniqueParts[2].sprite = headSprites[chosen];
                PlayerPrefs.SetInt("PieceHead", chosen);
                break;

            case "Weapon":
                PlayerRenderer.instance.uniqueParts[3].sprite = weaponSprites[chosen];
                PlayerPrefs.SetInt("PieceWeapon", chosen);
                break;

            case "Arms":
                if (chosen == 0)
                {
                    PlayerRenderer.instance.armsRenderer[0].sprite = leftArmsSprites[0];
                    PlayerRenderer.instance.armsRenderer[1].sprite = rightArmsSprites[0];
                    PlayerPrefs.SetInt("PieceArms", chosen);
                }

                else
                {
                    PlayerRenderer.instance.armsRenderer[0].sprite = leftArmsSprites[1];
                    PlayerRenderer.instance.armsRenderer[1].sprite = rightArmsSprites[1];
                    PlayerPrefs.SetInt("PieceArms", chosen);
                }
                break;

            case "Legs":
                if (chosen == 0)
                {
                    PlayerRenderer.instance.legsRenderer[0].sprite = leftLegsSprites[0];
                    PlayerRenderer.instance.legsRenderer[1].sprite = rightLegsSprites[0];
                    PlayerPrefs.SetInt("PieceLegs", chosen);
                }

                else
                {
                    PlayerRenderer.instance.legsRenderer[0].sprite = leftLegsSprites[1];
                    PlayerRenderer.instance.legsRenderer[1].sprite = rightLegsSprites[1];
                    PlayerPrefs.SetInt("PieceLegs", chosen);
                }
                break;

            case "Shoulders":
                if (chosen == 0)
                {
                    PlayerRenderer.instance.shouldersRenderer[0].sprite = leftShouldersSprites[0];
                    PlayerRenderer.instance.shouldersRenderer[1].sprite = rightShouldersSprites[0];
                    PlayerPrefs.SetInt("PieceShoulders", chosen);
                }

                else
                {
                    PlayerRenderer.instance.shouldersRenderer[0].sprite = leftShouldersSprites[1];
                    PlayerRenderer.instance.shouldersRenderer[1].sprite = rightShouldersSprites[1];
                    PlayerPrefs.SetInt("PieceShoulders", chosen);
                }
                break;

        }
    }

    // Turning to gray the parts that don't need highlighting
    private void BackToGray()
    {
        foreach (var item in PlayerRenderer.instance.uniqueParts)
        {
            item.color = grayColor;
        }

        foreach (var item in PlayerRenderer.instance.armsRenderer)
        {
            item.color = grayColor;
        }

        foreach (var item in PlayerRenderer.instance.legsRenderer)
        {
            item.color = grayColor;
        }

        foreach (var item in PlayerRenderer.instance.shouldersRenderer)
        {
            item.color = grayColor;
        }
    }    

    // Values of colors to use later
    private void DefaultValues()
    {
        grayColor = new Color(0.4f, 0.4f, 0.4f);
        normalColor = new Color(1f, 1f, 1f); 
    }
}
