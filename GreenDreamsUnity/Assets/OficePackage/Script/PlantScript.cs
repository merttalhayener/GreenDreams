using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public float growSpeed = 1; // Bitki büyüme hýzý
    public float maxSize = 2; // Bitki maksimum büyüklüðü
    public float deathTime = 10; // Bitki ölme süresi
    private float currentSize;
    private float timeAlive;
    private Renderer rend;
    private Color startColor;
    private float groundY;

    void Start()
    {
        currentSize = 0;
        timeAlive = 0;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        groundY = transform.position.y;
    }

    void Update()
    {
        Debug.Log(groundY);
        // Bitki zamanla büyüyor
       
        currentSize += Time.deltaTime * growSpeed;
        transform.localScale = new Vector3(transform.localScale.x, currentSize,transform.localScale.z);
        transform.position = new Vector3(transform.position.x, groundY + currentSize / 2, transform.position.z);

        // Bitki zamanla ölüyor
        timeAlive += Time.deltaTime;
        if (timeAlive >= deathTime)
        {
            Destroy(gameObject);
        }

        // Bitki maksimum büyüklüðe ulaþtýðýnda
        if (currentSize >= maxSize)
        {
            currentSize = maxSize;
        }

        // Bitki büyüdükçe rengi sarý olmasýný saðlar
        rend.material.color = Color.Lerp(startColor, Color.yellow, currentSize / maxSize);
    }
}
