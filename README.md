# The-White-Battlefield

This is a casual game that seamlessly integrates augmented reality technology with interactive fun. It utilizes Unity's ARFoundation and Holokit gesture recognition features, aiming to provide players with an unprecedented immersive winter experience. This game ingeniously merges the virtual world with the real environment, allowing players to initiate a unique snowball fight at home, in the park, or any open space. The objective is to hit lively and adorable virtual snowmen, offering enjoyment and challenge amidst the winter atmosphere.

## What is The-White-Battlefield

The project utilizes the meshing functionality of AR Foundation. This meshing feature provides environmental model information, allowing us to overlay visual effects on the physical environment to convey a specific theme, such as a snowy landscape. 

The project is positioned as an AR interactive experience, where users can participate through handheld devices or HoloKit. Whether it's indoors or outdoors, any space can become your battlefield for battling snowmen. 

At the start of the game, the scene is initially covered with a snowy texture. Players can then throw snowballs by pinching and spreading their fingers, aiming to hit randomly generated snowmen on surrounding physical surfaces. 

The game features an intense timed challenge - players must hit as many snowmen as possible within 60 seconds. The urgency of time prompts players to react quickly, strategically adjusting their throwing angles and strength. Each launch tests the player's observation and reaction speed, making the game competitive and entertaining.At the same time, special particle effects accompany each snowball hit, offering players a unique visual experience


## How does it work

In this project, we use gesture changes as input. Each time a pinch gesture is recognized, it triggers the generation of a snowball. When a "five" gesture is recognized, it launches the snowball. Snowballs are destroyed upon collision with mesh information (obtained using AR Foundation's meshing feature) and generated snowmen (also based on AR Foundation's plane detection feature). Upon hitting a snowman, points are scored and a particle system is triggered. Additionally, there is a fog object and a snow particle object used to add a snowing effect and atmosphere to our scene.

Our Unity scene consists of four parts: "Environmental Settings", "UI", "Destroy and Generate", and "Trigger and Score".



### Settings
<img src=https://github.com/Sraint2004/The-White-Battlefield/blob/main/Image/%E5%B1%8F%E5%B9%95%E6%88%AA%E5%9B%BE%202024-04-22%20021239.png?raw=true width="320" />


In Settings, we set our main light of scene with a Directional Light object. A Volume to add post-processing effect to our scene. AR Session and XR Origin is basic creating AR experience in Unity.

### UI

<img src="Documentation~/images/bat03.png" width="320" />


In UI, we got a Canvas called HoloKit UI Canvas and An EventSystem Obbect. Inside canvas there are four objects:

<img src="Documentation~/images/bat04.png" width="320" />


First two are default UI for HoloKit usage, we won’t discuss them here. 

The “Mesh Button” object is a button to control On&Off of ARMeshManager feature. if you always keep this feature On, meshing feature will kill the performance of your phone. Here the button on your own hands, you can disable this feature when you got enough mesh information of your surroundings.

The”Mesh Detail Slider” is a slider object used to control the level of detail of your mesh. A higher value means a more detailed mesh, resulting in a smoother and better matching between meshing information and real environment

### Sound and Effect

<img src="Documentation~/images/bat05.png" width="320" />


In “Sound and Effect”, “Mic Input” object detects all the voice input of your device, if the volume is beyond the pre-set value, it will trigger a event, which in this case is to make a visual effect from your current position.

The “BatVFXShaderController” objects grab some parameters and pass them to our visual effect. Our visual effect is made with ShaderGraph, relates to current position, needs an awareness of current position and trigger time to create a time-varying visual effect.

Finally, “SceneManager” object provides some functions for our UI elements. UI itself is a button, slider or sth else with no function in it. We create those functions and bind them to UI elements. Such as a function controls the On&Off of ARMeshManager.

## Requirements

This project aims to build an app runs on iOS device.

1. Unity 2022.3.8f1
2. Xcode 14.2
3. iPhone with lidar capability

## How to use

1. Clone the project, open with Unity
2. Open Assets -> BatSound -> Scene
3. Build this scene to an Xcode project
4. Open Xcode, build app to your mobile device