using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inven : MonoBehaviour
{
    public List<Slotdata> inven = new List<Slotdata>();
    public List<Slotdata> eqip = new List<Slotdata>();
    private int invenMaxSlot = 10;
    private int eqipMaxSlot = 6;
    public GameObject UI_Prefab;
    public GameObject invenPrefab;
    public GameObject eqipPrefab;

    private void Start()
    {
        GameObject UI = GameObject.Find("UI_Player");
        UI = Instantiate(UI_Prefab, this.transform);
        GameObject InvenPanel = GameObject.Find("inven");
        for (int i = 0; i < invenMaxSlot; i++)
        {
            GameObject go = Instantiate(invenPrefab, InvenPanel.transform, false);
            go.name = "Slot_" + i;
            Slotdata slot = new Slotdata();
            slot.isEmpty = true;
            slot.slotObj = go;
            inven.Add(slot);
        }

        GameObject EqipPanel = GameObject.Find("Eqip");
        for (int i = 0; i < eqipMaxSlot; i++)
        {
            GameObject go = Instantiate(invenPrefab, EqipPanel.transform, false);
            go.name = "Slot_" + i;
            Slotdata slot = new Slotdata();
            slot.isEmpty = true;
            slot.slotObj = go;
            eqip.Add(slot);
        }
    }
}
