using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    /// <summary>
    /// To be invoked whenever damage is recieved
    /// </summary>
    /// <param name="damage"> the amount of damage that has been done </param>
    void onDamageRecieved(float damage);

    /// <summary>
    /// Invoked when object cannot recieve damage any more and is about to destroy
    /// </summary>
    void onObjectDestroy();
}
