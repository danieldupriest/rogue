﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    public class Input
    {
        private class ConsoleKeyWrapper
        {
            public ConsoleKeyInfo key;
        }

        private static ConsoleKeyWrapper currentKey = null;

        public static ConsoleKeyInfo ReadKey()
        {
            if (currentKey == null && Console.KeyAvailable == true)
            {
                currentKey = new ConsoleKeyWrapper
                {
                    key = Console.ReadKey()
                };
            }
            return currentKey != null ? currentKey.key : new ConsoleKeyInfo();
        }

        public static bool AnyKey()
        {
            return currentKey != null;
        }

        public static void Reset()
        {
            currentKey = null;
            return;
        }
    }
}