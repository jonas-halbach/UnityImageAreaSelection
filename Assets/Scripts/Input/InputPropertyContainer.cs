using UnityEngine;
using System.Collections.Generic;

namespace com.halbach.imageselection.input {


    public class InputPropertyContainer : MonoBehaviour {

        [SerializeField]
        private List<MouseCursorTextureListElement> initializableTextures;

        [SerializeField]
        private Dictionary<TargetMousePosition, Texture2D> textures;
        
        [SerializeField]
        private int defaultCursorSize = 25;

        [SerializeField]
        private float triggerDistance = 0.5f;

        [SerializeField]
        private float minimumWidth = 0.5f;

        [SerializeField]
        private float minimumHeight = 0.5f;

        [SerializeField]
        private float maximumWidth = 3;

        [SerializeField]
        private float maximumHeight = 3;

        private Texture2D emptyTexture;

        public float TriggerDistance {
            get 
            {
                return triggerDistance;
            }
            set
            {
                triggerDistance = value;
            }
        }

        public float MinimumHeight {
            get { return minimumHeight; }
            set { minimumHeight = value; }
        }

        public float MinimumWidth {
            get { return minimumWidth; }
            set { minimumWidth = value; }
        }


        public float MaximumHeight {
            get { return maximumHeight; }
            set {maximumHeight = value;}

        }

        public float MaximumWidth {
            get { return maximumWidth; }
            set {maximumWidth = value;}

        }

        void Start()
        {
            emptyTexture = new Texture2D(defaultCursorSize, defaultCursorSize);
            FillMouseCursorTextureArray();
        }

        private void FillMouseCursorTextureArray()
        {
            textures = new Dictionary<TargetMousePosition, Texture2D>();

            foreach(MouseCursorTextureListElement elem in initializableTextures)
            {
                if(!textures.ContainsKey(elem.Key))
                {
                    textures.Add(elem.Key, elem.Value);   
                }
            }
        }

        public Texture2D GetTexture(TargetMousePosition mousePosition)
        {
            Texture2D texture = emptyTexture;
            if(textures != null) {
                if(textures.ContainsKey(mousePosition))
                {
                    texture = textures[mousePosition];
                }
            }

            return texture;
        }

    }
}