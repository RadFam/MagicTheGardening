using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ISaveable
{
    // Take the state of game object that we want to save, as c# object (to save it in binary format)
    object CaptureObject();

    // Restore the state of game object, by read from binary c# object
    void RestoreObject(object obj);
}
