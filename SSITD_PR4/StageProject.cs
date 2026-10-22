using System;
using System.Collections.Generic;

namespace SSITD_PR4
{
    class StageProject
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public HashSet<string> RequiredDocuments { get; private set; }
        public HashSet<string> CoreDocuments { get; private set; }

        public StageProject(
            int id,
            string name,
            HashSet<string> requiredDocuments,
            HashSet<string> coreDocuments)
        {
            Id = id;
            Name = name;
            RequiredDocuments = requiredDocuments;
            CoreDocuments = coreDocuments;
        }
    }
}
