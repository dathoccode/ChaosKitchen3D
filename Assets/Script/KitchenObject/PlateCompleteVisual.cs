using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject kitchenObject;
    }


    [SerializeField] private Plate plate;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;
    
    private void Start()
    {
        plate.OnIngredientAdded += Plate_OnIngredientAdded;
    }

    private void Plate_OnIngredientAdded(object sender, Plate.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            if (kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSOGameObject.kitchenObject.SetActive(true);
            }
        }
    }
}