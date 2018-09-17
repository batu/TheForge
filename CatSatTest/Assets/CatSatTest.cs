using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CatSAT;
using static CatSAT.Language;


public class CatSatTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CatSAT.Random.SetSeed();
        var p = new Problem("test");

        var a = (Proposition)"a";  // Add propositions to p
        var b = (Proposition)"b";
        var c = (Proposition)"c";


        // print(Problem.Current);
        var s = p.Solve();
      
        p = new Problem("monster");

        var bird = (Proposition)"bird";
        var fish = (Proposition)"fish";
        var mammal = (Proposition)"mammal";

        p.Unique(bird, fish, mammal);

        var bite = (Proposition)"bite";
        var claw = (Proposition)"claw";
        var fire = (Proposition)"fire breathing";

        p.Unique(bite, claw, fire);

        p.Inconsistent(fish, claw);
        p.Inconsistent(fish, fire);

        p.Assert(fish > bite);

        s = p.Solve();
        Debug.Log($"{s[bird]}, {s[mammal]}, {s[fish]}");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
