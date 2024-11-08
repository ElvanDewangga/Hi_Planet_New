using System.Collections;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1.X509;
using UnityEngine;

public class Tutorial_Quest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    npcDialog Target_Dialog;
    void Pemanggilan_Quest_Dialog () {
        StartDialog.instance.target = Target_Dialog;
        StartDialog.instance.BeginChat();
        // Tekan b di keyboard untuk memulai percakapan.
    }

    void Pemanggilan_Quest_Option () {
        StartDialog.instance.target = Target_Dialog;
        StartDialog.instance.BeginChat();
        // Tekan b di keyboard untuk memulai percakapan.
    }

    void Start() {
        Pemanggilan_Quest_Dialog ();
    }
}
