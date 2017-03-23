using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace TestApp.Models
{
    public class DataAccess
    {
        private MongoClient client;

        private MongoDatabase db;

        public DataAccess()
        {
            this.client = new MongoClient("mongodb://localhost:27017");
            this.db = this.client.GetDatabase("TestAppDB") as MongoDatabase;
        }

        public IEnumerable<User> GetUsers()
        {
            return this.db.GetCollection<User>("Users").FindAll();
        }


        public User GetUser(int userId)
        {
            var res = Query<User>.EQ(u => u.UserId, userId);

            return this.db.GetCollection<User>("Users").FindOne(res);
        }

        public User CreateUser(User user)
        {
            this.db.GetCollection<User>("Users").Save(user);

            return user;
        }

        public void UpdateUser(ObjectId id, User user)
        {
            user.Id = id;
            var res = Query<User>.EQ(u => u.Id, id);
            var operation = Update<User>.Replace(user);
            this.db.GetCollection<User>("Users").Update(res, operation);
        }

        public void DeleteUser(ObjectId id)
        {
            var res = Query<User>.EQ(u => u.Id, id);
            var operation = this.db.GetCollection<User>("Users").Remove(res);
        }
    }
}
