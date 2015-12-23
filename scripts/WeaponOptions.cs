using UnityEngine;
using System.Collections;

public class WeaponOptions {
    private static string[] menu = { "weapon1", "weapon2", "weapon3" };
    private static int selected = 0;

    public static string getSelectedWeaponName()
    {
        return menu[selected];
    }

    public static int getSelectedWeaponIndex()
    {
        return selected;
    }

    public static void changeWeapon(int scrollDirection)
    {
        selected = (selected + scrollDirection) % menu.Length;
        selected = (selected + menu.Length) % menu.Length; // -5 mod 2 equals -1 in C#
    }
}
