using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    [SerializeField] private TreeChopping treeChopping;
   
    public float treeHealth;
   
    [SerializeField] private GameObject log;
    [SerializeField] private GameObject root;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Transform rootSpawnPoint;
    
    private ObjectTypeManager typeManager;
    [SerializeField] public Types objectType;
    public Tree tree;

    

    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay = 0.002f;
    public float shake_intensity = 0.02f;

    private float temp_shake_intensity = 0;


    void Start()
    {
        treeChopping = GameObject.Find("Player").GetComponent<TreeChopping>();
        typeManager = GetComponent<ObjectTypeManager>();
        objectType = typeManager.objectType.type;
        treeHealth = tree.health;
    }
    private void Update()
    {
        CheckHealth();
       
        //SHAKE
        if (temp_shake_intensity > 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
            transform.rotation = new Quaternion(
                originRotation.x + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.y + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.z + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.w + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f);
            temp_shake_intensity -= shake_decay;
        }

    }

    public void TakeDamage()
    {
        if(treeHealth > 0)
        {
            treeHealth -= treeChopping.damage ;
        }
    }

    public void CheckHealth()
    {
        if(treeHealth <= 0)
        {
            for(int i = 0; i < 3; i++)
            {
            Instantiate(log, spawnPoint.transform.position, transform.rotation);
            }
            Instantiate(root, rootSpawnPoint.transform.position, transform.rotation);
            Destroy(gameObject);
        }
      
    }

    public void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        temp_shake_intensity = shake_intensity;
    }
}


