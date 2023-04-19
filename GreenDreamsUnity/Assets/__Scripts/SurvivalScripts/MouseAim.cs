using UnityEngine;

public class MouseAim : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    private Camera mainCamera;
    
    void Update()
    {
        mainCamera = Camera.main;
        Aim();
       
    }

    private (bool success,Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
         
        if(Physics.Raycast(ray,out var hitInfo, Mathf.Infinity, groundMask))
        {
            //Hit
            return (success: true, position: hitInfo.point);
        }
        else
        {
            //Not hit
            return (success: false, position: Vector3.zero);
        }
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            //calculate direction
            var direction = position - transform.position;
            
            //ignore height diffirence
             direction.y = 0;

            //Look direction
           // transform.forward = direction;
        }
    }
}
