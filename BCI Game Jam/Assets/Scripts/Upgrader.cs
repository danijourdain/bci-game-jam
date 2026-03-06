using UnityEngine;

public class Upgrader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject playerObject; // reference to the player GameObject
    public GameObject selector1; // reference to the first upgrade selector
    public GameObject selector2; // reference to the second upgrade selector
    public GameObject selector3; // reference to the third upgrade selector
    public GameObject selector4; // reference to the hidden selector
    private Vector3[] selectorPreviousPos;
    private Quaternion[] selectorPreviousRot;
    void Start()
    {
        ShowUpgradeOptions();
    }

    // Update is called once per frame
    void Update()
    {
        HideUpgradeOptions();
    }

    public void ShowUpgradeOptions()
    {        
        selectorPreviousPos = new Vector3[4] { selector1.transform.localPosition, selector2.transform.localPosition, selector3.transform.localPosition, selector4.transform.localPosition };
        selectorPreviousRot = new Quaternion[4] { selector1.transform.rotation, selector2.transform.rotation, selector3.transform.rotation, selector4.transform.rotation };
        selector1.transform.localPosition = new Vector3(-9,-3,16);
        selector2.transform.localPosition = new Vector3(0,-3,16);
        selector3.transform.localPosition = new Vector3(9,-3,16);
        selector1.transform.rotation = new Quaternion(0,0,0,1);
        selector2.transform.rotation = new Quaternion(0,0,0,1);
        selector3.transform.rotation = new Quaternion(0,0,0,1);
        selector4.transform.localPosition = new Vector3(0,0,-100);
    }

    public void HideUpgradeOptions()
    {
        Debug.Log("Hiding upgrade options");    
        selector1.transform.localPosition = selectorPreviousPos[0];
        selector2.transform.localPosition = selectorPreviousPos[1];
        selector3.transform.localPosition = selectorPreviousPos[2];
        selector4.transform.localPosition = selectorPreviousPos[3];
        selector1.transform.rotation = selectorPreviousRot[0];
        selector2.transform.rotation = selectorPreviousRot[1];
        selector3.transform.rotation = selectorPreviousRot[2];
        selector4.transform.rotation = selectorPreviousRot[3];
    }
}
