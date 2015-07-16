using UnityEngine;
using System.Collections;
using System;

public class Command : MonoBehaviour
{

	public virtual IEnumerator Execute(SIComponent c) { yield return null; }

}

