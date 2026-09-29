using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuSO", menuName = "Scriptable Objects/MenuSO")]
public class MenuSO : ScriptableObject
{
    public List<RecipeSO> recipeSOList;
}
