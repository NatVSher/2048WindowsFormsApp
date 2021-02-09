using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048WindowsFormsApp
{
    public class User
    {
        public string Name;        
        public int Score;

        public User(string name)
        {
            Name = name;
            Score = 0;
        }
        public void CountingScore(int number)
        {
            Score += number * 2;
        }
    }
}
