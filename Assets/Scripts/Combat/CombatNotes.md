## Melee Design Notes

### Classes:
- Character
  - Hero
  - Non-Heros  (Ex. Guard, Goblin, Bandit, Thug, Companions, etc)


### Class: Character
Stable Stats:
- finesse, might, wit, perceptiveness, toughness, willpower, focusLimit

Volitile Stats:
- focus, bleed, pain, Injury (List)

Equiptment/Perks:
- Armor, weapon, Active/Passive Spells and Abilities

#### Methods
- GenerateStats
    - Gets the base stats from a file(?) for the character type, applies randomness to it, and instantiates the stats based from this.
- TakeDamage: int Damage
    - Receives damage amount from enemy attack and subtracts from it based on stats and armor and spells
- DealDamage: int Damage int Focus
    - Checks if injury is given based on damage percentage to health, focus, and a crit-fail evasive roll
    - Gives the total amount of damage done to an enemy
- CheckHit: int HitChance
    - Returns whether the opponent's evasion roll and bonuses is greater than HitChance
- GetFocus:
    - Returns random focus amount
- Attack:
    - GetFocus()
    - Accuracy: the accumulation of rolls per focus point, plus a base accuracy
    - DamageAmount: the accumulation of might, weapon bonuses, with rolls decreasing damage per focus point added
    - CheckHit()
    - On hit, DealDamage()
- Death
    - If called, death animation is played and entity is desDasted

### Class: Hero
Child of Character

#### Methods
- GenerateStats
    - Gets the base stats from a file for the player and instantiates the stats for this scene
- GetFocus:
    - Returns focus amount from player input
- Attack:
    - GetFocus()
    - Accuracy: the accumulation of rolls per focus point, plus a base accuracy
    - DamageAmount: the accumulation of might, weapon bonuses, with rolls decreasing damage per focus point added
    - CheckHit()
    - On hit, DealDamage()

### Class: Generic NPC (Goblin, Bandit, etc)
Child of Character

Follows the same outline as the Character class, but will have child specific stats and abilities. Those will be outlined in their own seperate classes



## Things to be discussed before implimented
NPC classes spawn with randomized stats (given some limitations per class and area level), the limits of which will be defined in another file/from a database. This is described a bit more in the section below.

All character classes will have a special power attack that can be used once a meter is filled. This can be something as simple as time/"turns" ticking down until it is filled, to a beserk rage caused by the death of allies or from dealing lots of damage, to receiving lots of damage. The generic "limit break" kind of dealio.

### Stats
NPC and possibly initial player stats should be generated per encounter. An example of this can be found in Pokemon, where the stats are generated using the formula:

    Stat = floor(floor((2 * B + I + E) * L / 100 + 5) * N)

Where
- B = base stat
- I = Individual Values (IVs, randomly generated)
- E = Effort value (gained through training)
- L = Level
- N = Nature.

More in depth information can be found here: http://www.dragonflycave.com/stats.aspx

This kind of system could be used to impliment how the NPCs' stats are generated. An example could be something like:

    stat = floor(floor((2 * B + I) * L / 100 + 5))

Here, we only take the base, a random value, and the level to determine the stats, basically removing the bits that don't apply in our case.

This would ensure a variable combat experience and keep things interesting in the process. Hopefully make nearly every combat experience a bit different each time and keeping the combat interesting, versus it becoming a nuisance.

We may want to consider limiting the enemy's level and abilities based on the player's level, but I think a better implimentation would be a difficulty ranking system per mission that's deduced from the player's current level.


## Death/Incapacitation Conditions
We want to look at the character's overall condition, and from there decide whether they have been outright killed or otherwise incapacitated from their injuries. Whether these kinds of injuries will be applied to the player is up to discussion at the moment, or at least up to balancing mechanics.

- If an enemy is rendered blind from their injuries, assuming they don't have magic to guide them, we will render them immediately prone,incapacitated.
- If the enemy is bleeding, they will be given a count down until they are felled from their wounds, and a short time afterwards where they can be stabilized. This countdown will depend on the severity of the bleeding.
- If an opponent's limbs are damaged, their accuracy and number of focus points available to use will drop
- If the brain is damaged, their wit, willpower, and perceptiveness will drop, and will remain as such for the duration of the battle if not treated
- If an enemy's ears are damaged, their perceptiveness and evasive abilities as a result will drop.

We could treat a character's overall ability to fight as a function of their willpower. If enough damage is done, or enough injuries are accrued, an enemy's willpower can drop to zero and result in the enemy being incapacitated. This can be overridden depending on what type of enemy it is, as to whether they will fight to the death or not.
