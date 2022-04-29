using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// **** ABSTRACT FACTORY CLASS *****
// * used to make other factories

namespace FactoryNamespace {
    public abstract class Factory : MonoBehaviour
    {

        // NOTE: referenced https://www.patrykgalach.com/2019/03/28/implementing-factory-design-pattern-in-unity/
        // Reference to prefab.
        private GameObject prefab; // prefab allows for reuse of objects
        
        public GameObject GetNewInstance() {
            return Instantiate(prefab);
        }
    }
}

