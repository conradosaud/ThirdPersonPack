# EasyStart Third Person Controller 🎮

The __EasyStart Third Person Controller__ is the perfect solution for developers who want to create third-person games in Unity quickly and effortlessly.
This package offers a simplified approach to implementing character and camera controls in third-person games in Unity. The codes are structured in a simple and intuitive way, allowing you to continue developing your game with ease, without the need to delve deeply into the provided codes, as they are basic and easy to modularize. Whatever your level of experience, the EasyStart Third Person Controller is an accessible and user-friendly option to streamline your project, so you don't waste time creating your character's controls.

## Included Features 

### 🛠️️ Character Prefab with Basic Movement 
- Movement with arrow keys or WASD (left analog stick for joysticks)
- Jumping with the space bar (button Y)
- Crouching with the Ctrl key (button B)
- Running with the Shift key (button X)

### 🎥 Third Person Camera Control
- Free mouse movement (right stick for joysticks)
- Option for camera movement only when the right mouse button is pressed (not available for joysticks)

### 📝 Only Two Scripts: Basic and Simple
This package creates all third-person controls using only two scripts: one for the camera ([CameraController.cs](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Prefabs/Source/Scripts/ThirdPersonController.cs)) and the other for the player ([ThirdPersonController.cs](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Prefabs/Source/Scripts/CameraController.cs)). Both scripts are straightforward and use an easy-to-understand language. You can use them to continue your game's script as you wish or simply learn from them how to create your own movement.

### 🔎 Commented Scripts
Additionally, there are alternative scripts for the camera and movements. In the [Commented](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Prefabs/Source/Scripts/Commented) folder within Scripts, you'll find [CommentedCameraController.cs](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Prefabs/Source/Scripts/Commented/CommentedThirdPersonController.cs) and [CommentedThirdPersonController.cs](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Prefabs/Source/Scripts/Commented/CommentedCameraController.cs). These scripts have slightly extensive comments, explaining in detail how each line of code works. Great for beginners! I wrote them in a didactic way to teach exactly how each command works. It's worth reading. 

## How to Use
To implement the features of this package into your game, you have two options:
### 🦸 Character
1. You can add the [ThirdPersonController.prefab](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Prefabs/ThirdPersonController.prefab) to your game, which you'll find in the Prefabs directory. After that, just swap the Y Bot model (taken from Mixamo) for your character's model. Everything is ready!
2. Another option is to simply add the [ThirdPersonController.cs](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Prefabs/Source/Scripts/ThirdPersonController.cs) script to an object in your game that will be the character. To do this, make sure the object has a __CharacterController__ component and the __Player tag__, as well as an __Animator__ component.

### 📹 Camera
For the camera, it's even easier! Just add the [CameraController.prefab](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Prefabs/CameraController.prefab) to your project, located in the [Prefabs](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Prefabs) folder. That's it! Just remember to delete the original __MainCamera__ from your scene and keep only the camera imported from this package.

## Technical Specifications
This package contains the following folder structure and files:

[Prefabs](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Prefabs) (folder)
Inside prefabs, you will find two prefabs. Both are prefabs that the user can import into their game with ready-made components, prepared animations, and scripts already integrated, both for the character and the camera.

In addition to the prefabs, we have the [Source](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Prefabs/Source) folder which contains the following items:
- Prefabs > Source > [Animations](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Prefabs/Animations) – Contains the animations used by the game prefab. These are the animations for running, walking, crouching, and jumping, all imported from Mixamo. Also included here is the character's Animator Controller.
- Prefabs > Source > [Models](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Prefabs/Source/Models) – Contains only the Y Bot model used in the prefab, downloaded from Mixamo.
- Prefabs > Source > [Scripts](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Prefabs/Source/Scripts) – Contains the scripts used in the game's character and camera. Read the previous project description section to understand how to use the scripts.
- Prefabs > Source > Scripts > [Commenteds](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Prefabs/Source/Scripts/Commenteds) – Contains the same scripts as the previous folder, however, in a very detailed manner with comments. Additionally, these scripts are written in a simpler and less minified way. Refer to these files to better understand the programming of this asset.
- [Demo.scene](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Demo.scene) – Demo scene of the package. Accessing this file gives you a simple scenario structure to test jumps, sprints, crouches, and other game situations. Moreover, it's a really cool scenario; you can use the scenario in your games to perform gameplay tests.
[Tutorial.pdf](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Tutorial.pdf) – The PDF document of this description and technical data contained within the project itself. It also contains the [Tutorial_portuguese.pdf](https://github.com/conradosaud/ThirdPersonPack/blob/master/Assets/EasyStart%20Third%20Person%20Controller/Tutorial_portuguese.pdf) file with all the content written in the Portuguese language.

All content in this package is completely __free__. The codes were written, structured, and commented by me, based on my programming and game development experience, done in a simple and basic way so that everyone can understand and learn to develop their own character controls for third-person gameplay. Additionally, Mixamo models and animations are used, which are also public and free. The Jump animation was created by me and is also public and free.

🌟 Rate this package on the Unity Asset Store!

 Enjoy the EasyStart Third Person Controller, it's free and always will be!🚀

