using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [Serializable]
    public class WallFace
    {
        public WallFace(WallFace face)
        {
            faces = face.faces;
            Health = face.Health;
        }
        public float Health = 3f;
        public Sprite[] faces = new Sprite[4];
    }
    public GameObject WallPrefab;
    public WallFace[] WallFaces;
    public int CurrentUpgrade = 1;
}
