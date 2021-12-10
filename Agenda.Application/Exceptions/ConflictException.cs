using System;
using System.Collections.Generic;

namespace Agenda.Application.Exceptions
{
    public class ConflictException : Exception
    {

        public List<string> Conflicts { get; set; } = new List<string>();

        public ConflictException(List<string> conflicts)
        {
            Conflicts = conflicts;
        }

    }
}
