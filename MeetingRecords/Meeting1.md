## Team Meeting 1 - 23/09/2023 (8pm-9pm)
**Absent:** 
<br>
**Lead/scribe:** Taowen Huang

## Agenda Items
| Number | Item |
| :--- | ---: |
| 1 | UI Design |
| 2 | Map Design |
| 3 | Logic Design |
| 4 | Task Allocation |

## Meeting Minutes
**Item 1**
- Starting page, picking map and commandar
- Player Units: Distinct troop types for different characters, conclusive master abilities, and ability card slots.
- Enemy Units: 10 troops for each character, with 5 being common and 5 being unique for different commanders.
- Health Visualization: Display health and special effect activation status.
- Special Effects: e.g., fire, poison effects.


**Item 2**

- Additional elements such as obstacles (lakes, air walls, trees, and fences) and traps.
- Map hints(add the fence): Indications of where to deploy troops. 
- Write 3 GameManager – each map has the according gameManager

**Item 3**

- Level Mechanism: Continuous levels on the same map.
- AI Design. -- ? may be required, discussed after all the units developed
- Enemy Generation: A preset enemy list for each level. The structure of level-up would be discussed later.
- Shared scripts: all objects share the same script for one action.  e.g., moving, shooting, etc.
- The coordination of moving and attacks logic would be discussed in developing process
- Shooting would move straight to the initial direction
- The difficulty of each stage on each map will increase with stage that has been overcome by players

## TODO Items
| Task | Assignee |
| :--- | ---: |
| Design UI for character and map selection, parameter passing and camera binding, and complete the meeting records file. | Li |
| Design maps, such as forest, graveyard, and castle maze. | Wu |
| Fix the straight shooting: logic to ensure correct attack direction. May design two units (current all units are unity). Categorize actions for existing units. | Lai & Huang |


## Next Meeting

Wednesday 28/09/2022
