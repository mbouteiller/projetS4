using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement_drapeau : MonoBehaviour {

    void Update()
    {
        transform.Rotate(new Vector3(0, 100, 15) * Time.deltaTime);
    }
}
