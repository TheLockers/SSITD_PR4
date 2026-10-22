using System;
using System.Collections.Generic;

namespace SSITD_PR4
{
    class PlanProject
    {
        public List<StageProject> Stages { get; private set; }

        public PlanProject()
        {
            Stages = new List<StageProject>();
        }
    }
}
