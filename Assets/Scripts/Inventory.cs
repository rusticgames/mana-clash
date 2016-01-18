using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<GameObject> equippedItems;
    public List<Pickuppable> heldItems;

    public string ListItems()
    {
        string itemList = "\nWorn:";
        foreach (var item in equippedItems)
        {
            itemList += "\n\t" + item.name;
        }
        itemList += "\n\nHeld:";
        foreach (var item in heldItems)
        {
            itemList += "\n\t" + item.name;
        }
        return itemList;
    }
		
    public void useHeldItems()
    {
        foreach (var item in heldItems)
        {
            item.onUse.Invoke(gameObject.GetComponent<Platformer2DUserControl>().lastFacingDirection);
        }
    }

    public void startHolding(Pickuppable item)
    {
        heldItems.Add(item);
    }

    public void stopHolding(Pickuppable item)
    {
        heldItems.Remove(item);
    }
}
