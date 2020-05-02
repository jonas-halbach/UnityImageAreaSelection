using UnityEngine;
using System.Collections.Generic;

namespace com.halbach.imageselection.input {


    public class MousePropertyContainer : MonoBehaviour {

        [SerializeField]
        private List<MouseCursorTextureListElement> initializableTextures;

        [SerializeField]
        private Dictionary<TargetMousePosition, Texture2D> textures;
        
        [SerializeField]
        private int defaultCursorSize = 25;

        [SerializeField]
        private int triggerDistance = 25;

        private Texture2D emptyTexture;

        public int TriggerDistance {
            get 
            {
                return triggerDistance;
            }
            set
            {
                triggerDistance = value;
            }
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