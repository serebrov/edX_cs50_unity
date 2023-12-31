# CS50's Introduction to Game Development

[HarvardX CS50G: CS50's Introduction to Game Development](https://learning.edx.org/course/course-v1:HarvardX+CS50G+Games/home) 

Part 2: Unity projects.

# Lecture 8

Note: the `helicopter` project was created with Unity 2018.1.0b13,
but there is some bug and it hangs when trying to import sounds.

The 2018.4.28f1 works.

Procedure is the following:

* Create Unity account
* Download Unity Hub from the unity website (not the editor! the hub!)
* Install the Hub
* Start it, login to Unity account
* Swtich to "Installs" and click "Install Editor"
* Switch to the "Archive" tab
* Click the download archive link
* It opens https://unity.com/releases/editor/archive
* Find "Unity 2018.4.28" on the page
* Click "Unity Hub" button
* It will switch back to the Hub application
* Install the editor

Do not open the project just yet!

Install Blender 2.79b from this page: https://download.blender.org/release/Blender2.79/ (there is 2.79, but only tar.gz, download dmg installer for
2.79b).

Once blender is installed, open the helicopter project.

If the scene is empty, find the Scenes in the Project tree and double click
the main scene (the box with Unity icon titled "Main", not the "Main" folder).

Information from the [project page](https://cs50.harvard.edu/games/2018/projects/8/helicopter/):

> Downloading Blender
> Make sure to download the correct (older!) version of Blender, not the latest version, per these instructions!
> ... head [here](https://unity3d.com/unity/beta) to download version 2.79
> of Blender ...
> ... you can find the helicopter, skyscrapers, and airplane
> in the `Assets/Resources/Models` folder of the Unity project
> You can find some fantastic learning resources
[here](https://docs.blender.org/manual/en/dev/) and
[here](https://www.blender.org/support/tutorials/)!

> Downloading Unity
> Having some trouble with Unity? The staff has found that
> [Version 2018.4.28f1](https://unity3d.com/unity/qa/lts-releases)
> has worked well for them on a variety of different operating systems,
> if the below doesn’t work for you.

> You will need to download Unity before you can run the distro code
> follow the link [here](https://unity3d.com/unity/beta) to download Unity’s
> open beta.
>
> Once you’ve downloaded and logged in to Unity, click the “Open” button
> on the launcher and browse to the folder of the cloned code from the distro.
>
> But wait… nothing seems to load into the scene once you’ve opened it!
> With the project open in Unity, navigate to Assets/Resources/Scenes,
> and then select Main in the file browser at the bottom of the screen,
> double-clicking to open, and all should be loaded into the scene view!

> Note: If you find that some of the models in your scene are not showing up,
> it’s likely because you either don’t have Blender installed yet
> (see instructions above), or you opened the project prior to the
> Blender installation.
> If you already have Blender installed and still don’t see anything,
> do just right-click, in the Unity editor, any of the models located in
> `Assets/Resources/Models` and select the Reimport All option,
> which should fix missing models after a few moments of loading!

Unity notes:

* Inspector values can be changed by mouse drag:
  * Hover the label near the field and drag left or right
* We can play the game in the editor and we can pause it at any time
  * When paused, we can move in the scene to examine or adjust things
  * We can also play frame-by-frame (the third button after pause)
  * Note: changes that we do while the game is playing or paused do not get
    saved. Good side: we can change whatever we want and experiment without
    actually affecting actual game settings. Bad side: if we actually fixed
    something, we need to remember what it was, stop the playback and the
    re-apply changes.

Moving in the scene:
* Rotating the scene: hold "Option" + left click + mouse
* Use the x-y-z control in the top right corner to make it straight
  * Click the middle cube to change the perspective to isometric and back
* Move: hold right button
  * AWSD - move left/forward/backward/right
  * QE - move up/down

Related docs: https://docs.unity3d.com/Manual/SceneViewNavigation.html

## Helicopter implementation

Camera is an invisible game object (not rendered, but still a game object).

The camera and the helicopter stay in place when the game is on;
the background texture is scrolling.
The process is called UV mapping (U and V are X and Y coordinates inside
the texture, we use different letters since X, Y, Z already used for the scene
coordinates).

Related code: ./helicopter/Assets/Resources/Scripts/ScrollingBackground.cs
We use `offset = Time.time * scrollSpeed` and the texture constantly shifts
as the time goes (and it wraps around automatically, so the part that
shifts off the view on the left appears on the right).

Bump map: we do not have it for the texture in this game, but in general
it is used to model small bumps on the texture to make it more realistic.
The texture itself stays flat and the bump map only affects lightning.
This way it is still simple to render the texture (as it stays simple),
but it looks better.

All objects are containers (entities) and behaviors are achieved with
components (the Entity-Component system).

When we add a new object to the scene, it has one "Transform" component
that defines the object position, scaling and rotation.

Major property of the Camera is the Projection: perspective or orthographic.

Light is also an object that has Light component.
We can use it to model different types of lightning, light color, etc.
There is also an option to "back" the lightning, so the light will not
be rendered in runtime, but precomputed and added "statically".

We can create different types of components for the game, for example,
"CreatureComponent", "GoblinComponent", "PatrolComponent", "ChiefComponent",
"ToxicComponent".
And then we can attach these to object to get
- "Patrol goblin" = "CreatureComponent", "GoblinComponent"
- "Chief goblin (toxic)" = "CreatureComponent", "GoblinComponent",
"CheifComponent", "ToxicComponent".

Or we can add "PatrolComponent" to camera and make a "camera patrol" and
use it for the game intro (where the camera moves across the scene on its own).

The base class for components is "MonoBehaviour".
In Unity editor the component refers to the code via "script" property.

There is also a "Prefabs" field that is a list of related game objects.

For "CoinSpawner" we have:
* Script: ./helicopter/Assets/Resources/Scripts/CoinSpawner.cs
* Prefabs
  * Size: 1
  * Element 0: Coin

The prefab is also a game object with components, properties, scripts, etc.
Prefabs are placed under Project: Assets -> Prefabs in Unity editor.
In the game, prefab is used to create one or more game objects (like we
spawn multiple coins with coin spawner).

So "prefab" is like a JS prototype or a template - a predefined object that
is used to create multiple similar objects (either manually in the editor
or in the code).
We can modify the "prefab" and this will affect all objects based on it.
We can also modify individual objects created from the prefab to make them
slightly different.

To make a prefab: configure the object in the scene and then drag it to prefabs pane in Unity editor.

Unity has good documentation, for example the documentation on MonoBehavior:
https://docs.unity3d.com/2020.1/Documentation/ScriptReference/MonoBehaviour.html

Note: C# has optional "this", so when we access object properties
in the code, we can use object properties directly.
For example, `Destroy(gameObject)` means `this.Destroy(this.gameObject)`.

To implement collisions, we can attach the box collider component to
the game object, in this case, we have "Blades Collider" with "box collider"
component attached.

The collider component may be or may not be a trigger.
In this project, the helicopter body and blades are two colliders that are
not triggers. Skyscrapers, coins and airplane have colliders that are
triggers (so they control the colliding behavior).
Components that are triggers can implement the MonoBehavior.OnTriggerEnter()
and other "OnTrigger..." methods to add the necessary behavior.

Note: it is good for performance to use box colliders, even if the model
has more complex shape.
For example, for the helicopter there are two boxes - one for blades and
another for the helicopter body, so instead of having one collider with a
complex shape we have two simple/fast box colliders.

Another simple collider is capsule that can be used for rounded objects,
like the player head.

There is also a "RigidBody" component in Unity that will implement collisions
with other objects (it can also implement gravity).
We can still have triggers to add our own behavior, for example, the ball
will bounce off the wall if it is "RigidBody" and we can add a sound to
it or some visual effect that is shown on collision.

## Prefabs and Spawning

Most of the game objects are implemented as prefabs: airplane, blades and body, coin, skyscrappers and helicopter.

Only helicoper is pre-instantiated in the scene, all other objects are created (spawned) dynamically by "spanwer" objects: AirplaneSpawner, CoinSpawner and SkyscraperSpawner.

Spawners are empty game objects with attached scripts.

For example, the AirplaneSpawner (Assets/Resources/Scripts/AirplaneSpawner.cs).
The code:

```
public class AirplaneSpawner : MonoBehaviour {
    // This list is filled in in Unity editor.
    // In general, any public field will appear in the editor.
    public GameObject[] prefabs;

    void Start () {
        // trigger asynchronous randomized infinite spawning of airplanes
        StartCoroutine(SpawnAirplanes());
    }

    // Update is called once per frame
    void Update () {
    }

    IEnumerator SpawnAirplanes() {
        while (true) {
            // instantiate a random airplane past the right egde of the screen, facing left
            Instantiate(
              // Object to instantiate, we select one from the prefabs
              // array (we only have one airplane actually, but could
              // create more models and add to the prefabs list in the
              // editor).
              prefabs[Random.Range(0, prefabs.Length)],
              // X=26 (right edge of a screen), a bit random Y pos
              // and Z=11 - aligned with helicopter and skyscrapers.
              new Vector3(26, Random.Range(7, 10), 11),
              // Rotation of the prefab object (we could make the prefab
              // initially oriented the way it is in the game then we would
              // not have to rotate it here, could use Quaternion.identity
              Quaternion.Euler(-90f, -90f, 0f)
            );

            // pause this coroutine for 3-10 seconds and then repeat loop
            yield return new WaitForSeconds(Random.Range(3, 10));
        }
    }
}
```

It works asyncronously using coroutine - the `Start` method starts the coroutine and `SpawnAirplanes` yields one airplane at a time.

Note that we have infinite `while (true)` inside the coroutine - it is fine as it stops executing and waits on `yield`.

Without coroutine, we would have to do something like this:

```
public class AirplaneSpawner : MonoBehaviour {
    float spawnTime = 0;
    float spawnIncrement = 1000;

    public GameObject[] prefabs;

    void Update() {
      spawnTime += Time.deltaTime
      if (spawnTime > spawnIncrement) {
        Instantiate(...) // instantiate new airplane
        spawnTime = 0
      }
    }
}
```

## Assignment 8: Helicopter

Specification:

* Add Gems to the game that spawn in much the same way as Coins, though more rarely so. Gems should be worth 5 coins when collected and despawn when off the left edge of the screen. We have all of the pieces for this already implemented in the Coin and CoinSpawner classes, so it should suffice simply to make some new classes for the Gem and GemSpawner behaviors! In the Proto resource pack included in the Assets folder, you’ll find a model for a gem you can use, but feel free to import your own! You’ll need to make a prefab, recall, that you can attach to the GemSpawner component, should you choose to implement it similarly to what’s in the distro. There are of course other ways to implement this behavior, so feel free to experiment with the software as a chance to learn it all the more thoroughly if curious (but if you do decide to place it somewhere more unorthodox, make extremely sure when you commit your code that the staff is able to find it relatively quickly)! Do remember to make Gems worth 5 coins instead of just 1, and ensure they’re more rare than Coins as well! Aside from that, they should behave identically to Coins, including moving automatically from right to left and despawning when past the left edge of the screen!
* Fix the bug whereby the scroll speed of planes, coins, and buildings doesn’t reset when the game is restarted via the space bar. This one’s a one-liner; note that static variables aren’t actually reset upon loading a scene, so a place to check would be the SkyscraperSpawner, as the speed field therein is what actually drives the speed for Skyscrapers, Airplanes, and Coins! However, we won’t find that this is the place where the game is reset upon pressing the space bar, and thus changing speed here doesn’t make much sense; any guesses as to where the code for resetting the game could be located?

## Submission

Key points for the demo:

* Level counter increase
* Fall through hole/game over

Submission:

```sh
# git clone ssh://github.com/me50/serebrov.git
cd serebrov
git checkout main
gco -b games50/projects/2018/x/helicopter
cp -r ../unity_cs50/helicopter_8/* .
git add .
git commit -m "Helicopter submission"

git push origin HEAD
```

Github link: https://github.com/me50/serebrov/blob/games50/projects/2018/x/helicopter/Assets/Resources/Scripts
Youtube demo: https://youtu.be/ab5FABcP5JY

Notes:
I did not add a separate DiamondSpawner as it would be almost 
identical to the CoinSpawner. Instead I added more public fields to
CoinSpawner and configured it in Unity editor to make the difference:
diamonds are spawned not as often as coins and in a slightly different
horizontal position (also the Prefab for DimondSpawner is Diamond instead
of Coin).
The Diamond is implemented as Coin subclass so it reuses the `Update`
logic. Actually, Coin could also be made more configurable and reused
as a diamond (I could add a "value" field and make it 1 for coin and
5 for diamond).

# Lecture 9: Dreadhalls

It has two scenes:
- Title scene is a flat picture, similar to the Helicopter background
- Game scene is empty, with no light source, FPS controller and some objects to attach scripts to

Multiple scenes work similarly to multiple game states in love2d games, same idea of isolating specific game states into separate entities.

The game scene has "fog". It creates atmosphere as well as works as optimization since we do not have to render the scene for a far distance around the player.

The player is represented by capsule object with camera attached to where the player head is. This is handled by FPS controller.

Unity has different kinds of players in standards assets - with physics, no physics, etc.

## Materials and textures

We can attach a material to any object to define its visual properties.
The material has "albido" property where we can specify a texture or a color.

Note: in the lecture there was a problem with attaching material to the object (the object did not get new visual appearence automatically).
It looks like this is some bug in the imported asset with materials - some materials work correctly (like wall_A, floor_A and ceiling_A), but other materials do not apply texture to the object.
We can also attach texture manually by dragging a texture into the small box near the "albido" property.

Texturing complex objects is not easy. There was an example of a knight and a flat texture that can be applied to 3D object (this is called UV mapping).

We can not just apply a regular texture to a complex object. For example, if we have a table and apply a standard texture to it, it will not look good.

When we create a model in 3D modeling software, it usually also allows to export a material with special texture for the modeled object.

Material is a shader (program running on the graphic card) and parameters of the shader exposed as material settings.
Material also represents physical properties, for example, if it is slippery when you walk on it.

More information:
- https://www.pluralsight.com/courses/3ds-max-uv-mapping-fundamentals
- https://catlikecoding.com/unity/tutorials/rendering/part-9/

## Light sources

Point light emits light in all directions. Good for things like lamps, street lights, etc. We can change the color of the light.

Spot light emits light in a single direction. It can have "cookies" - a shape that light will shine thorough like a batman logo.

Directional light - casts light in a specific direction, but throughout the entire scene, like sun. It does not matter where exactly is the light source is, it will work the same for all objects in the scene.

Area light - light that is emitted from the surface of an object in the specified direction. Computationally expensive. For this reason area light is usually baked (precomputed and saved), which also means that we cannot dynamically affect the baked lightning.

Bump map is created by illuminating 3D objects and getting a flat image of that illumination. It allows to simulate a 3D contour on a flat surface.
When bump map is applied, we do not have actual 3D objects there, but the lightning system "thinks" that objects are there and calculates lightning accordingly.

Note: normal map and bump map is the same thing.

In unity editor, we have a "normal map" property for the material.
We can drag a normal map texture into this property to apply it. There is also a number associated with the normal map texture we can make it more "bumpy" by increasing that number.

About lightning:
- https://catlikecoding.com/unity/tutorials/rendering/part-15/
- https://docs.unity3d.com/Manual/Lighting.html
- https://en.wikipedia.org/wiki/Normal_mapping

Global lightning settings in Unity editor: window -> rendering -> lightning settings (unity 2018.4.28f1).

We use "Environment lightning", source=color for this game.
We also have "fog" enabled here. It is possible to set the color for the fog.

If we start the game, we can see the generated maze in the scene, but it looks like a box (there is roof, so we can not see inside it).

How to make the maze easier to see:
- disable fog in lightning settings
- add directional source to the scene
- disable "generate roof" setting for the DungeonGenerator.

## Maze

We have 2D array representing the maze.
We start with array full of walls and we add some corridors to it.

Maze generation:
- Do not touch external walls
- Choose a starting point
- Randomly choose the move direction - x or y
- Randomly choose the direction - +1 or -1
- Remove the wall in the tile we arrived to
- Repeat until we cleared given amount of tiles

Improvement to make the maze less random:
- Also randomly choose length of the move - from 1 to (maze length)-2
- Remove walls in tiles we go through

This way we get longer corridors and more organized maze structure.

Related links:
- [Maze, a Unity C# Tutorial](https://catlikecoding.com/unity/tutorials/maze/) 
- https://journal.stuffwithstuff.com/2014/12/21/rooms-and-mazes/
- Unity asset store also has some maze generators

## Character controller

The FPSController used in this game was imported from Unity asset packages.
Assets -> Import Package -> Characters. The imported object goes into the Assets/Standard Assets/Characters/First Person/Prefabs folder.
There is also source code (scripts) for the FPSController in the Scripts folder (near the Prefabs).
The FPSController exposes many configuration parameters available in the editor.

## Level generator

The level generator is in Assets/Scripts/LevelGenerator.cs.
It has a bunch of references to `GameObject` like floorPrefab, wallPrefab and characterController, so we can configure these in the editor.

Two special properties are floorParent and wallsParent.
These are needed to just group generated floor and wall objects under these parents. There are too many objects generated and it would be hard to inspect the game in Unity editor if they were not placed under these parents (so these parents do not affect the game object, they needed to group related objects in unity editor).
There is a helper `CreateChildPrefab` to create new object based on prefab and attach it to the given parent object.

The generator creates the maze as a X-Y array and then transfers it to Unity's X and Z axis (X-Z represent the surface in Unity and Y is for up/down axis). For the maze generator each block is 4 units high along the Y and we do not care about Y coordinate otherwise.

```
     y
     ^
 z   |
  \  |
   \ |
    \|______x

```

## Scene loading and reloading

The script is in Assets/GrabPickups.cs.
It is attached to the PickupController.
The script handles collision with the game object that has a "Pickup" tag - in that case it plays the pickup sound and reloads the "Play" scene by invoking `SceneManager.LoadScene("Play")`.

The same `SceneManager.LoadScene("Play")` call is used in title scene script to start the game. In the LoadSceneOnInput.cs we have `Input.GetAxis("Submit") == 1` condition which gets triggered when we press enter. The "Input" is a global Unity object and we can manage its settings (for example, change the key the Submit reacts to) in Edit -> Project Settings -> Input.

Note: when we reload the scene, all objects are get destroyed and re-created. It would mean, for example, that audio track gets interrupted and restarted again. We do not want that interruption and solving it by using  "don't destroy on load" Unity function.

This is implemented via `DontDestroy` behavior (component) that is attached to the audio source (any object this component is attached to would not be destroyed on scene reload).
We also implement a kind of singleton pattern in this behavior by keeping a reference to the first object source, keeping it on scene reload, but letting it destroy other instances of this object, so we do not get multiple copies of it.

Note: does it mean that we still may have multiple instances of the audio source at some points in the game lifecycle? Maybe these points are too short and we do not hear the effect of it?
Answer: yes, we destroy other instances almost immediately as the process is handled in the `Awake` method that is also called on object instantiation.

## Unity 2D mode

There is a button in the bar above the scene to switch to 2D mode.
To start working with 2D scene we need to add a canvas (or, if we add any of the 2D objects, Unity will create a canvas object automatically). For example, if we right click in the Hierarchy pane and then add UI -> Text, it will create a canvas and a text object on it.
It also creates an `EventSystem` object that is used to communicate with the canvas (for example, handle mouse or keyboard input).

To show the title screen on the black background (instead of standard view of ground and sky), we change the camera settings - set "Clear Flags" to "Solid Color" and then select the black color in "Background" field.

## Assignment 9

Create gaps in the floor 2 blocks deep. When the player falls into the gap, display "Game Over" text. Pressing "Enter" in this state restarts the Title scene.

Add a "text" object to the Play scene to show how many levels we passed.

### Specification

* Spawn holes in the floor of the maze that the player can fall through (but not too many; just three or four per maze is probably sufficient, depending on maze size).
  * This should be very easy and only a few lines of code.
  * The LevelGenerator script will be the place to look here; we aren’t keeping track of floors or ceilings in the actual maze data being generated, so best to take a look at where the blocks are being insantiated (using the comments to help find!).
* When the player falls through any holes, transition to a “Game Over” screen similar to the Title Screen, implemented as a separate scene.
  * When the player presses “Enter” in the “Game Over” scene, they should be brought back to the title.
  * Recall which part of a Unity GameObject maintains control over its position, rotation, and scale? This will be the key to testing for a game over; identify which axis in Unity is up and down in our game world, and then simply check whether our character controller has gone below some given amount (lower than the ceiling block, presumably).
  * Another fairly easy piece to put together, though you should probably create a MonoBehaviour for this one (something like DespawnOnHeight)! The “Game Over” scene that you should transition to can effectively be a copy of the Title scene, just with different wording for the Text labels.
  * Do note that transitioning from the Play to the Game Over and then to the Title will result in the Play scene’s music overlapping with the Title scene’s music, since the Play scene’s music is set to never destroy on load; therefore, how can we go about destroying the audio source object (named WhisperSource) at the right time to avoid the overlap?
* Add a Text label to the Play scene that keeps track of which maze they’re in, incrementing each time they progress to the next maze.
  * This can be implemented as a static variable, but it should be reset to 0 if they get a Game Over.
  * This one should be fairly easy and can be accomplished using static variables; recall that they don’t reset on scene reload. Where might be a good place to store it?

## Submission

Key points for the demo:

* Pick up the diamond
* Speed reset

Submission:

```sh
# git clone ssh://github.com/me50/serebrov.git
cd serebrov
git checkout main
gco -b games50/projects/2018/x/dreadhalls
cp -r ../unity_cs50/dreadhalls_9/* .
git add .
git commit -m "Dreadhalls submission"

git push origin HEAD
```

Github link: https://github.com/me50/serebrov/blob/games50/projects/2018/x/dreadhalls/Assets/Scripts
Youtube demo: https://youtu.be/YAS5Pb2ISXU

Notes:
Holes are implemented in the LevelGenerator, as suggested.

GameOver check is in the GameOverOnFall.cs script, attached to the FPSConroller object.
The static audio source object is also destroyed here (in GameOverOnFall.cs).

Maze count is implemented in MazeCount.cs script, attached to the UI text on the Play scene. The count is increased in the GrabPickups.cs script and reset to 0 in GameOverOnFall.cs.

The maze count is in the left top corner.

There is a typo in the specification:
"The LevelGenerator script will be the place to look here; ..., so best to take a look at where the blocks are being insantiated ..,.".
"insantiated" should be "instantiated"

# Lecture 9: Portal

Parenting:
The gun is attached to the FPS controller as a child.
In this case Unity automatically applies all transforms of the parent (FPS character) to the gun, so it moves along with the character.

Raycasting:
Part of the physics namespace, part of the scripting API.
We have a start point as a source and give a vector as a direction. We can use Transform.forward to specify a direction that takes current X and Y and make a vector along the Z, so if we are at the camera we have a vector pointing to the direction we are looking at.
As a result we get a point where that ray intersects with something.

Unity has a function `Debug.DrawRay` to show rays in the editor view (this is a debug function, it only applies to the editor).
We can attach it to, for example, PortalGun object, start the game and see in the editor view where rays are casted.
See [./portal_10/Assets/Scripts/DebugRay.cs](Assets/Scripts/DebugRay.cs) - the behavior that enables ray debugging on update.

In the game, we use `Physics.Raycast` in the [./portal_10/Assets/Scripts/PortalGun.cs](Assets/Scripts/PortalGun.cs) to find where to put the portal:

```csharp
void FirePortal(string type) {

	// struct object that will hold our raycast information
	RaycastHit hit;

	// if we collide with an object with our raycast, spawn a portal there
	if (Physics.Raycast(gunTip.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)) {
		portalSound.Play();
		
		// choose between the correct portals based on string input
		GameObject portal = type == "orange" ? orangePortal : bluePortal;

		// set the portal to the same position as the raycast point, and set
		// its rotation to orient to the wall relative to what its "up" direction is,
		// which is Vector.up in world space 
		portal.transform.SetPositionAndRotation(hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
	} else {
		errorSound.Play();
	}
}
```

To enable VR: edit - project settings - player - XR settings - virtual reality and, if you have a VR headset attached, it will work with the camera.

RenderTexture: asset that we use for rendering.
Portals are render textures.
Initially they are rendered outsize the level space (can be seen in the editor).
Create: right click in the Project view - Create - Render Texture.
It is important to have good resolution for the render texture, so it does not look pixelated. In this demo we use 1024x1024 which should be good enough, but in a real game we would need to adjust it depending on the actual game resolution settings (that can be changed dynamically).

We render camera view to render textures.
There are two additional cameras attached to each portal.
The camera object has target texture property, so we just specify the render texture in Unity editor to make it work: camera view is displayed on the render texture and we can see what that camera sees when we put the portal on the wall.

Texture masking:
We use it to make the portal to be oval, while it works based on the square render texture.
So we have a square render texture and an oval mask applied to it.

The mask is implemented as an image where white pixels represent areas that are rendered and black pixels represent parts that are not rendered and a masking shader [.portal_10/Assets/Shaders/MaskedTexture.shader](portal_10/Assets/Shaders/MaskedTexture.shader):

```
Shader "MaskedTexture"
{
   Properties
   {
      _MainTex ("Base (RGB)", 2D) = "white" {}
      _Mask ("Culling Mask", 2D) = "white" {}
   }
   SubShader
   {
      Tags {"Queue"="Transparent"}
      Lighting Off
      ZWrite Off
      Blend SrcAlpha OneMinusSrcAlpha
      Pass
      {
         SetTexture [_Mask] {combine texture}
         SetTexture [_MainTex] {combine texture, previous}
      }
   }
}
```

See: http://wiki.unity3d.com/index.php?title=Texture_Mask

Portals implementation
See [.portal_10/Assets/Scripts/Portal.cs](portal_10/Assets/Scripts/Portal.cs):

```csharp
// Note: `other` is the player object.

// cache player rotation to revert after teleport
// Note: without it, if we fall through the portal in the floor,
// players axis rotates in a way that instead of turning left and right,
// mouse starts turning the room around, as if the player was an airplane
// flying through it and we tilted the airplane left and right.
float xRot = other.transform.rotation.x;
float zRot = other.transform.rotation.z;

// set the player's position and rotation to the other portal's
other.transform.SetPositionAndRotation(linkedPortal.transform.position, 
	Quaternion.identity);
other.transform.rotation = linkedPortal.transform.parent.transform.rotation;

// Y rotation from portal
float yRot = other.transform.eulerAngles.y;

// combine previously cached axes with new Y to get new rotation
other.transform.eulerAngles = new Vector3(xRot, yRot, zRot);

// override FPSController's mouse look caching
other.GetComponent<FirstPersonController>().MouseReset();
```

## ProBuilder and ProGrid

ProBuilder allows to model 3D objects directly in Unity editor (this is normally done in 3D rendering software like Blender).
ProGrid adds a grid to the editor allowing to snap objects to it.

The "Flip Normals" feature in ProBuilder allows to turn a 3D object into a "room" (so we can have a scene inside the room shaped as this object).

Shadergraph: allows to model a shader with UI graph, connecting standard nodes, generates shader code (can be added via Window - Package Manager - Shadergraph).

## Assignment 10


- Create your own level in a new scene using ProBuilder and ProGrids!
- Ensure that the level has an FPSController to navigate with in the scene.
- Ensure that there is an object or region with a trigger at the very end that will trigger the end of the level (some zone with an invisible BoxCollider will work).
- When the level ends, display “You Won!” on the screen with a Text object.

Specification:

- Create your own level in a new scene using ProBuilder and ProGrids! The distro should already have ProBuilder and ProGrids imported and ready for use, but just in case they aren’t, you can easily find them by searching in the Asset Store (where they are now free, thanks to Unity having acquired them!). There are many resources for learning how to use ProGrids effectively, but two resources in particular that are worth checking out are here and here, which should more than prepare you for creating a simple level.
- Ensure that the level has an FPSController to navigate with in the scene. This part’s probably the easiest; just import an FPSController from the Standard Assets! It should already be imported into the project in the distro, where you can find the prefabs under Assets > Standard Assets > Characters > FirstPersonCharacter > Prefabs!
- Ensure that there is an object or region with a trigger at the very end that will trigger the end of the level (some zone with an invisible BoxCollider will work). This one should be easy as well, just relying on the creation of an empty GameObject and giving it a BoxCollider component, which you can then resize via its resize button in the component inspector!
- When the level ends, display “You Won!” on the screen with a Text object. Recall that OnTriggerEnter is the function you’ll need to write in a script you also associate with the BoxCollider trigger, and ensure that the BoxCollider is set to a trigger in the inspector as well! Then simply program the appropriate logic to toggle on the display of a Text object that you also include in your scene (for an example on how to do this, just see the Helicopter Game 3D project, specifically the GameOverText script)!


## Submission

Key points for the demo:

* “You won” message

Submission:

```sh
# git clone ssh://github.com/me50/serebrov.git
cd serebrov
git checkout main
gco -b games50/projects/2018/x/portal
cp -r ../unity_cs50/portal_10/* .
git add .
git commit -m "Jump puzzle submission"

git push origin HEAD
```

Github link: https://github.com/me50/serebrov/blob/games50/projects/2018/x/portal/Assets/Scripts
Youtube demo: https://youtu.be/M-IKvbcIRas

Notes:
* The player needs to go up the two blocks of stairs and jump down through the torus to win the level
* There is a "catch" plane below the level: the player is teleported back to the start box on collision (so the level restarts when you fall down)
* The default skybox was styled with "Wispy Skybox" asset (skybox material is assined in window - rendering - lightning settings - environment - skybox material)
* There was a problem with mouse beign locked to left-right movements when building the game (but worked fine when starting the game in Unity editor). Turned out to be a second camera (the "Main Camera" object added to the new scene automatically). Removing the main camera solved the problem.
