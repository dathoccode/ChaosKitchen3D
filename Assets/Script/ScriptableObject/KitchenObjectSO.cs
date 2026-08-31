using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObjectSO", menuName = "Scriptable Objects/KitchenObjectSO")]
public class KitchenObjectSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public string objectName;
}
