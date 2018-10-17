using FriendsWebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FriendsWebApp.Data
{
    public class Context : DbContext
    {
        private SqlConnection _database;
        public Context(DbContextOptions<Context> options)
       : base(options)
        {
            _database = new SqlConnection("Data Source=.;Initial Catalog=FriendsDB;Integrated Security=True");
            //_database = new SqlConnection("Data Source=ricardodbs.database.windows.net;Initial Catalog=FriendsDB;User ID=Ricardo;Password=$R1c4rdo$");
        }

        public Person GetPerson(int id)
        {
            try
            {
                Person person = new Person();
                SqlCommand cmd = new SqlCommand("USE [FriendsDB] SELECT Name, LastName, PersonId FROM PEOPLE WHERE PersonId = " + id, _database);
                cmd.CommandTimeout = 0;

                _database.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        person.Name = Convert.ToString(reader["Name"]);
                        person.LastName = Convert.ToString(reader["LastName"]);
                        person.PersonId = Convert.ToInt32(reader["PersonId"]);
                    }
                    _database.Close();
                    return person;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Person GetLastPersonInsert()
        {
            try
            {
                Person person = new Person();
                SqlCommand cmd = new SqlCommand("USE [FriendsDB] SELECT PersonId, Name, LastName FROM People ORDER BY PersonId", _database);
                cmd.CommandTimeout = 0;

                _database.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        person.Name = Convert.ToString(reader["Name"]);
                        person.LastName = Convert.ToString(reader["LastName"]);
                        person.PersonId = Convert.ToInt32(reader["PersonId"]);
                    }
                    _database.Close();
                    return person;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddPerson(Person person)
        {
            try
            {
                int check = 0;
                using (SqlCommand cmd = _database.CreateCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = @"USE [FriendsDB]
                                    EXECUTE [dbo].[AddPerson] 
                                       @Name
                                      ,@LastName";
                    cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = person.Name;
                    cmd.Parameters.Add("LastName", SqlDbType.NVarChar).Value = person.LastName;
                    _database.Open();
                    check = cmd.ExecuteNonQuery();
                    _database.Close();
                }
                if (check > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemovePerson(int id)
        {
            try
            {
                int check = 0;
                using (SqlCommand cmd = _database.CreateCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = @"USE [FriendsDB]
                                    EXECUTE [dbo].[RemovePerson]
                                        @PersonId";
                    cmd.Parameters.Add("PersonId", SqlDbType.Int).Value = id;
                    _database.Open();
                    check = cmd.ExecuteNonQuery();
                    _database.Close();
                }
                if (check > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EditPerson(Person person)
        {
            try
            {
                int check = 0;
                using (SqlCommand cmd = _database.CreateCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = @"USE [FriendsDB]
                                    EXECUTE [dbo].[EditPerson] 
                                        @Name
                                        ,@LastName
                                        ,@PersonId";
                    cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = person.Name;
                    cmd.Parameters.Add("LastName", SqlDbType.NVarChar).Value = person.LastName;
                    cmd.Parameters.Add("PersonId", SqlDbType.Int).Value = person.PersonId;
                    _database.Open();
                    check = cmd.ExecuteNonQuery();
                    _database.Close();
                }
                if (check > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool LinkFriends(int id, int idToLink)
        {
            try
            {
                int check = 0;
                using (SqlCommand cmd = _database.CreateCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = @"USE [FriendsDB]
                                    EXECUTE [dbo].[LinkFriends] 
                                        @PersonId
                                        ,@PersonId2";
                    cmd.Parameters.Add("PersonId", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("PersonId2", SqlDbType.Int).Value = idToLink;
                    _database.Open();
                    check = cmd.ExecuteNonQuery();
                    _database.Close();
                }
                if (check > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveFriendship(int id, int idToRemove)
        {
            try
            {
                int check = 0;
                using (SqlCommand cmd = _database.CreateCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = @"USE [FriendsDB]
                                    EXECUTE [dbo].[RemoveFriendship] 
                                        @PersonId
                                        ,@PersonId2";
                    cmd.Parameters.Add("PersonId", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("PersonId2", SqlDbType.Int).Value = idToRemove;
                    _database.Open();
                    check = cmd.ExecuteNonQuery();
                    _database.Close();
                }
                if (check > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Person> GetFriends(int id)
        {
            try
            {
                List<Person> People = new List<Person>();
                SqlCommand cmd = new SqlCommand("USE [FriendsDB] SELECT Name, LastName, PersonId FROM [dbo].[GetFriends](" + id + ")", _database);
                cmd.CommandTimeout = 0;

                _database.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        People.Add(new Person()
                        {
                            Name = Convert.ToString(reader["Name"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            PersonId = Convert.ToInt32(reader["PersonId"])
                        });
                    }
                    _database.Close();
                    return People;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Person> GetNoneFriends(int id)
        {
            try
            {
                List<Person> People = new List<Person>();
                SqlCommand cmd = new SqlCommand("USE [FriendsDB] SELECT Name, LastName,PersonId FROM [dbo].[GetNewFriends](" + id + ")", _database);
                cmd.CommandTimeout = 0;

                _database.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        People.Add(new Person()
                        {
                            Name = Convert.ToString(reader["Name"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            PersonId = Convert.ToInt32(reader["PersonId"])
                        });
                    }
                    _database.Close();
                    return People;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Person> GetPeople()
        {
            try
            {
                List<Person> People = new List<Person>();
                SqlCommand cmd = new SqlCommand("USE [FriendsDB] SELECT Name, LastName, PersonId FROM People", _database);
                cmd.CommandTimeout = 0;

                _database.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        People.Add(new Person()
                        {
                            Name = Convert.ToString(reader["Name"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            PersonId = Convert.ToInt32(reader["PersonId"])
                        });
                    }
                    _database.Close();
                    return People;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<JPerson> GetGraph(int id)
        {

            List<JPerson> People = new List<JPerson>();
            SqlCommand cmd = new SqlCommand("USE [FriendsDB] SELECT Id1, Name1, Last1, Id2, Name2, Last2 FROM [dbo].[GetGraph](" + id + ") ORDER BY case WHEN Id1 = (" + id + ") THEN 0 else 1 end, Id1, Id2", _database);
            cmd.CommandTimeout = 0;

            _database.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int Id1 = Convert.ToInt32(reader["Id1"]);
                    if (!People.Any(s => s.Id == Id1))
                    {
                        People.Add(new JPerson()
                        {
                            Id = Id1,
                            Text = Convert.ToString(reader["Name1"]) + " " + Convert.ToString(reader["Last1"])
                        });
                    }

                    int Id2 = Convert.ToInt32(reader["Id2"]);
                    if (!People.Any(s => s.Id == Id2))
                    {
                        People.Add(new JPerson()
                        {
                            Id = Id2,
                            Text = Convert.ToString(reader["Name2"]) + " " + Convert.ToString(reader["Last2"])
                        });
                    }

                    if (!People.FirstOrDefault(s => s.Id == Id1).Neighbors.Any(s => s == Id2))
                    {
                        People.FirstOrDefault(s => s.Id == Id1).Neighbors.Add(Id2);
                    }

                    if (!People.FirstOrDefault(s => s.Id == Id2).Neighbors.Any(s => s == Id1))
                    {
                        People.FirstOrDefault(s => s.Id == Id2).Neighbors.Add(Id1);
                    }
                }
                _database.Close();
                if (People.Count == 0)
                {
                    var initialPerson = GetPerson(id);
                    People.Add(new JPerson()
                    {
                        Id = id,
                        Text = initialPerson.Name + " " + initialPerson.LastName
                    });
                }
                return People;
            }

        }

    }
}
