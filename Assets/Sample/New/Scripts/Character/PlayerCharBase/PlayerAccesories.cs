using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item_Ability;

public class PlayerAccessories : MonoBehaviour
{
    #region Char_Body_Component
    [SerializeField] private Char_Body_Component charBodyComponent;
    [SerializeField] private DataItemInput dataItemInputAccessories;
    [SerializeField] private GameObject accessorySample;
    public string accessoryCode;

    private Transform accessoryTransform;
    public PlayerCharBase playerCharBase;
    Vector3 accesoriesOffset;
    public void SetCharacterAccessories(DataItemInput itemInput)
    {
        if (!playerCharBase) {
            playerCharBase = transform.parent.GetComponent <PlayerCharBase> ();
        }
        
        accessoryTransform = playerCharBase.currentSkinComponent.A_Accesories.transform;
        ClearOldAccessories();

        AttachAccessory (itemInput, accessoryTransform);
    }

    private void AttachAccessory(DataItemInput itemInput, Transform accessorySlot)
    {
        ConvertItemEffect itemEffectConverter = new ConvertItemEffect();
        itemEffectConverter.TransferDataItemInput(itemInput, dataItemInputAccessories);

        GameObject accessoryInstance = Instantiate(accessorySample, accessorySlot);
        Sample_Check sampleChecker = accessoryInstance.GetComponent<Sample_Check>();
        sampleChecker.On_b_Sample(false);
        accessoryInstance.SetActive(true);

        DisplayAccessory(itemInput, accessoryInstance);
    }

    private void DisplayAccessory(DataItemInput itemInput, GameObject accessoryInstance)
    {
        Item_Display_Logic displayLogic = new Item_Display_Logic();

        if (accessoryCode == "Helmet")
        {
            Item_Display _Item_Display = itemInput._Item_Input._Item_Display;
            accessoryInstance.GetComponent<SpriteRenderer> ().sprite = itemInput._Item_Input.Item_Sprite;
            accessoryInstance.transform.localPosition = _Item_Display.V3_Local_Position + accesoriesOffset;
            accessoryInstance.transform.localRotation = Quaternion.Euler (_Item_Display.V3_localRotation);
            accessoryInstance.transform.localScale = _Item_Display.V3_Local_Scale;

        }
    }

    public void ClearOldAccessories()
    {
        
            foreach (Transform accessory in accessoryTransform)
            {
                Sample_Check sampleChecker = accessory.GetComponent<Sample_Check>();
                if (sampleChecker != null && !sampleChecker.b_Sample)
                {
                    Destroy(accessory.gameObject);
                }
            }
        
    }
    #endregion
}
