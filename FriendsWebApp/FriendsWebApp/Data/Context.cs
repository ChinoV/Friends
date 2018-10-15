using FriendsWebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsWebApp.Data
{
    public class Context: DbContext
    {
        private SqlConnection _database;
        public Context(DbContextOptions<Context> options)
       : base(options)
        {
            _database = new SqlConnection("Data Source=.;Initial Catalog=FriendsDB;Integrated Security=True");
        }

        public Person GetPerson(int id)
        {
            try
            {
                Person person = new Person();
                SqlCommand cmd = new SqlCommand("USE [FriendsDB] SELECT Name, LastName, PersonId FROM [dbo].[GetFriends] WHERE Id = " + id, _database);
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

        public bool RemoveFriendhip(int id, int idToRemove)
        {
            try
            {
                int check = 0;
                using (SqlCommand cmd = _database.CreateCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = @"USE [FriendsDB]
                                    EXECUTE [dbo].[RemoveFriendhip] 
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

        public List<Person> GetFriends (int id)
        {
            try
            {
                List<Person> People = new List<Person>();
                SqlCommand cmd = new SqlCommand("USE [FriendsDB] SELECT Name, LastName, PersonId FROM [dbo].[GetFriends]", _database);
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
                SqlCommand cmd = new SqlCommand("USE [FriendsDB] SELECT Name, LastName,PersonId FROM [dbo].[GetNewFriends]", _database);
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

        public List<Person> GetGraph()
        {
            try
            {
                List<Person> People = new List<Person>();
                SqlCommand cmd = new SqlCommand("USE [FriendsDB] SELECT Name, LastName, PersonId FROM [dbo].[GetGraph]", _database);
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

    }
}
