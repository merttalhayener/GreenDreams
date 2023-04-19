using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    
    [SerializeField] private GameObject player;
    public InventoryObject inventory;
    Transform playerTransform;
    GroundItem ground;
    public AudioClip pickClip;
    public AudioSource source;
    

    
    private void Start()
    {
        player = GameObject.Find("Player");
        
        inventory = player.GetComponent<Player>().inventory;
        playerTransform = player.GetComponent<Transform>();
    }
    public float AttractorSpeed;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            
            Vector3 targetPos = new Vector3(playerTransform.position.x, player.transform.position.y + 0.75f, player.transform.position.z);
            
            other.transform.position = Vector3.MoveTowards(other.transform.position, targetPos, AttractorSpeed * Time.deltaTime);

            ground = other.gameObject.GetComponent<GroundItem>();
            var item =other.gameObject.GetComponent<GroundItem>();
            if (item && ground.collected == false)
            {
                inventory.AddItem(new Item(item.item), 1);
                ground.collected = true;
                source.PlayOneShot(pickClip,0.3f);
                Destroy(other.gameObject, 0.2f);
            }
        }
       
    }
}
