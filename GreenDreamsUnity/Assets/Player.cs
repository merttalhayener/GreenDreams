using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public GameObject buildingCanvas;

    public InventoryObject inventory;
    public InventoryObject equipment;

    public Attribute[] attributes;

    private Transform boots;
    private Transform chest;
    private Transform helmet;
    private Transform offhand;
    private Transform tool;

    public Transform toolTransform;
    public Transform offhandWristTransform;
    public Transform offhandHandTransform;


    private BoneCombiner boneCombiner;

    public SceneControl reset;

    private void Start()
    {
        boneCombiner = new BoneCombiner(gameObject);

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }

        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnRemoveItem;
            equipment.GetSlots[i].OnAfterUpdate += OnAddItem;
        }
    }


    public void OnRemoveItem(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type,
                    ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                    }
                }

                if (_slot.ItemObject.characterDisplay != null)
                {
                    switch (_slot.AllowedItems[0])
                    {
                        case ItemType.Helmet:
                            Destroy(helmet.gameObject);
                            break;
                        case ItemType.Tool:
                            Destroy(tool.gameObject);
                            break;
                        case ItemType.Consumables:
                            Destroy(offhand.gameObject);
                            break;
                        case ItemType.Boots:
                            Destroy(boots.gameObject);
                            break;
                        case ItemType.Chest:
                            Destroy(chest.gameObject);
                            break;
                    }
                }

                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }

    public void OnAddItem(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(
                    $"Placed {_slot.ItemObject}  on {_slot.parent.inventory.type}, Allowed Items: {string.Join(", ", _slot.AllowedItems)}");

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                    }
                }

                if (_slot.ItemObject.characterDisplay != null)
                {
                    switch (_slot.AllowedItems[0])
                    {
                        case ItemType.Helmet:
                            helmet = boneCombiner.AddLimb(_slot.ItemObject.characterDisplay,
                                _slot.ItemObject.boneNames);
                            break;
                        case ItemType.Tool:
                            if (_slot.item.Id == 16)
                            {
                                //Balta kontrolü.
                                tool = Instantiate(_slot.ItemObject.characterDisplay, offhandHandTransform).transform;
                                break;
                            }
                            else
                            {
                                tool = Instantiate(_slot.ItemObject.characterDisplay, toolTransform).transform;
                                break;
                            }
                        case ItemType.Consumables:
                            offhand = Instantiate(_slot.ItemObject.characterDisplay, offhandWristTransform)
                                .transform;
                            break; 
                        case ItemType.Boots:
                            boots = boneCombiner.AddLimb(_slot.ItemObject.characterDisplay, _slot.ItemObject.boneNames);
                            break;
                        case ItemType.Chest:
                            chest = boneCombiner.AddLimb(_slot.ItemObject.characterDisplay, _slot.ItemObject.boneNames);
                            break;
                    }
                }


                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        var groundItem = other.GetComponent<GroundItem>();
        if (groundItem)
        {
            Item _item = new Item(groundItem.item);
            if (inventory.AddItem(_item, 1))
            {
                Destroy(other.gameObject);
            }
        }
    }

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
    }


    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }
}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized] public Player parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(Player _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }

    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}