using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public string input1Name;   // tên item 1, phải khớp với InteractableObject.ItemName
    public string input2Name;   // tên item 2
    public string outputName;   // tên item output

    public Sprite input1Icon;
    public Sprite input2Icon;
    public Sprite outputIcon;

    public string recipeName;   // tên hiển thị trong Tool Library, vd "Axe"
}