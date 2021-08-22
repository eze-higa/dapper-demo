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
        private readonly IDapperContext _dapperContext;
        public NoteRepository(IDapperContext dapperContext)
        {
            this._dapperContext = dapperContext;
        }

        public async Task Create(Note note)
        {
            string query = "INSERT INTO Notes (Title, Description, UserID) VALUES (@title, @description, @userId)";

            using(var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { title = note.Title, description = note.Description, userId = note.UserID });
            }
        }

        public async Task<IEnumerable<Note>> GetNotes()
        {
            List<Note> notes = new List<Note>();
            var query = @"SELECT * FROM Notes n 
                            INNER JOIN Users u ON n.UserID = u.Id";
            using (var connection = _dapperContext.CreateConnection())
            {
                notes = (await connection.QueryAsync<Note, User, Note>(
                    query, 
                    (note, user) => {
                        note.User = user;
                        return note;
                    }
                    )).ToList();
            }
            return notes;
        }
    }
}
