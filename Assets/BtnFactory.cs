using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FactoryNamespace;


// *** CONCRETE FACTORY CLASS ***
// used for buttons

namespace FactoryNS {
    public class BtnFactory : Factory
    {

        // NOTE: referenced https://www.patrykgalach.com/2019/03/28/implementing-factory-design-pattern-in-unity/
        // Reference to prefab.
        [SerializeField]
        private Button prefab; // prefab allows for reuse of objects
        
        public Button GetNewInstance() {
            return Instantiate(prefab);
        }
    }

}

