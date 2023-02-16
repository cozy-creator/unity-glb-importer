# Unity GLTFAnimation Importer

Welcome to the Unity GLTFAnimation Importer! This is an extension to the [GLTFUtility](https://github.com/Siccity/GLTFUtility) Unity package, which has paved the way for importing GLTF files into Unity. This extension builds on the foundation of GLTFUtility and incorporates animations, as well as utilizing Animation Overrides for a dynamic runtime animation system.

## Getting Started

Before you can use the Unity GLTFAnimation Importer, you must have the GLTFUtility Unity package installed in your project. If you haven't already, head over to the [GLTFUtility repository](https://github.com/Siccity/GLTFUtility) and follow the installation instructions.

Once GLTFUtility is installed, you can add the Unity GLTFAnimation Importer by copying the contents of this repository into your project's Assets folder or even easier, import the [Unity Package](https://github.com/capsule-craft/unity-glb-importer/raw/main/GLBAnimationImporter.unitypackage).

## Importing GLTF Animations

To import a GLTF file with animations, simply drag and drop the **Importer** prefab into your scene. ![Unity GLTFAnimation Importer](https://i.imgur.com/JK49Au1.png)

### Importer Prefab
*Load On Start* - Do you want the model to be loaded on start?

*Wrapper* - This is where the imported model will be parented to.

*GLB To Import* - This is the link where the GLB is hosted. 

*Desired Scale* - This is the scale that you want the imported GLB to be in Unity. GLBs come in all sorts of sizes, but the *ScaleToFit.cs* script will scale down/up the GLB to fit inside the *desiredScale*.

*Loading Visual* - This is a game object that is displayed while you are fetching your GLB. It can be anything you like, but I've included a simple sphere animation.

*Possible Animations List* - This aspect is currently still being fleshed out. It takes a scriptable object that has a list of animations' strings that will be added to a runtime animator utilizing AnimationOverrides. Coming soon! :)

## Hosting your own GLB file on GoogleDrive
Hosting your file on GoogleDrive is a great fast way to get it going. However, you can't use the typical google drive share link. This is because it links you to their download interface. You can actually bypass that so long as you follow this format "https://drive.google.com/uc?export=download&id=downloadIdfromDrive".
Here is a typical GDrive link: https://drive.google.com/file/d/1x5N20EiHcErylC224A7CYkXjS__hVt96/view?usp=sharing
We're only interested in the content after d/ and before /view: *1x5N20EiHcErylC224A7CYkXjS__hVt96* becomes the "downloadIDfromDrive".

## Conclusion

We hope that the Unity GLTFAnimation Importer makes it easier for you to import and use GLTF files with animations in your Unity projects.If you have any questions or feedback, please feel free to open an issue in this repository.
