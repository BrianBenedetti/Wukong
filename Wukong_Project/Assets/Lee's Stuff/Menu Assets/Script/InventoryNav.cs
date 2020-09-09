using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryNav : MonoBehaviour
{
    public MenuInput InventoryIn;
    public Transform[] slotPos;
    public GameObject[] buttons;
    public GameObject temp;
    private void Awake()
    {
        InventoryIn = new MenuInput();
  
    }
    // Start is called before the first frame update
    void Start()
    {
        buttons[0].transform.position = slotPos[0].transform.position;
        buttons[1].transform.position = slotPos[1].transform.position;
        buttons[2].transform.position = slotPos[2].transform.position;
    }

    // Update is called once per frame
    void Update()
    {



        if (InventoryIn.InventoryInput.CycleRight.triggered)
        {
            var pos0 = buttons[0].transform.position;
            var pos1 = buttons[1].transform.position;
            var pos2 = buttons[2].transform.position;
            buttons[0].transform.position = Vector3.MoveTowards(slotPos[1].transform.position, pos1, Time.deltaTime);
            buttons[1].transform.position = Vector3.MoveTowards(slotPos[2].transform.position, pos2, Time.deltaTime);
            buttons[2].transform.position = Vector3.MoveTowards(slotPos[0].transform.position, pos0, Time.deltaTime);
            temp = buttons[0];
            buttons[0] = buttons[1];
            buttons[1] = buttons[2];
            buttons[2] = temp;
            Debug.Log("MoveRight");

        }
        else if (InventoryIn.InventoryInput.CycleLeft.triggered)
        {
            var pos0 = buttons[0].transform.position;
            var pos1 = buttons[1].transform.position;
            var pos2 = buttons[2].transform.position;
            buttons[0].transform.position = Vector3.MoveTowards(slotPos[2].transform.position, pos1, Time.deltaTime);
            buttons[1].transform.position = Vector3.MoveTowards(slotPos[0].transform.position, pos2, Time.deltaTime);
            buttons[2].transform.position = Vector3.MoveTowards(slotPos[1].transform.position, pos0, Time.deltaTime);
            temp = buttons[0];
            buttons[0] = buttons[2];
            buttons[2] = buttons[1];
            buttons[1] = temp;
          

            Debug.Log("MoveLeft");
        } 

    }
    private void OnEnable()
    {
        InventoryIn.InventoryInput.Enable();
    }
    private void OnDisable()
    {
        InventoryIn.InventoryInput.Disable();
    }
    //when something is picked up
    //for(int i = 0; 1< buttons.lenght; 1++)
}
