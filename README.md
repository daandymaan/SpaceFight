# SpaceFight

| Name          | Student ID    | Class   |
| ------------- |:-------------:| -------:|
| Daniel Simons | C17371946     | DT228/4 |

# Plot
This is a recreation of the battle of midway but is set in the future in an asteroid field.
The ostur forces (Good guys) are attempting a sneak attack on the unsuspsecting jibinis (Bad guys) and their new superweapon through an asteroid.

___
# Models
The plane models are based on WW2 planes, such as the spitfire and the messerschmitt BF109
The large ship in these scene is based on a WW2 japanese battle ship. 
These three models were modelled using blender.

___
### Spitfire V2
Model                      |  Blueprint
:-------------------------:|:-------------------------:
![Spitfire V2](./Images/ModelImages/SpitfireV2.png "Spitfire V2")  |  ![Spitfire blueprint](./Images/spitfire_blueprint.jpg "Spitfire blueprint") 

___
### Messerschmitt V2
Model                      |  Blueprint
:-------------------------:|:-------------------------:
![Messerschmitt V2](./Images/ModelImages/MesserschmittV2.png "Messerschmitt V2")  |  ![Messerschmitt blueprint](./Images/Messerschmitt_Blueprints.gif "Messerschmitt blueprint") 
___
### McCannon 
Model                      |  Blueprint
:-------------------------:|:-------------------------:
![McCannon](./Images/ModelImages/McCannon.png "McCannon")  |  ![McCannon blueprint](./Images/aircraftCarrier_blueprint.jpg "McCannon blueprint") 

___
# Story Board
Key :
| Plot          | Mechanics     | Camera  |
| ------------- |:-------------:| -------:|
| **P**         | **M**         | **C**   |

___
## Scene 1
![Scene1](./Images/StoryBoard/scene1.png "Scene 1")

**P** : The scene opens with the ostur forces travelling through an asteroid field, swerving through asteroids.

**M** : Each plane will be using a path following behaviour and using object avoidance to swerve around the asteroids while maintaining a current path. Each plane will also bank when turning to simulate an actual plane turning. The propellar on the end of the ship slowly spins as the ship navigates through. 

**C** : The camera will maintain a side view of the planes going through the asteroid field.

___
## Scene 2
![Scene2](./Images/StoryBoard/scene2.png "Scene 2")

**P** : The ostur planes navigate to an opening in the density of the asteroids to which the McCannon enemy ship becomes visisible which is 100 times the size of each of the ostur ships.

**M** : Each plane will be using a path following behaviour and using object avoidance to swerve around the asteroids while maintaining a current path. Each plane will also bank when turning to simulate an actual plane turning. The propellar on the end of the ship slowly spins as the ship navigates through. 

**C** : The camera now mantains a view slightly behind the ships so that the McCannon ship is in sight.

___
## Scene 3 
![Scene3](./Images/StoryBoard/scene3.png "Scene 3")

**P** : The enemy McCannon has been alerted to the ostur ships inbound, a siren is sounded and the release of enemy jibini ships.

**M** : The enemy ships act as a swarm behaviour and do not colide with each other and approach the asteroid field where the ostur ships are located.

**C** : The camera holds a view where the jibini ships are seen released from the hull of the ship and also can see the ostur ships in the distance asteroid field.

___
## Scene 4 
![Scene4](./Images/StoryBoard/scene4.png "Scene 4")

**P** : This scene follows one ostur ship swerving through the asteroid field chasing an enemy jibini ship, other ships can be seen crashing and shooting at each other.

**M** : Both the main ships in this scene use object avoidance to navigate through the asteroid field, while the ostur ship chases the enemy ship, they both exchange laser fire with finally the enemy ship destroyed.

**C** : The camera follows carefully a behind angle position on the ostur ship. 

___
## Scene 5
![Scene5](./Images/StoryBoard/scene5.png "Scene 5")

**P** : This scene captures the final descent onto the large McCannon ship where they hope to destroy it, the McCannon is facing them and charging up its weapon and firing at it.

**M** : The McCannon is firing large beams of laser towards the ships and the ships are trying to avoid it, the ostur ship are each charging up their ultimate weapon which spins the propellar even faster. The McCannon's side turrets are aimed and firing at the ships also.

**C** : The camera captures the whole battlefield and the ostur ships charging towards the McCannon while the McCannon fires at them.

___
## Scene 6
![Scene6](./Images/StoryBoard/scene6.png "Scene 6")

**P** : This scene shows the ostur ships firing their ultimate weapons at the engine of the ship and then swooping over it and flying away from it.

**M** : The propellar will shoot a beam from it which will be a thicker beam then regular laser fire in doing so the propellar will spin at max velocity.

**C** : The camera captures the scene just over the area where the McCannon ship is damaged, to capture the ostur ships swerving over it 

___
## Scene 7
![Scene7](./Images/StoryBoard/scene7.png "Scene 7")

**P** : This scene caputures the ostur ships flying away from the McCannon as it explodes in the distance.

**M** : This scene uses simple path following mechanics. 

**C** : The camera is facing the ships as they fly towards it they fly past the camera exposing the full scale of the destruction.
