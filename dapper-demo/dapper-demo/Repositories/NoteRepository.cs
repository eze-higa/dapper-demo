using Dapper;
using dapper_demo.Context;
using dapper_demo.Entities;
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

        public async Task<Note> Create(Note note)
        {
            string query = @"INSERT INTO Notes (Title, Description, UserID) VALUES (@title, @description, @userId)
                            SELECT CAST(SCOPE_IDENTITY() AS int)";            
            using(var connection = _dapperContext.CreateConnection())
            {
                note.Id = await connection.QuerySingleAsync<int>(query, new { title = note.Title, description = note.Description, userId = note.UserID });
            }
            return note;
        }

        public async Task<Note> GetNoteById(int id)
        {
            Note note = null;

            string query = "SELECT * FROM Notes WHERE Id = @noteId";
            
            using (var connection = _dapperContext.CreateConnection())
            {
                note = (await connection.QueryFirstOrDefaultAsync<Note>(query, new { noteId = id }));
            }

            return note;
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
