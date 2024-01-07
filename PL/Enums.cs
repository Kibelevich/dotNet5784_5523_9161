using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;

internal class EngineerExperieceCollection:IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperiece> s_enums =
        (Enum.GetValues(typeof(BO.EngineerExperiece)) as IEnumerable<BO.EngineerExperiece>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();

}
