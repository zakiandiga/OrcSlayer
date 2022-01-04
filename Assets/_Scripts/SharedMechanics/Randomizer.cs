using System;
using System.Collections;
using System.Collections.Generic;

public class Randomizer
{

    private string name;
    private List<Action> actionList;

    private Randomizer (string name, List<Action> availableAttacks)
    {
        this.name = name;
        this.actionList = availableAttacks;        
    }

}
