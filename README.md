**A Procedurally Generated World**

<img width="929" alt="Screenshot 2021-12-15 at 06 40 14" src="https://user-images.githubusercontent.com/55544267/146136510-da7a7869-f3e1-4dd0-8ee5-f2ee03706071.png">

Name:        Nicolas Condrea
Student No:  C18424946
Class Group: DT211C

**Reason for Choosing**

I wanted to do this for my assignment as for my final year project I will develop a 2D exploration game with some procedurally generated monuments and worlds. I grew up playing games such as Minecraft and Terraria which featured procedural terrain generation and has always intrigued me on how they are made, so it would be a great learning opportunity.

**How It Works**

Generating the map first starts of with our mesh which is essentially our grid for placing vertices. Each square of vertices contains 2 triangles. Once the terrain was generated I implemented perlin noise to shape the terrain as without it the terrain is flat and lacks any geographical features such as hills or lakes. Seed generation was added to ensure variation in landscape generation. Another simple cube mesh renderer acted as water which allowed the sea level to be adjusted to any y level. Gradients were used to give colour to the terrain depending on the height of the terrain. Unity's Universal Render Pipeline (URP) was used to display the shaders and materials onto the mesh. Tree prefabs were imported and used raycasting to randomly populate the world's landscape.

**Difficulties**

One of my first difficulties encountered was back-face culling. I had no idea prior to starting that the order in which the points of the triangles are declared effect whether they would render or not. I definitely went through trial and error whilst messing around with the noise function along with using URP which involved some figuring out. I had some scaling issues along with not being able to fully implement foliage placement. 

**What I'm Most Proud About**

Overall it would habe to be the final result as a whole, though not entirely finished it was definitely a step in the right direction. I am also proud with what I have learned throughout the development of this project. I had no previous experience with perlin noise and manipulating the shape of the noise with the use of octaves, it was also my first time messing around with gradients and Unity's Universal Render Pipeline. 

**Any Improvements?**

I would have liked to complete object placement around the map (trees, bushes, etc.) along with animals spawns. The addition of some sound effects or even music could have been added.

**List of Classes & Assets**

The projects consists of two classes:

MeshFormation.cs - Modified from https://catlikecoding.com/unity/tutorials/procedural-grid/ (Basic terrain generation tutorial, helped me get started with how to draw the triangles onto the mesh) and https://docs.unity3d.com/ScriptReference/Mathf.PerlinNoise.html (Perlin noise application and description).

ObjectPlacement.cs - Self Written

Tree prefabs were imported from the Unity Asset Store - https://assetstore.unity.com/packages/3d/vegetation/trees/stylized-trees-low-poly-49916

**Initial Project Proposal**

"A procedural Minecraft style voxel world, with Mountains, rivers and lakes, rendered appropriately"

