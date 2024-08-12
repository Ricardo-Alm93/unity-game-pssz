using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistCanvas : MonoBehaviour
{
    void Awake()
    {
        // Marcar este GameObject (y sus hijos) para que no se destruya al cargar una nueva escena
        DontDestroyOnLoad(gameObject);
    }
}
