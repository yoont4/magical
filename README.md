# MAGICAL

SCRIPT SPEC

Mouse:
	- generic pixel reticle
	- on mouse click, spawn *Cast* object (look at *Directional Output*)
	- while mouse held, show cast reticle
	
	UI:
		- mouse scroll should switch between magic
		- invisible when no input
		- visible for 1 second after last input
		- should follow reticle
	Directional Output:
		- tracks on mouse hold
		- track mouse movement direction
		- track mouse velocity
	Cast:
		- casting is comprised of a trail effect object (denotes the cast)
		and an actual spawn item.
		- trail should "lag" behind mouse
		- trail is spawned during mouse hold
		- trail is killed on mouse release
		- spawn is spawned on mouse release
		Trail:
			- should be a shuriken particle effect (not custom)
			- purely visual, should not interact with world
		Spawn:
			- some object is created with initial velocity set to the
			difference of trail position and *Directional Output*
		
Magic:
	- itself and all subelements should be killed after some set time/interaction
	- what SHOULD be passed to *Spawn*
	Fire:
		[d] uses custom particles
		[d] offensive magic
	Lightning: 
		[d] chaining effect with partial lag between chains
		[d] offensive magic
	Ice: 
		[d] creates structures
		[d] defensive magic

	