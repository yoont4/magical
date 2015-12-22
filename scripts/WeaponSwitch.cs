using UnityEngine;
using System.Collections;
//using WeaponOptions;

public class WeaponSwitch : MonoBehaviour {

    float lastScrollTime;
    float scrollChange;
    public float scrollInterval;
    public bool isMouseScrollInverted;
    public KeyCode prevWeaponKey;
    public KeyCode nextWeaponKey;
    int mouseInvert;

	// Use this for initialization
	void Start () {
        lastScrollTime = Time.realtimeSinceStartup;
        mouseInvert = isMouseScrollInverted ? -1 : 1;
	}

    // Update is called once per frame
    void Update()
    {
        //scrollChange = Input.mouseScrollDelta.magnitude;  // OR: 
        scrollChange = Input.GetAxis("Mouse ScrollWheel");
        float currentTime = Time.realtimeSinceStartup;
        if ((scrollChange * mouseInvert < 0f && (currentTime - lastScrollTime) > scrollInterval)
            || Input.GetKeyDown(nextWeaponKey))
        {
            WeaponOptions.changeWeapon(1);
            //show menu for one second

            lastScrollTime = currentTime;
            Debug.Log("Switch to next waepon: " + WeaponOptions.getSelectedWeaponName());
        }
        else if ((scrollChange * mouseInvert > 0f && currentTime - lastScrollTime > scrollInterval)
            || Input.GetKeyDown(prevWeaponKey))
        {
            WeaponOptions.changeWeapon(-1);
            //show menu for one second

            lastScrollTime = currentTime;
            Debug.Log("Switch to previous weapon: " + WeaponOptions.getSelectedWeaponName());
        }
    }
}
