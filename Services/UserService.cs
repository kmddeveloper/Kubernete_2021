﻿using EFModel;
using Kubernetes.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Kubernetes.Services
{
    public class UserService:IUserService
    {
        readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(int Id)
        {
            if (Id <= 0)
                throw new Exception("Invalid User Id!");

            return await _userRepository.GetUserByIdAsync(Id);
        }


        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (String.IsNullOrEmpty(email))
                throw new Exception("Invalid email address!");

            return await _userRepository.GetUserByEmailAsync(email);
        }

        public User GetUserById(int Id)
        {
            if (Id <= 0)
                throw new Exception("Invalid User Id!");

            return  _userRepository.GetUserById(Id);
        }





        public async Task Compute()
        {
            List<byte[]> memory = new List<byte[]>();
            await Task.Delay(1);
            for (var x=0; x < 2500; x++)
            {
                //memory.Add(new byte[20000000]);
                //await Task.Delay(1);
            }
            //for (var i = 1; i < 1000; i++)
            //{

            //    var fileName = @$"C:\MyStuffs\Temp\Test{i}.txt";

            //    for (var x = 0; x < 1000000; x++)
            //    {
            //        Random rand = new Random();
            //        int r = rand.Next(1, 100000);
            //        l.Add(r);
            //    }
            //    var s = l.ToString();
            //    WriteToFile(s, fileName);
            //}
        }

        public void WriteToFile(string s, string fileName)
        {

            var fs = File.OpenWrite(fileName);
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            fs.Write(bytes, 0, bytes.Length);
        }


    }
}
