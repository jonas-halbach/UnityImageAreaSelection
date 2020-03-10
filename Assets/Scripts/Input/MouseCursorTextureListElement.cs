using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.halbach.imageselection.input {
    
    [Serializable]
    public class MouseCursorTextureListElement
    {
        [SerializeField]
        private TargetMousePosition key;

        [SerializeField]
        private Texture2D value;

        public TargetMousePosition Key {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        public Texture2D Value {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        public MouseCursorTextureListElement(TargetMousePosition key, Texture2D value)
        {
            Key = key;
            Value = value;
        }

    }
}
