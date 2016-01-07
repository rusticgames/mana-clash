using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public string title;
    public List<GameObject> items;
    
    void Start()
    {
	title = name;
    }

    public string ListItems()
    {
	string itemList = "";
	foreach (var item in items) {
	    itemList += "\n" + item.name;
	}
	return itemList;
    }
}
