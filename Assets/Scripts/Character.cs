using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string ID { get; private set; }
    public int level {  get; private set; }
    public float exp { get; private set; }
    public string description { get; private set; }
    public int atk {  get; private set; }
    public int def {  get; private set; }
    public int hp { get; private set; }
    public float cri {  get; private set; }

    public Character(string name)
    {
        ID = name;
        level = 1;
        description = "Hello World!";
        atk = 3;
        def = 4;
        hp = 10;
        cri = 2;
    }
    
}
