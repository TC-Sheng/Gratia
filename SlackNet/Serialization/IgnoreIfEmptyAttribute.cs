﻿using System.Collections.Generic;
using System.Linq;

namespace SlackNet;

public class IgnoreIfEmptyAttribute : ShouldSerializeAttribute
{
    public override bool ShouldSerialize(object value)
    {
        return value is not IEnumerable<object> enumerable 
            || enumerable.Any();
    }
}