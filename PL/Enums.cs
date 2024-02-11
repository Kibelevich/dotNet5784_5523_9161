using System;
using System.Collections;
using System.Collections.Generic;

namespace PL;

/// <summary>
/// Enum for the engineerExperience
/// </summary>
internal class EngineerExperieceCollection:IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperiece> s_enums =
        (Enum.GetValues(typeof(BO.EngineerExperiece)) as IEnumerable<BO.EngineerExperiece>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();

}
