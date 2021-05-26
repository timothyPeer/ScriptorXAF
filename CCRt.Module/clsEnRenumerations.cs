using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCRt.Module
{
    public enum BomLevel
    {
        none = 0,
        lower_level = 1,
        this_level = 2
        
    };

    public enum ProcessLevelValidation
    {
        none = 0,
        lower_level_only = 1,
        this_level_only = 2,
        both_levels = 3

    };
}
