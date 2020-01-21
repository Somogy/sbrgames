using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    public static PlayerRenderer instance;

    [Header("Renderer of the body parts that have a renderer")]
    // Body parts that have a renderer
    public List<SpriteRenderer> uniqueParts;

    [Header("Renderer of the body section that have two renderers, left and right")]
    // Body parts that have two renderers, left and right
    public List<SpriteRenderer> armsRenderer;
    public List<SpriteRenderer> legsRenderer;
    public List<SpriteRenderer> shouldersRenderer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadSavedCustomization();
    }

    private void LoadSavedCustomization()
    {
        // Loading unique parts
        uniqueParts[0].sprite = CustomCharMenu.instance.bodySprites[PlayerPrefs.GetInt("PieceBody")];
        uniqueParts[1].sprite = CustomCharMenu.instance.capeSprites[PlayerPrefs.GetInt("PieceCape")];
        uniqueParts[2].sprite = CustomCharMenu.instance.headSprites[PlayerPrefs.GetInt("PieceHead")];
        uniqueParts[3].sprite = CustomCharMenu.instance.weaponSprites[PlayerPrefs.GetInt("PieceWeapon")];

        // Loading Arms
        armsRenderer[0].sprite = CustomCharMenu.instance.leftArmsSprites[PlayerPrefs.GetInt("PieceArms")];
        armsRenderer[1].sprite = CustomCharMenu.instance.rightArmsSprites[PlayerPrefs.GetInt("PieceArms")];

        // Loading Legs
        legsRenderer[0].sprite = CustomCharMenu.instance.leftLegsSprites[PlayerPrefs.GetInt("PieceLegs")];
        legsRenderer[1].sprite = CustomCharMenu.instance.rightLegsSprites[PlayerPrefs.GetInt("PieceLegs")];

        // Loading Soulders
        shouldersRenderer[0].sprite = CustomCharMenu.instance.leftShouldersSprites[PlayerPrefs.GetInt("PieceShoulders")];
        shouldersRenderer[1].sprite = CustomCharMenu.instance.rightShouldersSprites[PlayerPrefs.GetInt("PieceShoulders")];
    }
}
