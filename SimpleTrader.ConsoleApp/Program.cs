using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTrader.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataService<User> userService = new GenericDataService<User>(new SimpleTraderDbContextFactory());

            //GetAll
            Console.WriteLine($"Initial Count : {userService.GetAll().Result.Count()}");
            List<User> list = userService.GetAll().Result.ToList();
            list.ForEach(x => Console.WriteLine(x.Username));
            Console.WriteLine();

            //Get Single
            User singleFetchResult = userService.Get(2).Result;
            Console.WriteLine($"Single Fetched User : {singleFetchResult.Username}");

            //Create
            User createdUser = userService.Create(new User { Username = "Isaac Botchway", Email = "isaacbotchway75@gmail.com", Password = "pwd12345.", DateJoined = DateTime.Now }).Result;
            Console.WriteLine($"After Data added -- Count : {userService.GetAll().Result.Count()}");
            Console.WriteLine();
            int newUserId = userService.GetAll().Result.FirstOrDefault((x) => x.Id == createdUser.Id).Id;
            Console.WriteLine($"New User Id: {createdUser.Id}");
            Console.WriteLine($"New Username: {createdUser.Username}");
            Console.WriteLine();

            //Update
            User updatedResult = userService.Update(newUserId, new User { Username = "Ike Botchway" }).Result;
            Console.WriteLine($"Updated User : {userService.Get(newUserId).Result.Username}");
            Console.WriteLine();

            //Delete
            User deletedUser = userService.Get(newUserId).Result;
            bool deleted = userService.Delete(newUserId).Result;
            if (deleted)
            {
                Console.WriteLine($"{deletedUser.Username} Deleted Successfully!");
            }
            Console.WriteLine();

            Console.WriteLine($"Final Count : {userService.GetAll().Result.Count()}");
            List<User> listx = userService.GetAll().Result.ToList();
            listx.ForEach(x => Console.WriteLine(x.Username));
            Console.WriteLine();


            Console.ReadLine();
        }
    }
}
