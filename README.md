# The-White-Battlefield

This is a casual game that seamlessly integrates augmented reality technology with interactive fun. It utilizes Unity's ARFoundation and Holokit gesture recognition features, aiming to provide players with an unprecedented immersive winter experience. This game ingeniously merges the virtual world with the real environment, allowing players to initiate a unique snowball fight at home, in the park, or any open space. The objective is to hit lively and adorable virtual snowmen, offering enjoyment and challenge amidst the winter atmosphere.

![](https://github.com/Sraint2004/The-White-Battlefield/blob/main/Image/20240422195837.gif?raw=true)
## What is The-White-Battlefield

The project utilizes the meshing functionality of AR Foundation. This meshing feature provides environmental model information, allowing us to overlay visual effects on the physical environment to convey a specific theme, such as a snowy landscape. 

The project is positioned as an AR interactive experience, where users can participate through handheld devices or HoloKit. Whether it's indoors or outdoors, any space can become your battlefield for battling snowmen. 

At the start of the game, the scene is initially covered with a snowy texture. Players can then throw snowballs by pinching and spreading their fingers, aiming to hit randomly generated snowmen on surrounding physical surfaces. 

The game features an intense timed challenge - players must hit as many snowmen as possible within 60 seconds. The urgency of time prompts players to react quickly, strategically adjusting their throwing angles and strength. Each launch tests the player's observation and reaction speed, making the game competitive and entertaining.At the same time, special particle effects accompany each snowball hit, offering players a unique visual experience


## How does it work

In this project, we use gesture changes as input. Each time a pinch gesture is recognized, it triggers the generation of a snowball. When a "five" gesture is recognized, it launches the snowball. Snowballs are destroyed upon collision with mesh information (obtained using AR Foundation's meshing feature) and generated snowmen (also based on AR Foundation's plane detection feature). Upon hitting a snowman, points are scored and a particle system is triggered. Additionally, there is a fog object and a snow particle object used to add a snowing effect and atmosphere to our scene.

Our Unity scene consists of four parts: "Environmental Settings", "UI", "Generate and Destroy", and "Trigger and Score".

![](https://github.com/Sraint2004/The-White-Battlefield/blob/main/Image/032606.png?raw=true)


### Environmental Settings

In Environmental Settings, we set our main light of scene with a Directional Light object. Use Fog Particle and snow Particles to add a snowy atmosphere and effects to the scene. ARMeshMenager is used to detect planes and replace materials, achieving the effect of accumulated snowAR Session and XR Origin is basic creating AR experience in Unity.Use the snowballpose to determine the position where snowballs are generated.
![](https://github.com/Sraint2004/The-White-Battlefield/blob/main/Image/042507.png?raw=true)


### UI
In UI, The "score" object is a text used to count hits on the snowman, thus providing the final score. "Timer" is a text-based countdown tool used to remind players of the remaining time. "Image" is the snowman picture, serving as the score UI icon. "Gameover" is used to remind players that the game is over and display the final score, associated with the first "score" object. "Handgesture" is the default user interface for Holokit gesture recognition.
![](https://github.com/Sraint2004/The-White-Battlefield/blob/main/Image/42507.png?raw=true)

### Generate and Destroy

In "destroy and generate," the "enemymanager" object retrieves some plane parameters from the "armeshmanager" and randomly generates snowman prefabs on this plane. When the snowman prefab is hit by the snowball prefab, the "enemymanager" object destroys the snowman prefab and triggers the playback of the snowburst particle system, as well as triggering the next snowman generation script, thus allowing for the regeneration of snowmen after the initial ones are destroyed. 


![](https://github.com/Sraint2004/The-White-Battlefield/blob/main/Image/042509.png?raw=true)

## Trigger and Score

In "Trigger and Score","the Hand Gesture Recognition Manager "and "AR Snowball Throw" are designed to handle interactions between game objects and manage the mechanism of throwing snowballs in the gaming environment. The Hand Gesture Recognition Manager is used to identify gestures, recognizing the "pinch" and "five" gestures through hand joint recognition, and providing functions for the "AR Snowball Throw" object. This triggers scripts for generating and launching snowballs.

Finally, the "AR Snowball Throw" object provides some functions for our UI elements. It provides data to "score" by counting the number of snowmen destroyed.


![](https://github.com/Sraint2004/The-White-Battlefield/blob/main/Image/042508.png?raw=true)

## Requirements
  
This project aims to build an app runs on iOS device.

1. Unity 2022.3.17f1c1
2. Xcode 15.3
3. iPhone with lidar capability

## How to use

1. Clone the project, open with Unity
2. Open Assets -> ARSnowGames -> Sample Scene
3. Build this scene to an Xcode project
4. Open Xcode, build app to your mobile device