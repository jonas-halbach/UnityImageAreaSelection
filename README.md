# UnityImageAreaSelection
<a name="WTF"></a>
## WTF - What's the function?
The player of your game shall be able to extract a selection of the currently rendered camera image? This assethe t will assist you in implementing this feature.

This asset:
- right now just works for orthografic cameras, because it was initially implemented to get an extracted selection of a two dimensional image like the background or the current camera picture.
- is implemented to work independent from the plattform (mobile devices as well as PCs are supported).
- is based on the Unity3d UI system.

## How to use?

The usage of this asset should be very easy:

At the location **Assets/Prefabs** you will find the prefab **Selector.prefab**. Just drop this prefab onto the **canvas** gameobject in your scene and everything should already work.

As already mentioned in the previous chapter (s. [WTF - What's the function?](#WTF)) this Asset is build to work with the default Unity3d UI system. To mark this system work you have to **place a canvas gameobject in your scene**. Furthermore the main camera should be configured to work with this system. For this you should specify the main camera as **Render Camera** and set the **RenderMode of the Canvas to Screen Space - Camera**.

The **Scene Assets/Scenes/ReferenceImplementationTestScene** shows the most simplistic use of this asset.

### Getting the picture selection

The MonoBehaviour **SelectionContainer** which is directly attached to the gameobject **SelectionContainer** which is a child game object of the **Selector** prefab has a method called **public Color[] GrabSelectionImage()** which returns the image selection as Color array.

### Changing the cursor images and other settings

To change the cursor images like the images for moving or scaling the selection you have to navigate to : **Selector->SelectionContainer->Selection**. On this gameobject you will find the attached MonoBehaviour **InputPropertyContainer** which will allow you to tweak the UI a little.

Possible settings are:
- Changing the cursor images
- the size bounds of the selection rect
- the distance threshold between the mousecursor and a corner of the selection rect which is used to determine that the user will scale instead of move the selection rect.

Also the texture of the selection rect itself can be changed by changing the material of the **Seletion** gameobject, which is attached to the **Selector** prfab. 






