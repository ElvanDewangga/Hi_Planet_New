using UnityEngine;
using System.Collections;

public class DualJoystickPlayerController : MonoBehaviour
{
    // AccountManager :
    public void InitializePlayer (GameObject playerGoV) {
        On_Input_Data_Player (playerGoV);
    }
    private GameObject World_Camera;
    GameObject Player_Go;
    public LeftJoystick leftJoystick; // the game object containing the LeftJoystick script
    public RightJoystick rightJoystick; // the game object containing the RightJoystick script
    public float moveSpeed = 0.8f; // movement speed of the player character
    public int rotationSpeed = 8; // rotation speed of the player character
    private Vector3 leftJoystickInput; // holds the input of the Left Joystick
    private Vector3 rightJoystickInput; // hold the input of the Right Joystick
    public bool bool_Click_Left = false; // false = player x tekan. true = Player v Tekan.
    public CharacterController Player_3d_Controller;
    public string M_Character = "";
    [SerializeField]
    DualJoystickSource _DualJoystickSource;
    [SerializeField]
    float sphereDistance =0.0f;
    // Variabel Sphere_Direction untuk rotasi
    private GameObject Sphere_Direction;

    // Tambahkan variabel untuk memeriksa apakah pemain sedang menahan
    private bool isHolding = false;

    // Threshold untuk menentukan apakah jari dilepas di tengah atau tidak
    public float centerThreshold = 0.2f;
    public float distanceFromCenter;
    public Vector3 lastRightJoystickInput;
    public CharBase charBase;
    public void On_Input_Data_Player(GameObject s) {
        Player_Go = s;
        charBase = Player_Go.GetComponent<PlayerCharBase>();
        World_Camera = HiGameManager.cameraMove.gameObject;
        Player_3d_Controller = Player_Go.GetComponent<CharacterController>();
        Sphere_Direction = Player_Go.GetComponent<PlayerCharBase> () .sphereDirection;
    }

    #region Char_Dash
    public bool b_Dash = false;
    public void On_Dash (bool s) {
        b_Dash = s;
    }
    #endregion
    void FixedUpdate() {
        if (Player_Go != null) {
            leftJoystickInput = leftJoystick.GetInputDirection();
            rightJoystickInput = rightJoystick.GetInputDirection();

            float xMovementLeftJoystick = leftJoystickInput.x; 
            float zMovementLeftJoystick = leftJoystickInput.y; 
            float xMovementRightJoystick = rightJoystickInput.x; 
            float zMovementRightJoystick = rightJoystickInput.y; 
            
             if (leftJoystickInput != Vector3.zero) {
                
                
                if (!isHolding) {
                    isHolding = true;
                   // Debug.Log("Hold");
                    Control.instance.OnAim ();
                }

                // Simpan input joystick terakhir saat bergerak
                lastRightJoystickInput = leftJoystickInput;

                    // Hitung arah dari input leftJoystickInput
                Vector3 direction = new Vector3(leftJoystickInput.x, leftJoystickInput.y, 0);

                // Normalisasi arah untuk menjaga konsistensi gerakan
                direction.Normalize();

                // Gunakan sphereDistance untuk mengatur jarak dari pemain ke Sphere_Direction
                Vector3 newPosition = Player_Go.transform.position + direction * sphereDistance;

                // Perbarui posisi Sphere_Direction
                Sphere_Direction.transform.position = newPosition;

                // Optionally, jika ingin menambahkan rotasi tambahan
                Sphere_Direction.transform.RotateAround(Player_Go.transform.position, Vector3.forward, rotationSpeed * Time.fixedDeltaTime);
                Control.instance.OnAimingDirectionObjectRotateAround (direction);
                

            } else if (isHolding) {
                // Lepas jari dari joystick
                isHolding = false;
                distanceFromCenter = lastRightJoystickInput.magnitude;
                if (distanceFromCenter <= centerThreshold) {
                 //   Debug.Log("Cancel Attack");
                    Control.instance.OffAim ();
                } else {
                 //   Debug.Log("Attack");
                //    _Char_Utama._Char_Attack.PerformAttack();
                    Control.instance.OffAim ();
                }
            }

            if (leftJoystickInput != Vector3.zero && rightJoystickInput == Vector3.zero) {
                float tempAngle = Mathf.Atan2(zMovementLeftJoystick, xMovementLeftJoystick);
                xMovementLeftJoystick *= Mathf.Abs(Mathf.Cos(tempAngle));
                zMovementLeftJoystick *= Mathf.Abs(Mathf.Sin(tempAngle));

                Vector3 temp = World_Camera.transform.position;
                temp.x += xMovementLeftJoystick;
                temp.z += zMovementLeftJoystick;
                Vector3 lookDirection = temp - World_Camera.transform.position;

                if (lookDirection != Vector3.zero) {
                    leftJoystickInput = new Vector3(xMovementLeftJoystick, zMovementLeftJoystick, 0);
                    leftJoystickInput = World_Camera.transform.TransformDirection(leftJoystickInput);
                    leftJoystickInput *= moveSpeed;
                }

                Player_3d_Controller.Move(new Vector2(leftJoystickInput.x, leftJoystickInput.y) * Time.fixedDeltaTime);
                
                if (Player_Go.gameObject.activeInHierarchy) {
                    if (leftJoystickInput.x > 0) {
                        Player_Go.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                       // Latest _Char_Utama._Char_Direction_2d.On_Char_Direction_2d("Right");
                       charBase.On_Char_Direction_2d ("Right");
                    } else if (leftJoystickInput.x < 0) {
                        Player_Go.transform.localRotation = Quaternion.Euler(-90, 0, 180);
                      // Latest  _Char_Utama._Char_Direction_2d.On_Char_Direction_2d("Left");
                      charBase.On_Char_Direction_2d ("Left");
                    }
                }

                bool_Click_Left = true;

            } else {
                if (bool_Click_Left == true) {

                }
            }

            if (leftJoystickInput == Vector3.zero && rightJoystickInput != Vector3.zero) {
                rightJoystickInput = rightJoystick.GetInputDirection();
                if (Player_Go.gameObject.activeInHierarchy) {
                    if (rightJoystickInput.x > 0) {
                        Player_Go.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                        charBase.On_Char_Direction_2d("Right");
                    } else if (rightJoystickInput.x < 0) {
                        Player_Go.transform.localRotation = Quaternion.Euler(-90, 0, 180);
                        charBase.On_Char_Direction_2d("Left");
                    }
                }
            }

            if (leftJoystickInput != Vector3.zero && rightJoystickInput != Vector3.zero) {
                float tempAngleInputRightJoystick = Mathf.Atan2(zMovementRightJoystick, xMovementRightJoystick);
                xMovementRightJoystick *= Mathf.Abs(Mathf.Cos(tempAngleInputRightJoystick));
                zMovementRightJoystick *= Mathf.Abs(Mathf.Sin(tempAngleInputRightJoystick));

                Vector3 temp = transform.position;
                temp.x += xMovementRightJoystick;
                temp.z += zMovementRightJoystick;
                Vector3 lookDirection = temp - transform.position;

                float tempAngle = Mathf.Atan2(zMovementLeftJoystick, xMovementLeftJoystick);
                xMovementLeftJoystick *= Mathf.Abs(Mathf.Cos(tempAngle));
                zMovementLeftJoystick *= Mathf.Abs(Mathf.Sin(tempAngle));

                Vector3 temp_2 = World_Camera.transform.position;
                temp_2.x += xMovementLeftJoystick;
                temp_2.z += zMovementLeftJoystick;
                Vector3 lookDirection_2 = temp_2 - World_Camera.transform.position;

                if (lookDirection_2 != Vector3.zero) {
                    leftJoystickInput = new Vector3(xMovementLeftJoystick, zMovementLeftJoystick, 0);
                    leftJoystickInput = World_Camera.transform.TransformDirection(leftJoystickInput);
                    leftJoystickInput *= moveSpeed;
                }

                Player_3d_Controller.Move(new Vector2(leftJoystickInput.x, leftJoystickInput.y) * Time.fixedDeltaTime);
                if (Player_Go.gameObject.activeInHierarchy) {

                }
            }
        }
    }
}
