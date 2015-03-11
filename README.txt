George Macrae
260401458

Scripts:

-TerrainGenerate

uses midpoint bisection to generate a valley using cubes
creates cannons on opposite sides

-Valley

this is the overall game controlling script.  Change windRange, Air Res and shootPowerRange.


-Cannon

creates a cannon ball or dawg on space or tab.

-PhysicsBody

mass, velocity acceleration and forward vectors
forward and backward collision detection methods

update calls position += velocity, velocity += accelertion 

-CannonBall

extends physics body

-Dawg

holds verlets, sets verlet constraints

-verlet

extends physics body but does not call parent update, rather uses verlet integration to determine velocity.