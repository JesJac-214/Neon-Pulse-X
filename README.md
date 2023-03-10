# Neon Pulse X
Outrun, outgun, and outlive to outplay the competition

## Download Instructions
Download the latest [release](https://github.com/JesJac-214/Neon-Pulse-X/releases) for your operating system and unzip the file with 7-Zip or equivalent. Navigate into the folder and launch the executable file.

## Players Manual

### Controls
| Actions | Controller | Keyboard and Mouse |
| ----------- | ----------- | ----------- |
| Accelerate | Right Trigger | 'W' Key |
| Decelerate | Left Trigger | 'S' Key |
| Steering | Left Stick or D-Pad | 'A' and 'D' Keys |
| Use Equipment | Shoulder Buttons or Face Buttons (Not Drift Button) | Spacebar or Mouse Clicks |
| Drift | East Face Button | Left Shift |
| Pause | Start Button or Equivalent | ESC Key |

### Players
The game requires a minimum of two players and a maximum of four.

### Goal
Stay in view of the camera the longest. The camera tracks the lead car and if a car falls off screen they lose a life and teleport back in. Once someone has lost all of their lives they are out. When only one car is left they are declared the winner.

## TODO
- [x] Create core game loop
  - [x] Create a system that tracks player positions
  - [x] Make players who fall off screen teleport/bullet bill back into the fray
    - [x] Solve checkpoint problem (pretty much)
  - [x] Add pickups
    - [x] Weapons
    - [x] Items
  - [x] Add life tracking
  - [x] End Game when all other players have 0 lives
- [ ] UI
  - [x] Make Pause Screen
  - [x] Make Start Screen
  - [x] Make Main Menu
  - [x] Make Player Join Screen
    - [ ] Add multiple cars
  - [ ] Make Track Select Screen
    - [ ] Add multiple tracks to select
  - [x] Player HUD for ammo
- [ ] Add Weapons
  - [x] Cannon Ball
  - [x] Beam of Light
  - [x] EMP Grenade
  - [ ] Sound Wave
  - [x] Ice Beam
- [ ] Add Items
  - [x] Speed Boost
  - [x] Wall
  - [x] Mine
  - [ ] Ghost
  - [x] Shield
- [ ] Misc
  - [x] Add Tutorial to Player Join
  - [ ] Add Pathfinding for player who fell off screen
- [ ] Sound Effects
  - [ ] Vehicle Sounds
  - [ ] Weapons and Items Sounds
  - [x] Item and Weapon Box Sounds
