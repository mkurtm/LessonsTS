﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_8_Delegates
{
    public class ManagerActions
    {
        public List<Action> commands = new List<Action>();

        public void addCommand (Action action)
        {
            commands.Add(action);
        }

        public void printAll ()
        {
            foreach (var item in commands)
            {
                item();
            }
        }
        
    }   


}
