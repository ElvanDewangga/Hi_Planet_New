using Unity.VisualScripting;
using System;
using UnityEngine;
using System.Reflection;
using Item_Ability;

public class PlayerCharBase : CharBase {
   #region PlayerBody
    [SerializeField] private GameObject playerBodyPrefab;
    [HideInInspector] public PlayerBody playerBody;
    [HideInInspector] public PlayerBody.SkinComponent currentSkinComponent;
    [HideInInspector] public Object_Rotation_Target_With_Direction handLeftRotation;
    [HideInInspector] public Object_Rotation_Target_With_Direction handRightRotation;
    [HideInInspector] public Animator animatorBody;
    [HideInInspector] public Animator animatorHandLeft;
    [HideInInspector] public Animator animatorHandRight;
    public string latestSkin = "Normal";
    // CharBase
    public override void InitializePlayerBody () {
        AccountManager.instance.AddPlayer (this);
        playerBody = GameObject.Instantiate (playerBodyPrefab).GetComponent <PlayerBody> ();
        playerBody.transform.SetParent (this.transform);
        playerBody.gameObject.SetActive (true);
        ChangeSkinComponent (latestSkin);
        playerBody.transform.localScale = new Vector3 (1,1,1);
        playerBody.transform.localPosition = new Vector3 (0,0,0);
    }
    
    public void ChangeSkinComponent (string skinName) {
        currentSkinComponent = playerBody.ChangeSkinComponent (skinName);
        animatorBody = currentSkinComponent.animatorBody.GetComponent <Animator> ();
        animatorHandLeft = currentSkinComponent.animatorHandLeft.GetComponent <Animator> ();
        animatorHandRight = currentSkinComponent.animatorHandRight.GetComponent <Animator> ();

        animatorHandLeft.runtimeAnimatorController = charStatus.animatorHandLeft;
        animatorHandRight.runtimeAnimatorController = charStatus.animatorHandRight;
        animatorBody.runtimeAnimatorController = charStatus.animatorBody;
        RefreshAllEquipmentDisplay ();
    }

    public void ChangeLatestSkinComponent () {
        currentSkinComponent = playerBody.ChangeSkinComponent (latestSkin);
        animatorBody = currentSkinComponent.animatorBody.GetComponent <Animator> ();
        animatorHandLeft = currentSkinComponent.animatorHandLeft.GetComponent <Animator> ();
        animatorHandRight = currentSkinComponent.animatorHandRight.GetComponent <Animator> ();

        animatorHandLeft.runtimeAnimatorController = charStatus.animatorHandLeft;
        animatorHandRight.runtimeAnimatorController = charStatus.animatorHandRight;
        animatorBody.runtimeAnimatorController = charStatus.animatorBody;
        RefreshAllEquipmentDisplay ();
    }

    public void SetCharBodyComponentSprite(string componentCode, Sprite sprite)
    {
        
    }

    public void DeactivateAllBodyComponents()
    {
        
    }
    #endregion
    #region PlayerData
    public override void SetupPlayerData(string Username, string[] Result)
    {
            int.TryParse(Result[2], out var Level_V);
            int.TryParse(Result[3], out var Bonus_Point_V);
            int.TryParse(Result[9], out var Cur_Exp_V);
            InitializeCharacterLevel(Level_V, Bonus_Point_V, Cur_Exp_V);

            int.TryParse(Result[4], out var life_V);
            int.TryParse(Result[5], out var attack_V);
            int.TryParse(Result[6], out var defense_V);
            int.TryParse(Result[7], out var speed_V);
            int.TryParse(Result[8], out var intelligence_V);
            lifeBonus = life_V; attackBonus = attack_V; defenseBonus = defense_V; speedBonus = speed_V; intelligenceBonus = intelligence_V;
            UpdateBonusPoint (life_V, attack_V, defense_V, speed_V, intelligence_V);

            int.TryParse(Result[10], out var Helmet_V);
            int.TryParse(Result[11], out var Armor_V);
            int.TryParse(Result[12], out var Drone_V);
            int.TryParse(Result[13], out var Wing_V);
            int.TryParse(Result[14], out var intelligence_Cube_V);
            int.TryParse(Result[15], out var Fire_V);
            int.TryParse(Result[16], out var Water_V);
            int.TryParse(Result[17], out var Wood_V);
            int.TryParse(Result[18], out var Metal_V);
            int.TryParse(Result[19], out var Stone_V);

            SetupInitialEquipment(Helmet_V, Armor_V, Drone_V, Wing_V, intelligence_Cube_V);
            UpdateUsername(Result[1]);
            Initializelife();
    }

     public void UpdateBonusPoint(int lifeBonus, int attackBonus, int defenseBonus, int speedBonus, int intelligenceBonus)
    {
       
        life += lifeBonus;
        attack += attackBonus;
        defense += defenseBonus;
        speed += speedBonus;
        intelligence += intelligenceBonus;
        
    }
    
    public void UpdateUsername(string username)
    {
        Username = username;
        SetUsernameText (username);
        HUDManager.instance.UpdateUsername (username);
    }
    #endregion
    #region CharBase
    public override void Initializelife()
    {
        base.Initializelife ();
    }

    public override void ChangeGameObjectName () {
        this.gameObject.name = "Player(Clone)";
    }

   // public override void SetCurrentHpBar(int curHp){ base.SetCurrentHpBar (curHp);}
   // public override void SetMaxHpBar(int maxHp) { base.SetCurrentHpBar (maxHp);}
    #endregion
    #region Equipment

        // Data item untuk equipment
     public DataItemInput dataItemHelmet;
     public DataItemInput dataItemArmor;
     public DataItemInput dataItemDrone;
     public DataItemInput dataItemWing;
     public DataItemInput dataItemintelligenceCube;

    private int targetId;

    private void RefreshAllEquipmentDisplay()
    {
            SetCharAccessories(dataItemHelmet);
            SetCharAccessories(dataItemArmor);
            SetCharAccessories(dataItemDrone);
            SetCharAccessories(dataItemWing);
            SetCharAccessories(dataItemintelligenceCube);
    }

    // Char_Data_Equipment
    public DataItemInput GetEquipmentDataItem(string equipmentType)
    {
        return equipmentType switch
        {
            "Helmet" => dataItemHelmet,
            "Armor" => dataItemArmor,
            "Drone" => dataItemDrone,
            "Wing" => dataItemWing,
            "intelligence_Cube" => dataItemintelligenceCube,
             _ => null
        };
    }

        private void SetEquipmentId(string equipmentType, int value)
        {
            var fieldInfo = GetType().GetField($"{equipmentType}_Id", BindingFlags.Public | BindingFlags.Instance);
            fieldInfo?.SetValue(this, value);
        }

        private void GetEquipmentId(string equipmentType)
        {
            var fieldInfo = GetType().GetField($"{equipmentType}_Id", BindingFlags.Public | BindingFlags.Instance);
            if (fieldInfo != null)
            {
                var fieldValue = fieldInfo.GetValue(this);
                targetId = Convert.ToInt32(fieldValue);
            }
        }

        public void SetupInitialEquipment(int helmetId, int armorId, int droneId, int wingId, int intelligenceCubeId)
        {

            if (helmetId >= 0) RetrieveInventoryItem(helmetId, dataItemHelmet);
            SetEquipmentId("Helmet", helmetId);
            SetEquipmentId("Armor", armorId);
            SetEquipmentId("Drone", droneId);
            SetEquipmentId("Wing", wingId);
            SetEquipmentId("intelligence_Cube", intelligenceCubeId);

            RefreshEquipmentStatus(dataItemHelmet);
            RefreshEquipmentStatus(dataItemArmor);
            RefreshEquipmentStatus(dataItemDrone);
            RefreshEquipmentStatus(dataItemWing);
            RefreshEquipmentStatus(dataItemintelligenceCube);

            RefreshAllEquipmentDisplay();
            
        }

        private void RefreshEquipmentStatus(DataItemInput dataItem)
        {
            foreach (var effect in dataItem.L_Data_Item_Effect)
            {
                UpdateEquipmentValue("Equip", effect.Code_Effect[0], effect.Code_Value[0]);
            }
            Restore();
        }

        [SerializeField] private DataItemInput itemInventory;

        public void EquipItem(int inventoryItemId, string slotType)
        {
            UnequipItem (slotType);
            
            targetId = inventoryItemId;
            var equipmentData = GetEquipmentDataItem(slotType);

            RetrieveInventoryItem(inventoryItemId, itemInventory);
            TransferItemData(itemInventory, equipmentData);

            foreach (var effect in equipmentData.L_Data_Item_Effect)
            {
                UpdateEquipmentValue("Equip", effect.Code_Effect[0], effect.Code_Value[0]);
            }

            RefreshAllEquipmentDisplay();
        }

        public void UnequipItem(string slotType)
        {
            var equipmentData = GetEquipmentDataItem(slotType);

            GetEquipmentId(slotType);

            foreach (var effect in equipmentData.L_Data_Item_Effect)
            {
                UpdateEquipmentValue("Unequip", effect.Code_Effect[0], effect.Code_Value[0]);
            }

            RefreshItemData(itemInventory);
            TransferItemData(itemInventory, equipmentData);

            UnequipCharAccessory(slotType);
        }
    
    #endregion
    #region HUDManager
     public override void RefreshHUDCurrentHp(int hp)
    {
        HUDManager.instance.UpdateCurrentHp (hp);
    }

    public override void RefreshHUDMaxHp(int maxHp)
    {
        HUDManager.instance.UpdateMaxHp (maxHp);
    }
   
    #endregion
    
    #region CharLevel
        private int level;
        private int exp;
        private int maxExp;
        public int bonusPoint; 
        private void InitializeMaxExp()
        {
            maxExp = LevelManager.instance.GetNextLevelExpRequirement(level);
            CheckLevelUp();
        }

        // Example function to increment exp
        private void IncrementExpExample()
        {
            IncreaseExp(15);
        }

        // Increases exp by a specified amount
        // Exp_Particle
        public void IncreaseExp(int amount)
        {
            if (level < LevelManager.instance.MaxLevel)
            {
                exp += amount;
                CheckLevelUp();
            }
            HUDManager.instance.UpdateExp(exp, maxExp);
            SaveLevelAndExp();
        }

        // Checks if character qualifies for level up
        private void CheckLevelUp()
        {
            while (exp >= maxExp)
            {
                exp -= maxExp;
                level++;
                AddBonusPoints(4);
                HUDManager.instance.UpdateLevel(level);
                InitializeMaxExp();
                HUDManager.instance.UpdateExp(exp, maxExp);
            }
        }



        public void InitializeCharacterLevel(int initialLevel, int initialBonusPoints, int initialExp)
        {
            level = initialLevel;
            bonusPoint = initialBonusPoints;
            exp = initialExp;
            InitializeMaxExp();
            HUDManager.instance.UpdateLevel(level);
            HUDManager.instance.UpdateExp (exp,maxExp);
        }

        
        private void AddBonusPoints(int points)
        {
            bonusPoint += points;
            var Host_Server_Value = new string [4]; // 1 For Id *3 for (table,title,value) *2 for (Id & Own)
            var Host_Server_Field = new string [4];
            Host_Server_Field[0] = "Id";
            Host_Server_Value[0] = HiGameManager.instance._dataGameManager.id;
            Host_Server_Field[1] = "table_1";
            Host_Server_Value[1] = "Db_Equipment";
            Host_Server_Field[2] = "title_1";
            Host_Server_Value[2] = "Bonus_Point";
            Host_Server_Field[3] = "value_1";
            Host_Server_Value[3] = bonusPoint.ToString ();

            HiGameManager.instance._dataGameManager._DataGameEquipment.StartSave(Host_Server_Field, Host_Server_Value);
        }
        // Char_Data_Bonus_Point
        public void RemoveBonusPoints(int points)
        {
            bonusPoint -= points;
        }

        int attackBonus, defenseBonus, lifeBonus, speedBonus, intelligenceBonus;
        public int On_Char_Data_Bonus_Point(string code)
        {
            var Result = 0;
            switch (code)
            {
                case "Attack":
                    attackBonus += 1;
                    attack += 1;
                    Result = attackBonus;
                    break;
                case "Defense":
                    defenseBonus += 1;
                    defense += 1;
                    Result = defenseBonus;
                    break;
                case "Life":
                    lifeBonus += 1;
                    life += 1;
                    Initializelife ();
                    Result = lifeBonus;
                    break;
                case "Speed":
                    speedBonus += 1;
                    speed += 1;
                    Result = speedBonus;
                    break;
                case "Intelligence":
                    intelligenceBonus += 1;
                    intelligence += 1;
                    Result = intelligenceBonus;
                    break;
                default:
                    Debug.LogError("Status Code " + code + "tidak ditemui atau error.");
                    break;
            }

            return Result;
        }

        private void SaveLevelAndExp()
        {
            // Implement save logic for level and exp
            string [] Host_Server_Value = new string [7]; // 1 For Id *3 for (table,title,value) *2 for (Id & Own)
            string [] Host_Server_Field = new string [7];
            Host_Server_Field[0] = "Id";Host_Server_Value[0] = HiGameManager.instance._dataGameManager.id;
            Host_Server_Field[1] = "table_1";Host_Server_Value[1] = "Db_Equipment";
            Host_Server_Field[2] = "title_1";Host_Server_Value[2] = "Level";
            Host_Server_Field[3] = "value_1";Host_Server_Value[3] = level.ToString ();

            Host_Server_Field[4] = "table_2";Host_Server_Value[4] = "Db_Equipment";
            Host_Server_Field[5] = "title_2";Host_Server_Value[5] = "Cur_Exp";
            Host_Server_Field[6] = "value_2";Host_Server_Value[6] = exp.ToString ();

            HiGameManager.instance._dataGameManager._DataGameEquipment.StartSave (Host_Server_Field, Host_Server_Value);
            Debug.Log ("Save Exp and level");
        }
    #endregion
    #region Control
    public GameObject sphereDirection;
    [SerializeField] PlayerAccessories [] playerAccesories;
    #endregion
    #region Char Accessories

    public void SetCharAccessories(DataItemInput accessoryData)
    {
        foreach (var accessory in playerAccesories)
        {
            if (accessory.accessoryCode == accessoryData._Item_Input.Type)
            {
                accessory.SetCharacterAccessories(accessoryData);
                return;
            }
        }
    }

    public void UnequipCharAccessory(string accessoryType)
    {
        foreach (var accessory in playerAccesories)
        {
            if (accessory.accessoryCode == accessoryType)
            {
                accessory.ClearOldAccessories();
                return;
            }
        }
    }
    #endregion
    #region Inventory Management

    public void RetrieveInventoryItem(int index, DataItemInput targetData)
    {
        var itemData = HiGameManager.inventoryManager.dataItemInputs[index];
        TransferItemData(itemData, targetData);
    }

    public void TransferItemData(DataItemInput sourceData, DataItemInput targetData)
    {
        ConvertItemEffect itemEffectConverter = new ConvertItemEffect();
        itemEffectConverter.TransferDataItemInput(sourceData, targetData);
    }

    public void RefreshItemData(DataItemInput targetData)
    {
        ConvertItemEffect itemEffectConverter = new ConvertItemEffect();
        itemEffectConverter.RefreshDataItemInput(targetData);
    }

    #endregion
}
