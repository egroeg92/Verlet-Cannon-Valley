# Verlet-Cannon-Valley

2D valley cannon simulation using Unity.  Physics is programmed (There are no rigidbodies)

A valley is generated using a midpoint bisection algorithm to add roughness to the slopes.

A cannon is placed on each slope; the cannon on the left fires ragdoll (connected verlets) dawgs, the left fires cannonballs.

Cannonballs and verlets have different physics applied to them.  
Cannonballs have regular particle physics applied to them defined by their super class (physicsBody).
Verlets are also subclasses of physicsBodies, however, they use verlet integration to determine their motion.

CannonBalls affect Dawgs but dawgs do not affect cannonBalls.

There is also air resistance, varrying fire power ,cannon angles and wind.
