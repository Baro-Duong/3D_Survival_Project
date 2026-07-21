using UnityEngine;

// Data asset describing a 2-ingredient crafting recipe: input1 + input2 = output
[CreateAssetMenu(fileName = "NewRecipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public string input1Name;   // must match an item's name exactly
    public int input1Count = 1;
    public string input2Name;
    public int input2Count = 1;
    public string outputName;

    public Sprite input1Icon;
    public Sprite input2Icon;
    public Sprite outputIcon;

    public string recipeName;   // display name shown in the Tool Library, e.g. "Axe"
}
