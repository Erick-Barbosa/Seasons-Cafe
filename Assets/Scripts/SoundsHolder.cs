using System.Collections.Generic;
using UnityEngine;

public class SoundsHolder : MonoBehaviour {
    static public AudioSource[] clipList;

    void Awake() {
        clipList = GetComponents<AudioSource>();
    }
}
