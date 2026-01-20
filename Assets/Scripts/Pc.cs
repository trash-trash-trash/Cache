using System.Collections;
using TMPro;
using UnityEngine;
public class Pc : Interactable
{
    public ItemSO disk;

    public TextMeshPro text;
    public TextMeshPro dataText;

    public GameObject loadingParent;

    private float index;

    private Inventory inv;

    public string data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = "INSERT DISK TO WRITE DATA";
        dataText.text = data;
         inv= FindFirstObjectByType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        
        print("WRITING DATA");
        text.text = "WRITING DATA...";
        //remove disk
        inv.RemoveItem(disk);
        //write data animation
        StartCoroutine(WriteData());
        //return new disk with data
        
        PlayerInteract player = iInteractTransform.GetComponent<PlayerInteract>();
        CloseInteractable(player);
    }
    
    public override string InteractString()
    {
        //bool playerHasKey = inv.playerItems.Contains(disk);
        bool playerHasKey = inv.FindSpecificItemInInventory(disk);
        if (!playerHasKey) return "NO DISK FOUND";

        bool keyEquipped = (inv.selectedItem.item == disk) ? true : false;
        if (!keyEquipped) return "EQUIP DISK";

        return "E: WRITE DATA";
    }

    IEnumerator WriteData()
    {
        while (index <= 1)
        {
            loadingParent.transform.localScale = new Vector3(loadingParent.transform.localScale.x,
                loadingParent.transform.localScale.y, index);
            index += 0.1f;
            print("index is at " +index);
            yield return new WaitForSeconds(1);
        }
        
        WriteDone();
    }

    void WriteDone()
    {
        text.text = "DATA WRITE COMPLETE";
        
        InventoryItem diskItem = inv.CreateInventoryItem(disk, data);
        print("Created disk with data: " + diskItem.data);
        inv.AddItem(diskItem);
        data = "NULL";
    }
}
