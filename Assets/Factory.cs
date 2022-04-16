using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{

    // NOTE: referenced https://www.patrykgalach.com/2019/03/28/implementing-factory-design-pattern-in-unity/
    // Reference to prefab.
    [SerializeField]
    private MonoBehaviour prefab; // prefab allows for reuse of objects
    
    public MonoBehaviour GetNewInstance() {
        return Instantiate(prefab);
    }
}
