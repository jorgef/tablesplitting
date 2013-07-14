using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Xml;

namespace TableSplitting
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize();

            SaveDbmxToDisk();

            InsertData();

            ReadData();

            ClearDb();

            Console.ReadLine();
        }

        private static void ReadData()
        {
            using (var context = new Context())
            {
                var user = context.Users.Include(u => u.Address).First();
                Console.WriteLine("User '{0}' lives in '{1}'", user.Name, user.Address.Street);
                var itinerary = context.Itineraries.Include(i => i.Addresses).First();
                Console.WriteLine("Itinerary including the following addresses '{0}'", String.Join(",", itinerary.Addresses.Select(a => a.Street)));
            }
        }

        private static void InsertData()
        {
            var user = new User
                {
                    Name = "John",
                    Address = new Address
                        {
                            Street = "Victoria Avenue"
                        }
                };

            var itinerary = new Itinerary
                {
                    Addresses = new List<Address> { user.Address }
                };

            using (var context = new Context())
            {
                context.Users.Add(user);
                context.Itineraries.Add(itinerary);
                context.SaveChanges();
            }
        }

        private static void ClearDb()
        {
            using (var context = new Context())
            {
                foreach (var itinerary in context.Itineraries.ToList())
                    context.Itineraries.Remove(itinerary);

                foreach (var user in context.Users.Include(u => u.Address).ToList())
                    context.Users.Remove(user);

                context.SaveChanges();
            }
        }

        private static void Initialize()
        {
            var initializer = new MigrateDatabaseToLatestVersion<Context, DbConfig>();
            Database.SetInitializer(initializer);
            using (var context = new Context())
            {
                context.Database.Initialize(force: true);
                context.SaveChanges();
            }
        }

        private static void SaveDbmxToDisk()
        {
            using (var context = new Context())
            {
                var xmlWriter = new XmlTextWriter(@"..\..\SavedModel.edmx", new UTF8Encoding());
                EdmxWriter.WriteEdmx(context, xmlWriter);
            }
        }
    }
}
