#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Object_Variant_Config))]
public class ObjectVariantConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Reference ke ScriptableObject yang sedang di-edit
        var config = (Object_Variant_Config)target;
        // Menampilkan fi       eld Base_Duration
        // EditorGUILayout.HelpBox("Atur waktu Vfx ini sebelum dihancurkan", MessageType.Info);
        EditorGUILayout.LabelField("Cd Time Skill ini", EditorStyles.boldLabel);
        config.Base_Duration = EditorGUILayout.FloatField("Base Duration", config.Base_Duration);
        EditorGUILayout.LabelField("Waktu Vfx Hancur", EditorStyles.boldLabel);
        config.Destroy_Duration = EditorGUILayout.FloatField("Destroy Duration", config.Destroy_Duration);
        EditorGUILayout.LabelField("Bagian Part untuk Animasi ini", EditorStyles.boldLabel);
        config._Animation_Part =
            (Object_Variant_Config.Animation_Part)EditorGUILayout.EnumPopup("Use Delay Animation",
                config._Animation_Part);
        EditorGUILayout.LabelField("Apakah ada Animasi aba-aba ? sebelum memulai Vfx / Animasi Attack",
            EditorStyles.boldLabel);
        config.b_Delay_Animation = EditorGUILayout.Toggle("Use Delay Animation", config.b_Delay_Animation);
        if (config.b_Delay_Animation)
            config.Delay_Animation = (AnimationClip)EditorGUILayout.ObjectField("Delay Animation",
                config.Delay_Animation, typeof(AnimationClip), false);
        EditorGUILayout.LabelField("Apakah Vfx ini bisa dispam ?", EditorStyles.boldLabel);
        config.b_Vfx_Spam = EditorGUILayout.Toggle("Use Vfx Spam", config.b_Vfx_Spam);
        EditorGUILayout.LabelField("Apakah Vfx ini memiliki Skill Effect ? Contoh Heal + 10, dll",
            EditorStyles.boldLabel);
        config.b_Skill_Effect = EditorGUILayout.Toggle("Use Skill Effect", config.b_Skill_Effect);
        if (config.b_Skill_Effect)
            config._Skill_Effect_Setup = (Skill_Effect_Setup)EditorGUILayout.ObjectField("_Skill_Effect",
                config._Skill_Effect_Setup, typeof(Skill_Effect_Setup), false);
        EditorGUILayout.LabelField("Apakah Vfx memiliki Configuration Hit ?",
            EditorStyles.boldLabel);
        config.b_Hit_Vfx = EditorGUILayout.Toggle("Use Config_Char_Hit", config.b_Hit_Vfx);
        if (config.b_Hit_Vfx)
        config._Config_Char_Hit = (Config_Char_Hit)EditorGUILayout.ObjectField("_Config_Char_Hit",
                config._Config_Char_Hit, typeof(Config_Char_Hit), false);
        EditorGUILayout.LabelField("Apakah Vfx ini memberikan Efek Dash ?", EditorStyles.boldLabel);
        config.b_Vfx_Dash = EditorGUILayout.Toggle("b_Vfx_Dash", config.b_Vfx_Dash);
        if (config.b_Vfx_Dash)
            config.V3_Vfx_Dash_Target = EditorGUILayout.Vector3Field("V3_Target_Dash", config.V3_Vfx_Dash_Target);
        // EditorGUILayout.LabelField("Animation Clips", EditorStyles.boldLabel);
        // Field untuk memilih AnimationClip

        // Jika array belum terinisialisasi, inisialisasi dengan array kosong
        if (config.objectsWithDelay == null) config.objectsWithDelay = new Object_Variant_Config.Object_Delay[0];

        var customStyle = new GUIStyle();
        customStyle.fontSize = 20; // Mengatur ukuran font
        customStyle.fontStyle = FontStyle.Bold; // Bold atau Italic
        customStyle.normal.textColor = Color.white; // Mengubah warna teks menjadi putih
        customStyle.margin = new RectOffset(0, 0, 10, 10); // Menambahkan margin (jarak) antar elemen

        // Loop untuk menampilkan array Object_Delay
        for (var i = 0; i < config.objectsWithDelay.Length; i++)
        {
            GUILayout.Space(15); // jarak enter 15 pixel 
            EditorGUILayout.LabelField("Object Vfx " + (i + 1), customStyle);
            GUILayout.Space(15); // jarak enter 15 pixel 
            config.objectsWithDelay[i]._Object_Type =
                (Object_Variant_Config.Object_Type)EditorGUILayout.EnumPopup("Object Type",
                    config.objectsWithDelay[i]._Object_Type);

            config.objectsWithDelay[i].gameObject = (GameObject)EditorGUILayout.ObjectField("GameObject",
                config.objectsWithDelay[i].gameObject, typeof(GameObject), true);
            config.objectsWithDelay[i].delayTime =
                EditorGUILayout.FloatField("Delay Time", config.objectsWithDelay[i].delayTime);
            //  EditorGUILayout.HelpBox("Waktu Vfx dimainkan:", MessageType.Info);
            EditorGUILayout.LabelField("Waktu Vfx ditampilkan :", EditorStyles.boldLabel);
            config.objectsWithDelay[i].Vfx_Time =
                EditorGUILayout.FloatField("VFX Time", config.objectsWithDelay[i].Vfx_Time);
            EditorGUILayout.LabelField("Efek ini hilang sesuai waktu Skill_Effect_Setup ?");
            config.objectsWithDelay[i].b_Disable_Equals_Effect_Duration = EditorGUILayout.Toggle(
                "b_Disable_Equals_Effect_Dureation", config.objectsWithDelay[i].b_Disable_Equals_Effect_Duration);
            if (config.objectsWithDelay[i]._Object_Type == Object_Variant_Config.Object_Type.Vfx ||
                config.objectsWithDelay[i]._Object_Type == Object_Variant_Config.Object_Type.Default)
            {
                config.objectsWithDelay[i].V3_Scale =
                    EditorGUILayout.Vector3Field("Scale", config.objectsWithDelay[i].V3_Scale);
                config.objectsWithDelay[i].V3_Child_Scale =
                    EditorGUILayout.Vector3Field("V3 Child_Scale", config.objectsWithDelay[i].V3_Child_Scale);
                config.objectsWithDelay[i].V3_Offset_Vfx =
                    EditorGUILayout.Vector3Field("VFX Offset", config.objectsWithDelay[i].V3_Offset_Vfx);
                EditorGUILayout.LabelField("Vfx Spawn Target :", EditorStyles.boldLabel);
                config.objectsWithDelay[i]._Vfx_Spawn_Target =
                    (Object_Variant_Config.Vfx_Spawn_Target)EditorGUILayout.EnumPopup("Vfx Spawn Target",
                        config.objectsWithDelay[i]._Vfx_Spawn_Target);
                config.objectsWithDelay[i].V3_Fix_Position =
                    EditorGUILayout.Vector3Field("Fixed Position", config.objectsWithDelay[i].V3_Fix_Position);

                if (config.objectsWithDelay[i].V3_Fix_Position.y != 0)
                {
                    EditorGUILayout.LabelField(
                        "Object Rotasi: Untuk Nomor bisa dilihat script A_Asesprite (A_Object_Target_Rotation_With_Direction)",
                        EditorStyles.boldLabel);
                    config.objectsWithDelay[i].Code_Target_Position_Fix = EditorGUILayout.IntField("Object Rotate",
                        config.objectsWithDelay[i].Code_Target_Position_Fix);

                    /*
                    // Menggunakan IntField untuk menerima input
                    int byteValue = EditorGUILayout.IntField("Byte Field", (int)yourByteVariable);

                    // Validasi untuk memastikan bahwa nilai tetap dalam rentang byte (0 hingga 255)
                    byteValue = Mathf.Clamp(byteValue, 0, 255);

                    // Konversi kembali ke byte
                    yourByteVariable = (byte)byteValue;
                    */

                    EditorGUILayout.LabelField(
                        "JIka ada posisi yang kurang tepat, pas mengatur sumbu Fixed Position Y, silahkan centang yang salah :",
                        EditorStyles.boldLabel);
                    config.objectsWithDelay[i].b_Up_Left =
                        EditorGUILayout.Toggle("b_Up_Left", config.objectsWithDelay[i].b_Up_Left);
                    config.objectsWithDelay[i].b_Up_Right =
                        EditorGUILayout.Toggle("b_Up_Right", config.objectsWithDelay[i].b_Up_Right);
                    config.objectsWithDelay[i].b_Down_Left =
                        EditorGUILayout.Toggle("b_Down_Left", config.objectsWithDelay[i].b_Down_Left);
                    config.objectsWithDelay[i].b_Down_Right =
                        EditorGUILayout.Toggle("b_Down_Right", config.objectsWithDelay[i].b_Down_Right);
                }

                EditorGUILayout.LabelField("Vfx Rotation :", EditorStyles.boldLabel);
                config.objectsWithDelay[i].V3_Rotation =
                    EditorGUILayout.Vector3Field("Rotation", config.objectsWithDelay[i].V3_Rotation);

                // EditorGUILayout.HelpBox("Jika ingin vfx tidak rotasi sesuai arah karakter (Contoh Cosmic_Voltage_Attack) bisa centang ini:", MessageType.Info);
                EditorGUILayout.LabelField("Jika Vfx tidak rotasi sesuai arah karakter :", EditorStyles.boldLabel);
                config.objectsWithDelay[i].b_Fix_Rotation =
                    EditorGUILayout.Toggle("b_Fix_Rotation", config.objectsWithDelay[i].b_Fix_Rotation);


                // Checkbox untuk b_Down_Target
                EditorGUILayout.LabelField("Jika Vfx target enemy dari atas dan pasti kena Damage (Included Damage) :",
                    EditorStyles.boldLabel);
                config.objectsWithDelay[i].b_Down_Target =
                    EditorGUILayout.Toggle("Down Target", config.objectsWithDelay[i].b_Down_Target);

                // Menampilkan Down_Target_Offset hanya jika b_Down_Target dicentang
                if (config.objectsWithDelay[i].b_Down_Target)
                {
                    config.objectsWithDelay[i].b_Vfx_Damage = false;
                    config.objectsWithDelay[i].Down_Target_Col_Scale =
                        EditorGUILayout.Vector3Field("Down Target Col Scale",
                            config.objectsWithDelay[i].Down_Target_Col_Scale);
                    config.objectsWithDelay[i].Down_Target_Offset = EditorGUILayout.Vector3Field("Down Target Offset",
                        config.objectsWithDelay[i].Down_Target_Offset);
                    config.objectsWithDelay[i]._Config_Vfx_Down_Target =
                        (Config_Vfx_Down_Target)EditorGUILayout.ObjectField("Down Target Position",
                            config.objectsWithDelay[i]._Config_Vfx_Down_Target, typeof(Config_Vfx_Down_Target), true);
                }

                // EditorGUILayout.HelpBox("Jika ingin membuat vfx rotasi sekitar karakter (Contoh : Mac_Skill_2) ,bisa centang ini :", MessageType.Info);
                EditorGUILayout.LabelField("Vfx akan mengelilingi Pengguna (Mac_Skill_2) :", EditorStyles.boldLabel);
                config.objectsWithDelay[i].b_Rotate_Around =
                    EditorGUILayout.Toggle("Rotate Around", config.objectsWithDelay[i].b_Rotate_Around);
                if (config.objectsWithDelay[i].b_Rotate_Around)
                {
                    config.objectsWithDelay[i].Titik_Tengah_Start = EditorGUILayout.Vector3Field("Titik Tengah Start",
                        config.objectsWithDelay[i].Titik_Tengah_Start);
                    config.objectsWithDelay[i].Rotate_Around_Speed = EditorGUILayout.FloatField("Rotate Around Speed",
                        config.objectsWithDelay[i].Rotate_Around_Speed);
                }

                // EditorGUILayout.HelpBox("Jika ingin membuat vfx mengikuti karakter (Contoh : Laser / Beam / Mac_Skill_2) ,bisa centang ini :", MessageType.Info);
                EditorGUILayout.LabelField("Vfx akan mengikuti pemain sambil berjalan (Laser/Beam/Mac_Skill_2) :",
                    EditorStyles.boldLabel);
                config.objectsWithDelay[i].b_Follow_Char =
                    EditorGUILayout.Toggle("Follow_Char", config.objectsWithDelay[i].b_Follow_Char);

                // EditorGUILayout.HelpBox("Centang ini jika saat mengaktifkan effect Beberapa bagian objek di sembunyikan (Contoh : Mac_Skill_2) ,bisa centang ini :", MessageType.Info);
                EditorGUILayout.LabelField("Menyembunyikan Object saat vfx dimainkan (Mac_Skill_2) :",
                    EditorStyles.boldLabel);
                config.objectsWithDelay[i].b_Hide_Object =
                    EditorGUILayout.Toggle("Hide_Object", config.objectsWithDelay[i].b_Hide_Object);
                if (config.objectsWithDelay[i].b_Hide_Object)
                {
                    config.objectsWithDelay[i].Hide_Object_Hand_Left = EditorGUILayout.Toggle("Hand Left",
                        config.objectsWithDelay[i].Hide_Object_Hand_Left);
                    config.objectsWithDelay[i].Hide_Object_Hand_Right = EditorGUILayout.Toggle("Hand Right",
                        config.objectsWithDelay[i].Hide_Object_Hand_Right);
                }

                EditorGUILayout.LabelField("Vfx dapat memberikan Damage ketika tersentuh :", EditorStyles.boldLabel);
                config.objectsWithDelay[i].b_Vfx_Damage =
                    EditorGUILayout.Toggle("Vfx Damage", config.objectsWithDelay[i].b_Vfx_Damage);
                if (config.objectsWithDelay[i].b_Vfx_Damage)
                {
                    config.objectsWithDelay[i].b_Down_Target = false;
                    config.objectsWithDelay[i].Vfx_Damage_Power =
                        EditorGUILayout.IntField("Power", config.objectsWithDelay[i].Vfx_Damage_Power);
                    config.objectsWithDelay[i].V3_Scale_Damage =
                        EditorGUILayout.Vector3Field("Scale", config.objectsWithDelay[i].V3_Scale_Damage);
                    config.objectsWithDelay[i].V3_Offset_Damage =
                        EditorGUILayout.Vector3Field("Offset", config.objectsWithDelay[i].V3_Offset_Damage);
                }

                EditorGUILayout.LabelField("Centang ini jika serangan memiliki batas Hit (misal : 1 pemain)",
                    EditorStyles.boldLabel);
                config.objectsWithDelay[i].b_Limit_Hit =
                    EditorGUILayout.Toggle("b_Limit_Hit", config.objectsWithDelay[i].b_Limit_Hit);

                EditorGUILayout.LabelField("Ketika Pengguna Vfx kena damage, maka akan memainkan Vfx ini",
                    EditorStyles.boldLabel);
                config.objectsWithDelay[i].b_Active_When_Damage = EditorGUILayout.Toggle("b_Active_When_Damage",
                    config.objectsWithDelay[i].b_Active_When_Damage);

                EditorGUILayout.LabelField(
                    "Vfx akan disembunyikan (Biasa digunakan dan dipanggil dengan metode khusus Contoh : Config_Vfx_Down_Target [Config_A_Position])",
                    EditorStyles.boldLabel);
                config.objectsWithDelay[i].b_Hide_Vfx =
                    EditorGUILayout.Toggle("b_Hide_Vfx", config.objectsWithDelay[i].b_Hide_Vfx);

                EditorGUILayout.LabelField("b_Light_Setup", EditorStyles.boldLabel);
                config.objectsWithDelay[i].b_Light_Setup =
                    EditorGUILayout.Toggle("b_Light_Setup", config.objectsWithDelay[i].b_Light_Setup);
                if (config.objectsWithDelay[i].b_Light_Setup)
                    config.objectsWithDelay[i]._Config_Light_Setup = (Config_Light_Setup)EditorGUILayout.ObjectField(
                        "Light Setup", config.objectsWithDelay[i]._Config_Light_Setup, typeof(Config_Light_Setup),
                        false);
            }
            else if (config.objectsWithDelay[i]._Object_Type == Object_Variant_Config.Object_Type.Shader)
            {
                #region Object_Type (Shader)

                EditorGUILayout.LabelField(
                    "C_Code_Skin bisa didapat ditiap karakter (Char_Pack - A_Seprite - A_Char_Skin)",
                    EditorStyles.boldLabel);
                config.objectsWithDelay[i].C_Code_Skin =
                    EditorGUILayout.TextField("C_Code_Skin", config.objectsWithDelay[i].C_Code_Skin);

                EditorGUILayout.LabelField("Nama Component yang ingin Di apply :", EditorStyles.boldLabel);

                // Menampilkan array A_Shader_Component_Name sebagai list yang bisa diedit
                for (var xx = 0; xx < config.objectsWithDelay[i].A_Shader_Component_Name.Length; xx++)
                    // Menampilkan field array untuk di-edit
                    config.objectsWithDelay[i].A_Shader_Component_Name[xx] =
                        EditorGUILayout.TextField("Component " + (xx + 1),
                            config.objectsWithDelay[i].A_Shader_Component_Name[xx]);

                // Tombol untuk menambah elemen baru
                if (GUILayout.Button("Add Shader Component", GUILayout.Width(175), GUILayout.Height(25)))
                {
                    Array.Resize(ref config.objectsWithDelay[i].A_Shader_Component_Name,
                        config.objectsWithDelay[i].A_Shader_Component_Name.Length + 1);
                    config.objectsWithDelay[i]
                            .A_Shader_Component_Name[config.objectsWithDelay[i].A_Shader_Component_Name.Length - 1] =
                        "New Component"; // Nama default untuk komponen baru
                }

                // Pilih elemen untuk dihapus
                if (config.objectsWithDelay[i].A_Shader_Component_Name.Length > 0)
                {
                    EditorGUILayout.LabelField("Pilih Component untuk dihapus:", EditorStyles.boldLabel);

                    // Pilihan array dalam bentuk popup/dropdown
                    var selectedIndex = 0; // Inisialisasi index untuk dropdown
                    var shaderComponentNames =
                        config.objectsWithDelay[i].A_Shader_Component_Name; // Ambil array nama komponen
                    selectedIndex = EditorGUILayout.Popup("Select Component", selectedIndex, shaderComponentNames);

                    // Tombol untuk menghapus elemen yang dipilih
                    if (GUILayout.Button("Delete Component", GUILayout.Width(150), GUILayout.Height(25)))
                    {
                        // Hapus elemen array yang dipilih
                        for (var xx = selectedIndex;
                             xx < config.objectsWithDelay[i].A_Shader_Component_Name.Length - 1;
                             xx++)
                            config.objectsWithDelay[i].A_Shader_Component_Name[xx] =
                                config.objectsWithDelay[i].A_Shader_Component_Name[xx + 1];

                        // Resize array untuk menghapus elemen terakhir setelah pergeseran
                        Array.Resize(ref config.objectsWithDelay[i].A_Shader_Component_Name,
                            config.objectsWithDelay[i].A_Shader_Component_Name.Length - 1);
                    }
                }

                #endregion
            }
            // Loop untuk array A_V3_Child_Scale
            /*
 if (config.objectsWithDelay[i].A_V3_Child_Scale != null)
 {
     EditorGUILayout.LabelField("Child Scales", EditorStyles.boldLabel);

     // Tampilkan elemen-elemen dari array
     for (int j = 0; j < config.objectsWithDelay[i].A_V3_Child_Scale.Length; j++)
     {
         config.objectsWithDelay[i].A_V3_Child_Scale[j] = EditorGUILayout.Vector3Field($"Child Scale {j+1}", config.objectsWithDelay[i].A_V3_Child_Scale[j]);
     }

     // Tombol untuk menambah elemen ke array A_V3_Child_Scale

     if (GUILayout.Button("Add Child Scale"))
     {
         AddNewChildScale(config.objectsWithDelay[i]);
     }

     // Tombol untuk menghapus elemen terakhir dari array A_V3_Child_Scale
     if (config.objectsWithDelay[i].A_V3_Child_Scale.Length > 0 && GUILayout.Button("Remove Last Child Scale"))
     {
         RemoveLastChildScale(config.objectsWithDelay[i]);
     }
 }
 */


            // Simpan perubahan
            if (GUI.changed) EditorUtility.SetDirty(config);
            // Tombol untuk menghapus elemen
            if (GUILayout.Button("Remove Object Delay")) RemoveObjectDelayAtIndex(i, config);
        }

        // Tombol untuk menambah elemen baru
        if (GUILayout.Button("Add Object Delay")) AddNewObjectDelay(config);

        // Simpan perubahan pada ScriptableObject
        if (GUI.changed) EditorUtility.SetDirty(target);
    }

/*
// Method untuk menambah elemen baru ke array A_V3_Child_Scale
private void AddNewChildScale(Object_Variant_Config.Object_Delay objectDelay)
{
    var newList = new Vector3[objectDelay.A_V3_Child_Scale.Length + 1];
    for (int k = 0; k < objectDelay.A_V3_Child_Scale.Length; k++)
    {
        newList[k] = objectDelay.A_V3_Child_Scale[k];
    }
    newList[newList.Length - 1] = Vector3.one;  // Tambahkan elemen baru dengan nilai default
    objectDelay.A_V3_Child_Scale = newList;
}

// Method untuk menghapus elemen terakhir dari array A_V3_Child_Scale
private void RemoveLastChildScale(Object_Variant_Config.Object_Delay objectDelay)
{
    if (objectDelay.A_V3_Child_Scale.Length > 0)
    {
        var newList = new Vector3[objectDelay.A_V3_Child_Scale.Length - 1];
        for (int k = 0; k < newList.Length; k++)
        {
            newList[k] = objectDelay.A_V3_Child_Scale[k];
        }
        objectDelay.A_V3_Child_Scale = newList;
    }
}
*/
    // Method untuk menambah elemen baru ke array
    private void AddNewObjectDelay(Object_Variant_Config config)
    {
        var newList = new Object_Variant_Config.Object_Delay[config.objectsWithDelay.Length + 1];
        for (var i = 0; i < config.objectsWithDelay.Length; i++) newList[i] = config.objectsWithDelay[i];
        newList[newList.Length - 1] = new Object_Variant_Config.Object_Delay();
        config.objectsWithDelay = newList;
    }

    // Method untuk menghapus elemen dari array
    private void RemoveObjectDelayAtIndex(int index, Object_Variant_Config config)
    {
        var newList = new Object_Variant_Config.Object_Delay[config.objectsWithDelay.Length - 1];
        for (int i = 0, j = 0; i < config.objectsWithDelay.Length; i++)
        {
            if (i == index) continue;
            newList[j++] = config.objectsWithDelay[i];
        }

        config.objectsWithDelay = newList;
    }
}
#endif