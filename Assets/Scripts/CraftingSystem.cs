using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance
    {
        get; set;
    }

    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;
    public List<string> inventoryItemList = new List<string>();
    public bool isOpen;
    Button toolsBtn; // ���ť
    Button craftAxeBtn; // ������ť
    Text axeReq1, axeReq2; // ��������Ҫ��

    public Blueprint axeBLP = new Blueprint("����", 2, "ʯͷ", 3, "ľ��", 3);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        isOpen = false;
        toolsBtn = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBtn.onClick.AddListener(delegate { OpenToolsCategory(); });

        // ����
        craftAxeBtn = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeBtn.onClick.AddListener(delegate { CraftAnyItem(axeBLP); });
        axeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("Req1").GetComponent<Text>();
        axeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("Req2").GetComponent<Text>();
    }

    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }

    void CraftAnyItem(Blueprint blueprintToCraft)
    {
        InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);

        if (blueprintToCraft.numOfRequirements == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.req1, blueprintToCraft.req1Amount);
        }
        else if (blueprintToCraft.numOfRequirements == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.req1, blueprintToCraft.req1Amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.req2, blueprintToCraft.req2Amount);
        }

        StartCoroutine(caculate());
    }

    public IEnumerator caculate()
    {
        yield return 0;
        InventorySystem.Instance.ReCalculateList();
        RefreshNeededItems();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            isOpen = false;
        }
    }

    public void RefreshNeededItems()
    {
        int stone_count = 0;
        int stick_count = 0;

        inventoryItemList = InventorySystem.Instance.itemList;

        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "ʯͷ":
                    stone_count += 1;
                    break;
                case "ľ��":
                    stick_count += 1;
                    break;
            }
        }

        // ----- ���� ----- //
        axeReq1.text = "3��ʯͷ[" + stone_count + "]";
        axeReq2.text = "3��ľ��[" + stick_count + "]";

        if (stone_count >= 3 && stick_count >= 3)
        {
            craftAxeBtn.gameObject.SetActive(true);
        }
        else
        {
            craftAxeBtn.gameObject.SetActive(false);
        }
    }
}
