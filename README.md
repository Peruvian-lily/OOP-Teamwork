OOP -Teamwork
============

*Git repository for the OOP teamwork for team "Peruvian lily"*

###General Instructions
1. Clone the GitHub repository locally
2. Download and install Monogame - [Monogame Homepage](http://www.monogame.net/)

###Project description 
1. Tools
	- GitHub
	- C#
	- Monogame

2. Structure
	- Namespaces
		- Enums
		- Factories
			* holds factories for creating game objects. Factory design pattern :
					http://sourcemaking.com/design_patterns/factory_method
		- Interfaces
		- Items
		- Stats
	- Interfaces
		- IGameObject
		- IPotion
		- IStatistics
	- Classes
		- Currency
		- Stat (abstract)
			- AttackStats
			- DefenceStats
			- BodyStats
			- CritStats
		- GameObject (abstract)
			- Item (abstract)
				- Wearable (abstract)
					- Armor
					- Weapon
					- Boots
					- Helmet
					- Gloves
				- Consumable (abstract)
					- HealthPotion
					- ManaPotion

*notes : 
	1. Every class that we might want to serialize needs to have an empty constructor.
	2. The currency class is simular to the World of Warcraft system - (Gold[0..INT_MAX], Silver[0..100], Cooper[0..100]).
		It has overriden operators for addision and subtraction which are tested and work.
	
3. Content Ideas - deadline: **06.02.2015**
			Idea - 
			Details:

					
###Project realisation					
1. Think out structure - deadline: **08.02.2015**
 

2. Final tuning and upload - deadline: **26.02.2015**
