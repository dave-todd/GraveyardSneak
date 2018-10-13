using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes { Menu, Graveyard, Dungeon };

[CreateAssetMenu()]

public class TeleportData : ScriptableObject
{

    public Vector3 Graveyard0;
    public Vector3 Graveyard1;
    public Vector3 Dungeon1;
    public Vector3 Graveyard2;
    public Vector3 Dungeon2;
    public Vector3 Graveyard3;
    public Vector3 Dungeon3;
    public Vector3 Graveyard4;
    public Vector3 Dungeon4;
    public Vector3 Graveyard5;
    public Vector3 Dungeon5;
    public Vector3 Graveyard6;
    public Vector3 Dungeon6;

    public Vector3 GetLocation(Scenes scene, int teleportIndex)
    {
        if (scene == Scenes.Graveyard)
        {
            switch (teleportIndex)
            {
                case 0: return Graveyard0;
                case 1: return Graveyard1;
                case 2: return Graveyard2;
                case 3: return Graveyard3;
                case 4: return Graveyard4;
                case 5: return Graveyard5;
                case 6: return Graveyard6;
                default: return new Vector3(0, 0, 0);
            }
        }
        else if (scene == Scenes.Dungeon)
        {
            switch (teleportIndex)
            {
                case 1: return Dungeon1;
                case 2: return Dungeon2;
                case 3: return Dungeon3;
                case 4: return Dungeon4;
                case 5: return Dungeon5;
                case 6: return Dungeon6;
                default: return new Vector3(0, 0, 0);
            }
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }

    public Scenes GetScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { return Scenes.Graveyard; }
        if (SceneManager.GetActiveScene().buildIndex == 2) { return Scenes.Dungeon; }
        else { return Scenes.Menu; }
    }

    public Scenes GetOtherScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { return Scenes.Dungeon; }
        else { return Scenes.Graveyard; }
    }

}
