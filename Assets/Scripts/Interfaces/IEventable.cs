using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventAble 
{
    public UnityEvent Ended();
    public UnityEvent Started();
}
