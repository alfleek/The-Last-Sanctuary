# The Last Sanctuary
##### Survive Against All Odds and Build the Last Sanctuary
### Concept: 
A tense zombie survival game where players scavenge, craft, and farm essential materials to build and fortify a base. Every decision matters as you struggle to survive against relentless, roaming zombies in a harsh, post-apocalyptic world.

### Team/ Roles: 
- Jazmin Carlos: Game Designer (Terrain & UI)  
- Alexander Lewis: Game Programmer(Player Controls & Core Mechanics)

###Format: 
3D | First-Person | Open-World Survival / Tower Defense

## Check-In #1 : 
- How did you structure your main game loop or update cycle in Unity?
<t>The main game loop is mostly handled by a InputManager script that works with unity's Input System. Within its Update() function, it calls two methods, ProcessMove() and ProcessLook() from the scripts PlayerMotor and PlayerLook. When calling these methods, it passes values read from the PlayerInput Input Action Asset that contains our control schemes. These functions then move the character and the camera accordingly. The InputManager script also subscribes several methods to PlayerInput, so that they can listen for when controls are used for certain actions. These methods are Jump, Crouch, Sprint, and OnControlsChanged (which is used to detect when the player swaps from K&B to gamepad or vice versa, to apply appropriate look sensitivity scalars). The use of the Input System means that innstead of having input reading within our player controller scripts, we simply create functions for different actions, that are called by the PlayerInput Input Action, which contains all of the actions with bindings to controls.

- What tools or methods did you use to design and texture your environment (e.g., terrain tools, tilemaps, layers)?  
<t>I mainly used prefabs and assets from the Unity Asset Store to build the terrain and environment by hand. Using Unity’s Terrain Tools, I shaped the landscape and added different textures like grass, dirt, and rock. I placed props such as trees, rocks, and structures manually to make the world feel more realistic. Lighting and post-processing were added to create a dark, immersive atmosphere that fits the survival theme.

- How did you implement player controls (movement, jumping, camera/view rotation, etc.)?
<t>Originally I implemented the PlayerMovement and MouseMovement scripts from the Canvas modules, however there were some issues. First of all, the player looking implementation would rotate the entire player object, wherever you were looking. This meant that when you looked up or down, your feet would be up in the air. To fix this, when the player looks around, the whole player model is rotated based on the x vector, while only the camera is rotated by the y vector. Additionally, I wanted to use Unity's new Input System, rather than relying on the default input manager. This gives more customization and flexibility to input schemes. Using it, I created basic input schemes for both keyboard & mouse, and gamepad. Additionally, I added look sensitivity scalars for both control schemes that can be manipulated seperately, and will be turned into sliders in our options menu in the future to easily adjust during play. The current movement consists of looking around, moving in any direction, jumping, and toggleable sprinting and crouching. Crouching and sprinting have different move speeds than walking. In the future, I'd like to add a setting to choose whether sprint and crouch are toggles or press and hold actions.
