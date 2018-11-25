using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefixSet {

    public string prefix;
    public bool[] statTemplate;
    public string flavortext;

    public PrefixSet()
    {

    }

    public PrefixSet(string prefix, bool[] statTemplate, string flavortext)
    {
        this.prefix = prefix;
        this.statTemplate = statTemplate;
        this.flavortext = flavortext;
    }


}
