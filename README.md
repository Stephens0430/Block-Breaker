# Block-Breaker

This is a simple Block Breaker game where you use the mouse to aim and the spacebar to shoot.

Here are some notes on some of my design choices and more.

Starting off with the game events. I went with the observer pattern, where the event caster and listeners are decoupled and all of the events are centralized in the GameEventsScriptableObject.cs. Making this script a Scriptable Object also makes it easier to reference these events in other scripts without the need for an instance and the potential issues that maintaining an instance may cause (Also could have potentially went with a static event approach).

In the GameManager.cs I just manage the State.cs objects that the game may be in at any time. I created States that Manage what happens in each particular given state. 
 - For example the gameover state listens for the OnGameOver event then starts by turning on the gameoverscreen UI transform then waits to hear back from the Replay button when it is pressed. When it recieves that event it simply fires an event letting anything     subscribed know that this state is over and passes what the next state should be along.
This makes creating new states really easy to implement.
 - If for example you wanted to create a new state that goes to the main menu. You can just created a new state called MainMenuState and pass that from the gameoverstate when it ends. Then add functionality that handles the main menu in this MainMenuState script. 

For the ball I actually was going back and forth on how I actually wanted it to work. There are pretty much 2 ways that I was thinking about doing this:
 - Using physics where the ball has a rigidbody and I aim the ball and send it on its way using an Impulse force. (This is what I ended up doing)
 - 2nd way would have been a more controlled approach where I could aim which ever Vector direction (Like Vector3.forward) and Lerp in that direction. When the ball hits a collider I could get the angle of the collision (then to keep it "simple") rotate the ball at the same reflected collision angle and keep lerping forward.

The turret is pretty simple, just 2 scripts that each have a single responsibility one for aiming and the other that detects the spacebar press to shoot.

For the blocks and balls I created Block/ or Ball TypeCatalogScriptableObjects that serve as "parts containers" and the Block/Ball PoolScriptableObjects are sort of factories that assemble the properties from those scriptable objects.

For optimization here are some things that I implemented:
 - For the balls and blocks I used object pooling to keep cpu spiking at a minimum. All blocks have one material, all balls have one material. The materials are using GPU instancing.
 - On the physics side of things all blocks are static. The static blocks used as bounds are stripped of scripts and colliders and all share 3 colliders one on top, left, and right. I also created layers and changed the physics collision matrix to make sure that there are less collision checks happening.   

---------------------------------------------------------
Things that I would have liked to improve on/ change
 - I would definitely create my own pooling scripts rather than using Unity's built in system. Unity has known bugs with some of the helper functions like ObjectPool.CountActive that would normally have been pretty useful.
 - Normally when I create my own pooling classes I prewarm the pool (instantiate a minimum amount immediately). Unity's built in ObjectPool does not prewarm by default, instead it instantiates and reuses what's been instantiated so it warms up as you use them. (Either way could be a good way to go, just depends on the actual object and goal)   
