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

* Rotating the scene: hold "Option" + mouse
  * Use the x-y-z in the top right corner to make it straight
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


