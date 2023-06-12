using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public float growSpeed = 1; // Bitki b�y�me h�z�
    public float maxSize = 2; // Bitki maksimum b�y�kl���
    public float deathTime = 10; // Bitki �lme s�resi
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
        // Bitki zamanla b�y�yor
       
        currentSize += Time.deltaTime * growSpeed;
        transform.localScale = new Vector3(transform.localScale.x, currentSize,transform.localScale.z);
        transform.position = new Vector3(transform.position.x, groundY + currentSize / 2, transform.position.z);

        // Bitki zamanla �l�yor
        timeAlive += Time.deltaTime;
        if (timeAlive >= deathTime)
        {
            Destroy(gameObject);
        }

        // Bitki maksimum b�y�kl��e ula�t���nda
        if (currentSize >= maxSize)
        {
            currentSize = maxSize;
        }

        // Bitki b�y�d�k�e rengi sar� olmas�n� sa�lar
        rend.material.color = Color.Lerp(startColor, Color.yellow, currentSize / maxSize);
    }
}
