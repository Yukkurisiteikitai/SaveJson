using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class TrapController : MonoBehaviour
{
    public int TrapNumber;
    [SerializeField] private TrapItem TrapDateBase;
    //save
    public SaveMapping saveMapping = new SaveMapping();
    public GameObject sM_trap;
    
    void Start()
    {
        // save Setting
        saveMapping.saveX = transform.position.x;
        saveMapping.saveY = transform.position.y;

        int changeX = (int)saveMapping.saveX;
        int changeY = (int)saveMapping.saveY;

        // change ren number
        if(changeX < 0)
        {
            changeX = 11 + changeX;
            
        }else if(changeX >= 0)
        {
            changeX += 11;
        }
        if(changeY < 0)
        {
            changeY = (changeY * -1) + 4;
        }else if (changeY >= 0)
        {
            changeY = 4 - changeY;
        }

        saveMapping.saveCode = 3;
        sM_trap = GameObject.Find("SaveManager");
        saveMapping.sM = sM_trap.GetComponent<SaveManager>();
        saveMapping.sM.InputTo(changeY, changeX,saveMapping.saveCode);        
        //save End

    }
    
}
