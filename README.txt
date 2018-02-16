
https://sdocy.github.io/dark-kingdoms/



2-03-18

A small project in which I am attempting to design large-scale, autonomous battles. The units include code for movement, targeting and attacks.

- the prototype can handle 200 v 200 battles before starting to lag

- sliders for choosing army mix automatically adjust to allow a maximum of 200 units per army

- I use Physics2D.OverlapCircleAll() to get enemies within visibility range and then choose the closet one using Vector3.Distance()

- a number of movement strategies are implemented, including random, march and moveWithinDistanceOfTarget

- random movement is not completely random, it includes a stride attribute which tends to keep units going in the same direction,
occasionally changing direction, for more natural looking wandering

- armies are setup at the beginning in rows of 100 units, melee in front, archers in back.....or will be once I get the kinks out



2-15-18

It's been a few releases of Unity since I first implemented this. Added some major performance improvements and some movement tweaks

- simply turning off all Physics2D collisions for the layers of both armies allowed me to scale up to 800 v 800 with no lag, 1200 v 1200
with a little lag

- I tried replacing Physics2D.OverlapCircleAll() with Physics2D.OverlapCircleNonAlloc() but I didn't really see any reduction in lag

- updated "move to target" algorithm to something that moves directly towards the enemy unit rather going on a diagonal and then straight
See my code gist for details....... mvt_stayWithin()

- added small per-unit variance in movement speed, now the armies look much more natural as they charge at each other. I love when a simple
idea adds a lot of depth to what you are working on

- I need to update the sliders to handle the larger army sizes (I currently just hacked my army spawner to get bigger armies) and that will fix
the troop counters on the battle page



TODO:

- Play with something I am calling "dynamic visibility". Currently, visibility (the radius used for OverLapCircleAll()) is set to 2.0f for all units.
I am thinking that I could reduce this range when there are lots of units on the board (perhaps 1000+) and increase it as units get killed off.
My assumption is that this could significantly improve performance by reducing the work overLapCircleAll() must do and also reduce the number of
objects my code must examine with Vector3.Distance() to find the closest. This should be especially useful for melee units who get in close to
to each other. It will likely have the undesirable effect of causing archers to move closer than I want to their targets, so I will likely
only implement it for melee.

- play with Vector2.Distance() instead of Vector3.Distance()

- play with turning off remaining Physics.2D layers for brown and red team, see if it helps performance any more even though there are no
other objects for them to collide with

- replace army size hack with actual support for larger army sizes, requires updates to sliders and correctly breaking the armies into rows

