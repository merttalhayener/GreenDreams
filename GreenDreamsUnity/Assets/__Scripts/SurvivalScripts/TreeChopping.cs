using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChopping : MonoBehaviour
{
   [SerializeField] private OutlineSelecter selector;
   [SerializeField] private Animator animator;
   [SerializeField] private TreeManager treeManager;
   [SerializeField] private ThirdPersonController thirdPersonController;
                    public float damage;
    private PlayerStatsManager playerStatsManager;

    private ObjectTypeManager typeManager;
    
    public Vector3 distanceToTree;

    public AudioClip choppingClip;
    public AudioSource source;

    private void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.E) && selector.selectedObject !=null && playerStatsManager.HasStamina)
        {
            treeManager = selector.selectedObject.GetComponent<TreeManager>();
            typeManager = selector.selectedObject.GetComponent<ObjectTypeManager>();
            ChopTree();
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("TreeChopHorizontal"))
        {
            thirdPersonController.enabled = false;
        }
        else
        {
            thirdPersonController.enabled = true;
        }
    }

    public void ChopTree()
    {
        if (typeManager.objectType.type.ToString() == "Tree" && selector.distance <= 1.5f)
        {
            transform.LookAt(selector.selectedObject.transform.position);
         
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("TreeChopHorizontal"))
            {
                animator.SetTrigger("ChopTree");
            }

        }
    }

    public void CalculateDistance()
    {
        // distanceToTree = Vector3.Distance(transform.position, selector.selectedObject.transform.position);
    }

    public void GiveDamage()
    {
        treeManager.CheckHealth();
        source.PlayOneShot(choppingClip, 0.3f);
        treeManager.TakeDamage();
        treeManager.Shake();
        
    }
}
    