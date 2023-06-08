using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Animator playerAnimator;

    [SerializeField] AudioClip placedSound;
    [SerializeField] AudioSource source;

    public CheckPlacement checkPlacement;
    [SerializeField] Terrain gridTerrain;
    public GameObject[] objects;
    public GameObject pendingObject;
    [SerializeField] private int lastIndex;


    public ParticleSystem childrenParticleSystem;
    public CursorLock cursorLock;


    private Vector3 pos;
    private RaycastHit hit;

    [SerializeField] private LayerMask placableLayers;

    public float rotateAmount;

    public float gridSize;
    bool gridOn = true;
    [SerializeField] private Toggle gridToggle;

    public bool canPlace;
    public bool buildingOpen;
    bool rotating;


    public Material[] materials;


    public float maxSlopeAngle = 30f; // inþaat yapýlacak maksimum eðim açýsý


    public UnityEvent<string> OnObjectPlaced; // BuildingQuest'i tetiklemek için event
    public QuestManager questManager;


    private void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000, placableLayers))
        {
            pos = hit.point;
        }
        MouseLock();
    }

    void Update()
    {
        if (pendingObject != null)
        {
            gridTerrain.enabled = true;
            buildingOpen = true;

            if (gridOn)
            {
                Vector3 targetPos = new Vector3(RoundToNearestGrid(pos.x), (pos.y), RoundToNearestGrid(pos.z));
                pendingObject.transform.position = Vector3.Lerp(pendingObject.transform.position, targetPos, Time.deltaTime * 40f);
            }

            if (Input.GetMouseButtonDown(0) && canPlace && !IsMouseOverUI())
            {
                PlaceObject();
                buildingOpen = false;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(pendingObject);
            }

            ChangeMaterials();
        }
        else
        {
            buildingOpen = false;
            gridTerrain.enabled = false;
            canPlace = true;
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void CreateParticleEffect()
    {
        if (pendingObject != null)
        {
            childrenParticleSystem = pendingObject.GetComponentInChildren<ParticleSystem>();
            if (childrenParticleSystem != null)
            {
                childrenParticleSystem.Play();
            }
        }
        else
        {
            return;
        }
    }

    public void PlaceObject()
    {
        checkPlacement = pendingObject.GetComponent<CheckPlacement>();
        checkPlacement.isPlaced = true;

        // Materyal deðiþiklikleri
        materials = pendingObject.GetComponent<MeshRenderer>().materials;

        foreach (var material in materials)
        {
            material.color = Color.white;
        }

        CreateParticleEffect();

        // player.transform.LookAt(pendingObject.transform.position , Vector3.up);
        if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("BuildWithHammer"))
        {
            playerAnimator.SetTrigger("Build");
        }

        pendingObject = null;
        source.PlayOneShot(placedSound, 0.3f);
        buildingOpen = false;

        // Duvar inþa edildiðinde eventi tetikle ve görev adýný parametre olarak gönder
        if (OnObjectPlaced != null)
        {
            OnObjectPlaced.Invoke("BuildWall");
        }

        SelectObject(lastIndex);

        // Yeni eklenen kod satýrý - QuestManager'ý bul ve HandleObjectPlaced metodunu çaðýr
        QuestManager questManager = FindObjectOfType<QuestManager>();
        if (questManager != null)
        {
            questManager.HandleBuildQuest();
        }
    }
    IEnumerator RotateSmooth(Vector3 byAngles, float inTime)
    {
        var fromAngle = pendingObject.transform.rotation;
        var toAngle = Quaternion.Euler(pendingObject.transform.eulerAngles + byAngles);
        for (var t = 0f; t <= 1; t += Time.deltaTime / inTime)
        {
            pendingObject.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        pendingObject.transform.rotation = toAngle;
        rotating = false;
    }

    public void RotateObject()
    {
        if (!rotating)
        {
            rotating = true;
            StartCoroutine(RotateSmooth(Vector3.up * 45f, 0.1f));
        }
    }

    public void UpdateMaterials()
    {
        if (canPlace && pendingObject != null)
        {
            pendingObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        if (!canPlace && pendingObject != null)
        {
            pendingObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }

    }

    public void ChangeMaterials()
    {
        if (pendingObject != null)
        {
            materials = pendingObject.GetComponent<MeshRenderer>().materials;
        }

        if (canPlace && pendingObject != null)
        {
            foreach (var material in materials)
            {
                material.color = Color.green;
            }
        }

        if (!canPlace && pendingObject != null)
        {
            foreach (var material in materials)
            {
                material.color = Color.red;
            }
        }
    }

    public void SelectObject(int index)
    {
        lastIndex = index;

        Destroy(pendingObject);
        pendingObject = Instantiate(objects[index], pos, objects[index].transform.rotation);
        buildingOpen = true;
    }

    public void MouseLock()
    {
        if (buildingOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!buildingOpen && cursorLock.buildingPanelIsClosed == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ToggleGrid()
    {
        gridOn = gridToggle.isOn;
    }

    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;

        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }

        if (xDiff * (-1f) > (gridSize / 2))
        {
            pos -= gridSize;
        }
        return pos;
    }
}
