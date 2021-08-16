using dapper_demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_demo.Repositories
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetNotes();
    }
}
