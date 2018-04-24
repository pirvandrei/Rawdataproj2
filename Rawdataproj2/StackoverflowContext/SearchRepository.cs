using DataRepository;
using DataRepository.Dto.SearchDto;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace StackoverflowContext
{
    public class SearchRepository : ISearchRepository
    {
        public async Task<IList<BestmatchDto>> Bestmatch(string query)
        {
            
                using (var db = new StackoverflowDbContext())
                {
                var result = new List<BestmatchDto>();
                var conn = (MySqlConnection)db.Database.GetDbConnection();
                    conn.Open();
                    var cmd = new MySqlCommand();
                    cmd.Connection = conn;

                    cmd.Parameters.Add("@1", DbType.String);
                    cmd.Parameters["@1"].Value = query;
                    cmd.CommandText = "call bestmatch(@1)";

                    var reader = cmd.ExecuteReader();
                 
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine("{0}, {1}", reader.GetInt32(0), reader.GetInt32(1));
                        result.Add( new BestmatchDto 
                        {
                            Id = reader.GetInt32(0),
                            Rank = reader.GetInt32(1)
                        });
                    }

                    return result; 
            }
        }
    }
}
