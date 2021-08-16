using Dapper;
using dapper_demo.Context;
using dapper_demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_demo.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly IDapperContext dapperContext;
        public NoteRepository(IDapperContext dapperContext)
        {
            this.dapperContext = dapperContext;
        }
        public async Task<IEnumerable<Note>> GetNotes()
        {
            List<Note> notes = new List<Note>();
            var query = "SELECT * FROM Notes";
            using (var connection = dapperContext.CreateConnection())
            {
                notes = (await connection.QueryAsync<Note>(query)).ToList();
            }
            return notes;
        }
    }
}
