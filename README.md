# The-White-Battlefield

This is a casual game that seamlessly integrates augmented reality technology with interactive fun. It utilizes Unity's ARFoundation and Holokit gesture recognition features, aiming to provide players with an unprecedented immersive winter experience. This game ingeniously merges the virtual world with the real environment, allowing players to initiate a unique snowball fight at home, in the park, or any open space. The objective is to hit lively and adorable virtual snowmen, offering enjoyment and challenge amidst the winter atmosphere.	 :snowman_with_snow:  

![](https://github.com/Sraint2004/The-White-Battlefield/blob/main/Image/20240422195837.gif?raw=true)

## What is The-White-Battlefield

The project utilizes the meshing functionality of AR Foundation. This meshing feature provides environmental model information, allowing us to overlay visual effects on the physical environment to convey a specific theme, such as a snowy landscape. 

The project is positioned as an AR interactive experience, where users can participate through handheld devices or HoloKit. Whether it's indoors or outdoors, any space can become your battlefield for battling snowmen. 

At the start of the game, the scene is initially covered with a snowy texture. :snowflake:  Players can then throw snowballs by pinching and spreading their fingers, aiming to hit randomly generated snowmen  :snowman:   on surrounding physical surfaces. 

The game features an intense timed challenge - players must hit as many snowmen as possible within 60 seconds. The urgency of time prompts players to react quickly, strategically adjusting their throwing angles and strength. Each launch tests the player's observation and reaction speed, making the game competitive and entertaining.At the same time, special particle effects accompany each snowball hit, offering players a unique visual experience


## How does it work

In this project, we use gesture changes as input. Each time a "pinch" gesture :pinched_fingers:  is recognized, it triggers the generation of a snowball. When a "five" gesture :raised_hand_with_fingers_splayed:  is recognized, it launches the snowball. Snowballs are destroyed upon collision with mesh information (obtained using AR Foundation's meshing feature) and generated snowmen (also based on AR Foundation's plane detection feature). Upon hitting a snowman, points are scored and a particle system is triggered. Additionally, there is a fog object and a snow particle object used to add a snowing effect and atmosphere to our scene.

Our Unity scene consists of four parts: "Environmental Settings", "UI", "Generate and Destroy", and "Trigger and Score".

![](https://github.com/Sraint2004/The-White-Battlefield/blob/main/Image/032606.png?raw=true)


### Environmental Settings

In Environmental Settings, we set our main light of scene with a Directional Light object. Use Fog Particle and snow Particles to add a snowy atmosphere and effects to the scene. ARMeshMenager is used to detect planes and replace materials, achieving the effect of accumulated snowAR Session and XR Origin is basic creating AR experience in Unity.Use the snowballpose to determine the position where snowballs are generated.
![](https://github.com/Sraint2004/The-White-Battlefield/blob/main/Image/042507.png?raw=true)


### UI
In UI, The "score" object is a text used to count hits on the snowman, thus providing the final score. "Timer" is a text-based countdown tool used to remind players of the remaining time. "Image" is the snowman picture, serving as the score UI icon. "Gameover" is used to remind players that the game is over and display the final score, associated with the first "score" object. "Handgesture" is the default user interface for Holokit gesture recognition.
![](https://github.com/Sraint2004/The-White-Battlefield/blob/main/Image/42507.png?raw=true)

### Generate and Destroy

In "destroy and generate," the "enemymanager" object retrieves some plane parameters from the "armeshmanager" and randomly generates snowman prefabs on this plane. When the snowman prefab is hit by the snowball prefab, the "enemymanager" object destroys the snowman prefab and triggers the playback of the snowburst particle system, as well as triggering the next snowman generation script, thus allowing for the regeneration of snowmen after the initial ones are destroyed and achieving the effect of random generation.


![](https://github.com/Sraint2004/The-White-Battlefield/blob/main/Image/042509.png?raw=true)

### Trigger and Score

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

# Roadmap

## Released

1. Plane detection and material replacement
2. Random enemy generation on AR planes
3. Particle system and special effects production
4. Gesture recognition and interaction
5. Scoring determination and game timing
6. Existing UI design.

## In progress

1. Design and interaction settings for the start screen UI
2. Creation of snowball trailing effects and effects for when enemies are not hit
3. Sound effects production
4. Optimization of the end game screen and game restart

## Planned

1. Snow particle landing effects
2. Update game boss types
3. Game level design
4. Snowball appearance customization

## Under Consideration

1. Two-player battle mode
2. Character movement trajectories and snow footprints
3. Snow depth variation

## Project Development Process 

### Phase One: Project Preparation

Establish the game concept and core gameplay mechanics: including the theme of the game, gameplay features, etc. Create a conceptual diagram to visually represent the game concept and mechanics, understand the production process of AR games, and study the process and technology involved in augmented reality (AR) game development, including the use of relevant tools and platforms. Set up the basic environment and rendering pipeline required for game development.

### Phase Two: Implementing Basic Functionality

Set up the basic rendering pipeline, shaders, and a snowball snowman model. Utilize ARFoundation's meshing to achieve plane detection and coverage effects. Use the plane manager to recognize planes and implement snowball generation and throwing functionality through gesture recognition. Add basic collision detection and gravity components. Design a mechanism for enemy (snowman) generation - randomly spawn on detected planes and add movement animation upon spawning. Implement hit effects using particle systems. Design and implement a scoring system and a settlement interface.

### Phase Three: Beautification and Optimization

Optimize the snowball launch position, enhance the character models, and improve visual effects. Identify and rectify flaws discovered during real-device testing, such as adjusting snowman spawn quantity and interval time, adding masks, changing UI icons, modifying light intensity, and conducting tests to ensure game compatibility and stability.




