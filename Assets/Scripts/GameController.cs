using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [HideInInspector]
    public float MAX_WEIGHT;

    public float SPEED_MULTIPLICATOR_HORIZONTAL = 1.5f;
    public float SPEED_MULTIPLICATOR_VERTICAL = 3.0f;

    public float SPAWNING_DELAY = 8.0f;

    public float SPRITE_STEP_1 = 2.0f;
    public float SPRITE_STEP_2 = 2.0f;
    public float SPRITE_STEP_3 = 2.0f;
}
