﻿using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface INoteRepository : IDataService<Note, int>
    {
       
    }
}
