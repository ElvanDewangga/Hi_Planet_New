using UnityEngine;
using System.Collections;


public class SelfDestruct : MonoBehaviour {
	public float selfdestruct_in = 4; // Setting this to 0 means no selfdestruct.
	[SerializeField]
	Char_Technique _Char_Technique;
	// Char_Technique :
	public void On_Set_Time (float Cd_Time_V) {
		if (!_Char_Technique) {
			selfdestruct_in = Cd_Time_V;
		}
	}

	void Start () {
		if (_Char_Technique) {
			selfdestruct_in = _Char_Technique.Cd_Time;
		}
		if ( selfdestruct_in != 0){ 
			Destroy (gameObject, selfdestruct_in + 0.2f);
		}
	}
}
