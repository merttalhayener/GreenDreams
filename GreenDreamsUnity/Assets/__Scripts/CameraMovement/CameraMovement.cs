using UnityEngine;
using Cinemachine;
using System.Drawing;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    CinemachineComponentBase componentBase;
    float cameraDistance;
    [SerializeField] float sensivity = 10f;

    public Transform PlayerCameraRoot;
    public GameObject PlayerFollowCamera;
    private Vector3 _cameraOffset;
    
    [Range(0.01f, 100.0f)]
    public float SmoothFactor ;

    public bool LookAtPlayer = false;
    public bool RotateAroundPlayer = true;
    public float RotationSpeed = 5f;

    public bool pressedScroolButton;
    void Start()
    {
        _cameraOffset = transform.position - PlayerCameraRoot.position;
    }

   
    void LateUpdate()
    {
        
        
        if (componentBase == null)
        {
            componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        }

        if(Input.GetAxis("Mouse ScrollWheel") !=0)
        {
            cameraDistance = Input.GetAxis("Mouse ScrollWheel") * sensivity;
            if (componentBase is CinemachineFramingTransposer)
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance = Mathf.Clamp(((componentBase as CinemachineFramingTransposer).m_CameraDistance),5,10);
                (componentBase as CinemachineFramingTransposer).m_CameraDistance -= cameraDistance;
            }
        }
     

        if (Input.GetMouseButton(2))
        {
           // PlayerFollowCamera.SetActive(false);
            pressedScroolButton = true;
          //  PlayerFollowCamera.transform.position = this.transform.position;
           // PlayerFollowCamera.transform.rotation = this.transform.rotation;
           
        }
        else
        {
            pressedScroolButton = false;
           // PlayerFollowCamera.SetActive(true);
        }
        if (RotateAroundPlayer && pressedScroolButton)
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);
            _cameraOffset = camTurnAngle* _cameraOffset;
        }

        Vector3 newPos = PlayerCameraRoot.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer || RotateAroundPlayer)
        {
            transform.LookAt(PlayerCameraRoot);
        }
    }
}
