using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeChopping : MonoBehaviour
{
    [SerializeField] private OutlineSelecter selector;
    [SerializeField] private Animator animator;
    [SerializeField] private TreeManager treeManager;
    [SerializeField] private ThirdPersonController thirdPersonController;
    public float damage;
    private PlayerStatsManager playerStatsManager;

    public ObjectTypeManager typeManager;

    public Vector3 distanceToTree;

    public AudioClip choppingClip;
    public AudioSource source;

    public bool axeEquiped;

    private void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }

    void Update()
    {
        AxeControl();
        if (Input.GetKeyDown(KeyCode.E) && selector.selectedObject != null && playerStatsManager.HasStamina)
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
            AxeControl();
            Debug.Log(axeEquiped);

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("TreeChopHorizontal"))
            {
                
                
                if (axeEquiped)
                {
                    transform.LookAt(selector.selectedObject.transform.position);
                    animator.SetTrigger("ChopTree");
                }
                
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

    public void AxeControl()
    {

        Player _player = (Player)Component.FindObjectsOfType(typeof(Player), false).FirstOrDefault();
        InventorySlot[] _slots = _player.equipment.GetSlots;
        axeEquiped = false;

        foreach (var slot in _slots)
        {
            if (slot.item.Id == 16)
            {
                axeEquiped = true;
                break;
            }
        }
    }
}
