﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.Exceptions
{
    internal class EntityNotFoundException:ApplicationException
    {
        public EntityNotFoundException(string message)
            :base(message)
            { }
    }
}
